namespace finefin.api.Http.Responses
{
    public class SummaryResponse
    {
        public decimal TotalBalance { get; set; }
        public decimal CurrentMonthIncomes { get; set; }
        public decimal CurrentMonthExpenses { get; set; }
        public decimal CurrentMonthBalance { get; set; }

    }
}
