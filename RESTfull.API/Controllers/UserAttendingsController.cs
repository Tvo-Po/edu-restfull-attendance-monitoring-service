using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RESTfull.Infrastructure;
using RESTfull.Infrastructure.Repository;

namespace RESTfull.API.Controllers;

[
    AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false
    )
]
public class RangeLastPastYearAttribute : RangeAttribute
{
    public RangeLastPastYearAttribute() :
           base(DateTime.Now.Year - 4, DateTime.Now.Year)
    {
    }
}

public class UserAttendingsUrlParams
{
    public Guid userId { get; set; }

    [RangeLastPastYear(ErrorMessage = "Only past 4 years allowed.")]
    public uint year { get; set; }

    [Range(1, 12, ErrorMessage = "Invalid month number.")]
    public uint month { get; set; }
}

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class UserAttendingsController : ControllerBase
{
    private readonly ILogger<UserAttendingsController> _logger;
    private readonly Context _context;

    public UserAttendingsController(
        ILogger<UserAttendingsController> logger,
        Context context

    )
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<List<UserAttending>> Get(
        [FromQuery] UserAttendingsUrlParams urlParams
    )
    {
        var userAttendingsRepository = new UserAttendingsRepository(_context);
        return await userAttendingsRepository.GetMonthAttendings(
            userId: urlParams.userId,
            year: urlParams.year,
            month: urlParams.month
        );
    }
}
