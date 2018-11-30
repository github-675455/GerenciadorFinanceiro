using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model
{
    [Table("RECEITA")]
    public class Receita
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("DESCRICAO")]
        [Required]
        [MaxLength(150)]
        public string Descricao { get; set; }   

        [Column("DATA_RECEITA")]
        [Required]
        public DateTime DataReceita  { get; set; }

        [ForeignKey("ID_CONTA")]
        public Conta Conta { get; set; }

    }
}