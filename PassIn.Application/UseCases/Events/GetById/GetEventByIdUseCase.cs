﻿using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{
    public ResponseEventJson Execute(Guid id)
    {
        var dbContext = new PassInDbContext();
        //dbContext.Events.FirstOrDefault(ev => ev.Id == id);
        // return null or the entity
        var entity = dbContext.Events.Find(id);

        if (entity is null)
            throw new NotFoundException ("Event not found.");

        return new ResponseEventJson
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            AttendeesAmount = -1
        };

    }
}
