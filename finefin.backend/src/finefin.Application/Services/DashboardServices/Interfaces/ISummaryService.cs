using finefin.Shared.Communication.Responses.Dashboard;

namespace finefin.Application.Services.DashboardServices.Interfaces
{
    public interface ISummaryService
    {
        Task<SummaryResponse> GetSummary(string userId, DateTime date);
    }
}
