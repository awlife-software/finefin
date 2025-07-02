using finefin.Shared.Communication.Responses;

namespace finefin.Application.Providers.Services.DashboardServices.Interfaces
{
    public interface ISummaryService
    {
        Task<SummaryResponse> GetSummary(string userId, DateTime date);
    }
}
