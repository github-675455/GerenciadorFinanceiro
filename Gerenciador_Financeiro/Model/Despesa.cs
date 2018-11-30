using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model
{
    [Table("DESPESA")]
    public class Despesa
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("DESCRICAO")]
        [Required]
        [MaxLength(150)]
        public string Descricao { get; set; }   

        [Column("DATA_DESPESA")]
        [Required]
        public DateTime DataDespesa  { get; set; }

        [ForeignKey("ID_CONTA")]
        public Conta Conta { get; set; }

    }
}