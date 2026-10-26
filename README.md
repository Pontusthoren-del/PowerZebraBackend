# Meetly – Backend

REST-API för **Meetly**, en tjänst där användare kan se meetups, söka bland dem och anmäla sig. Backend är byggt med ASP.NET Core Web API och körs som en Docker-container i Azure App Service, med Azure SQL Database som databas.

Frontend finns i ett eget repo: [PowerZebraFrontend](https://github.com/Pontusthoren-del/PowerZebraFrontend).

## Live

| | URL |
|---|---|
| Frontend | https://meetly-frontend-a9g6b7gvhdd2afga.norwayeast-01.azurewebsites.net |
| Backend (API) | https://meetly-aed8cwgec3cshjhv.norwayeast-01.azurewebsites.net |
| Hälsokoll | `/api/health` |

Testkonto: `demo@meetly.se` / `Meetly123!`

## Teknik

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core 10 med SQL Server (Azure SQL Database i drift, LocalDB lokalt)
- ASP.NET Core Identity (`AddIdentityApiEndpoints`) för registrering och inloggning med bearer-token
- Scalar för API-dokumentation (bara i Development)
- Docker, Docker Hub och Azure App Service (container)
- GitHub Actions för CI/CD

## Projektstruktur

```
PowerZebraBackend/
├── .github/workflows/
│   ├── ci.yaml            # Bygger vid PR och push mot dev/main
│   └── deploy.yaml        # Docker build, push och deploy vid push till main
├── Meetly.Api/
│   ├── Controllers/       # Auth, Health, Me, Meetups, Registrations
│   ├── Data/              # DbContext, seed-data, UTC-konvertering
│   ├── Dtos/              # Request- och response-modeller
│   ├── Migrations/        # EF Core-migreringar
│   ├── Models/            # User, Meetup, Registration, Review
│   └── Program.cs
├── Dockerfile
└── Meetly.slnx
```

## API

Alla endpoints ligger under `/api`. Fel returneras som Problem Details (`application/problem+json`). Endpoints märkta med 🔒 kräver inloggning, och token skickas som `Authorization: Bearer <accessToken>`.

### Konto och inloggning

| Metod | Endpoint | Beskrivning |
|---|---|---|
| POST | `/api/auth/signup` | Skapar konto. Body: `firstName`, `lastName`, `email`, `password`. |
| POST | `/api/auth/login` | Loggar in med `email` och `password`. Svaret innehåller `accessToken`. |
| GET 🔒 | `/api/me` | Den inloggade användarens id, namn och e-post. |
| GET 🔒 | `/api/me/registrations` | Den inloggades meetups, uppdelade i `upcoming` och `past`. |

Inloggning sker med e-post och lösenord. Det finns inget separat användarnamn: Identitys fält `UserName` sätts till e-postadressen vid registrering. Övriga Identity-endpoints (t.ex. `/api/auth/refresh`) ligger också under `/api/auth`.

### Meetups

| Metod | Endpoint | Beskrivning |
|---|---|---|
| GET | `/api/meetups` | Kommande meetups, sorterade på starttid. Se filter nedan. |
| GET | `/api/meetups/{id}` | En meetup. Svarar `404` om den inte finns. |
| GET | `/api/meetups/categories` | Alla kategorier som finns. |

Filter på `GET /api/meetups` (alla valfria och kombinerbara):

| Parameter | Beskrivning |
|---|---|
| `search` | Fritext. Söker i titel, beskrivning, plats, värd och kategori. |
| `category` | Exakt kategori. |
| `location` | Del av platsnamnet. |
| `from`, `to` | Datumintervall, `yyyy-MM-dd` (svenskt datum). `to` räknas till och med hela dagen. `from` får inte vara efter `to` (`400`). |

Exempel: `/api/meetups?search=react&category=Teknik&from=2026-11-01`

### Anmälningar

| Metod | Endpoint | Beskrivning |
|---|---|---|
| POST 🔒 | `/api/meetups/{meetupId}/registrations` | Anmäler den inloggade till meetupen. Svarar `201` med antal anmälda och lediga platser. |
| DELETE 🔒 | `/api/meetups/{meetupId}/registrations` | Tar bort den inloggades anmälan. Svarar `204`. |

Regler som backend kontrollerar:

- Meetupen måste finnas (`404`).
- Man kan inte anmäla sig till eller avanmäla sig från en meetup som redan har startat (`400`).
- Man kan bara vara anmäld en gång per meetup (`409`). Det säkras även av ett unikt index i databasen.
- Är meetupen full går det inte att anmäla sig (`409`). Kapaciteten kontrolleras i backend, inte bara i frontend.
- Man kan bara avanmäla sig om man faktiskt är anmäld (`404`).

### Övrigt

| Metod | Endpoint | Beskrivning |
|---|---|---|
| GET | `/api/health` | Returnerar `{ "status": "ok", "timeUtc": ... }`. |

## Datamodell

- **User**: förnamn, efternamn, e-post och lösenordshash (via Identity). E-post är unik.
- **Meetup**: titel, beskrivning, starttid, plats, värd, kapacitet och kategori.
- **Registration**: kopplar en användare till en meetup. Unikt index på (användare, meetup).
- **Review**: betyg 1–5 och kommentar. Modellen och tabellen finns, men det finns inga endpoints för den än.

Alla tider sparas i **UTC**. Frontend visar dem i svensk tid, och datumfilter i `from`/`to` tolkas som svenska datum. Databasen skapas och migreras automatiskt när API:et startar, och seed-data med demoanvändare och exempel-meetups läggs in via migreringarna.

## Konfiguration

Inga hemligheter ligger i repot. Värdena nedan läses från konfigurationen och sätts i Azure App Service (Application settings) eller som miljövariabler.

| Inställning | Miljövariabel | Beskrivning |
|---|---|---|
| `ConnectionStrings:DefaultConnection` | `ConnectionStrings__DefaultConnection` | Connection string till databasen. Utan den hoppas migreringen över. |
| `Cors:AllowedOrigins` | `Cors__AllowedOrigins__0`, `Cors__AllowedOrigins__1`, … | Frontend-URL:er som får anropa API:et. |

Lokalt hämtas värdena från `appsettings.Development.json` (LocalDB och `http://localhost:5173`).

## Köra lokalt

Du behöver .NET 10 SDK och SQL Server LocalDB (följer med Visual Studio).

1. Starta API:t i Visual Studio med profilen `https` eller `http`, eller kör `dotnet run --project Meetly.Api`.
2. Databasen skapas och migreras automatiskt i LocalDB vid start.
3. Testa endpoints i Scalar på `/scalar`. API:et lyssnar på `http://localhost:5156` (och `https://localhost:7265` med profilen `https`).

Ny migrering efter ändrad modell:

```sh
dotnet ef migrations add <Namn> --project Meetly.Api
```

## Docker

```sh
docker build -t meetlyapi .
docker run -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="<connection string>" \
  -e Cors__AllowedOrigins__0="http://localhost:5173" \
  meetlyapi
```

API:et svarar sedan på `http://localhost:8080/api/health`.

## CI/CD

Workflows ligger i `.github/workflows/`.

- **Backend CI** (`ci.yaml`): körs vid Pull Request mot `dev` och `main`, samt vid push till dessa. Gör `dotnet restore` och `dotnet build` i Release.
- **Deploy** (`deploy.yaml`): körs vid push till `main`, det vill säga när en Pull Request har mergats. Loggar in på Docker Hub, bygger och pushar imagen `meetlyapi` taggad med commit-SHA, och deployar den imagen till Azure App Service.

Secrets som krävs i GitHub (*Settings → Secrets and variables → Actions*):

| Secret | Innehåll |
|---|---|
| `DOCKERHUB_USERNAME` | Docker Hub-användare |
| `DOCKERHUB_TOKEN` | Access token för Docker Hub |
| `AZURE_WEBAPP_PUBLISH_PROFILE` | Publish profile för Azure App Service |

## Arbetssätt (Git Flow)

- `main` innehåller produktionsklar kod och uppdateras bara via Pull Request. Merge till `main` driftsätter backend.
- `dev` är integrationsbranch för färdiga features.
- Varje user story utvecklas i en egen branch från `dev`, t.ex. `feature/meetup-list`, och går via Pull Request mot `dev`.
- Rulesets i GitHub blockerar direktpush till `main` och `dev`.
