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
using AutoMapper;
using Gerenciador_Financeiro.Model.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Gerenciador_Financeiro.Interfaces;
using Gerenciador_Financeiro.Model.DTO.Response;

namespace Gerenciador_Financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly GerenciadorFinanceiroContext _context;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(GerenciadorFinanceiroContext context, IMapper mapper, IUsuarioService usuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpPost("autenticar")]
        [AllowAnonymous]
        public async Task<UsuarioDtoResponse> Autenticar([FromBody] UsuarioDto usuarioInformado)
        {
            return await _usuarioService.LoginAsync(usuarioInformado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Obter(long id)
        {
            var usuarioAutenticado = await _usuarioService.GetUsuarioFromJwtAsync(User);
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e == usuarioAutenticado);

            if (usuarioEncontrado == null)
                return NotFound();

            return usuarioEncontrado;
        }

        [HttpPost("novo")]
        public async Task<IActionResult> Novo([FromBody] UsuarioDto usuarioInformado)
        {
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e.Login == usuarioInformado.login);

            if(usuarioEncontrado != null)
                return Conflict();
            
            var novoUsuario = _mapper.Map<Usuario>(usuarioInformado);

            byte[] salt = Utils.gerarRandomSalt();

            novoUsuario.Salt = salt;
            novoUsuario.Senha = Utils.hashWithSalt(usuarioInformado.senha, salt);

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] Usuario usuario)
        {
            var usuarioAutenticado = await _usuarioService.GetUsuarioFromJwtAsync(User);
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(e => e == usuarioAutenticado);
            if (usuarioEncontrado == null)
                return NotFound();

            usuarioEncontrado.Login = usuario.Login;

            _context.Usuarios.Update(usuarioEncontrado);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var usuarioAutenticado = await _usuarioService.GetUsuarioFromJwtAsync(User);
            var item = await _context.Usuarios.FirstOrDefaultAsync(e => e == usuarioAutenticado);

            if (item == null)
                return NotFound();

            _context.Usuarios.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }
    }
}