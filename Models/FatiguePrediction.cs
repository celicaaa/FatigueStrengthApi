namespace FatigueStrengthApi.Models
{
    public class FatiguePrediction
    {
        public int Id { get; set; }
        public int ExperimentId { get; set; }
        public double PredictedStrength { get; set; } //Прогнозируемая усталостная прочность
        public double TargetStrength { get; set; } //Целевая прочность
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
