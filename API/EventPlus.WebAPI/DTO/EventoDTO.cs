using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class EventoDTO
{
    [Required(ErrorMessage = "O Nome do evento é obrigatorio")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A Descricao do evento é obrigatória.")]
    public string? Descricao { get; set; }

    [Required]
    public DateTime DataEvento { get; set; }

    [Required]
    public Guid Id_TipoEvento { get; set; }

    [Required]
    public Guid Id_Instituicao { get; set; }
}
    


