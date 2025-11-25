using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api")]
    public class SecurityController : ControllerBase
    {
        private readonly IMfaService _mfa;
        private readonly ICurrentUserService _currentUser; // resolves current user id/claims
        private readonly IClock _clock;

        public SecurityController(IMfaService mfa, ICurrentUserService currentUser, IClock clock)
        {
            _mfa = mfa;
            _currentUser = currentUser;
            _clock = clock;
        }

        // ========= /api/mfa/start =========
        [HttpPost("mfa/start")]
        public async Task<ActionResult<MfaStartResponse>> Start([FromBody] MfaStartRequest req)
        {
            if (req is null || string.IsNullOrWhiteSpace(req.Operation))
                return BadRequest(new { message = "Missing operation" });

            var userId = _currentUser.UserId;
            if (userId == null) return Unauthorized();

            var start = await _mfa.CreateChallengeAsync(new CreateMfaChallengeArgs
            {
                UserId = userId.Value,
                Operation = req.Operation.Trim().ToLowerInvariant(),
                PayloadHash = req.PayloadHash,
                PreferFactorOrder = req.PreferFactorOrder?.ToArray()
            });

            if (start == null) return BadRequest(new { message = "No MFA factor available" });

            return Ok(new MfaStartResponse
            {
                ChallengeId = start.ChallengeId,
                Factor = start.Factor,                       // "webauthn" | "totp" | "push"
                TimeoutSeconds = start.TimeoutSeconds,
                AssertionOptions = start.AssertionOptions,   // only for WebAuthn
                Metadata = start.Metadata
            });
        }

        // ========= /api/mfa/verify =========
        [HttpPost("mfa/verify")]
        public async Task<ActionResult<MfaVerifyResponse>> Verify([FromBody] MfaVerifyRequest req)
        {
            if (req is null || string.IsNullOrWhiteSpace(req.ChallengeId))
                return BadRequest(new { message = "Missing challengeId" });

            var userId = 1;
            //if (userId == null) return Unauthorized();

            var result = await _mfa.VerifyAsync(new VerifyMfaArgs
            {
                UserId = userId,
                ChallengeId = req.ChallengeId,
                TotpCode = string.IsNullOrWhiteSpace(req.Code) ? null : req.Code,
                WebAuthnAssertion = req.Assertion
            });

            if (!result.Success)
            {
                return Ok(new MfaVerifyResponse
                {
                    Success = false,
                    Error = result.Error ?? "invalid_or_expired"
                });
            }

            var ticket = await _mfa.IssueStepUpTicketAsync(new IssueStepUpTicketArgs
            {
                UserId = userId,
                Operation = result.Operation,
                PayloadHash = result.PayloadHash,
                ExpiresAtUtc = _clock.UtcNow.AddMinutes(5) // freshness window
            });

            return Ok(new MfaVerifyResponse
            {
                Success = true,
                StepUpTicket = ticket.Token,
                ExpiresAtUtc = ticket.ExpiresAtUtc
            });
        }

        // ========= /api/esign =========
        [HttpPost("esign")]
        public async Task<ActionResult<EsignResponse>> Esign([FromBody] EsignRequest req)
        {
            if (req is null || string.IsNullOrWhiteSpace(req.StepUpTicket))
                return Unauthorized(new { message = "Missing step-up ticket" });

            var userId = _currentUser.UserId;
            if (userId == null) return Unauthorized();

            // Validate the step-up ticket is fresh & bound to esign (+ same payloadHash if supplied)
            var validation = await _mfa.ValidateStepUpTicketAsync(new ValidateStepUpTicketArgs
            {
                UserId = userId.Value,
                Token = req.StepUpTicket,
                RequiredOperation = "esign",
                RequiredPayloadHash = req.PayloadHash
            });

            if (!validation.Success)
                return Unauthorized(new { message = validation.Error ?? "Step-up invalid or expired" });

            // TODO: perform your domain signing logic here, bind to payload/document
            // await _esignService.SignAsync(userId.Value, req.DocumentId, req.PayloadHash);

            await _mfa.AuditAsync(new MfaAuditRecord
            {
                UserId = userId.Value,
                Operation = "esign",
                PayloadHash = req.PayloadHash,
                TimestampUtc = _clock.UtcNow,
                Factor = validation.Factor,
                ChallengeId = validation.ChallengeId
            });

            return Ok(new EsignResponse { Success = true, Message = "Signed successfully." });
        }
    }

    // ======================= DTOs that match the frontend =======================

    public class MfaStartRequest
    {
        public string Operation { get; set; } = default!;           // "esign"
        public string? PayloadHash { get; set; }                    // optional binding to content hash
        public List<string>? PreferFactorOrder { get; set; }        // e.g. ["webauthn","totp"]
    }

    public class MfaStartResponse
    {
        public string ChallengeId { get; set; } = default!;
        public string Factor { get; set; } = default!;              // "webauthn" | "totp" | "push"
        public int TimeoutSeconds { get; set; }
        public object? AssertionOptions { get; set; }               // WebAuthn GetAssertion options
        public Dictionary<string, object>? Metadata { get; set; }
    }

    public class MfaVerifyRequest
    {
        public string ChallengeId { get; set; } = default!;
        public string? Code { get; set; }                           // TOTP path
        public WebAuthnAssertionDTO? Assertion { get; set; }        // WebAuthn path
    }

    public class MfaVerifyResponse
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public string? StepUpTicket { get; set; }
        public DateTime? ExpiresAtUtc { get; set; }
    }

    public class WebAuthnAssertionDTO
    {
        public string Id { get; set; } = default!;
        public string RawId { get; set; } = default!;
        public string Type { get; set; } = default!;
        public WebAuthnAssertionResponseDTO Response { get; set; } = default!;
    }
    public class WebAuthnAssertionResponseDTO
    {
        public string ClientDataJSON { get; set; } = default!;
        public string AuthenticatorData { get; set; } = default!;
        public string Signature { get; set; } = default!;
        public string? UserHandle { get; set; }
    }

    public class EsignRequest
    {
        public string StepUpTicket { get; set; } = default!;
        public string? PayloadHash { get; set; }
        public string? DocumentId { get; set; }
    }

    public class EsignResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }

    // ======================= service contracts used by the controller =======================

    public interface ICurrentUserService
    {
        int? UserId { get; } // resolve from HttpContext.User claims
    }

    public interface IClock { DateTime UtcNow { get; } }

    public interface IMfaService
    {
        Task<MfaStartData?> CreateChallengeAsync(CreateMfaChallengeArgs args);
        Task<MfaVerifyResult> VerifyAsync(VerifyMfaArgs args);
        Task<StepUpTicket> IssueStepUpTicketAsync(IssueStepUpTicketArgs args);
        Task<ValidateTicketResult> ValidateStepUpTicketAsync(ValidateStepUpTicketArgs args);
        Task AuditAsync(MfaAuditRecord record);
    }

    public record CreateMfaChallengeArgs
    {
        public int UserId { get; init; }
        public string Operation { get; init; } = default!;
        public string? PayloadHash { get; init; }
        public string[]? PreferFactorOrder { get; init; }
    }

    public record MfaStartData
    {
        public string ChallengeId { get; init; } = default!;
        public string Factor { get; init; } = default!;
        public int TimeoutSeconds { get; init; }
        public object? AssertionOptions { get; init; }
        public Dictionary<string, object>? Metadata { get; init; }
    }

    public record VerifyMfaArgs
    {
        public int UserId { get; init; }
        public string ChallengeId { get; init; } = default!;
        public string? TotpCode { get; init; }
        public WebAuthnAssertionDTO? WebAuthnAssertion { get; init; }
    }

    public record MfaVerifyResult
    {
        public bool Success { get; init; }
        public string? Error { get; init; }
        public string Operation { get; init; } = default!;
        public string? PayloadHash { get; init; }
    }

    public record IssueStepUpTicketArgs
    {
        public int UserId { get; init; }
        public string Operation { get; init; } = default!;
        public string? PayloadHash { get; init; }
        public DateTime ExpiresAtUtc { get; init; }
    }

    public record StepUpTicket(string Token, DateTime ExpiresAtUtc);

    public record ValidateStepUpTicketArgs
    {
        public int UserId { get; init; }
        public string Token { get; init; } = default!;
        public string RequiredOperation { get; init; } = default!;
        public string? RequiredPayloadHash { get; init; }
    }

    public record ValidateTicketResult
    {
        public bool Success { get; init; }
        public string? Error { get; init; }
        public string Factor { get; init; } = default!;
        public string ChallengeId { get; init; } = default!;
    }

    public record MfaAuditRecord
    {
        public int UserId { get; init; }
        public string Operation { get; init; } = default!;
        public string? PayloadHash { get; init; }
        public DateTime TimestampUtc { get; init; }
        public string Factor { get; init; } = default!;
        public string ChallengeId { get; init; } = default!;
    }

    public sealed class SystemClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }

    public sealed class HttpContextCurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextCurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user?.Identity?.IsAuthenticated != true)
                    return null;

                // adjust claim name depending on your JWT contents ("uid", "sub", etc.)
                var id = user.FindFirst("uid")?.Value
                      ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return int.TryParse(id, out var val) ? val : null;
            }
        }
    }

    public sealed class InMemoryMfaService : IMfaService
    {
        private readonly IClock _clock;
        private readonly ConcurrentDictionary<string, (int UserId, string Operation, string? PayloadHash, DateTime Expiry)> _tickets = new();

        public InMemoryMfaService(IClock clock)
        {
            _clock = clock;
        }

        public Task<MfaStartData?> CreateChallengeAsync(CreateMfaChallengeArgs args)
        {
            // demo: always totp
            return Task.FromResult<MfaStartData?>(new MfaStartData
            {
                ChallengeId = Guid.NewGuid().ToString("N"),
                Factor = "totp",
                TimeoutSeconds = 120
            });
        }

        public Task<MfaVerifyResult> VerifyAsync(VerifyMfaArgs args)
        {
            // demo: accept TOTP code "000000"
            bool ok = args.TotpCode == "000000";

            return Task.FromResult(new MfaVerifyResult
            {
                Success = ok,
                Operation = "esign",
                PayloadHash = args.TotpCode
            });
        }

        public Task<StepUpTicket> IssueStepUpTicketAsync(IssueStepUpTicketArgs args)
        {
            var token = Guid.NewGuid().ToString("N");
            _tickets[token] = (args.UserId, args.Operation, args.PayloadHash, args.ExpiresAtUtc);
            return Task.FromResult(new StepUpTicket(token, args.ExpiresAtUtc));
        }

        public Task<ValidateTicketResult> ValidateStepUpTicketAsync(ValidateStepUpTicketArgs args)
        {
            if (!_tickets.TryGetValue(args.Token, out var entry))
                return Task.FromResult(new ValidateTicketResult { Success = false, Error = "not_found" });

            if (entry.UserId != args.UserId || entry.Expiry < _clock.UtcNow)
                return Task.FromResult(new ValidateTicketResult { Success = false, Error = "expired_or_invalid" });

            return Task.FromResult(new ValidateTicketResult
            {
                Success = true,
                Factor = "totp",
                ChallengeId = "demo"
            });
        }

        public Task AuditAsync(MfaAuditRecord record)
        {
            Console.WriteLine($"MFA audit: user={record.UserId} op={record.Operation} at={record.TimestampUtc:o}");
            return Task.CompletedTask;
        }
    }
}
