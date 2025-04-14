using finefin.api.Models.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.api.Data.Repositories.Interfaces
{
    public interface IRecurrenceRepository : IRepository<Recurrence>
    {
        Task<Recurrence> CreateAndGetAsync(Recurrence recurrence);
    }
}
