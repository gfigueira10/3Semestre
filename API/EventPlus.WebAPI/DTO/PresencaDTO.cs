using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class PresencaDTO
{
    [Required]
    public bool Situacao { get; set; }

    [Required]
    public Guid? IdUsuario { get; set; }

    [Required]
    public Guid? IdEvento { get; set; }
}
