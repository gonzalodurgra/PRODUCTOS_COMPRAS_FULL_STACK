using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRODUCTOS_COMPRAS_FULL_STACK.Models
{
    public class Orden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column(TypeName = "FLOAT")]
        public float total { get; set; }
        public ICollection<ProductoOrden> ProductoOrdenes { get; set; }
    }
}

