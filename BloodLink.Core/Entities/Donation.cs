using BloodLink.Core.Exceptions;
using BloodLink.Core.Resources;

namespace BloodLink.Core.Entities
{
    public class Donation : BaseEntity
    {
        public Donation(int donorId, int bloodVolumeInML)
        {
            DonorId = donorId;
            DonationDate = DateTime.Now;
            BloodVolumeInML = bloodVolumeInML;
        }

        public int DonorId { get; private set; }
        public DateTime DonationDate { get; private set; }

        //quantidadeML
        public int BloodVolumeInML { get; private set; }
        public Donor Donor { get; private set; }

        public void ValidateBloodQuantity()
        {
            const int minVolume = 420;
            const int maxVolume = 470;

            if (BloodVolumeInML < minVolume || BloodVolumeInML > maxVolume)
                throw new DomainException(Messages.BloodVolumeInvalid);
        }
    }
}
