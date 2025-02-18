namespace finefin.api.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
