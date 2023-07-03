namespace Store.PL.WebPL.Models
{
    public class IndexViewModel
    {
        public PagingInfo PageInfo { get; set; }

        public DataVM Data { get; set; }

        public FiltersVM Filter { get; set; }
    }
}
