using AppExtensionista.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExtensionista.Models
{
    [Table("REGISTRO_DESCARTE")]
    public class RegistroDescarteModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_LISTA_DESCARTE")]
        public int IdListaDescarte { get; set; }

        [Required]
        [Column("ID_USUARIO_RESPONSAVEL")]
        public int IdUsuarioResponsavel { get; set; }

        [ForeignKey(nameof(IdUsuarioResponsavel))]
        public virtual UsuarioModel UsuarioResponsavel { get; set; }

        [Required]
        [Column("ID_ESTOQUE")]
        public int IdEstoque { get; set; }

        [ForeignKey(nameof(IdEstoque))]
        public virtual EstoqueModel Estoque { get; set; }

        [Required]
        [Column("QUANTIDADE")]
        public decimal Quantidade { get; set; }

        [Required]
        [Column("DATA_DESCARTE")]
        public DateTime DataDescarte { get; set; }

        [Column("DESCRICAO_MOTIVO")]
        public string? DescricaoMotivo { get; set; }

        [Required]
        [Column("TIPO_DESCARTE")]
        public DescarteEnum TipoDescarte { get; set; }

    }
}