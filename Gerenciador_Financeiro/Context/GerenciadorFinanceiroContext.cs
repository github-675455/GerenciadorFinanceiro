using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Gerenciador_Financeiro.Model;
using System;

namespace Gerenciador_Financeiro.Context
{
    public class GerenciadorFinanceiroContext : DbContext
    {
        public GerenciadorFinanceiroContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>()
            .HasIndex("ID_USUARIO", nameof(Conta.Nome))
            .IsUnique();

            modelBuilder.Entity<Usuario>()
            .HasIndex(b => b.Login)
            .IsUnique();

            modelBuilder.Entity<Usuario>()
            .HasData(new Usuario[]{
                new Usuario{ Id = -1, Login = "vinicius", Senha = Convert.FromBase64String("rCScaeX1dYd62O6zRXI9u8VbZBRPerUhNaxfuC4Igiw5/J6AzG8gix9Q8uTDJTps0ymqO4PZ0ybW2XVucgXYyxiOeswfvmYydUZrGShJt6PpGWUfStBAK+qtLsZHXvxqqIAw2WRyO14FS7gA2OtTcEj0uGfxwe5eAtMforoXHZY="), Salt = Convert.FromBase64String("30Gc22i66cwjJa3HDsZOFRALA32bDcFDwucbMczgT7C9oaKRAiaWnCVAoG0brYt3Wd25tfAirUdOi0RnrCBwDykbyAIAn1CzEef0od7beZIw7w6KQ0OlJOwL6/FKZiBrXl3TW080+aiVow/KaHBBR/Tr7xnAi1OIX+lgJaDxVlc=") },
                new Usuario{ Id = -2, Login = "alcyon", Senha = Convert.FromBase64String("rCScaeX1dYd62O6zRXI9u8VbZBRPerUhNaxfuC4Igiw5/J6AzG8gix9Q8uTDJTps0ymqO4PZ0ybW2XVucgXYyxiOeswfvmYydUZrGShJt6PpGWUfStBAK+qtLsZHXvxqqIAw2WRyO14FS7gA2OtTcEj0uGfxwe5eAtMforoXHZY="), Salt = Convert.FromBase64String("30Gc22i66cwjJa3HDsZOFRALA32bDcFDwucbMczgT7C9oaKRAiaWnCVAoG0brYt3Wd25tfAirUdOi0RnrCBwDykbyAIAn1CzEef0od7beZIw7w6KQ0OlJOwL6/FKZiBrXl3TW080+aiVow/KaHBBR/Tr7xnAi1OIX+lgJaDxVlc=") }
            });
        }
    }
}