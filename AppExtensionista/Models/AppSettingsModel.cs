using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExtensionista.Models
{
    [Table("APP_SETTINGS")]
    public class AppSettingsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_SETTINGS")]
        public int Id { get; set; } = 1; // Registro único global do dispositivo

        [Required]
        [Column("DIAS_ALERTA_VENCIMENTO")]
        public int DiasAlertaVencimento { get; set; } = 7;

        [Required]
        [Column("DIAS_HISTORICO_DESCARTADOS")]
        public int DiasHistoricoDescartados { get; set; } = 30; 
    }
}