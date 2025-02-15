using System.ComponentModel.DataAnnotations;

namespace AspNetWebApiStarter.Models.DTOs.User
{
  public class CreateUserDTO
  {
    [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    public string? FirstName { get; set; }

    [MaxLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(255, ErrorMessage = "La contraseña no puede tener más de 255 caracteres.")]
    public string Password { get; set; } = null!;
  }
}
