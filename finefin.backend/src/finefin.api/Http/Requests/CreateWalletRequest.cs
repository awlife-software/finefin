using finefin.api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace finefin.api.Http.Requests
{
    public class CreateWalletRequest
    {
        public string? Type { get; set; } = WalletType.CHECKING.ToString();
        public string Name { get; set; } = string.Empty;
        public string? Color { get; set; } = WalletColor.BLUE.ToString();
        public decimal? Balance { get; set; } = decimal.Zero;
    }
}
