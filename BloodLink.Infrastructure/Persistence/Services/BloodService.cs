using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BloodLink.Infrastructure.Persistence.Services
{
    public class BloodService
    {
        public static (string BloodType, string FactorRh) ParseBloodQuery(string query)
        {
            // Regex para capturar o tipo sanguíneo e o fator Rh
            var pattern = @"(?<BloodType>[A|B|AB|O]{1,2})(?<FactorRh>[+-]?)";
            var match = Regex.Match(query, pattern, RegexOptions.IgnoreCase);

            // Se houver correspondência, extraímos os grupos
            if (match.Success)
            {
                var bloodType = match.Groups["BloodType"].Value;
                var factorRh = match.Groups["FactorRh"].Value;

                return (bloodType.ToUpper(), factorRh);
            }

            // Retorna vazio se não houver correspondência
            return (string.Empty, string.Empty);
        }
    }

}
