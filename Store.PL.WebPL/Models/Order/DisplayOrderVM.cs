using Store.Entities;

namespace Store.PL.WebPL.Models.Order
{
    public class DisplayOrderVM
    {
        public int ID { get; set; }

        public string Number { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Provider { get; set; } = null!;

        public IEnumerable<OrderItem> Items { get; set; } = null!;
    }
}
