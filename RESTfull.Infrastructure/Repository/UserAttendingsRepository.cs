using Microsoft.EntityFrameworkCore;
using RESTfull.Domain.Model;

namespace RESTfull.Infrastructure.Repository;

public class UserAttending
{
    public required string Subject { get; set; }
    public DateTime SubjectStart { get; set; }
    public AttendingStatus Status { get; set; }
}

public class UserAttendingsRepository : BaseRepository
{
    public UserAttendingsRepository(Context context) : base(context)
    {
    }

    public async Task<List<UserAttending>> GetMonthAttendings(Guid userId, uint year, uint month)
    {
        return await _context.Users
                        .Join(
                            _context.Attendings,
                            u => u.Id,
                            a => a.StudyUserId,
                            (u, a) => new
                            {
                                UserId = u.Id,
                                ClassId = a.ClassId,
                                Status = a.Status
                            }
                        )
                        .Join(
                            _context.Classes,
                            joinResult => joinResult.ClassId,
                            c => c.Id,
                            (joinResult, c) => new
                            {
                                UserId = joinResult.UserId,
                                Subject = c.Subject,
                                Start = c.Start,
                                Status = joinResult.Status,
                            }
                        )
                        .Where(
                            fullResult => fullResult.UserId.ToString() == userId.ToString() &&
                                          fullResult.Start.Year == year &&
                                          fullResult.Start.Month == month
                        )
                        .Select(
                            fullResult => new UserAttending
                            {
                                Subject = fullResult.Subject,
                                SubjectStart = fullResult.Start,
                                Status = fullResult.Status,
                            }
                        )
                        .OrderBy(Result => Result.SubjectStart)
                        .ToListAsync();
    }
}
