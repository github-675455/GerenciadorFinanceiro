using System.Text;
using AutoMapper;
using Gerenciador_Financeiro.Model;
using Gerenciador_Financeiro.Model.DTO;

namespace Gerenciador_Financeiro
{
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<ContaDto, Conta>();
            CreateMap<DespesaDto, Despesa>();
            CreateMap<ReceitaDto, Receita>();
            CreateMap<UsuarioDto, Usuario>()
            .ForMember(destination => destination.Senha, options => options.MapFrom(source => Encoding.UTF8.GetBytes(source.senha)));
        }
    }
}