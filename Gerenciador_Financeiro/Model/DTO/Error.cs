namespace Gerenciador_Financeiro.Model.DTO
{
    public sealed class Error
    {
        public double Code;
        public string Description;

        public Error(double code, string description){
            Code = code;
            Description = description;
        }
    }
}