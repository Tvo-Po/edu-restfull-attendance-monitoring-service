using System.ComponentModel.DataAnnotations;

namespace RESTfull.Domain.Model;

public enum UserType
{
    Administrator = 1,
    Teacher = 2,
    Student = 3,
}

public class User
{
    public Guid Id { get; set; }
    public required string FIO { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email { get; set; }
    
    public UserType Type { get; set; }
    public ICollection<Attending> Attendings { get; set; }
}
