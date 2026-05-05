# DoctorIA

DoctorIA is an ASP.NET Core Web API that uses OpenAI to generate medical suggestion summaries from symptoms and vital signs.

This API is for support only and does not replace medical evaluation.

## Stack

- .NET 9
- ASP.NET Core Web API
- Swagger / OpenAPI
- FluentValidation
- OpenAI Chat Completions API

## Requirements

- .NET 9 SDK
- OpenAI API key

## Configuration

Set the API key in `DoctorIA/appsettings.json`:

```json
"OpenAI": {
  "ApiKey": "your_api_key_here"
}
```

Or use an environment variable:

```powershell
$env:OpenAI__ApiKey="your_api_key_here"
```

## Run

```powershell
dotnet restore
dotnet run --project .\DoctorIA\DoctorIA.csproj
```

Default development URLs:

- `http://localhost:5227`
- `https://localhost:7039`

Swagger is available in development at the same URLs.

## Endpoints

- `GET /health`
- `POST /api/Consultation?userId=1`
- `GET /api/Consultation`

Example request:

```json
{
  "age": 34,
  "gender": "Male",
  "bloodPressure": "120/80",
  "heartRate": 88,
  "temperature": 37.4,
  "symptoms": ["headache", "fatigue", "nausea"],
  "listHistoric": ["migraine", "allergy"]
}
```

Example response:

```json
{
  "Hyphoteses": ["possible condition"],
  "Suggestions": ["recommended next step"],
  "DangerLevel": "baixo"
}
```

## Notes

- Conversation history is kept only in memory.
- The history endpoint may need route adjustment to work correctly.
- AI output must be reviewed before real-world use.
