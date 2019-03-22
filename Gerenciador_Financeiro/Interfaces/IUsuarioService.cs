using Gerenciador_Financeiro.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Gerenciador_Financeiro.Model.DTO.Response;
using Gerenciador_Financeiro.Model.DTO;

namespace Gerenciador_Financeiro.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuarioFromJwtAsync(ClaimsPrincipal user);
        Task<UsuarioDtoResponse> LoginAsync(UsuarioDto usuarioInformado);
    }
}