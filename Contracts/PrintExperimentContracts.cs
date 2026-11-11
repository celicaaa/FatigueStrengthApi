using System.ComponentModel.DataAnnotations;

namespace FatigueStrengthApi.Contracts;

public record PrintExperimentRequest(
    [property: Required] string MaterialType,
    [property: Range(0.01, double.MaxValue)] double LayerThickness,
    [property: Range(0, 100)] double InfillDensity,
    [property: Required] string InfillPattern,
    [property: Range(0.01, double.MaxValue)] double? FatigueStrength,
    [property: Range(1, double.MaxValue)] long? CycleCount);

public record PrintExperimentResponse(
    int Id,
    string MaterialType,
    double LayerThickness,
    double InfillDensity,
    string InfillPattern,
    double? FatigueStrength,
    long? CycleCount);
