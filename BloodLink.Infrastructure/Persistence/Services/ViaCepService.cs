using BloodLink.Core.Entities;
using BloodLink.Core.Exceptions;
using BloodLink.Core.Services;
using BloodLink.Core.Results;
using Flurl;
using Flurl.Http;
using BloodLink.Core.Resources;

namespace BloodLink.Infrastructure.Persistence.Services
{
    public class ViaCepService : IViaCepService
    {
        private const string BaseUrl = "https://viacep.com.br/ws/";

        public async Task<Result<Address>> GetAddressByZipCodeAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = new Url($"{BaseUrl}{zipCode}/json/");

                var result = await url.WithTimeout(10).GetJsonAsync<ViaCepResponse>(cancellationToken: cancellationToken);

                if (result.Erro == "true")
                {
                    var error = new DomainException(string.Format(Messages.AddressNotFound, zipCode));
                    return Result<Address>.Fail(error.Message);
                }
                                        

                var address = new Address(result.Logradouro, result.Localidade, result.Uf, result.Cep);

                return Result<Address>.Ok(address);
            }
            catch (FlurlHttpException ex)
            {
                throw new DomainException(string.Format(Messages.ExternalServiceUnavailable, ex.Message));
            }
        }

        private class ViaCepResponse
        {
            public string Logradouro { get; set; } = string.Empty;
            public string Bairro { get; set; } = string.Empty;
            public string Localidade { get; set; } = string.Empty;
            public string Uf { get; set; } = string.Empty;
            public string Cep { get; set; } = string.Empty;
            public string Erro { get; set; } = "false";
        }
    }
}
