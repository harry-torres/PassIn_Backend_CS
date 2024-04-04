using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace PassIn.Infrastructure;
public class Event
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    [Required]
    public string Slug { get; set; } = string.Empty;

    public int Maximum_Attendees { get; set; }
}
