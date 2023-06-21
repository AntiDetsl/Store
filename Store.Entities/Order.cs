namespace Store.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string Number { get; set; } = null!;

        public DateTime Date { get; set; }

        public int ProviderId { get; set; }

        public Provider Provider { get; set; } = null!;

        public ICollection<OrderItem> Items { get; set; } = null!;
    }
}
