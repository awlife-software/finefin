namespace finefin.Shared.Communication.Requests.Wallet
{
    public class CreateWalletRequest
    { // TODO: CHECK ENUM STATE FOR REQUESTS
        public string? Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Color { get; set; }
        public decimal? Balance { get; set; } = decimal.Zero;
    }
}
