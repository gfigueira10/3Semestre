using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class TipoUsuarioDTO
{
    [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
    public string? Titulo { get; set; }
}

