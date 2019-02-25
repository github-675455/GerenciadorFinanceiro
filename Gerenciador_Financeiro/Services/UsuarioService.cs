using System.Security.Claims;
using System.Threading.Tasks;
using Gerenciador_Financeiro.Context;
using Gerenciador_Financeiro.Interfaces;
using Gerenciador_Financeiro.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_Financeiro.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly GerenciadorFinanceiroContext _context;

        public UsuarioService(GerenciadorFinanceiroContext context)
        {
            _context = context;
        }

        public Task<Usuario> GetUsuarioFromJwtAsync(ClaimsPrincipal user)
        {
            var usuarioInformado = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return _context.Usuarios.FirstOrDefaultAsync(e => e.Login == usuarioInformado);
        }
    }
}