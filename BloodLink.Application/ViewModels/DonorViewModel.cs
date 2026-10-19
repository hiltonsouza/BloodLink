namespace BloodLink.Application.ViewModels
{
    public class DonorViewModel
    {
        public DonorViewModel() { }

        public DonorViewModel(
            int id, 
            string fullName, 
            string email, 
            DateTime birthDate, 
            string gender, 
            double weight, 
            string bloodType, 
            string rhFactor, 
            AddressViewModel? address, 
            List<DonationViewModel> donations)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            Weight = weight;
            BloodType = bloodType;
            RhFactor = rhFactor;
            Address = address;
            Donations = donations ?? new();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public double Weight { get; set; }
        public string BloodType { get; set; } = string.Empty;
        public string RhFactor { get; set; } = string.Empty;
        public AddressViewModel? Address { get; set; }
        public List<DonationViewModel> Donations { get; set; } = new();
    }
}
