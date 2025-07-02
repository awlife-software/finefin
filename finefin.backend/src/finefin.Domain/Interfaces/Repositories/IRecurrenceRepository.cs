using finefin.Domain.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.Domain.Interfaces.Repositories
{
    public interface IRecurrenceRepository : IRepository<Recurrence>
    {
        Task<Recurrence> CreateAndGetAsync(Recurrence recurrence);
    }
}
