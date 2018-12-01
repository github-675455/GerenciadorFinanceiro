using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Gerenciador_Financeiro.Model;

namespace Gerenciador_Financeiro.Context
{
    public class GerenciadorFinanceiroContext : DbContext
    {
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}