using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PRODUCTOS_COMPRAS_FULL_STACK.DAO;
using PRODUCTOS_COMPRAS_FULL_STACK.Models;
using PRODUCTOS_COMPRAS_FULL_STACK.Resources;

namespace PRODUCTOS_COMPRAS_FULL_STACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoOrdenContext _context;
        private readonly LocService _locator;
        private ProductoDAO productoDAO;

        public ProductosController(ProductoOrdenContext context, IStringLocalizerFactory locator)
        {
            _context = context;
            _locator = new LocService(locator);
            productoDAO = new ProductoDAO(_context, _locator);
        }

        [HttpGet]
        public async Task<List<Producto>> GetProducto()
        {
            return await productoDAO.ObtenerTodoAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoById([FromRoute] int id)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto([FromRoute] int id, Producto p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != p.Id)
                return BadRequest();

            if (!await productoDAO.ModificarAsync(p))
            {
                return StatusCode(productoDAO.customError.StatusCode, productoDAO.customError.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await productoDAO.AgregarAsync(producto))
            {
                return StatusCode(productoDAO.customError.StatusCode, productoDAO.customError.Message);
            }
            return CreatedAtAction("GetProducto",
                                      new { id = producto.Id }, producto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            if (!await productoDAO.BorraAsync(id))
            {
                return StatusCode(productoDAO.customError.StatusCode, productoDAO.customError.Message);
            }
            return Ok();
        }
    }
}
