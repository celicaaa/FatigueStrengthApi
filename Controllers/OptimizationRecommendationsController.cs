using System.ComponentModel.DataAnnotations;
using FatigueStrengthApi.Contracts;
using FatigueStrengthApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FatigueStrengthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OptimizationRecommendationsController(
    OptimizationRecommendationsService service) : ControllerBase
{
    // GET: api/OptimizationRecommendations
    [HttpGet]
    public async Task<ActionResult<List<OptimizationRecommendationResponse>>>
        GetAll()
    {
        return Ok(await service.GetAll());
    }
    // GET: api/OptimizationRecommendations/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OptimizationRecommendationResponse>>
        GetById(int id)
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
    // POST: api/OptimizationRecommendations
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<OptimizationRecommendationResponse>> Create(
        OptimizationRecommendationRequest request)
    {
        try
        {
            return Ok(await service.Create(request));
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // PUT: api/OptimizationRecommendations/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(
        int id,
        OptimizationRecommendationRequest request)
    {
        try
        {
            await service.Update(id, request);
            return NoContent();
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
    // DELETE: api/OptimizationRecommendations/5
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
