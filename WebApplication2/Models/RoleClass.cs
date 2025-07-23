
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;
public class RoleClass
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ID_home_page { get; set; }
    public PageClass? HomePage { get; set; }
    public DateTime Added { get; set; }
}

public class RoleClassEdit 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? DefaultPageId { get; set; }
}