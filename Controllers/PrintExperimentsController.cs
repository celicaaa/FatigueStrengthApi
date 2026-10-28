using FatigueStrengthApi.Data;
using FatigueStrengthApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FatigueStrengthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrintExperimentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PrintExperimentsController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PrintExperiments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrintExperiment>>> GetAll()
        {
            var experiments = await _context.PrintExperiments
                .AsNoTracking()
                .ToListAsync();

            return Ok(experiments);
        }

        // GET: api/PrintExperiments/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrintExperiment>> GetById(int id)
        {
            var experiment = await _context.PrintExperiments
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (experiment == null)
                return NotFound();

            return Ok(experiment);
        }

        // POST: api/PrintExperiments
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<PrintExperiment>> Create(
            PrintExperiment experiment)
        {
            _context.PrintExperiments.Add(experiment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = experiment.Id },
                experiment);
        }

        // PUT: api/PrintExperiments/1
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            PrintExperiment updatedExperiment)
        {
            var experiment = await _context.PrintExperiments
                .FindAsync(id);

            if (experiment == null)
                return NotFound();

            experiment.MaterialType = updatedExperiment.MaterialType;
            experiment.LayerThickness = updatedExperiment.LayerThickness;
            experiment.InfillDensity = updatedExperiment.InfillDensity;
            experiment.InfillPattern = updatedExperiment.InfillPattern;
            experiment.FatigueStrength = updatedExperiment.FatigueStrength;
            experiment.CycleCount = updatedExperiment.CycleCount;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/PrintExperiments/1
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var experiment = await _context.PrintExperiments
                .FindAsync(id);

            if (experiment == null)
                return NotFound();

            _context.PrintExperiments.Remove(experiment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
