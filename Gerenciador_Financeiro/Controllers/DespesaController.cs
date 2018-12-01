using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Gerenciador_Financeiro.Model;
using Gerenciador_Financeiro.Context;

namespace Gerenciador_Financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {        
        private readonly GerenciadorFinanceiroContext _context;

        public DespesaController(GerenciadorFinanceiroContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<Despesa> Todos()
        {
            return _context.Despesas;
        }
     
        [HttpGet("{id}")]
        public ActionResult<Despesa> Obter(long id)
        {
            var despesaEncontrado = _context.Despesas.Find(id);

            if (despesaEncontrado == null)
                return NotFound();
            
            return despesaEncontrado;
        }

        [HttpPost]
        public void Novo([FromBody] Despesa despesa)
        {
            _context.Despesas.Add(despesa);
            _context.SaveChanges();
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Despesa despesa)
        {
            var despesaEncontrado = _context.Despesas.Find(despesa.Id);
            if (despesaEncontrado == null)
                return NotFound();

            despesaEncontrado.DataDespesa = despesa.DataDespesa;

            _context.Despesas.Update(despesaEncontrado);
            _context.SaveChanges();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var item = _context.Despesas.Find(id);

            if (item == null)
                return NotFound();

            _context.Despesas.Remove(item);
            _context.SaveChanges();

            return Ok(item);
        }
    }
}