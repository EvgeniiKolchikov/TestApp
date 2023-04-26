using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = "";
    [Required] public string Surname { get; set; } = "";
    [Required] public string PatronymicName { get; set; } = "";
    public string ActiveDirectoryLogin { get; set; } = "";
}