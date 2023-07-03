using System.ComponentModel.DataAnnotations;

namespace Store.PL.WebPL.Models
{
    public class FiltersVM
    {
        public IEnumerable<string> Numbers { get; set; }

        public IEnumerable<string> Providers { get; set; }

        [UIHint("date")]
        public DateTime? StartDate { get; set; }

        [UIHint("date")]
        public DateTime? EndDate { get; set; }

        public IEnumerable<string> ItemNames { get; set; }

        public IEnumerable<string> ItemUnits { get; set; }
    }
}
