using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_Financeiro.Model
{
    [Table("CONTA")]
    public class Conta
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        
        [Column("NOME")]
        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }   

        [Column("DESCRICAO")]
        [MaxLength(150)]
        public string Descricao  { get; set; }

        [ForeignKey("ID_USUARIO")]
        public Usuario Usuario { get; set; }
        
        public IList<Receita> Receitas { get; set; }
        public IList<Despesa> Despesas { get; set; }
    }
}