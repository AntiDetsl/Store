using System.ComponentModel.DataAnnotations;

namespace Store.PL.WebPL.Models.Order
{
    public class OrderFiltersVM
    {
        public IEnumerable<string> Numbers { get; set; }

        public IEnumerable<string> Providers { get; set; }

        [UIHint("date")]
        public DateTime? StartDate { get; set; }

        [UIHint("date")]
        public DateTime? EndDate { get; set; }
    }
}
