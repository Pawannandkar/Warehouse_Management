using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagement.Core.Model;

namespace WarehouseManagement.Data.Data
{
    public class WarehouseContext : DbContext
    {

        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {

        }

        public DbSet<Goods> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Goods)
                .WithMany()
                .HasForeignKey(oi => oi.GoodsId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
