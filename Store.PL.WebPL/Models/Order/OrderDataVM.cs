namespace Store.PL.WebPL.Models.Order
{
    public class OrderDataVM
    {
        public IEnumerable<DisplayOrderVM> Orders { get; set; }

        public IEnumerable<string> Numbers { get; set; }

        public IEnumerable<string> Providers { get; set; }
    }
}
