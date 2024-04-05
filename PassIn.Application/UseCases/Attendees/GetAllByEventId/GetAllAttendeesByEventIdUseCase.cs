﻿using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventId;

public class GetAllAttendeesByEventIdUseCase
{
    private readonly PassInDbContext _dbContext;
    public GetAllAttendeesByEventIdUseCase()
    {
        _dbContext = new PassInDbContext();
    }

    public ResponseAllAttendeesJson Execute(Guid eventId)
    {
        //var attendees = _dbContext.Attendees.Where(attendee => attendee.Event_Id == eventId).ToList();

        var eventEntity = _dbContext.Events.Include(ev => ev.Attendees)
            .FirstOrDefault(ev => ev.Id == eventId);

        if (eventEntity is null)
            throw new NotFoundException("Event not found.");

        return new ResponseAllAttendeesJson
        {
            Attendees = eventEntity.Attendees.Select(attendee => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At
            }).ToList()
        };
    }
}
