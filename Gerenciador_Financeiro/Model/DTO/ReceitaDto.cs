using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model.DTO
{
    public class ReceitaDto
    {           
        [Required, MinLength(5), MaxLength(150)]
        public string Descricao { get; set; }   

        public DateTime DataDespesa  { get; set; }

        public Decimal Valor  { get; set; }

        [Required]
        public virtual Conta Conta { get; set; }
    }
}