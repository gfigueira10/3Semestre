namespace Filmes.WebAPI.DTO;

public class FilmeDTO
{
    public string? Nome { get; set; }
    public IFormFile? imagem { get; set; }
    public Guid? IdGenero { get; set; }
}
