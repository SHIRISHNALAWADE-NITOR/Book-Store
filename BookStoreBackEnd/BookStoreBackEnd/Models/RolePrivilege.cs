using System.ComponentModel.DataAnnotations;
public class RolePrivilege
{
    [Key]
    public int Id { get; set; }
    public Role Role { get; set; }
    public int? RoleId { get; set; }
    public string Privilege { get; set; }
}
