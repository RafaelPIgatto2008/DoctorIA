using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DoctorIA.Models;

namespace DoctorIA.Services;

public class ServiceIA : IServicesIa
{
    
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ConcurrentDictionary<int, List<ChatMessage>> _session = new();
    
    public ServiceIA(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task<string> GetDiagnosticsAsync(int userId, PatientRequest request, CancellationToken cancellationToken)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];
        
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        if (!_session.ContainsKey(userId))
        {
            _session[userId] = new List<ChatMessage>()
            {
                new ChatMessage
                {
                    Role = "system",
                    Content = @"Você é um médico experiente.
                    Sempre responda em JSON com:

                    Formato obrigatório:
                    {
                      """"Hyphoteses"""": [""""string""""],
                      """"Suggestions"""": [""""string""""],
                      """"DangerLevel"""": """"baixo | medio | critico | urgente""""
                    }

                    Você mantém histórico das últimas 10 consultas."
                }
            };
        }
        
        var messages = _session[userId];
        
        var userMessage = $@"
        Nova consulta:
        Idade: {request.Age}
        Genero: {request.Gender}
        Pressão: {request.BloodPressure}
        Batimentos: {request.HeartRate}
        Temperatura: {request.Temperature}
        Sintomas: {string.Join(", ", request.Symptoms ?? new())}
        Historico: {string.Join(", ", request.ListHistoric ?? new())}
        ";
        
        messages.Add(new ChatMessage { Role = "user", Content = userMessage });

        while (messages.Count > 20)
        {
            messages.RemoveAt(1);
        }

        var body = new
        {
            model = "gpt-4.1-mini",
            response_format = new { type = "json_object" },
            messages = messages
        };
        
        var response = await _httpClient.PostAsJsonAsync(
            "https://api.openai.com/v1/chat/completions",
            body,
            cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            return $"Erro na API: {error}";
        }

        var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken);

        if (!json.TryGetProperty("choices", out var choices))
            return "Erro ao processar resposta da IA";

        var content = json
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        DiagnosticResult diagnosis;
        
        try
        {
            diagnosis = JsonSerializer.Deserialize<DiagnosticResult>(content!) 
                        ?? new DiagnosticResult();
        }
        catch
        {
            diagnosis = new DiagnosticResult();
        }
        
        var validLevels = new[] { "baixo", "medio", "critico", "urgente" };

        if (!validLevels.Contains(diagnosis.DangerLevel?.ToLower()))
        {
            diagnosis.DangerLevel = "baixo";
        }
        
        messages.Add(new ChatMessage{ Role = "assistant", Content = content!});

        return diagnosis.ToString();
    }

    public Task<string> GetHistoricAsync(int userId, CancellationToken cancellationToken)
    {
        if (!_session.ContainsKey(userId))
        {
            return Task.FromResult("Nenhum historico encontrado");
        }
        
        var history = _session[userId]
            .Where(m => m.Role == "assistant")
            .Select(m => JsonSerializer.Deserialize<DiagnosticResult>(m.Content));

        return Task.FromResult(JsonSerializer.Serialize(history));
    }
}