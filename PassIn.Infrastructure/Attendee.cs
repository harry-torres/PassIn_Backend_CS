﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure;
public class Attendee
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public Guid Event_Id { get; set; }
    public DateTime Created_At { get; set; }
    public CheckIn? CheckIn{ get; set; }


}