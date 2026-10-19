namespace BloodLink.Application.ViewModels
{
    public class DonationReportViewModel
    {
        public DonationReportViewModel(int donationId, int donorId, string donorName, string donorEmail, string bloodType, int bloodVolumeML, DateTime donationDate)
        {
            DonationId = donationId;
            DonorId = donorId;
            DonorName = donorName;
            DonorEmail = donorEmail;
            BloodType = bloodType;
            BloodVolumeML = bloodVolumeML;
            DonationDate = donationDate;
        }

        public int DonationId { get; private set; }
        public int DonorId { get; private set; }
        public string DonorName { get; private set; } = string.Empty;
        public string DonorEmail { get; private set; } = string.Empty;
        public string BloodType { get; private set; } = string.Empty;
        public int BloodVolumeML { get; private set; }
        public DateTime DonationDate { get; private set; }
    }
}
