using System.ComponentModel.DataAnnotations;

namespace Store.PL.WebPL.Models.OrderItemVM
{
    public class CreateOrderItemVM
    {
        public int OrderId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public string Unit { get; set; }
    }
}
