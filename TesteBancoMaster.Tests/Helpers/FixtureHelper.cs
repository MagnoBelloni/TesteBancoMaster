using TesteBancoMaster.Infra.Entities;

namespace TesteBancoMaster.Tests.Helpers
{
    public static class FixtureHelper
    {
        public static List<Viagem> ObterListaLocalizacoes()
        {
            return new List<Viagem>()
            {
                new Viagem{Id = 1,Origem = "GRU",Destino = "BRC",ValorPassagem = 10},
                new Viagem{Id = 2,Origem = "BRC",Destino = "SCL",ValorPassagem = 5},
                new Viagem{Id = 3,Origem = "GRU",Destino = "CDG",ValorPassagem = 75},
                new Viagem{Id = 4,Origem = "GRU",Destino = "SCL",ValorPassagem = 20},
                new Viagem{Id = 5,Origem = "GRU",Destino = "ORL",ValorPassagem = 56},
                new Viagem{Id = 6,Origem = "ORL",Destino = "CDG",ValorPassagem = 5},
                new Viagem{Id = 7,Origem = "SCL",Destino = "ORL",ValorPassagem = 20},
            };
        }
    }
}
