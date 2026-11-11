namespace FatigueStrengthApi.Models
{
    public class PrintExperiment
    {
        public int Id { get; set; }
        public string MaterialType { get; set; } = string.Empty;
        public double LayerThickness { get; set; } //Толщина слоя
        public double InfillDensity { get; set; } //Плотность заполнения
        public string InfillPattern { get; set; } = string.Empty; //Тип заполнения
        public double? FatigueStrength { get; set; } //Усталостная прочность
        public long? CycleCount { get; set; } //Количество циклов нагружения
        public void RegisterTestResult(double fatigueStrength,long cycleCount)
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
            return FatigueStrength.HasValue && CycleCount.HasValue;
        }
    }
}
