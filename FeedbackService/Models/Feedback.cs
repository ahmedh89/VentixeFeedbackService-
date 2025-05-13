using System;

namespace FeedbackService.Models;

public class Feedback
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime SubmittedAt { get; set; }
    public string Status { get; set; } = "New";

}
