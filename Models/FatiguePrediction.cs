
namespace FatigueStrengthApi.Models
{
    public class FatiguePrediction
    {
        public int Id { get; set; }
        public int ExperimentId { get; set; }
        public double PredictedStrength { get; set; }
        public double TargetStrength { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool MeetsTarget()
        {
            return PredictedStrength >= TargetStrength;
        }

        public double CalculateDeviation()
        {
            return PredictedStrength - TargetStrength;
        }
    }
}
