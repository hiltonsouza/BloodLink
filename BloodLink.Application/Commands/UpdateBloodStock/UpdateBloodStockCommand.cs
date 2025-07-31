using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Application.Commands.UpdateBloodStock
{
    public class UpdateBloodStockCommand : IRequest<Unit>
    {
        public int Id { get; set; } 
        public string BloodType { get; set; }
        public string FactorRh { get; set; }
        public int BloodVolumeInML { get; set; }
    }
}
