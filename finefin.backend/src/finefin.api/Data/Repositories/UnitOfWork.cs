using finefin.api.Data.Repositories.Interfaces;

namespace finefin.api.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db) => _db = db;
        public async Task Commit() => await _db.SaveChangesAsync();
    }
}
