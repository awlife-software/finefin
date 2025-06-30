using finefin.api.Http.Responses;

namespace finefin.api.Providers.Services.DashboardServices.Interfaces
{
    public interface ISummaryService
    {
        Task<SummaryResponse> GetSummary(string userId, DateTime date);
    }
}
