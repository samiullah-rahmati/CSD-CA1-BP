using System.ComponentModel.DataAnnotations;
using BPCalculator;
using Xunit;

namespace BPCalculator.Tests
{
    public class BloodPressureTests
    {
        // ---- Category rules (lower limits are inclusive) ----

        [Theory]
        // Low: both numbers below the Low limits
        [InlineData(70, 40, BPCategory.Low)]
        [InlineData(85, 55, BPCategory.Low)]
        [InlineData(89, 59, BPCategory.Low)]
        // Ideal: including readings that are low on only one number
        [InlineData(85, 70, BPCategory.Ideal)]
        [InlineData(89, 60, BPCategory.Ideal)]
        [InlineData(90, 59, BPCategory.Ideal)]
        [InlineData(90, 60, BPCategory.Ideal)]
        [InlineData(100, 60, BPCategory.Ideal)]
        [InlineData(119, 79, BPCategory.Ideal)]
        // Pre-High: either number reaches its Pre-High limit
        [InlineData(120, 70, BPCategory.PreHigh)]
        [InlineData(110, 80, BPCategory.PreHigh)]
        [InlineData(139, 89, BPCategory.PreHigh)]
        // High: either number reaches its High limit
        [InlineData(140, 70, BPCategory.High)]
        [InlineData(120, 90, BPCategory.High)]
        [InlineData(190, 100, BPCategory.High)]
        public void Category_ReturnsExpectedCategory(int systolic, int diastolic, BPCategory expected)
        {
            // Arrange
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };

            // Act
            var actual = bp.Category;

            // Assert
            Assert.Equal(expected, actual);
        }

        // ---- Input range validation ----

        [Theory]
        [InlineData(70, 40, true)]     // lowest allowed values
        [InlineData(190, 100, true)]   // highest allowed values
        [InlineData(69, 60, false)]    // systolic just below range
        [InlineData(191, 60, false)]   // systolic just above range
        [InlineData(120, 39, false)]   // diastolic just below range
        [InlineData(120, 101, false)]  // diastolic just above range
        public void Validation_EnforcesAllowedRanges(int systolic, int diastolic, bool expectedValid)
        {
            // Arrange
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            var results = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(
                bp, new ValidationContext(bp), results, validateAllProperties: true);

            // Assert
            Assert.Equal(expectedValid, isValid);
        }
    }
}