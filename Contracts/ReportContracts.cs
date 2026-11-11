namespace FatigueStrengthApi.Contracts;

public record ExperimentWithPredictionResponse(
    int Id,
    string MaterialType,
    double LayerThickness,
    double PredictedStrength,
    double TargetStrength);

public record ExperimentWithRecommendationResponse(
    int Id,
    string MaterialType,
    double InfillDensity,
    string RecommendedMaterial,
    double RecommendedLayerThickness,
    double RecommendedInfillDensity,
    string RecommendedInfillPattern);

public record FullSummaryResponse(
    int ExperimentId,
    string MaterialType,
    double PredictedStrength,
    double TargetStrength,
    bool MeetsTarget,
    string RecommendedMaterial,
    double RecommendedLayerThickness,
    string Explanation);
