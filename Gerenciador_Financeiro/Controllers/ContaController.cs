using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gerenciador_Financeiro.Model;
using Gerenciador_Financeiro.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_Financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly GerenciadorFinanceiroContext _context;

        public ContaController(GerenciadorFinanceiroContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<Conta> Todos()
        {
            return _context.Contas
            .Include(e => e.Despesas)
            .Include(e => e.Receitas);
        }
     
        [HttpGet("{id}")]
        public ActionResult<Usuario> Obter(long id)
        {
            var contaEncontrado = _context.Contas.Find(id);

            if (contaEncontrado == null)
                return NotFound();
            
            return Ok(contaEncontrado);
        }

        [HttpPost]
        public void Novo([FromBody] Conta conta)
        {
            _context.Contas.Add(conta);
            _context.SaveChanges();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Conta conta)
        {
            var contaEncontrado = _context.Contas.Find(conta.Id);
            if (contaEncontrado == null)
                return NotFound();

            contaEncontrado.Nome = conta.Nome;
            contaEncontrado.Descricao = conta.Descricao;

            _context.Contas.Update(contaEncontrado);
            _context.SaveChanges();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var item = _context.Contas.Find(id);

            if (item == null)
                return NotFound();

            _context.Contas.Remove(item);
            _context.SaveChanges();

            return Ok(item);
        }
    }
}
