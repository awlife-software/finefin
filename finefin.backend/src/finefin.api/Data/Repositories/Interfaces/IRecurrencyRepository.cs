using finefin.api.Models.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace finefin.api.Data.Repositories.Interfaces
{
    public interface IRecurrencyRepository : IRepository<Recurrency>
    {
        Task<Recurrency> CreateAndGetAsync(Recurrency recurrency);
    }
}
