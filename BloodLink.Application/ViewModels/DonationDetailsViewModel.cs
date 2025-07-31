using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Application.ViewModels
{
    public class DonationDetailsViewModel
    {
        public int id { get; set; }
        public int DonorId { get; set; }
        public DateTime DonationDate { get; set; }
        public int BloodVolumeInML { get; set; }
    }
}
