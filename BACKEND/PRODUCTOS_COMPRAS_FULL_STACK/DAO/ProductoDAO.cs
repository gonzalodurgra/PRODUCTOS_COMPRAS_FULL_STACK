using Microsoft.EntityFrameworkCore;
using PRODUCTOS_COMPRAS_FULL_STACK.Core;
using PRODUCTOS_COMPRAS_FULL_STACK.Models;
using PRODUCTOS_COMPRAS_FULL_STACK.Resources;

namespace PRODUCTOS_COMPRAS_FULL_STACK.DAO
{
        public class ProductoDAO
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
            public ProductoDAO(ProductoOrdenContext context, LocService locService)
            {
                this.contexto = context;
                this.localizacion = locService;
            }

            /// <summary>
            /// Obtiene todas las Productos
            /// </summary>
            /// <returns></returns>
            public async Task<List<Producto>> ObtenerTodoAsync()
            {
                return await contexto.Producto.ToListAsync();
            }

            /// <summary>
            /// Obtiene una Producto por us Id
            /// </summary>
            /// <param name="id">Id de la Producto</param>
            /// <returns></returns>
            public async Task<Producto> ObtenerPorIdAsync(int id)
            {
                return await contexto.Producto.FindAsync(id);
            }

            /// <summary>
            /// Permite agregar una nueva Producto
            /// </summary>
            /// <param name="Producto"></param>
            /// <returns></returns>
            public async Task<bool> AgregarAsync(Producto Producto)
            {
                Producto registroRepetido;
                registroRepetido = contexto.Producto.FirstOrDefault(c => c.Nombre == Producto.Nombre);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400,
                            String.Format(this.localizacion
                                .GetLocalizedHtmlString("Repeated"),
                                              "Producto",
                                              "nombre"), "Nombre");
                    return false;
                }
                contexto.Producto.Add(Producto);
                await contexto.SaveChangesAsync();
                return true;
            }

            /// <summary>
            /// Modidica una Producto
            /// </summary>
            /// <param name="Producto">Datos de la Producto</param>
            /// <returns></returns>
            public async Task<bool> ModificarAsync(Producto Producto)
            {
                Producto registroRepetido;

                //Se busca si existe una Producto con el mismo nombre 
                //pero diferente Id
                registroRepetido = contexto.Producto.FirstOrDefault(c => c.Nombre == Producto.Nombre && c.Id != Producto.Id);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400,
                             String.Format(this.localizacion
                                 .GetLocalizedHtmlString("Repeated"),
                                         "Producto", "nombre"), "Nombre");
                    return false;
                }
                contexto.Entry(Producto).State = EntityState.Modified;
                await contexto.SaveChangesAsync();
                if (!ExisteProducto(Producto.Id))
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
                var Producto = await ObtenerPorIdAsync(id);
                if (Producto == null)
                {
                    customError = new CustomError(404,
                        String.Format(this.localizacion
                            .GetLocalizedHtmlString("NotFound"),
                                                    "El Producto"), "Id");
                    return false;
                }

                contexto.Producto.Remove(Producto);
                await contexto.SaveChangesAsync();
                return true;
            }

            private bool ExisteProducto(int id)
            {
                return contexto.Producto.Any(e => e.Id == id);
            }

            public bool EsNombreRepetido(int id, string nombre)
            {
                var registroRepetido = contexto.Producto
                                         .FirstOrDefault(c => c.Nombre == nombre
                                 && c.Id != id);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400, String.Format(
                      this.localizacion.GetLocalizedHtmlString("Repeated"),
                                                "Producto", "nombre"), "Nombre");
                    return true;
                }
                return false;
            }
        }
}


