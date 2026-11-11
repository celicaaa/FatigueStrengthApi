using System.ComponentModel.DataAnnotations;
using FatigueStrengthApi.Contracts;
using FatigueStrengthApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FatigueStrengthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrintExperimentsController(PrintExperimentService service)
    : ControllerBase
{ 
    // GET: api/PrintExperiments
    [HttpGet]
    public async Task<ActionResult<List<PrintExperimentResponse>>> GetAll()
    {
        return Ok(await service.GetAll());
    }
    // GET: api/PrintExperiments/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PrintExperimentResponse>> GetById(int id)
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
    // POST: api/PrintExperiments
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<PrintExperimentResponse>> Create(
        PrintExperimentRequest request)
    {
        try
        {
            var result = await service.Create(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // PUT: api/PrintExperiments/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(
        int id,
        PrintExperimentRequest request)
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
    // DELETE: api/PrintExperiments/5
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
