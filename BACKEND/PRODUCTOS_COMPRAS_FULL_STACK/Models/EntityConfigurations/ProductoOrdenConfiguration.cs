using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PRODUCTOS_COMPRAS_FULL_STACK.Models.EntityConfigurations
{
    public class ProductoOrdenConfiguration : IEntityTypeConfiguration<ProductoOrden>
    {
        public void Configure(EntityTypeBuilder<ProductoOrden> builder)
        {
            builder.HasIndex(po => new { po.OrdenId, po.ProductoId }).IsUnique();
            builder.HasOne(po => po.Producto)
            .WithMany(p => p.ProductoOrdenes)
            .HasForeignKey(po => po.ProductoId);
            builder.HasOne(po => po.Orden)
            .WithMany(o => o.ProductoOrdenes)
            .HasForeignKey(po => po.OrdenId);
        }
    }
}
