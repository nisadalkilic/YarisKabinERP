using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UretimERP.Models
{
    public class KritikStokViewModel
    {
        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public decimal MinimumStok { get; set; }

        public decimal MevcutStok { get; set; }
    }
}