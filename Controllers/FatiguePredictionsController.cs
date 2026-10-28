using FatigueStrengthApi.Data;
using FatigueStrengthApi.Models;
using FatigueStrengthApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FatigueStrengthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FatiguePredictionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly FatiguePredictionService _predictionService;

        public FatiguePredictionsController(
            ApplicationDbContext context,
            FatiguePredictionService predictionService)
        {
            _context = context;
            _predictionService = predictionService;
        }

        // GET: api/FatiguePredictions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FatiguePrediction>>> GetAll()
        {
            return Ok(await _context.FatiguePredictions
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: api/FatiguePredictions/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FatiguePrediction>> GetById(int id)
        {
            var prediction = await _context.FatiguePredictions
                .FindAsync(id);

            if (prediction == null)
                return NotFound();

            return Ok(prediction);
        }

        // POST: api/FatiguePredictions/from-experiment/1?targetStrength=40
        [Authorize(Roles = "admin")]
        [HttpPost("from-experiment/{experimentId:int}")]
        public async Task<ActionResult<FatiguePrediction>> Create(
            int experimentId,
            double targetStrength)
        {
            var experiment = await _context.PrintExperiments
                .FindAsync(experimentId);

            if (experiment == null)
                return NotFound("Эксперимент не найден.");

            if (targetStrength <= 0 ||
                double.IsNaN(targetStrength) ||
                double.IsInfinity(targetStrength))
            {
                return BadRequest(
                    "Целевая прочность должна быть положительной.");
            }

            var prediction = new FatiguePrediction
            {
                ExperimentId = experimentId,
                PredictedStrength =
                    _predictionService.PredictStrength(experiment),
                TargetStrength = targetStrength,
                CreatedAt = DateTime.UtcNow
            };

            _context.FatiguePredictions.Add(prediction);
            await _context.SaveChangesAsync();

            return Ok(prediction);
        }

        // PUT: api/FatiguePredictions/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            FatiguePrediction updatedPrediction)
        {
            var prediction = await _context.FatiguePredictions
                .FindAsync(id);

            if (prediction == null)
                return NotFound();

            if (updatedPrediction.TargetStrength <= 0 ||
                double.IsNaN(updatedPrediction.TargetStrength) ||
                double.IsInfinity(updatedPrediction.TargetStrength))
            {
                return BadRequest(
                    "Целевая прочность должна быть положительной.");
            }

            prediction.TargetStrength = updatedPrediction.TargetStrength;
            prediction.PredictedStrength =
                updatedPrediction.PredictedStrength;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/FatiguePredictions/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var prediction = await _context.FatiguePredictions
                .FindAsync(id);

            if (prediction == null)
                return NotFound();

            _context.FatiguePredictions.Remove(prediction);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
