using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMeter.Data.Context;
using SmartMeter.Data.Entities;


namespace SmartMeter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // only authenticated users can access
    public class ConsumersController : ControllerBase
    {
        private readonly SmartMeterDbContext _db;

        public ConsumersController(SmartMeterDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var consumers = await _db.Consumers
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            return Ok(consumers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var consumer = await _db.Consumers.FindAsync(id);
            if (consumer == null) return NotFound();
            return Ok(consumer);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Consumer consumer)
        {
            consumer.CreatedAt = DateTimeOffset.UtcNow;
            consumer.CreatedBy = User.Identity?.Name ?? "system";
            _db.Consumers.Add(consumer);
            await _db.SaveChangesAsync();
            return Ok(consumer);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Consumer updated)
        {
            var consumer = await _db.Consumers.FindAsync(id);
            if (consumer == null) return NotFound();

            consumer.Name = updated.Name;
            consumer.Email = updated.Email;
            consumer.Phone = updated.Phone;
            consumer.Status = updated.Status;
            consumer.UpdatedAt = DateTimeOffset.UtcNow;
            consumer.UpdatedBy = User.Identity?.Name ?? "system";

            await _db.SaveChangesAsync();
            return Ok(consumer);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var consumer = await _db.Consumers.FindAsync(id);
            if (consumer == null) return NotFound();

            consumer.IsDeleted = true;
            await _db.SaveChangesAsync();
            return Ok(new { message = "Consumer deleted" });
        }
    }
}
