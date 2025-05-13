using FeedbackService.Data;
using FeedbackService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FeedbackService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackDbContext _ctx;
        public FeedbackController(FeedbackDbContext ctx) => _ctx = ctx;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Feedback fb)
        {
            fb.Id = Guid.NewGuid();
            fb.SubmittedAt = DateTime.UtcNow;
            _ctx.Feedbacks.Add(fb);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = fb.Id }, fb);
        }

        [HttpGet]
        public async Task<IEnumerable<Feedback>> GetAll() =>
            await _ctx.Feedbacks.ToListAsync();

        [HttpGet("{id}")]
        public async Task<Feedback?> GetById(Guid id) =>
            await _ctx.Feedbacks.FindAsync(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Feedback updated)
        {
            var existing = await _ctx.Feedbacks.FindAsync(id);
            if (existing is null) return NotFound();
            existing.Status = updated.Status;
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
