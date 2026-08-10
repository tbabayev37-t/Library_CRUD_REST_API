using CRUD_REST_API.Business.DTOs.OrderDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Business.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        ///<summary>Butun sifarisleri gosterir</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllOrder()
        {
            var order = await _orderService.GetAllAsync();
            return Ok(order);
        }
        ///<summary>Yeni sifaris yaradir</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            await _orderService.CreateAsync(orderCreateDto);
            return StatusCode(201, new { message = "Order successfully created." });
        }
        ///<summary>ID-e gore sifarisleri gosterir</summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>GetOrderById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return Ok(order);
        }
        ///<summary>Sifarisi yenileyir</summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateDto orderDto)
        {
            if (id != orderDto.Id)
                return BadRequest(new { message = "IDs don't fall on top of each other!" });

            await _orderService.UpdateAsync(orderDto);
            return NoContent();
        }
        ///<summary>ID-e gore sifarisi silir</summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>DeleteOrder(int id)
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }
    }
}
