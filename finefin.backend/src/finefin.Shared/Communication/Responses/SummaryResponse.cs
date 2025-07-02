namespace finefin.Shared.Communication.Responses
{
    public class SummaryResponse
    {
        public decimal TotalBalance { get; set; }
        public decimal CurrentMonthIncomes { get; set; }
        public decimal CurrentMonthExpenses { get; set; }
        public decimal CurrentMonthBalance { get; set; }

    }
}
