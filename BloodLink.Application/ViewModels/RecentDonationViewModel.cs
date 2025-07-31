namespace BloodLink.Application.ViewModels
{
    public class RecentDonationViewModel
    {
        public int DonationId { get; set; }
        public DateTime DonationDate { get; set; }
        public int BloodVolumeInML { get; set; }
        public string DonorName { get; set; }
        public string DonorEmail { get; set; }
        public string DonorBloodType { get; set; }
        public string DonorRhFactor { get; set; }
    }
}