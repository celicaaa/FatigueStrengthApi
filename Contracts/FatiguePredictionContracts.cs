using System.ComponentModel.DataAnnotations;

namespace FatigueStrengthApi.Contracts;

public record FatiguePredictionRequest(
    [property: Range(0, double.MaxValue)] double PredictedStrength,
    [property: Range(0.01, double.MaxValue)] double TargetStrength);

public record FatiguePredictionResponse(
    int Id,
    int ExperimentId,
    double PredictedStrength,
    double TargetStrength,
    DateTime CreatedAt);
