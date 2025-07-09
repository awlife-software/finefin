using finefin.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using valet.lib.Core.Domain.Entities;

namespace finefin.Domain.Entities
{
    public class Recurrence : BaseEntity
    {
        protected Recurrence() { }
        public Recurrence(RecurrenceType type, int ocurrences)
        {
            this.Type = type;
            SetOcurrences(ocurrences);
            this.Transactions = [];
        }
        public RecurrenceType Type { get; private set; }
        public int Occurrences { get; private set; }
        public virtual ICollection<Transaction> Transactions { get; private set; }



        private void SetOcurrences(int occurrences)
        {
            if (occurrences <= 0)
                throw new ArgumentException("Occurrences must be greater than zero.", nameof(occurrences));
            this.Occurrences = occurrences;
        }
    }
}
