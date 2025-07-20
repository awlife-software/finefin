using finefin.Shared.Communication.Responses;

namespace finefin.Application.UseCases.Dashboard.Interfaces
{
    public interface ISummary
    {
        Task<SummaryResponse> GetSummary(string userId, DateTime date);
    }
}
