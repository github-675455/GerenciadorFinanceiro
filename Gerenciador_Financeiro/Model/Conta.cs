using System.Collections.Generic;

namespace Gerenciador_Financeiro.Model
{
    public class Conta
    {
        public long Id { get; set; }
        public string Nome { get; set; }   
        public string Descricao  { get; set; }
        public IList<Receita> Receitas { get; set; }
        public IList<Despesa> Despesas { get; set; }
    }
}