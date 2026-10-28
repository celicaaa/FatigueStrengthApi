using FatigueStrengthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FatigueStrengthApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PrintExperiment> PrintExperiments { get; set; }

        public DbSet<FatiguePrediction> FatiguePredictions { get; set; }

        public DbSet<OptimizationRecommendation>
            OptimizationRecommendations
        { get; set; }
    }
}

