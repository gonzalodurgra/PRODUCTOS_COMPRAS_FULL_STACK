using Microsoft.EntityFrameworkCore;
using PRODUCTOS_COMPRAS_FULL_STACK.Core;
using PRODUCTOS_COMPRAS_FULL_STACK.Models;
using PRODUCTOS_COMPRAS_FULL_STACK.Resources;

namespace PRODUCTOS_COMPRAS_FULL_STACK.DAO
{
    public class ProductoOrdenDAO
    {
        private readonly ProductoOrdenContext _context;
        private readonly LocService _locator;

        public CustomError customError;

        public ProductoOrdenDAO(ProductoOrdenContext context, LocService locService)
        {
            this._context = context;
            this._locator = locService;
        }

        public Task<List<ProductoOrden>> ObtenerTodo()
        {
            return _context.ProductoOrden.ToListAsync();
        }

        public async Task<ProductoOrden> ObtenerPorIdAsync(int id)
        {
            return await _context.ProductoOrden.FindAsync(id);
        }

        public async Task<bool> AgregarAsync(List<int> productoIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Crear orden vacía
                var orden = new Orden { Fecha = DateTime.UtcNow };
                _context.Orden.Add(orden);
                await _context.SaveChangesAsync();

                float total = 0;

                foreach (var productoId in productoIds)
                {
                    var producto = await _context.Producto.FindAsync(productoId);
                    if (producto == null)
                        continue; // O puedes lanzar excepción si el producto no existe

                    // Sumar al total
                    total += producto.Precio;

                    // Crear relación con producto
                    var productoOrden = new ProductoOrden
                    {
                        OrdenId = orden.Id,
                        ProductoId = producto.Id
                    };
                    _context.ProductoOrden.Add(productoOrden);
                }

                // Guardar relaciones
                await _context.SaveChangesAsync();

                // Actualizar total de la orden
                orden.total = total;
                _context.Orden.Update(orden);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }



        public async Task<bool> ModificarAsync(ProductoOrden productoOrden) 
        {
            var registroRepetido = _context.ProductoOrden.FirstOrDefault(p => p.OrdenId == productoOrden.OrdenId && p.ProductoId == productoOrden.ProductoId);
            if (registroRepetido != null)
            {
                customError = new CustomError(400,
                        String.Format(this._locator
                            .GetLocalizedHtmlString("Repeated"),
                                          "ProductoOrden",
                                          "Claves"), "Productos y orden");
                return false;
            }
            if (ExisteProductoOrden(productoOrden.Id))
            {
                return false;
            }
            _context.Entry(productoOrden).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BorrarAsync(int id)
        {
            var productoOrden = await ObtenerPorIdAsync(id);
            if (productoOrden == null)
            {
                customError = new CustomError(400,
                        String.Format(this._locator
                            .GetLocalizedHtmlString("Not Found"),
                                          "ProductoOrden",
                                          "El producto"), "Productos y orden");
                return false;
            }

            _context.ProductoOrden.Remove(productoOrden);
            await _context.SaveChangesAsync();
            return true;
        }
        private bool ExisteProductoOrden(int id)
        {
            return _context.ProductoOrden.Any(po => po.Id == id);
        }
    } 
}
