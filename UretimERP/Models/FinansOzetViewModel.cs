using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UretimERP.Models
{
    public class FinansOzetViewModel
    {
        public decimal ToplamAlacak { get; set; }

        public decimal ToplamTahsilat { get; set; }

        public decimal ToplamTedarikciBorcu { get; set; }

        public decimal ToplamTedarikciOdeme { get; set; }

        public decimal ToplamGenelGider { get; set; }

        public decimal ToplamUretimMaliyeti { get; set; }
    }
}
