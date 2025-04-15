using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PRODUCTOS_COMPRAS_FULL_STACK.DAO;
using PRODUCTOS_COMPRAS_FULL_STACK.Models;
using PRODUCTOS_COMPRAS_FULL_STACK.Resources;

namespace PRODUCTOS_COMPRAS_FULL_STACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly ProductoOrdenContext _context;
        private readonly LocService _locator;
        private OrdenDAO ordenDAO;

        public OrdenesController(ProductoOrdenContext context, IStringLocalizerFactory locator)
        {
            _context = context;
            _locator = new LocService(locator);
            ordenDAO = new OrdenDAO(_context, _locator);
        }

        [HttpGet]
        public async Task<List<Orden>> GetCompra()
        {
            return await ordenDAO.ObtenerTodo();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompraById([FromRoute] int id)
        {
            var compra = await _context.Orden.FindAsync(id);
            if(compra == null)
            {
                return NotFound();
            }
            return Ok(compra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra([FromRoute] int id, Orden c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != c.Id)
                return BadRequest();

            if (!await ordenDAO.ModificarAsync(c))
            {
                return StatusCode(ordenDAO.customError.StatusCode, ordenDAO.customError.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostCompra([FromBody] Orden orden)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await ordenDAO.AgregarAsync(orden))
            {
                return StatusCode(ordenDAO.customError.StatusCode, ordenDAO.customError.Message);
            }
            orden.total = await _context.ProductoOrden.Where(po => po.OrdenId == orden.Id)
                .Include(po => po.Producto).SumAsync(po => po.Producto.Precio * po.Producto.stock);
            await ordenDAO.ActualizarTotalOrden(orden.Id, orden.total);
            return CreatedAtAction("PostCompra", new { id = orden.Id }, orden);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            if(!await ordenDAO.BorraAsync(id))
            {
                return StatusCode(ordenDAO.customError.StatusCode, ordenDAO.customError.Message);
            }
            return Ok();
        }
    }
}
