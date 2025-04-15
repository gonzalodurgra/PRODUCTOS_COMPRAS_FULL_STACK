using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PRODUCTOS_COMPRAS_FULL_STACK.DAO;
using PRODUCTOS_COMPRAS_FULL_STACK.Models;
using PRODUCTOS_COMPRAS_FULL_STACK.Resources;

namespace PRODUCTOS_COMPRAS_FULL_STACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosOrdenesController : ControllerBase
    {
        private readonly ProductoOrdenContext _context;
        private readonly LocService _locator;
        private ProductoOrdenDAO productoOrdenDAO;

        public ProductosOrdenesController(ProductoOrdenContext context, IStringLocalizerFactory locator)
        {
            _context = context;
            _locator = new LocService(locator);
            productoOrdenDAO = new ProductoOrdenDAO(_context, _locator);
        }

        [HttpGet]
        public async Task<List<ProductoOrden>> GetProductoOrden()
        {
            return await productoOrdenDAO.ObtenerTodo();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoOrdenById([FromRoute] int id)
        {
            var productoOrden = await _context.Producto.FindAsync(id);

            if (productoOrden == null)
            {
                return NotFound();
            }
            return Ok(productoOrden);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoOrden([FromRoute] int id, ProductoOrden p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != p.Id)
                return BadRequest();

            if (!await productoOrdenDAO.ModificarAsync(p))
            {
                return StatusCode(productoOrdenDAO.customError.StatusCode, productoOrdenDAO.customError.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostProductoOrden([FromBody] ProductoOrden p)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await productoOrdenDAO.AgregarAsync(p.ProductoId))
            {
                return StatusCode(productoOrdenDAO.customError.StatusCode, productoOrdenDAO.customError.Message);
            }
            return CreatedAtAction("GetProductoOrden",
                                      new { id = p.Id }, p);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductoOrden(int id)
        {
            if (!await productoOrdenDAO.BorrarAsync(id))
            {
                return StatusCode(productoOrdenDAO.customError.StatusCode, productoOrdenDAO.customError.Message);
            }
            return Ok();
        }
    }
}
