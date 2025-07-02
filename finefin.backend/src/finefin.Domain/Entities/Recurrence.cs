using finefin.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using valet.lib.Core.Domain.Entities;

namespace finefin.Domain.Entities
{
    public class Recurrence : BaseEntity
    {
        public RecurrenceType Type { get; set; }
        public int Occurrences { get; set; } = 1;
        public virtual ICollection<Transaction> Transactions { get; set; } = [];
    }
}
