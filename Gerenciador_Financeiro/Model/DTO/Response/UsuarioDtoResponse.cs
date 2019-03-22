using System.Collections.Generic;

namespace Gerenciador_Financeiro.Model.DTO.Response
{
    public class UsuarioDtoResponse
    {
        public string tokenType;
        public string accessToken;
        public string refreshToken;
        public double expiresIn;
        public List<Error> Errors = new List<Error>();
    }
}