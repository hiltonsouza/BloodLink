namespace BloodLink.Application.ViewModels
{
    public class DonationViewModel
    {
        public DonationViewModel() { }

        public DonationViewModel(
            int id, 
            int donorId, 
            string donorName, 
            string bloodType, 
            string factorRh, 
            DateTime donationDate, 
            int bloodVolumeInML)
        {
            Id = id;
            DonorId = donorId;
            DonorName = donorName;
            BloodType = bloodType;
            FactorRh = factorRh;
            DonationDate = donationDate;
            BloodVolumeInML = bloodVolumeInML;
        }

        public DonationViewModel(int id, DateTime donationDate, int bloodVolumeInML)
            : this(id, 0, string.Empty, string.Empty, string.Empty, donationDate, bloodVolumeInML)
        {
        }

        public int Id { get; set; }
        public int DonorId { get; set; }
        public string DonorName { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public string FactorRh { get; set; } = string.Empty;
        public DateTime DonationDate { get; set; }
        public int BloodVolumeInML { get; set; }
    }
}
