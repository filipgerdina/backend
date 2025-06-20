

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
}


public class UserClass
{
    public int Id { get; set;  }
    public string Username { get; set; }
    public string? PasswordHash { get; set; }
    public string? DisplayName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool IsSystem { get; set; }
    public bool IsLocked { get; set; }
    public DateTime Added { get; set; }
    public int? SettingsId { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = new();
}

public class DomainUserClass: UserClass
{
    public string Domain { get; set; }
}

public class UserClassEdit
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string? Password { get; set; }
    public string? DisplayName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public int? RoleId { get; set; }
    public int? LanguageId { get; set; }
    public int? DateTimeFormatId { get; set; }
    public int? DecimalSeperatorId { get; set; }
    public RefreshToken? RefreshToken { get; set; }
}

public class DomainUserClassEdit: UserClassEdit
{ 
    public string Domain { get; set; }
}