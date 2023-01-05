using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfull.Domain.Model;

public enum AttendingStatus {
    FutureBlocked = 1,
    InvalidReasonSkip = 2,
    ValidReasonSkip = 3,
    Presence = 4,
}

public class Attending
{
    public Guid Id { get; set; }

    public Guid StudyUserId { get; set; }
    [ForeignKey(nameof(StudyUserId))]
    public required User StudyUser { get; set; }

    public Guid ClassId { get; set; }
    [ForeignKey(nameof(ClassId))]
    public required Class Class { get; set; }

    public AttendingStatus Status { get; set; }
}
