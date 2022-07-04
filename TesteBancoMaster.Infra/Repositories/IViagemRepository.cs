using System.Linq.Expressions;
using TesteBancoMaster.Infra.Data;
using TesteBancoMaster.Infra.Entities;

namespace TesteBancoMaster.Infra.Repositories
{
    public interface IViagemRepository : IDisposable 
    {
        IUnitOfWork UnitOfWork { get; }

        Task<Viagem> ObterViagem(Expression<Func<Viagem, bool>> query);
        Task<List<Viagem>> ObterViagens(Expression<Func<Viagem, bool>> query);
        Task<List<Viagem>> ObterTodos();
        Task CadastrarViagem(Viagem request);
        void AtualizarViagem(Viagem request);
        bool DeletarViagem(Viagem viagemId);
    }
}
