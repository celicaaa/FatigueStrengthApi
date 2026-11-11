using System.ComponentModel.DataAnnotations;
using FatigueStrengthApi.Contracts;
using FatigueStrengthApi.Data;
using FatigueStrengthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FatigueStrengthApi.Services;

public class FatiguePredictionService(ApplicationDbContext context)
{
    // Прогноз усталостной прочности по параметрам печати
    public double CalculateStrength(PrintExperiment experiment)
    {
        var materialFactor = experiment.MaterialType switch
        {
            "PETG" => 1.1,
            "ABS" => 1.05,
            _ => 1.0
        };

        var result = 100.0 * materialFactor
            * (1.0 / (1.0 + experiment.LayerThickness))
            * (experiment.InfillDensity / 100.0);

        return Math.Round(result, 2);
    }

    public async Task<List<FatiguePredictionResponse>> GetAll()
    {
        return await context.FatiguePredictions
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Select(p => new FatiguePredictionResponse(
                p.Id, p.ExperimentId, p.PredictedStrength,
                p.TargetStrength, p.CreatedAt))
            .ToListAsync();
    }

    public async Task<FatiguePredictionResponse> GetById(int id)
    {
        var prediction = await context.FatiguePredictions
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id)
            ?? throw new KeyNotFoundException("Prediction not found.");

        return ToResponse(prediction);
    }

    public async Task<FatiguePredictionResponse> CreateFromExperiment(
        int experimentId, double targetStrength)
    {
        if (targetStrength <= 0)
        {
            throw new ValidationException(
                "Target strength must be greater than zero.");
        }

        var experiment = await context.PrintExperiments
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == experimentId)
            ?? throw new KeyNotFoundException("Experiment not found.");

        var prediction = new FatiguePrediction
        {
            ExperimentId = experimentId,
            PredictedStrength = CalculateStrength(experiment),
            TargetStrength = targetStrength
        };

        context.FatiguePredictions.Add(prediction);
        await context.SaveChangesAsync();

        return ToResponse(prediction);
    }

    public async Task Update(int id, FatiguePredictionRequest request)
    {
        Validator.ValidateObject(
            request, new ValidationContext(request), true);

        var prediction = await context.FatiguePredictions
            .SingleOrDefaultAsync(p => p.Id == id)
            ?? throw new KeyNotFoundException("Prediction not found.");

        prediction.PredictedStrength = request.PredictedStrength;
        prediction.TargetStrength = request.TargetStrength;

        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var prediction = await context.FatiguePredictions
            .SingleOrDefaultAsync(p => p.Id == id)
            ?? throw new KeyNotFoundException("Prediction not found.");

        context.FatiguePredictions.Remove(prediction);
        await context.SaveChangesAsync();
    }

    private static FatiguePredictionResponse ToResponse(
        FatiguePrediction prediction)
    {
        return new FatiguePredictionResponse(
            prediction.Id,
            prediction.ExperimentId,
            prediction.PredictedStrength,
            prediction.TargetStrength,
            prediction.CreatedAt);
    }
}
