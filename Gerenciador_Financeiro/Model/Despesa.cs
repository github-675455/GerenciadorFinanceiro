using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model
{
    public class Despesa
    {
        [Key]
        public long Id { get; set; }
                
        [Required, MinLength(5), MaxLength(150)]
        public string Descricao { get; set; }   

        [Column("DATA_DESPESA"), Required]        
        public DateTime DataDespesa  { get; set; }

        [ForeignKey("ID_CONTA"), Required]
        public virtual Conta Conta { get; set; }

    }
}