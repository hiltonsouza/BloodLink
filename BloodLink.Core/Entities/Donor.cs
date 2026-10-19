using BloodLink.Core.Enums;
using BloodLink.Core.Exceptions;
using BloodLink.Core.Resources;

namespace BloodLink.Core.Entities
{
    public class Donor : BaseEntity
    {
        private Donor() { }
        public Donor(string fullName, string email, DateTime birthDate, string gender, double weight, string bloodType, string rhFactor, Address address)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            Weight = weight;
            BloodType = ParseBloodType(bloodType);
            RhFactor = rhFactor;
            Address = address;
            //ver como vai se comportar
            Donations = new List<Donation>();

        }

        public string FullName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public DateTime BirthDate { get; private set; }
        public string Gender { get; private set; } = null!;
        public double Weight { get; private set; }
        public BloodType BloodType { get; private set; }
        public string RhFactor { get; private set; } = null!;
        public List<Donation> Donations { get; private set; } = new();
        public Address Address { get; private set; } = null!;
        public void Update(string fullName, string email, DateTime birthDate, string gender, double weight, string bloodType, string rhFactor, Address address)
        {
            if (!string.IsNullOrEmpty(fullName))
                FullName = fullName;
            if (!string.IsNullOrEmpty(email))
                Email = email;
            if (birthDate != default)
                BirthDate = birthDate;
            if (!string.IsNullOrEmpty(gender))
                Gender = gender;
            if (weight > 49)
                Weight = weight;
            if (!string.IsNullOrEmpty(bloodType))
                BloodType = ParseBloodType(bloodType);
            if (!string.IsNullOrEmpty(rhFactor))
                RhFactor = rhFactor;
            if (address != null)
                Address = address;
        }


        public bool IsAdult() => GetAge() >= 18;
        public bool IsEligibleByAge() => GetAge() >= 18 && GetAge() <= 69;
        public bool IsEligibleByWeight() => Weight >= 50;
        private int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate > today.AddYears(-age)) age--;
            return age;
        }

        private BloodType ParseBloodType(string bloodType)
        {
            if (Enum.TryParse(bloodType, true, out BloodType parsed)
                && Enum.IsDefined(typeof(BloodType), parsed))
                return parsed;

            throw new DomainException(Messages.InvalidBloodType);
        }

        public void EnsureEligibility()
        {
            if (!IsEligibleByAge())
                throw new DomainException(Messages.DonorAgeEligibility);

            if (!IsEligibleByWeight())
                throw new DomainException(Messages.DonorWeightEligibility);
        }
        public void EnsureDonationInterval()
        {
            if (Donations is null || Donations.Count == 0)
                return; // never donated, can donate

            var lastDonation = Donations.OrderByDescending(d => d.DonationDate).FirstOrDefault();
            var daysSinceLastDonation = (DateTime.Today - lastDonation.DonationDate.Date).TotalDays;

            if (Gender.Equals("Female", StringComparison.OrdinalIgnoreCase) && daysSinceLastDonation < 90)
                throw new DomainException(Messages.DonorFemaleDonationInterval);
            if (Gender.Equals("Male", StringComparison.OrdinalIgnoreCase) && daysSinceLastDonation < 60)
                throw new DomainException(Messages.DonorMaleDonationInterval);
        }
    }
}
