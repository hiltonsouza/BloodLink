namespace BloodLink.Application.ViewModels
{
    public class DonorViewModel
    {
        public DonorViewModel(int id, string fullName, DateTime birthDate, string gender, double weight, string bloodType, string rhFactor)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
            Weight = weight;
            BloodType = bloodType;
            RhFactor = rhFactor;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public double Weight { get; set; }
        public string BloodType { get; set; }
        public string RhFactor { get; set; }
    }
}
