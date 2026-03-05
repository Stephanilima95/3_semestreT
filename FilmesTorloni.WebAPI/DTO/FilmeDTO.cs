namespace FilmesTorloni.WebAPI.DTO
{
    public class FilmeDTO
    {
        public string? Nome { get; set; } = null!;
        public IFormFile? Imagem { get; set; }
        public Guid? IdGenero { get; set; }
    }
}
