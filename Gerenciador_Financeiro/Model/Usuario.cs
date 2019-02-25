using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model
{    
    public class Usuario
    {
        [Key]
        public long Id { get; set; }
                
        [Required, MinLength(1), MaxLength(256)]
        public string Login { get; set; }

        [Required, MaxLength(128)]
        public byte[] Senha { get; set; } 

        [Required, MaxLength(128)]
        public byte[] Salt { get; set; }   

        public IList<Conta> Contas  { get; set; }

    }
}