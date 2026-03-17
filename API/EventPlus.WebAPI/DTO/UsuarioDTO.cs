using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class UsuarioDTO
{
    [Required(ErrorMessage = "O Nome do usuario é obrigatorio")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O Email do usúario é obrigatório.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A Senha do usúario é obrigatório.")]
    public string? Senha { get; set; }
}
