using TesteBancoMaster.Infra.Data;

namespace TesteBancoMaster.API.Services
{
    public abstract class Service
    {
        protected async Task PersistirDados(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Commit())
                throw new Exception("Houve um erro ao persistir os dados");
        }
    }
}
