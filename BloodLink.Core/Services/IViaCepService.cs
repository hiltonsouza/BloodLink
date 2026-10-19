using BloodLink.Core.Entities;
using BloodLink.Core.Results;

namespace BloodLink.Core.Services
{
    public interface IViaCepService
    {
        Task<Result<Address>> GetAddressByZipCodeAsync(string zipCode, CancellationToken cancellationToken = default);
    }
}
