using Gerenciador_Financeiro.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuarioFromJwtAsync(ClaimsPrincipal user);
    }
}