using System.Collections.Generic;

namespace Gerenciador_Financeiro.Model
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Login { get; set; }   
        public IList<Conta> Contas  { get; set; }

    }
}