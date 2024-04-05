using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure;

public class CheckIn
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public Guid Attendee_Id { get; set; }

    //[ForeignKey("AttendeeId")]
    //public Attendee Attendee { get; set; }
}