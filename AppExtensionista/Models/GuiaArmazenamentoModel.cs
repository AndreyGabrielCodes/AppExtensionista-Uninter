using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExtensionista.Models
{
    [Table("GUIA_ARMAZENAMENTO")]
    public class GuiaArmazenamentoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_GUIA_ARMAZENAMENTO")]
        public int IdGuiaArmazenamento { get; set; }

        [Required]
        [Column("ID_ALIMENTO")]
        public int IdAlimento { get; set; }

        [ForeignKey(nameof(IdAlimento))]
        public virtual AlimentoModel Alimento { get; set; }

        [Required]
        [Column("ID_USUARIO_RESPONSAVEL")]
        public int IdUsuarioResponsavel { get; set; }

        [ForeignKey(nameof(IdUsuarioResponsavel))]
        public virtual UsuarioModel UsuarioResponsavel { get; set; }

        [Required]
        [Column("TITULO")]
        public string Titulo { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Required]
        [Column("DATA_CRIACAO")]
        public DateTime DataCriacao { get; set; }

        [Column("DATA_ULTIMA_ALTERACAO")]
        public DateTime? DataUltimaAlteracao { get; set; }
    }
}