using System.ComponentModel.DataAnnotations;

namespace AspNetWebApiStarter.Models.DTOs.User
{
  public class UpdateUserDTO
  {
    [Required(ErrorMessage = "El ID es obligatorio.")]
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    public string? FirstName { get; set; }

    [MaxLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string Email { get; set; } = null!;
  }
}
