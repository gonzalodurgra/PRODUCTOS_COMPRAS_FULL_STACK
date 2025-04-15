using Microsoft.EntityFrameworkCore;
using PRODUCTOS_COMPRAS_FULL_STACK.Core;
using PRODUCTOS_COMPRAS_FULL_STACK.Models;
using PRODUCTOS_COMPRAS_FULL_STACK.Resources;
using System.Linq;

namespace PRODUCTOS_COMPRAS_FULL_STACK.DAO
{
    public class OrdenDAO
    {
		private readonly ProductoOrdenContext contexto;
		private readonly LocService localizacion;

		/// <summary>
		/// Mensaje de error personalizado
		/// </summary>
		public CustomError customError;

		/// <summary>
		/// Clase para acceso a la base de datos
		/// </summary>
		/// <param name="context"></param>
		public OrdenDAO(ProductoOrdenContext context, LocService locService)
		{
			this.contexto = context;
			this.localizacion = locService;
		}

		/// <summary>
		/// Obtiene todas las Productos
		/// </summary>
		/// <returns></returns>
		public async Task<List<Orden>> ObtenerTodo()
		{
			return await contexto.Orden.Include(o => o.ProductoOrdenes).ThenInclude(po => po.Producto).ToListAsync();
		}

		/// <summary>
		/// Obtiene una Producto por us Id
		/// </summary>
		/// <param name="id">Id de la Producto</param>
		/// <returns></returns>
		public async Task<Orden> ObtenerPorIdAsync(int id)
		{
			return await contexto.Orden.FindAsync(id);
		}

		/// <summary>
		/// Permite agregar una nueva Producto
		/// </summary>
		/// <param name="Producto"></param>
		/// <returns></returns>
		public async Task<bool> AgregarAsync(Orden Compra)
		{
            try
            {
                contexto.Orden.Add(Compra);
                await contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
				customError = new CustomError(500, ex.Message);
                return false;
            }
        }

		/// <summary>
		/// Modidica una Producto
		/// </summary>
		/// <param name="Producto">Datos de la Producto</param>
		/// <returns></returns>
		public async Task<bool> ModificarAsync(Orden Compra)
		{
			//Se busca si existe una Producto con el mismo nombre 
			//pero diferente Id
			contexto.Entry(Compra).State = EntityState.Modified;
			await contexto.SaveChangesAsync();
			if (!ExisteCompra(Compra.Id))
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Permite borrar una Producto por Id
		/// </summary>
		/// <param name="id">Id de la Producto</param>
		/// <returns></returns>
		public async Task<bool> BorraAsync(int id)
		{
			var Compra = await ObtenerPorIdAsync(id);
			if (Compra == null)
			{
				customError = new CustomError(404,
					String.Format(this.localizacion
						.GetLocalizedHtmlString("NotFound"),
												"El Producto"), "Id");
				return false;
			}

			contexto.Orden.Remove(Compra);
			await contexto.SaveChangesAsync();
			return true;
		}

		public async Task<bool> ActualizarTotalOrden(int ordenId, float total)
		{
			var orden = await contexto.Orden.FindAsync(ordenId);
			if(orden == null) return false;
			orden.total = total;
			await contexto.SaveChangesAsync();
			return true;
		}

		private bool ExisteCompra(int id)
		{
			return contexto.Orden.Any(e => e.Id == id);
		}
	}
}
