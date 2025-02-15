using finefin.api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace finefin.api.Models.Entities
{
    public class Recurrency : BaseEntity
    {
        [EnumDataType(typeof(RecurrencyType))]
        public string Type { get; set; } = string.Empty;
        public int Occurrences { get; set; } // "Installments" vai ser usado para parcelas de crédito pois é mais adequado
        public virtual ICollection<Transaction> Transactions { get; set; } = [];
    }
}
