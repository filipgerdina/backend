using MediatR;
using WebApplication2.Models.CodeList;
using WebApplication2.Models.Exceptions;
using WebApplication2.Models.Responses;
using System.Security.Cryptography;
using WebApplication2.Services;
using WebApplication2.Controllers;

namespace WebApplication2.Business.User.Commands.EditProfileCommand
{
    public class EditProfileCommandHandler : IRequestHandler<EditProfileCommand, RecordIDResponse>
    {
        private readonly UserService userService;
        private readonly RoleService roleService;

        public EditProfileCommandHandler(UserService userService, RoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public async Task<RecordIDResponse> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            var response = new RecordIDResponse();

            var userClass = new UserClassEdit
            {
                Username = request.Data.ExtraParamsFormValues.Username,
                Email = request.Data.ExtraParamsFormValues.Email,
                FirstName = request.Data.ExtraParamsFormValues.FirstName,
                LastName = request.Data.ExtraParamsFormValues.LastName,
                LanguageId = request.Data.ExtraParamsFormValues.LanguageId,
                DateTimeFormatId = request.Data.ExtraParamsFormValues.DateTimeFormatId,
                DecimalSeperatorId = request.Data.ExtraParamsFormValues.DecimalSeperatorId,
            };

            userService.EditUserProfile(userClass);
            return response;
        }
    }
}
