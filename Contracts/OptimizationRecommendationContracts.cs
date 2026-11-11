using System.ComponentModel.DataAnnotations;

namespace FatigueStrengthApi.Contracts;

public record OptimizationRecommendationRequest(
    [property: Range(1, int.MaxValue)] int ExperimentId,
    [property: Required] string RecommendedMaterial,
    [property: Range(0.01, double.MaxValue)] double RecommendedLayerThickness,
    [property: Range(0, 100)] double RecommendedInfillDensity,
    [property: Required] string RecommendedInfillPattern,
    [property: Required(AllowEmptyStrings = true)] string Explanation);

public record OptimizationRecommendationResponse(
    int Id,
    int ExperimentId,
    string RecommendedMaterial,
    double RecommendedLayerThickness,
    double RecommendedInfillDensity,
    string RecommendedInfillPattern,
    string Explanation,
    DateTime CreatedAt);
