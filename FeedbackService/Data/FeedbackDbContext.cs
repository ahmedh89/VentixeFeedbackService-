using FeedbackService.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Data;

public class FeedbackDbContext : DbContext
{
    public FeedbackDbContext(DbContextOptions<FeedbackDbContext> opts)
      : base(opts) { }

    public DbSet<Feedback> Feedbacks => Set<Feedback>();
}
