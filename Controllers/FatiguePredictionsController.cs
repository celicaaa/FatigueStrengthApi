using System.ComponentModel.DataAnnotations;
using FatigueStrengthApi.Contracts;
using FatigueStrengthApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FatigueStrengthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FatiguePredictionsController(FatiguePredictionService service)
    : ControllerBase
{
    // GET: api/FatiguePredictions
    [HttpGet]
    public async Task<ActionResult<List<FatiguePredictionResponse>>> GetAll()
    {
        return Ok(await service.GetAll());
    }
    // GET: api/FatiguePredictions/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FatiguePredictionResponse>> GetById(int id)
    {
        try
        {
            return Ok(await service.GetById(id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    // POST: api/FatiguePredictions/from-experiment/5?targetStrength=40
    [HttpPost("from-experiment/{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<FatiguePredictionResponse>>
        CreateFromExperiment(int id, double targetStrength)
    {
        try
        {
            return Ok(await service.CreateFromExperiment(id, targetStrength));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // PUT: api/FatiguePredictions/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(
        int id,
        FatiguePredictionRequest request)
    {
        try
        {
            await service.Update(id, request);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // DELETE: api/FatiguePredictions/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await service.Delete(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
