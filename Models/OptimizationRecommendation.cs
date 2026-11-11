namespace FatigueStrengthApi.Models
{
    public class OptimizationRecommendation
    {
        public int Id { get; set; }
        public int ExperimentId { get; set; }
        public string RecommendedMaterial { get; set; } = string.Empty;
        public double RecommendedLayerThickness { get; set; } //Рекомендуемая толщина слоя
        public double RecommendedInfillDensity { get; set; } //Рекомендуемая плотность заполнения
        public string RecommendedInfillPattern { get; set; } = string.Empty; //Рекомендуемый тип заполнения
        public string Explanation { get; set; } = string.Empty; //Объяснение рекомендации
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
