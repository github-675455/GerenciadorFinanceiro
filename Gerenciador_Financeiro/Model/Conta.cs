using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Gerenciador_Financeiro.Model
{
    public class Conta
    {        
        [Key]        
        public long Id { get; set; }
                
        [Required, MinLength(1), MaxLength(150)]
        public string Nome { get; set; }   
        
        [MaxLength(150)]
        public string Descricao  { get; set; }

        [ForeignKey("ID_USUARIO"), JsonIgnore]
        public Usuario Usuario { get; set; }
        
        public virtual IList<Receita> Receitas { get; set; }

        public virtual IList<Despesa> Despesas { get; set; }
    }
}