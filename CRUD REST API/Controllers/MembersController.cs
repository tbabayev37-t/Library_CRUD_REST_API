using CRUD_REST_API.Business.DTOs.MemberDto;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Business.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _memberService.GetAllAsync();
            return Ok(members);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var member = await _memberService.GetByIdAsync(id);
                return Ok(member);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(MemberCreateDto dto)
        {
            await _memberService.CreateAsync(dto);
            return Ok(dto);
        }
        [HttpPut("{id}")] 
        public async Task<IActionResult> Update(int id, [FromBody] MemberUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest(new { message = "ID-ler ust-uste dusmur!" });

            try
            {
                await _memberService.UpdateAsync(dto); 
                return Ok(new { message = "Uzv ugurla yenilendi!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _memberService.DeleteAsync(id);
                return Ok("Uzv silindi");
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }
    }
}