using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement.Core.Model
{
    public class OrderItem
    {
        public int Id { get; set; } // Primary Key
        public int GoodsId { get; set; } // Foreign Key
        public int Quantity { get; set; }
        public Goods Goods { get; set; } // Navigation property
    }
}
