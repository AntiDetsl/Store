namespace Store.Entities.Filters
{
    public class OrderFilters
    {
        public IEnumerable<string> Numbers { get; set; }

        public IEnumerable<string> Providers { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ItemFilters? ItemFilters { get; set; }
    }
}
