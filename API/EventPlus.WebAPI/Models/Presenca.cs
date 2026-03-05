using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("Presenca")]
public partial class Presenca
{
    [Key]
    [Column("Id_Presenca")]
    public Guid IdPresenca { get; set; }

    public bool Situacao { get; set; }

    [Column("Id_Usuario")]
    public Guid? IdUsuario { get; set; }

    [Column("Id_Evento")]
    public Guid? IdEvento { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("Presencas")]
    public virtual Evento? IdEventoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Presencas")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
