using VeteransMuseum.Application.Abstractions.Clock;

namespace VeteransMuseum.Infrastructure.Clock;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}