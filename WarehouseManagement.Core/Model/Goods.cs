using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement.Core.Model
{
    public class Goods
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
