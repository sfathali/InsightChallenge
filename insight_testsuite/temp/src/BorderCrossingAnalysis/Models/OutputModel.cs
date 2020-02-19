using System;

namespace BorderCrossingAnalysis.Models
{
    public class OutputModel : IComparable<OutputModel>
    {
        public string Border { get; set; }
        public DateTime? Date { get; set; }
        public string Measure { get; set; }
        public int Value { get; set; }
        public int Average { get; set; }

        public int CompareTo(OutputModel other)
        {
            //perform sorting
            if (this.Date.Equals(other.Date) || !this.Date.HasValue || !other.Date.HasValue)
            {
                if (this.Value.Equals(other.Value))
                {
                    if (this.Measure.Equals(other.Measure))
                    {
                        return String.Compare(other.Border, this.Border, StringComparison.Ordinal);
                    }
                    return String.Compare(other.Measure, this.Measure, StringComparison.Ordinal);
                }
                return other.Value.CompareTo(this.Value);
            }
            return other.Date.Value.CompareTo(this.Date.Value);
        }
    }
}
