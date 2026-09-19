using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExtensionista.Models
{
    [Table("ESTOQUE")]
    public class EstoqueModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_ESTOQUE")]
        public int IdEstoque { get; set; }

        [Required]
        [Column("ID_USUARIO_CRIADOR")]
        public int IdUsuarioCriador { get; set; }

        [ForeignKey(nameof(IdUsuarioCriador))]
        public virtual UsuarioModel UsuarioCriador { get; set; }

        [Required]
        [Column("ID_ALIMENTO")]
        public int IdAlimento { get; set; }

        [ForeignKey(nameof(IdAlimento))]
        public virtual AlimentoModel Alimento { get; set; }

        [Required]
        [Column("QUANTIDADE_ORIGINAL")]
        public decimal QuantidadeOriginal { get; set; }

        [Required]
        [Column("QUANTIDADE")]
        public decimal Quantidade { get; set; }

        [Required]
        [Column("DATA_VALIDADE")]
        public DateTime DataValidade { get; set; }

        [Column("LOCAL_ARMAZENAMENTO")]
        public string LocalArmazenamento { get; set; }

        [Column("FORMA_ARMAZENAMENTO")]
        public string FormaArmazenamento { get; set; }
    }
}