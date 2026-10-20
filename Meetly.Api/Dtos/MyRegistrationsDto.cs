namespace Meetly.Api.Dtos;

public record MyRegistrationsDto(List<MeetupDto> Upcoming, List<MeetupDto> Past);