using System.ComponentModel.DataAnnotations;

namespace ConnectPlus.DTO
{
    public class ContatoDTO
    {
        [Required(ErrorMessage = "O Nome do contato é obrigatório!")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O FormaContato do contato é obrigatório!")]
        public string? FormaContato { get; set; }
        public IFormFile? Imagem { get; set; }
        public Guid IdTipoContato { get; set; }
    }
}
