using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Gerenciador_Financeiro.Model;
using Gerenciador_Financeiro.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Gerenciador_Financeiro.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Gerenciador_Financeiro.Model.DTO;

namespace Gerenciador_Financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DespesaController : ControllerBase
    {        
        private readonly GerenciadorFinanceiroContext _context;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public DespesaController(GerenciadorFinanceiroContext context, IMapper mapper, IUsuarioService usuarioService)
        {
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }
        
        [HttpGet]
        public  async Task<IList<Despesa>> Todas()
        {
            var usuarioEncontrado = await _usuarioService.GetUsuarioFromJwtAsync(User);
            
            return await _context.Despesas
            .Where(e => e.Conta.Usuario == usuarioEncontrado)
            .Include(e => e.Conta)
            .ToListAsync();
        }
     
        [HttpGet("{id}")]
        public async Task<ActionResult<Despesa>> Obter(long id)
        {
            var usuarioEncontrado = await _usuarioService.GetUsuarioFromJwtAsync(User);

            var despesaEncontrada = await _context.Despesas.FirstOrDefaultAsync(e => e.Id == id && e.Conta.Usuario == usuarioEncontrado);

            if (despesaEncontrada == null)
                return NotFound();
            
            return despesaEncontrada;
        }

        [HttpPost]
        public async Task<ActionResult<Despesa>> Novo([FromBody] DespesaDto despesaInformada)
        {
            var usuarioEncontrado = await _usuarioService.GetUsuarioFromJwtAsync(User);
            
            var despesaEncontrada = await _context.Despesas.FirstOrDefaultAsync(e => e.Descricao == despesaInformada.Descricao && e.Conta.Usuario == usuarioEncontrado);

            if(despesaEncontrada != null)
                return Conflict();

            if(despesaInformada.Conta == null || despesaInformada.Conta.Id == 0)
                return BadRequest();

            var contaEncontrada = await _context.Contas.FirstOrDefaultAsync(e => e.Id == despesaInformada.Conta.Id && e.Usuario == usuarioEncontrado);
            
            if(contaEncontrada == null)
                return NotFound();

            var novaDespesa = _mapper.Map<Despesa>(despesaInformada);

            if(contaEncontrada != null)
                novaDespesa.Conta = contaEncontrada;

            _context.Despesas.Add(novaDespesa);
            await _context.SaveChangesAsync();

            return Ok(novaDespesa);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] Despesa despesa)
        {
            var usuarioEncontrado = await _usuarioService.GetUsuarioFromJwtAsync(User);

            var despesaEncontrada = await _context.Despesas.FirstOrDefaultAsync(e => e.Id == despesa.Id && e.Conta.Usuario == usuarioEncontrado);
            if (despesaEncontrada == null)
                return NotFound();

            despesaEncontrada.DataDespesa = despesa.DataDespesa;
            despesaEncontrada.Descricao = despesa.Descricao;
            despesaEncontrada.Valor = despesa.Valor;

            var contaEncontrado = await _context.Contas.FirstOrDefaultAsync(e => e.Id == despesa.Conta.Id && e.Usuario == usuarioEncontrado);

            if(contaEncontrado == null)
                return NotFound();

            despesaEncontrada.Conta = contaEncontrado;

            _context.Despesas.Update(despesaEncontrada);
            await _context.SaveChangesAsync();

            return Ok(despesaEncontrada);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var usuarioEncontrado = await _usuarioService.GetUsuarioFromJwtAsync(User);

            var despesaEncontrada = await _context.Despesas.FirstOrDefaultAsync(e => e.Id == id && e.Conta.Usuario == usuarioEncontrado);

            if (despesaEncontrada == null)
                return NotFound();

            _context.Despesas.Remove(despesaEncontrada);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}