using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model
{    
    public class Receita
    {
        [Key]
        public long Id { get; set; }
        
        [Required, MinLength(5), MaxLength(150)]
        public string Descricao { get; set; }   

        [Column("DATA_RECEITA"), Required]        
        public DateTime DataReceita  { get; set; }

        public Decimal Valor  { get; set; }

        [ForeignKey("ID_CONTA")]
        public Conta Conta { get; set; }

    }
}