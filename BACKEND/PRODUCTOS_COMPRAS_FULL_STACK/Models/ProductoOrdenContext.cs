using Microsoft.EntityFrameworkCore;
using PRODUCTOS_COMPRAS_FULL_STACK.Models.EntityConfigurations;

namespace PRODUCTOS_COMPRAS_FULL_STACK.Models
{
    public class ProductoOrdenContext : DbContext
    {
        public ProductoOrdenContext(DbContextOptions<ProductoOrdenContext> options) : base(options) 
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductoOrdenConfiguration());
        }

        public virtual DbSet<Producto> Producto { get; set; }

        public virtual DbSet<Orden> Orden { get; set; }

        public virtual DbSet<ProductoOrden> ProductoOrden { get; set; }
    }
}
