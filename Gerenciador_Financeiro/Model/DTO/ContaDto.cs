using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model.DTO
{
    public class ContaDto
    {           
        [Required, MinLength(1), MaxLength(150)]
        public string Nome { get; set; }   
        
        [MaxLength(150)]
        public string Descricao  { get; set; }
    }
}