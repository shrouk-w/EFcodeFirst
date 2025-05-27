using EFcodefirst.Model;
using EFcodefirst.Model.DTOs;
using EFcodefirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFcodefirst.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;
    
    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> InsertNewPresriptionsAsync([FromBody] PrescrptionInsertRequestDTO prescription, CancellationToken token)
    {
        _prescriptionService.InsertNewPresriptionsAsync(prescription, token);
        return Ok();
    }
}
