using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model
{    
    public class Usuario
    {
        [Key]
        public long Id { get; set; }
                
        [Required, MinLength(1), MaxLength(100)]
        public string Login { get; set; }   

        public IList<Conta> Contas  { get; set; }

    }
}