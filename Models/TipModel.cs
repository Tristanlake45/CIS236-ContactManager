using System.ComponentModel.DataAnnotations;

namespace PriceQuotation.Models
{
    public class TipModel
    {
        [Required(ErrorMessage = "Please enter a meal cost.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Meal cost must be greater than 0.")]
        public decimal? MealCost { get; set; }

        // Helper to compute a tip for a given percent (e.g., 15 means 15%)
        public decimal CalculateTip(decimal percent)
        {
            var baseCost = MealCost ?? 0m;
            return decimal.Round(baseCost * (percent / 100m), 2);
        }
    }
}
