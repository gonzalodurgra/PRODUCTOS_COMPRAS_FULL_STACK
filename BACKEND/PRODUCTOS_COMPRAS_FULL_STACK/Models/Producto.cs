using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace PRODUCTOS_COMPRAS_FULL_STACK.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(80)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "FLOAT")]
        public float Precio { get; set; }

        [Required]
        [Column(TypeName = "INT")]
        public int stock { get; set; }
        
        public ICollection<ProductoOrden>? ProductoOrdenes { get; set; }
    }
}
