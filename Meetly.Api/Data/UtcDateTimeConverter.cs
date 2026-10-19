using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Meetly.Api.Data;

// Märker tider från databasen som UTC, så att API:t skickar dem med Z på slutet
public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter()
        : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
