using FatigueStrengthApi.Contracts;
using FatigueStrengthApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FatigueStrengthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController(ReportsService service) : ControllerBase
{
    // GET: api/Reports/experiments-with-predictions
    [HttpGet("experiments-with-predictions")]
    public async Task<ActionResult<List<ExperimentWithPredictionResponse>>>
        ExperimentsWithPredictions()
    {
        return Ok(await service.GetExperimentsWithPredictions());
    }
    // GET: api/Reports/experiments-with-recommendations
    [HttpGet("experiments-with-recommendations")]
    public async Task<ActionResult<List<ExperimentWithRecommendationResponse>>>
        ExperimentsWithRecommendations()
    {
        return Ok(await service.GetExperimentsWithRecommendations());
    }
    // GET: api/Reports/full-summary
    [HttpGet("full-summary")]
    public async Task<ActionResult<List<FullSummaryResponse>>> FullSummary()
    {
        return Ok(await service.GetFullSummary());
    }
}
