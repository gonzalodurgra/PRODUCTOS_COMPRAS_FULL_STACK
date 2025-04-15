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

        public async Task<bool> AgregarAsync(int productoId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var producto = await _context.Producto.FindAsync(productoId);
                var total = producto.Precio * producto.stock;
                // Crear nueva orden
                var orden = new Orden { Fecha = DateTime.Now.ToUniversalTime(), total = total };
                _context.Orden.Add(orden);
                await _context.SaveChangesAsync();

                // Crear relación con producto
                var productoOrden = new ProductoOrden
                {
                    OrdenId = orden.Id,
                    ProductoId = productoId
                };
                _context.ProductoOrden.Add(productoOrden);
                await _context.SaveChangesAsync();

                // Calcular total si quieres
                

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
