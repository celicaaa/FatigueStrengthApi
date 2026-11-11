using System.ComponentModel.DataAnnotations;
using FatigueStrengthApi.Contracts;
using FatigueStrengthApi.Data;
using FatigueStrengthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FatigueStrengthApi.Services;

public class OptimizationRecommendationsService(ApplicationDbContext context)
{
    public async Task<List<OptimizationRecommendationResponse>> GetAll()
    {
        return await context.OptimizationRecommendations
            .AsNoTracking()
            .OrderBy(r => r.Id)
            .Select(r => new OptimizationRecommendationResponse(
                r.Id, r.ExperimentId, r.RecommendedMaterial,
                r.RecommendedLayerThickness, r.RecommendedInfillDensity,
                r.RecommendedInfillPattern, r.Explanation, r.CreatedAt))
            .ToListAsync();
    }

    public async Task<OptimizationRecommendationResponse> GetById(int id)
    {
        var recommendation = await context.OptimizationRecommendations
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == id)
            ?? throw new KeyNotFoundException("Recommendation not found.");

        return ToResponse(recommendation);
    }

    public async Task<OptimizationRecommendationResponse> Create(
        OptimizationRecommendationRequest request)
    {
        Validator.ValidateObject(
            request, new ValidationContext(request), true);
        await EnsureExperimentExists(request.ExperimentId);

        var recommendation = new OptimizationRecommendation
        {
            ExperimentId = request.ExperimentId,
            RecommendedMaterial = request.RecommendedMaterial.Trim(),
            RecommendedLayerThickness = request.RecommendedLayerThickness,
            RecommendedInfillDensity = request.RecommendedInfillDensity,
            RecommendedInfillPattern = request.RecommendedInfillPattern.Trim(),
            Explanation = request.Explanation.Trim()
        };

        context.OptimizationRecommendations.Add(recommendation);
        await context.SaveChangesAsync();

        return ToResponse(recommendation);
    }

    public async Task Update(
        int id, OptimizationRecommendationRequest request)
    {
        Validator.ValidateObject(
            request, new ValidationContext(request), true);
        await EnsureExperimentExists(request.ExperimentId);

        var recommendation = await context.OptimizationRecommendations
            .SingleOrDefaultAsync(r => r.Id == id)
            ?? throw new KeyNotFoundException("Recommendation not found.");

        recommendation.ExperimentId = request.ExperimentId;
        recommendation.RecommendedMaterial = request.RecommendedMaterial.Trim();
        recommendation.RecommendedLayerThickness = request.RecommendedLayerThickness;
        recommendation.RecommendedInfillDensity = request.RecommendedInfillDensity;
        recommendation.RecommendedInfillPattern = request.RecommendedInfillPattern.Trim();
        recommendation.Explanation = request.Explanation.Trim();

        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var recommendation = await context.OptimizationRecommendations
            .SingleOrDefaultAsync(r => r.Id == id)
            ?? throw new KeyNotFoundException("Recommendation not found.");

        context.OptimizationRecommendations.Remove(recommendation);
        await context.SaveChangesAsync();
    }

    private async Task EnsureExperimentExists(int experimentId)
    {
        if (!await context.PrintExperiments.AnyAsync(e => e.Id == experimentId))
        {
            throw new ValidationException(
                "The specified experiment does not exist.");
        }
    }

    private static OptimizationRecommendationResponse ToResponse(
        OptimizationRecommendation recommendation)
    {
        return new OptimizationRecommendationResponse(
            recommendation.Id,
            recommendation.ExperimentId,
            recommendation.RecommendedMaterial,
            recommendation.RecommendedLayerThickness,
            recommendation.RecommendedInfillDensity,
            recommendation.RecommendedInfillPattern,
            recommendation.Explanation,
            recommendation.CreatedAt);
    }
}
