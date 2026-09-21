using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppExtensionista.Enums;

namespace AppExtensionista.Models
{
    [Table("ALIMENTO")]
    public class AlimentoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_ALIMENTO")]
        public int IdAlimento { get; set; }

        [Required]
        [Column("ID_USUARIO_CRIADOR")]
        public int IdUsuarioCriador { get; set; }

        [ForeignKey(nameof(IdUsuarioCriador))]
        public virtual UsuarioModel UsuarioCriador { get; set; }

        [Required]
        [Column("NOME")]
        public string Nome { get; set; }

        [Required]
        [Column("UNIDADE_MEDIDA")]
        public UnidadeMedidaEnum UnidadeMedida { get; set; }
    }
}