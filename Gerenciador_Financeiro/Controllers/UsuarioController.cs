using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Gerenciador_Financeiro.Model;
using Gerenciador_Financeiro.Context;

namespace Gerenciador_Financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly GerenciadorFinanceiroContext _context;

        public UsuarioController(GerenciadorFinanceiroContext context)
        {
            _context = context;
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

        [HttpPost]
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