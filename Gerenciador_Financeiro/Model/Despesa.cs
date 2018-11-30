using System;

namespace Gerenciador_Financeiro.Model
{
    public class Despesa
    {
        public long Id { get; set; }
        public string Descricao { get; set; }   
        public DateTime DataDespesa  { get; set; }

    }
}