﻿using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventsUseCase
    {
        public ResponseRegisteredEventJson Execute(RequestEventJson request)
        {
            Validate(request);

            var dbContext = new PassInDbContext();
            var entity = new Event
            {
                Title = request.Title,
                Details = request.Details,
                Maximum_Attendees = request.MaximumAttendees,
                Slug = request.Title.ToLower().Replace(" ", "-")
            };

            dbContext.Events.Add(entity);
            dbContext.SaveChanges();

            return new ResponseRegisteredEventJson
            {
                Id = entity.Id
            };
        }

        public void Validate(RequestEventJson request)
        {
            if(request.MaximumAttendees <= 0)
            {
                throw new ArgumentException("The Maximum attendees is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ArgumentException("Invalid Title.");
            }
        }
    }
}