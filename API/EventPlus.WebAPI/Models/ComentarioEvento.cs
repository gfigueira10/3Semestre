using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("ComentarioEvento")]
public partial class ComentarioEvento
{
    [Key]
    [Column("Id_ComentarioEvento")]
    public Guid IdComentarioEvento { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    public bool Exibe { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataComentarioEvento { get; set; }

    [Column("Id_Usuario")]
    public Guid? IdUsuario { get; set; }

    [Column("Id_Eveto")]
    public Guid? IdEveto { get; set; }

    [ForeignKey("IdEveto")]
    [InverseProperty("ComentarioEventos")]
    public virtual Evento? IdEvetoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("ComentarioEventos")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
