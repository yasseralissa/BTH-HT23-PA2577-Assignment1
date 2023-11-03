using System.ComponentModel.DataAnnotations.Schema;

namespace BTH.H23.PA2577.Assignment1.OrderService.Models
{
    public class OrderLine
    {
        public Guid Id { get; set; }
        public int LineNum { get; set; }

        //public Order Order { get; set; }
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }
        public int OrderedQuantity { get; set; }
        
    }
}