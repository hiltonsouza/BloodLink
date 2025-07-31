namespace BloodLink.Application.Queries.GetDonationsByDonorId
{
    public class DonationViewModel
    {
        public DonationViewModel(int donorId, DateTime donationDate, int bloodVolumeInML)
        {
            DonorId = donorId;
            DonationDate = donationDate;
            BloodVolumeInML = bloodVolumeInML;
        }

        public int DonorId { get; set; }
        public DateTime DonationDate { get; set; }
        public int BloodVolumeInML { get; set; }
    }
}
