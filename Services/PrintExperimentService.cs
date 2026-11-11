using System.ComponentModel.DataAnnotations;
using FatigueStrengthApi.Contracts;
using FatigueStrengthApi.Data;
using FatigueStrengthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FatigueStrengthApi.Services;

public class PrintExperimentService(ApplicationDbContext context)
{
    public async Task<List<PrintExperimentResponse>> GetAll()
    {
        return await context.PrintExperiments
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .Select(e => new PrintExperimentResponse(
                e.Id, e.MaterialType, e.LayerThickness, e.InfillDensity,
                e.InfillPattern, e.FatigueStrength, e.CycleCount))
            .ToListAsync();
    }

    public async Task<PrintExperimentResponse> GetById(int id)
    {
        var experiment = await context.PrintExperiments
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id)
            ?? throw new KeyNotFoundException("Experiment not found.");

        return ToResponse(experiment);
    }

    public async Task<PrintExperimentResponse> Create(
        PrintExperimentRequest request)
    {
        Validator.ValidateObject(
            request, new ValidationContext(request), true);

        var experiment = new PrintExperiment
        {
            MaterialType = request.MaterialType.Trim(),
            LayerThickness = request.LayerThickness,
            InfillDensity = request.InfillDensity,
            InfillPattern = request.InfillPattern.Trim(),
            FatigueStrength = request.FatigueStrength,
            CycleCount = request.CycleCount
        };

        context.PrintExperiments.Add(experiment);
        await context.SaveChangesAsync();

        return ToResponse(experiment);
    }

    public async Task Update(int id, PrintExperimentRequest request)
    {
        Validator.ValidateObject(
            request, new ValidationContext(request), true);

        var experiment = await context.PrintExperiments
            .SingleOrDefaultAsync(e => e.Id == id)
            ?? throw new KeyNotFoundException("Experiment not found.");

        experiment.MaterialType = request.MaterialType.Trim();
        experiment.LayerThickness = request.LayerThickness;
        experiment.InfillDensity = request.InfillDensity;
        experiment.InfillPattern = request.InfillPattern.Trim();
        experiment.FatigueStrength = request.FatigueStrength;
        experiment.CycleCount = request.CycleCount;

        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var experiment = await context.PrintExperiments
            .SingleOrDefaultAsync(e => e.Id == id)
            ?? throw new KeyNotFoundException("Experiment not found.");

        context.PrintExperiments.Remove(experiment);
        await context.SaveChangesAsync();
    }

    private static PrintExperimentResponse ToResponse(
        PrintExperiment experiment)
    {
        return new PrintExperimentResponse(
            experiment.Id,
            experiment.MaterialType,
            experiment.LayerThickness,
            experiment.InfillDensity,
            experiment.InfillPattern,
            experiment.FatigueStrength,
            experiment.CycleCount);
    }
}
