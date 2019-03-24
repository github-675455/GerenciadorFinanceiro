using System.ComponentModel.DataAnnotations;

namespace Gerenciador_Financeiro.Model.DTO
{
    public class UsuarioDto
    {
        [Required, MinLength(3), MaxLength(256)]
        public string login { get; set; }

        [Required, MinLength(8), MaxLength(256)]
        public string senha { get; set; } 
    }
}