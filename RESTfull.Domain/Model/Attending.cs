namespace RESTfull.Domain.Model;


public enum AttendingStatus {
    InvalidReasonSkip = 1,
    ValidReasonSkip = 2,
    Presence = 3,
}

public class Attending
{
    public Guid Id { get; set; }
    public required User Student { get; set; }
    public required Class Class { get; set; }
    public AttendingStatus Status { get; set; }
}
