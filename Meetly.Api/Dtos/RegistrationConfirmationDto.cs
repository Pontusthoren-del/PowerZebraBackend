namespace Meetly.Api.Dtos;

// Svar när en anmälan lyckats. Frontend kan använda siffrorna för att uppdatera
// antal anmälda och lediga platser utan att hämta om meetupen.
public record RegistrationConfirmationDto(
    int MeetupId,
    DateTime RegisteredAt,
    int RegisteredCount,
    int AvailableSpots);
