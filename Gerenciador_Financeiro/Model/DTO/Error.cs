namespace Gerenciador_Financeiro.Model.DTO
{
    public sealed class Error
    {
        public double code;
        public string field;
        public string description;

        public Error(double code, string description){
            this.code = code;
            this.description = description;
        }
        public Error(double code, string field, string description){
            this.code = code;
            this.field = field;
            this.description = description;
        }
    }
}