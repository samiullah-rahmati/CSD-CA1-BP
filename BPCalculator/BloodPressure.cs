using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name="Low Blood Pressure")] Low,
        [Display(Name="Ideal Blood Pressure")]  Ideal,
        [Display(Name="Pre-High Blood Pressure")] PreHigh,
        [Display(Name ="High Blood Pressure")]  High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        // category thresholds (mmHg); lower limits are inclusive
        public const int HighSystolic = 140;
        public const int HighDiastolic = 90;
        public const int PreHighSystolic = 120;
        public const int PreHighDiastolic = 80;
        public const int LowSystolic = 90;
        public const int LowDiastolic = 60;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                if (Systolic >= HighSystolic || Diastolic >= HighDiastolic)
                {
                    return BPCategory.High;
                }

                if (Systolic >= PreHighSystolic || Diastolic >= PreHighDiastolic)
                {
                    return BPCategory.PreHigh;
                }

                if (Systolic < LowSystolic && Diastolic < LowDiastolic)
                {
                    return BPCategory.Low;
                }

                return BPCategory.Ideal;
            }
        }
    }
}