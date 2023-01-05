using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfull.Domain.Model;

public class Class
{
    public Guid Id { get; set; }
    public required string Subject { get; set; }
    
    public Guid TeacherId { get; set; }
    [ForeignKey(nameof(TeacherId))]
    public required User Teacher { get; set; }

    [RegularExpression(@"^[А-Я]+-\d+$")]
    public required string Group { get; set; }
    
    public DateTime Start { get; set; }
    public ICollection<Attending> Attendings { get; set; }
}
