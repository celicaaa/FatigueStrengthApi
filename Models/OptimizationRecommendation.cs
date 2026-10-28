
namespace FatigueStrengthApi.Models
{
    public class OptimizationRecommendation
    {
        public int Id { get; set; }
        public int ExperimentId { get; set; }
        public string RecommendedMaterial { get; set; } = string.Empty;
        public double RecommendedLayerThickness { get; set; }
        public double RecommendedInfillDensity { get; set; }
        public string RecommendedInfillPattern { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
