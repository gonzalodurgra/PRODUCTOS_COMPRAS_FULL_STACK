using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PRODUCTOS_COMPRAS_FULL_STACK.Models
{
    public class ProductoOrden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductoId { get; set; }

        [JsonIgnore]
        public Producto? Producto { get; set; }

        public int OrdenId { get; set; }

        [JsonIgnore]
        public Orden? Orden { get; set; }
    }

}
