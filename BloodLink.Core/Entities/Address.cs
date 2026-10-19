namespace BloodLink.Core.Entities
{
    public class Address
    {
        private Address() { }
        public Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string Street { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string State { get; private set; } = null!;
        public string ZipCode { get; private set; } = null!;
        public override bool Equals(object? obj)
        {
            return obj is Address address &&
                Street == address.Street &&
                City == address.City &&
                State == address.State &&
                ZipCode == address.ZipCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, State, ZipCode); 
        }
    }
}
