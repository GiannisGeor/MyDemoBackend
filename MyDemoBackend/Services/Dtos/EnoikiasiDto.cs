using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class EnoikiasiDto
    {
        public int IDPelati { get; set; }
        public int IDKasetas { get; set; }
        public DateTime Apo { get; set; }
        public DateTime? Eos { get; set; }
    }
}
