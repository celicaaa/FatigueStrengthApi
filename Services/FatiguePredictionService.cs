
using FatigueStrengthApi.Models;

namespace FatigueStrengthApi.Services
{
    public class FatiguePredictionService
    {
        public double PredictStrength(PrintExperiment experiment)
        {
            if (experiment == null)
                throw new ArgumentNullException(nameof(experiment));

            // Демонстрационная формула
            double materialFactor =
                experiment.MaterialType.ToUpperInvariant() switch
                {
                    "PLA" => 1.0,
                    "PETG" => 1.1,
                    "ABS" => 1.05,
                    _ => 1.0
                };

            double thicknessFactor =
                1.0 / (1.0 + experiment.LayerThickness);

            double infillFactor =
                experiment.InfillDensity / 100.0;

            double predictedStrength =
                100.0 * materialFactor
                * thicknessFactor
                * infillFactor;

            return Math.Round(predictedStrength, 2);
        }

        public bool MeetsTarget(
            double predictedStrength,
            double targetStrength)
        {
            if (targetStrength <= 0)
                throw new ArgumentException(
                    "Целевая прочность должна быть положительной.");

            return predictedStrength >= targetStrength;
        }

        public double CalculateDeviation(
            double predictedStrength,
            double targetStrength)
        {
            return Math.Round(
                predictedStrength - targetStrength, 2);
        }
    }
}
