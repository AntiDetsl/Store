using Store.PL.WebPL.Models.Order;

namespace Store.PL.WebPL.Models
{
    public class OrderIndexViewModel
    {
        public PagingInfo PageInfo { get; set; }

        public IEnumerable<DisplayOrderVM> Data { get; set; }
    }
}
