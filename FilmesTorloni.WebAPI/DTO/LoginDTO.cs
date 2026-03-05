using System.ComponentModel.DataAnnotations;

namespace FilmesTorloni.WebAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string? Email { get; set; } = null!;

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string? Senha { get; set; } = null!;
    }
}
