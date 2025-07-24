

using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public int UserId { get; set; }
}


public class UserClass
{
    [Key]
    public int Id { get; set;  }
    public string Username { get; set; }
    public string? Password { get; set; }
    public string? Display_Name { get; set; }
    public string? First_Name { get; set; }
    public string? Last_Name { get; set; }
    public string? Email { get; set; }
    public bool Is_System { get; set; }
    public bool Is_Locked { get; set; }
    public DateTime Added { get; set; }
    public int? ID_Setting { get; set; }
    public string? Domain { get; set; }
    public ApplicationSettingsClass Settings { get; set; }
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
    public string? Domain { get; set; }
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