using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("ID")]        
        public long Id { get; set; }

        [Column("LOGIN")]
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }   

        public IList<Conta> Contas  { get; set; }

    }
}