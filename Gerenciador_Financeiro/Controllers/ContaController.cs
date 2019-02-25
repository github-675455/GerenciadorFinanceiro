using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gerenciador_Financeiro.Model;
using Gerenciador_Financeiro.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Gerenciador_Financeiro.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Gerenciador_Financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContaController : ControllerBase
    {
        private readonly GerenciadorFinanceiroContext _context;
        private readonly IMapper _mapper;

        public ContaController(GerenciadorFinanceiroContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IList<Conta>> Todos()
        {
            var usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e.Login == usuario);

            return await _context.Contas
            .Where(e => e.Usuario == usuarioEncontrado)
            .Include(e => e.Despesas)
            .Include(e => e.Receitas)
            .ToListAsync();
        }
     
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Obter(long id)
        {
            var usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e.Login == usuario);            

            var contaEncontrada = await _context.Contas.FindAsync(id);

            if(contaEncontrada.Usuario != usuarioEncontrado)
                return Forbid();

            if (contaEncontrada == null)
                return NotFound();
            
            return Ok(contaEncontrada);
        }

        [HttpPost]
        public async Task<IActionResult> Nova([FromBody] ContaDto contaInformada)
        {
            var usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e.Login == usuario);

            var contaEncontrada = await _context.Contas.FirstOrDefaultAsync(e => e.Descricao == contaInformada.Descricao && e.Usuario == usuarioEncontrado);

            if(contaEncontrada != null)
                return Conflict();

            var novaConta = _mapper.Map<Conta>(contaInformada);

            novaConta.Usuario = usuarioEncontrado;

            _context.Contas.Add(novaConta);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] Conta conta)
        {
            var usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e.Login == usuario);

            var contaEncontrada = await _context.Contas.FindAsync(conta.Id);

            if(contaEncontrada.Usuario != usuarioEncontrado)
                return Forbid();

            if (contaEncontrada == null)
                return NotFound();

            contaEncontrada.Nome = conta.Nome;
            contaEncontrada.Descricao = conta.Descricao;

            _context.Contas.Update(contaEncontrada);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e.Login == usuario);

            var contaEncontrada = await _context.Contas.FindAsync(id);

            if(contaEncontrada.Usuario != usuarioEncontrado)
                return Forbid();

            if (contaEncontrada == null)
                return NotFound();

            _context.Contas.Remove(contaEncontrada);
            await _context.SaveChangesAsync();

            return Ok(contaEncontrada);
        }
    }
}
