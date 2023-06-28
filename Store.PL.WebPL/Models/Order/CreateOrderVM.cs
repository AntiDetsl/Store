using Store.Entities;
using System.ComponentModel.DataAnnotations;

namespace Store.PL.WebPL.Models.Order
{
    public class CreateOrderVM
    {
        [Required]
        public string Number { get; set; } = null!;

        [Required]
        [UIHint("date")]
        public DateTime Date { get; set; }

        [Required]
        public int Provider { get; set; }
    }
}
