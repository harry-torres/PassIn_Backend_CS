using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;

public class RegisterAttendeeOnEventUseCase
{
    private readonly PassInDbContext _dbContext;
    public RegisterAttendeeOnEventUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseRegisteredJson Execute(Guid eventId, RequestRegisteredEventJson request)
    {
        Validate(eventId, request);

        var entity = new PassIn.Infrastructure.Attendee
        {
            Email = request.Email,
            Name = request.Name,
            Event_Id = eventId,
        //  gets time in local timezone of the server... incompatible with cloud servers
        //  Created_At = DateTime.Now,
            Created_At = DateTime.UtcNow
        };

        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id
        };
    }

    private void Validate(Guid eventId, RequestRegisteredEventJson request)
    {
        var eventEntity = _dbContext.Events.Find(eventId);
        if (eventEntity is null)
            throw new NotFoundException("Event not found.");

        if(string.IsNullOrWhiteSpace(request.Name))
            throw new ErrorOnValidationException("Invalid name.");

        if(EmailIsValid(request.Email) is false)
            throw new ErrorOnValidationException("Invalid email.");

        var attendeeAlreadyRegistered = _dbContext.Attendees
            .Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id == eventId);

        if (attendeeAlreadyRegistered)
            throw new ConflictException("Attendee already registered.");

        var attendeesForEvent = _dbContext.Attendees.Count(attendee => attendee.Event_Id == eventId);

        if (attendeesForEvent >= eventEntity.Maximum_Attendees)
            throw new ErrorOnValidationException("Event is full.");
    }

    private bool EmailIsValid(string email)
    {
        try
        {
            new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }

    }
}
