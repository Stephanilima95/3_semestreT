using System.ComponentModel.DataAnnotations;

namespace EventPlusTorloni.WebAPI.DTO
{
    public class InstituicaoDTO
    {
        public string? NomeFantasia { get; set; } = null!;
        public string? Cnpj { get; set; } = null!;
        public string? Endereco { get; set; } = null!;
    }
}