using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Gerenciador_Financeiro.Model;
using Gerenciador_Financeiro.Context;

namespace Gerenciador_Financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly GerenciadorFinanceiroContext _context;

        public UsuarioController(GerenciadorFinanceiroContext context)
        {
            _context = context;
        }


        [HttpPost("autenticar")]
        [AllowAnonymous]
        public ActionResult Autenticar([FromBody] Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Login)
            };
            var secret = Environment.GetEnvironmentVariable("appsecretkey");
            if(secret == null)
                secret = "test secret key, please change it";
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "backend-gerenciador-financeiro",
                audience: "frontend-gerenciador-financeiro",
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
        
        [HttpGet]
        public IEnumerable<Usuario> Todos()
        {
            return _context.Usuarios;
        }
     
        [HttpGet("{id}")]
        public ActionResult<Usuario> Obter(long id)
        {
            var usuarioEncontrado = _context.Usuarios.Find(id);

            if (usuarioEncontrado == null)
                return NotFound();
            
            return usuarioEncontrado;
        }

        [HttpPost("novo")]
        public void Novo([FromBody] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Usuario usuario)
        {
            var usuarioEncontrado = _context.Usuarios.Find(usuario.Id);
            if (usuarioEncontrado == null)
                return NotFound();

            usuarioEncontrado.Login = usuario.Login;

            _context.Usuarios.Update(usuarioEncontrado);
            _context.SaveChanges();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var item = _context.Usuarios.Find(id);

            if (item == null)
                return NotFound();

            _context.Usuarios.Remove(item);
            _context.SaveChanges();

            return Ok(item);
        }
    }
}