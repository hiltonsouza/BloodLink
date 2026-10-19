using MediatR;

namespace BloodLink.Application.Events.DonationCreated
{
    public class DonationCreatedNotification : INotification
    {
        public DonationCreatedNotification(int donationId, string bloodType, string rhFactor, int bloodVolumeInML)
        {
            DonationId = donationId;
            BloodType = bloodType;
            RhFactor = rhFactor;
            BloodVolumeInML = bloodVolumeInML;
        }

        public int DonationId { get; private set; }
        public string BloodType { get; private set; }
        public string RhFactor { get; private set; }
        public int BloodVolumeInML { get; private set; }
    }
}
