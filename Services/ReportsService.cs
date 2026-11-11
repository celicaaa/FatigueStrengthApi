using FatigueStrengthApi.Contracts;
using FatigueStrengthApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FatigueStrengthApi.Services;

public class ReportsService(ApplicationDbContext context)
{
    public async Task<List<ExperimentWithPredictionResponse>>
        GetExperimentsWithPredictions()
    {
        return await context.FatiguePredictions
            .AsNoTracking()
            .Where(p => p.PredictedStrength >= p.TargetStrength)
            .Join(context.PrintExperiments,
                p => p.ExperimentId,
                e => e.Id,
                (p, e) => new ExperimentWithPredictionResponse(
                    e.Id, e.MaterialType, e.LayerThickness,
                    p.PredictedStrength, p.TargetStrength))
            .ToListAsync();
    }

    public async Task<List<ExperimentWithRecommendationResponse>>
        GetExperimentsWithRecommendations()
    {
        return await context.PrintExperiments
            .AsNoTracking()
            .Where(e => e.InfillDensity > 0)
            .Join(context.OptimizationRecommendations,
                e => e.Id,
                r => r.ExperimentId,
                (e, r) => new ExperimentWithRecommendationResponse(
                    e.Id, e.MaterialType, e.InfillDensity,
                    r.RecommendedMaterial, r.RecommendedLayerThickness,
                    r.RecommendedInfillDensity, r.RecommendedInfillPattern))
            .ToListAsync();
    }

    public async Task<List<FullSummaryResponse>> GetFullSummary()
    {
        return await context.FatiguePredictions
            .AsNoTracking()
            .Where(p => p.TargetStrength > 0)
            .Join(context.PrintExperiments,
                p => p.ExperimentId,
                e => e.Id,
                (p, e) => new { p, e })
            .Join(context.OptimizationRecommendations,
                x => x.e.Id,
                r => r.ExperimentId,
                (x, r) => new FullSummaryResponse(
                    x.e.Id, x.e.MaterialType,
                    x.p.PredictedStrength, x.p.TargetStrength,
                    x.p.PredictedStrength >= x.p.TargetStrength,
                    r.RecommendedMaterial, r.RecommendedLayerThickness,
                    r.Explanation))
            .ToListAsync();
    }
}
