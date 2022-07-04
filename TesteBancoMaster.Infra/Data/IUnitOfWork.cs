namespace TesteBancoMaster.Infra.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
