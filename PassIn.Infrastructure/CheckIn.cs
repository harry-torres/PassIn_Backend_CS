using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure;

public class CheckIn
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Created_At { get; set; } = DateTime.UtcNow;

    [Required]
    public Guid Attendee_Id { get; set; }

    [ForeignKey("Attendee_Id")]
    public Attendee Attendee { get; set; } = default!;
}