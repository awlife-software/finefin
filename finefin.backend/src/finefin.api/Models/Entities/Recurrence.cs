using finefin.api.Models.Enums;
using System.ComponentModel.DataAnnotations;
using valet.lib.Core.Domain.Entities;

namespace finefin.api.Models.Entities
{
    public class Recurrence : BaseEntity
    {
        [EnumDataType(typeof(RecurrenceType))]
        public string Type { get; set; } = string.Empty;
        public int Occurrences { get; set; } = 1;// "Installments" vai ser usado para parcelas de crédito pois é mais adequado
        public virtual ICollection<Transaction> Transactions { get; set; } = [];
    }
}
