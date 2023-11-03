#nullable disable
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTH.H23.PA2577.Assignment1.OrderService.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public string InvoiceAddress { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}

