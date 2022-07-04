using TesteBancoMaster.API.Models;
using TesteBancoMaster.Infra.Entities;

namespace TesteBancoMaster.API.Services.Interfaces
{
    public interface IViagemService
    {
        Task<List<Viagem>> ObterTodos();
        Task<List<Viagem>> ObterDestinos(string origem);
        Task<Viagem> CadastrarViagem(ViagemCadastroModelRequest request);
        Task<Viagem> AtualizarViagem(int idOrigem, ViagemAtualizarModelRequest request);
        Task<bool> DeletarViagem(int id);
        Task<string> ObterRotaCustoBaixo(ViagemObterRotaCustoBaixoModelRequest request);
    }
}
