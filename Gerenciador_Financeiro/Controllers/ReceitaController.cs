using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Gerenciador_Financeiro.Model;
using Gerenciador_Financeiro.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Gerenciador_Financeiro.Model.DTO;
using AutoMapper;
using Gerenciador_Financeiro.Interfaces;

namespace Gerenciador_Financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReceitaController : ControllerBase
    {        
        private readonly GerenciadorFinanceiroContext _context;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public ReceitaController(GerenciadorFinanceiroContext context, IMapper mapper, IUsuarioService usuarioService)
        {
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }
        
        [HttpGet]
        public async Task<IList<Receita>> Todos()
        {
            var usuarioEncontrado = await _usuarioService.GetUsuarioFromJwtAsync(User);

            return await _context.Receitas
            .Where(e => e.Conta.Usuario == usuarioEncontrado)
            .Include(e => e.Conta)
            .ToListAsync();
        }
     
        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> Obter(long id)
        {
            var usuarioEncontrado = await _usuarioService.GetUsuarioFromJwtAsync(User);

            var receitaEncontrada = await _context.Receitas.FirstOrDefaultAsync(e => e.Id == id && e.Conta.Usuario == usuarioEncontrado);

            if (receitaEncontrada == null)
                return NotFound();
            
            return receitaEncontrada;
        }

        [HttpPost]
        public async Task<ActionResult<Receita>> Novo([FromBody] ReceitaDto receitaInformada)
        {
            var usuarioEncontrado = await _usuarioService.GetUsuarioFromJwtAsync(User);
            
            var receitaEncontrada = await _context.Receitas.FirstOrDefaultAsync(e => e.Descricao == receitaInformada.Descricao && e.Conta.Usuario != usuarioEncontrado);

            if(receitaEncontrada != null)
                return Conflict();

            if(receitaInformada.Conta == null || receitaInformada.Conta.Id == 0)
                return BadRequest();

            var contaEncontrada = await _context.Contas.FirstOrDefaultAsync(e => e.Id == receitaInformada.Conta.Id && e.Usuario == usuarioEncontrado);

            if(contaEncontrada == null)
                return NotFound();
            
            var novaReceita = _mapper.Map<Receita>(receitaInformada);

            if(contaEncontrada != null)
                novaReceita.Conta = contaEncontrada;

            _context.Receitas.Add(novaReceita);
            await _context.SaveChangesAsync();

            return Ok(novaReceita);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] Receita receita)
        {
            var usuarioAutenticado = await _usuarioService.GetUsuarioFromJwtAsync(User);

            var receitaEncontrada = await _context.Receitas.FirstOrDefaultAsync(e => e.Id == receita.Id && e.Conta.Usuario == usuarioAutenticado);
            if (receitaEncontrada == null)
                return NotFound();

            receitaEncontrada.DataReceita = receita.DataReceita;
            receitaEncontrada.Descricao = receita.Descricao;
            receitaEncontrada.Valor = receita.Valor;

            var contaEncontrado = await _context.Contas.FirstOrDefaultAsync(e => e.Id == receita.Conta.Id && e.Usuario == usuarioAutenticado);

            if(contaEncontrado == null)
                return NotFound();

            receitaEncontrada.Conta = contaEncontrado;

            _context.Receitas.Update(receitaEncontrada);
            await _context.SaveChangesAsync();

            return Ok(receitaEncontrada);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var usuarioEncontrado = await _usuarioService.GetUsuarioFromJwtAsync(User);

            var receitaEncontrada = await _context.Receitas.FirstOrDefaultAsync(e => e.Id == id && e.Conta.Usuario == usuarioEncontrado);

            if (receitaEncontrada == null)
                return NotFound();

            _context.Receitas.Remove(receitaEncontrada);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}