namespace finefin.Shared.Communication.Responses.Dashboard
{
    public class SummaryResponse
    {
        public decimal TotalBalance { get; set; }
        public decimal CurrentMonthIncomes { get; set; }
        public decimal CurrentMonthExpenses { get; set; }
        public decimal CurrentMonthBalance { get; set; }

    }
}
