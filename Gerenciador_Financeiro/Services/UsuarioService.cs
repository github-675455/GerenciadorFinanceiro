using System.Security.Claims;
using System.Threading.Tasks;
using Gerenciador_Financeiro.Context;
using Gerenciador_Financeiro.Interfaces;
using Gerenciador_Financeiro.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Gerenciador_Financeiro.Model.DTO.Response;
using Gerenciador_Financeiro.Model.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace Gerenciador_Financeiro.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly GerenciadorFinanceiroContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioService(GerenciadorFinanceiroContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Task<Usuario> GetUsuarioFromJwtAsync(ClaimsPrincipal user)
        {
            var usuarioInformado = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return _context.Usuarios.FirstOrDefaultAsync(e => e.Login == usuarioInformado);
        }
        
        public async Task<UsuarioDtoResponse> LoginAsync(UsuarioDto usuarioInformado)
        {
            var usuarioResponse = new UsuarioDtoResponse();

            var issuer = _configuration["Issuer"];
            var audience = _configuration["Audience"];

            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e.Login == usuarioInformado.Login);

            if (usuarioEncontrado == null){
                usuarioResponse.Errors.Add(new Error(404, "Usuário não encontrado"));
                return usuarioResponse;
            }

            var senhaInformadaComputada = Utils.hashWithSalt(usuarioInformado.Senha, usuarioEncontrado.Salt);

            if (!senhaInformadaComputada.SequenceEqual(usuarioEncontrado.Senha))
            {
                usuarioResponse.Errors.Add(new Error(401, "Senha incorreta"));
            }

            if(usuarioResponse.Errors.Any())
                return usuarioResponse;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioInformado.Login),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.Id.ToString())
            };
            var secret = Environment.GetEnvironmentVariable("appsecretkey");
            if (secret == null)
                secret = "test secret key, please change it";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var now = DateTime.UtcNow;

            const double expiraEmSegundos = 600;

            var expires = now.AddSeconds(expiraEmSegundos);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds);

            var accessToken = new JwtSecurityTokenHandler();

            usuarioResponse.tokenType = "Bearer";
            usuarioResponse.accessToken = accessToken.WriteToken(token);
            usuarioResponse.expiresIn = expiraEmSegundos;

            return usuarioResponse;
        }
    }
}