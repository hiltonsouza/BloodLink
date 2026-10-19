namespace BloodLink.Core.Entities
{
    /// <summary>
    /// Represents a blood stock entity in the system.
    /// </summary>
    public class BloodStock : BaseEntity
    {
        public BloodStock()
        {
        }
        public BloodStock(string bloodType, string factorRh, int bloodVolumeInML)
        {
            BloodType = bloodType;
            FactorRh = factorRh;
            BloodVolumeInML = bloodVolumeInML;
        }
     
        public string BloodType { get; private set; } = null!;
        public string FactorRh {  get; private set; } = null!;
        public int BloodVolumeInML { get; private set; }

        public void Update(string bloodType, string factorRh, int bloodVolumeInML)
        {
            BloodType = bloodType;
            FactorRh = factorRh;
            BloodVolumeInML = bloodVolumeInML;
        }

        public void IncreaseBloodVolume(int bloodVolumeInMl)
        {
            BloodVolumeInML += bloodVolumeInMl;
        }
    }
}
