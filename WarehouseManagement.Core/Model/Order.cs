using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement.Core.Model
{
    public class Order
    {
        public int Id { get; set; } // Primary Key
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; } // Navigation property
    }
}
