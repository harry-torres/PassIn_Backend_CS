using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.CheckIns;

public class DoAttendeeCheckInUseCase
{
    private readonly PassInDbContext _dbContext;
    public DoAttendeeCheckInUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    public ResponseRegisteredJson Execute(Guid attendeeId)
    {

        Validate(attendeeId);

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            CreatedAt = DateTime.UtcNow,
        };

        _dbContext.CheckIns.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id
        };
    }

    private void Validate(Guid attendeeId)
    {
        var existsAttendee = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeId);
    
        if(existsAttendee)
        {
            throw new NotFoundException("Attendee not found.");
        }

        var existsCheckIn = _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);

        if (existsCheckIn)
        {
            throw new ConflictException("Attendee already checked in.");
        }
    }
}

