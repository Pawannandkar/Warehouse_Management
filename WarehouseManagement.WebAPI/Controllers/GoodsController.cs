using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Core.Model;
using WarehouseManagement.Data.Data;

namespace WarehouseManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly WarehouseContext _context;

        public GoodsController(WarehouseContext context)
        {
            //here I can implement service layer insted of directly calling database layer
            //for future enhancement we can create repository layer as well
            _context = context;
        }

        /// <summary>
        /// GetGoods
        /// </summary>
        /// <param name="name"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetGoods([FromQuery] string? name, [FromQuery] string? productCode)
        {
            var goods = _context.Goods.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                goods = goods.Where(g => g.Name.Contains(name));

            if (!string.IsNullOrEmpty(productCode))
                goods = goods.Where(g => g.ProductCode.Contains(productCode));

            return Ok(await goods.ToListAsync());
        }

        /// <summary>
        /// api/Goods/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoodsById(int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods == null)
                return NotFound();

            return Ok(goods);
        }

        /// <summary>
        /// AddGoods
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddGoods([FromBody] Goods goods)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Goods.Add(goods);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGoodsById), new { id = goods.Id }, goods);
        }

        /// <summary>
        /// UpdateGoods
        /// </summary>
        /// <param name="id"></param>
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoods(int id, [FromBody] Goods goods)
        {
            if (id != goods.Id)
                return BadRequest();

            _context.Entry(goods).State = EntityState.Modified;

            //this changes we can perform in repository layer but for sample solution I'm implementing in controller itself
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Goods.Any(g => g.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// DeleteGoods
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoods(int id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods == null)
                return NotFound();

            // Check if there are any associated orders
            var hasOrders = _context.OrderItems.Any(oi => oi.GoodsId == id);
            if (hasOrders)
                return BadRequest("Cannot delete goods associated with existing orders.");
            //this changes we can perform in repository layer but for sample solution I'm implementing in controller itself
            _context.Goods.Remove(goods);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
