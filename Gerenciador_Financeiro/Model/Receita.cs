using System;

namespace Gerenciador_Financeiro.Model
{
    public class Receita
    {
        public long Id { get; set; }
        public string Descricao { get; set; }   
        public DateTime DataReceita  { get; set; }

    }
}