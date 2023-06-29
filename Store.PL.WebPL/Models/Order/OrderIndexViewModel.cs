namespace Store.PL.WebPL.Models.Order
{
    public class OrderIndexViewModel
    {
        public PagingInfo PageInfo { get; set; }

        public OrderDataVM Data { get; set; }

        public OrderFiltersVM Filter { get; set; }
    }
}
