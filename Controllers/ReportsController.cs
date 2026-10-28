using FatigueStrengthApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FatigueStrengthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reports/experiments-with-predictions
        [HttpGet("experiments-with-predictions")]
        public async Task<IActionResult> GetExperimentsWithPredictions()
        {
            var result = await (
                from experiment in _context.PrintExperiments
                join prediction in _context.FatiguePredictions
                    on experiment.Id equals prediction.ExperimentId
                where prediction.PredictedStrength >= prediction.TargetStrength
                select new
                {
                    experiment.Id,
                    experiment.MaterialType,
                    experiment.LayerThickness,
                    prediction.PredictedStrength,
                    prediction.TargetStrength
                }
            ).ToListAsync();

            return Ok(result);
        }

        // GET: api/Reports/experiments-with-recommendations
        [HttpGet("experiments-with-recommendations")]
        public async Task<IActionResult> GetExperimentsWithRecommendations()
        {
            var result = await (
                from experiment in _context.PrintExperiments
                join recommendation in _context.OptimizationRecommendations
                    on experiment.Id equals recommendation.ExperimentId
                where experiment.InfillDensity > 0
                select new
                {
                    experiment.Id,
                    experiment.MaterialType,
                    experiment.InfillDensity,
                    recommendation.RecommendedMaterial,
                    recommendation.RecommendedLayerThickness,
                    recommendation.RecommendedInfillDensity,
                    recommendation.RecommendedInfillPattern
                }
            ).ToListAsync();

            return Ok(result);
        }

        // GET: api/Reports/full-summary
        [HttpGet("full-summary")]
        public async Task<IActionResult> GetFullSummary()
        {
            var result = await (
                from experiment in _context.PrintExperiments
                join prediction in _context.FatiguePredictions
                    on experiment.Id equals prediction.ExperimentId
                join recommendation in _context.OptimizationRecommendations
                    on experiment.Id equals recommendation.ExperimentId
                where prediction.TargetStrength > 0
                select new
                {
                    ExperimentId = experiment.Id,
                    experiment.MaterialType,
                    prediction.PredictedStrength,
                    prediction.TargetStrength,
                    MeetsTarget =
                        prediction.PredictedStrength >= prediction.TargetStrength,
                    recommendation.RecommendedMaterial,
                    recommendation.RecommendedLayerThickness,
                    recommendation.Explanation
                }
            ).ToListAsync();

            return Ok(result);
        }
    }
}
