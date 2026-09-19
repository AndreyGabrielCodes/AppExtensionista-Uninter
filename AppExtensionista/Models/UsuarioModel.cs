using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExtensionista.Models
{
    [Index(nameof(Login), IsUnique = true)]
    [Table("USUARIO")]
    public class UsuarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("LOGIN")]
        public string Login { get; set; }

        [Required]
        [Column("NOME")]
        public string Nome { get; set; }

        [Required]
        [Column("SENHA")]
        public string Senha { get; set; }

        [Required]
        [Column("DIAS_ALERTA_PADRAO")]
        public int DiasAlertaPadrao { get; set; }
    }
}