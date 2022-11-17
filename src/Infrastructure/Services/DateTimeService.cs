using ERPSystem.Application.Common.Interfaces;

namespace ERPSystem.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
