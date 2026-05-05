using DoctorIA.Models;
using DoctorIA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorIA.Controllers;


[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]

public class ConsultationController(IServicesIa servicesIa) : ControllerBase
{
    
    private readonly IServicesIa _servicesIa = servicesIa ?? throw new ArgumentNullException(nameof(servicesIa));

    /// <summary>
    /// Generate the diagnostic with the solicited request
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateDiagnosticAsync(int userId, PatientRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _servicesIa.GetDiagnosticsAsync(userId, request, cancellationToken);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get historic of conversation
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHistoricAsync([FromRoute] int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = _servicesIa.GetHistoricAsync(userId, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}