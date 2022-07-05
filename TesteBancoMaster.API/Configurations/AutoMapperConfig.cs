using AutoMapper;
using TesteBancoMaster.API.Models;
using TesteBancoMaster.Infra.Entities;

namespace TesteBancoMaster.API.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            IMapper mapper = AutoMapperConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static MapperConfiguration RegisterMaps()
        {
            //var mappingConfig = new MapperConfiguration(config =>
            //{
            //    config.CreateMap<ViagemCadastroModelRequest, Viagem>().ReverseMap();
            //    config.CreateMap<ViagemAtualizarModelRequest, Viagem>().ReverseMap();
            //});

            var config = new MapperConfiguration(x => x.AddProfile<MyProfile>());

            return config;
        }
    }

    public class MyProfile : Profile
    {
        public MyProfile()
        {
            CreateMap<ViagemCadastroModelRequest, Viagem>().ReverseMap();
            CreateMap<ViagemAtualizarModelRequest, Viagem>().ReverseMap();
        }
    }
}
