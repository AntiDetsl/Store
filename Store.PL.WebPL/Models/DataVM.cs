using Store.PL.WebPL.Models.Order;
using Store.PL.WebPL.Models.OrderItemVM;

namespace Store.PL.WebPL.Models
{
    public class DataVM
    {
        public IEnumerable<DisplayOrderVM> Orders { get; set; }

        public ItemInfoVM ItemInfo { get; set; }

        public IEnumerable<string> Numbers { get; set; }

        public IEnumerable<string> Providers { get; set; }
    }
}
