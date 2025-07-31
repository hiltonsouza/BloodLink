namespace BloodLink.Application.ViewModels
{
    public class BloodStockReportViewModel
    {
        public BloodStockReportViewModel(string bloodType, string factorRh, int bloodVolumeInML)
        {
            BloodType = bloodType;
            FactorRh = factorRh;
            BloodVolumeInML = bloodVolumeInML;
        }

        public string BloodType { get; set; }
        public string FactorRh { get; set; }
        public int BloodVolumeInML { get; set; }
    }
}
