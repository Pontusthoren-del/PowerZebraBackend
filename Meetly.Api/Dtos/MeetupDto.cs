namespace Meetly.Api.Dtos;

public record MeetupDto(
    int Id,
    string Title,
    string Description,
    DateTime StartTime,
    string Location,
    string Host,
    int Capacity,
    string Category,
    int RegisteredCount,
    int AvailableSpots);
