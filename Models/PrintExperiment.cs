
namespace FatigueStrengthApi.Models
{
    public class PrintExperiment
    {
        public int Id { get; set; }
        public string MaterialType { get; set; } = string.Empty;
        public double LayerThickness { get; set; }
        public double InfillDensity { get; set; }
        public string InfillPattern { get; set; } = string.Empty;
        public double? FatigueStrength { get; set; }
        public long? CycleCount { get; set; }
        public void RegisterTestResult(
            double fatigueStrength,
            long cycleCount)
        {
            if (fatigueStrength <= 0)
                throw new ArgumentException(
                    "Усталостная прочность должна быть положительной.");

            if (cycleCount <= 0)
                throw new ArgumentException(
                    "Количество циклов должно быть положительным.");

            FatigueStrength = fatigueStrength;
            CycleCount = cycleCount;
        }

        public bool HasTestResult()
        {
            return FatigueStrength.HasValue
                && CycleCount.HasValue;
        }
    }
}
