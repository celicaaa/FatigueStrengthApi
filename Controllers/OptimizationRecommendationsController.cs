using FatigueStrengthApi.Data;
using FatigueStrengthApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FatigueStrengthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OptimizationRecommendationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OptimizationRecommendationsController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OptimizationRecommendations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OptimizationRecommendation>>> GetAll()
        {
            var recommendations = await _context
                .OptimizationRecommendations
                .AsNoTracking()
                .ToListAsync();

            return Ok(recommendations);
        }

        // GET: api/OptimizationRecommendations/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OptimizationRecommendation>> GetById(int id)
        {
            var recommendation = await _context
                .OptimizationRecommendations
                .FindAsync(id);

            if (recommendation == null)
                return NotFound();

            return Ok(recommendation);
        }

        // POST: api/OptimizationRecommendations
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<OptimizationRecommendation>> Create(
            OptimizationRecommendation recommendation)
        {
            _context.OptimizationRecommendations.Add(recommendation);
            await _context.SaveChangesAsync();

            return Ok(recommendation);
        }

        // PUT: api/OptimizationRecommendations/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            OptimizationRecommendation updatedRecommendation)
        {
            var recommendation = await _context
                .OptimizationRecommendations
                .FindAsync(id);

            if (recommendation == null)
                return NotFound();

            recommendation.ExperimentId =
                updatedRecommendation.ExperimentId;
            recommendation.RecommendedMaterial =
                updatedRecommendation.RecommendedMaterial;
            recommendation.RecommendedLayerThickness =
                updatedRecommendation.RecommendedLayerThickness;
            recommendation.RecommendedInfillDensity =
                updatedRecommendation.RecommendedInfillDensity;
            recommendation.RecommendedInfillPattern =
                updatedRecommendation.RecommendedInfillPattern;
            recommendation.Explanation =
                updatedRecommendation.Explanation;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/OptimizationRecommendations/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recommendation = await _context
                .OptimizationRecommendations
                .FindAsync(id);

            if (recommendation == null)
                return NotFound();

            _context.OptimizationRecommendations.Remove(recommendation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
