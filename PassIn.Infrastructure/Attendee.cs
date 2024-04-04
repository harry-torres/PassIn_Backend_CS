using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure;
public class Attendee
{
    [Key]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string EventId { get; set; }

    [ForeignKey("EventId")]
    public Event Event { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}