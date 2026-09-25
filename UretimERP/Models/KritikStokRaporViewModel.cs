using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UretimERP.Models
{
    public class KritikStokRaporViewModel
    {
        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public decimal KullanilabilirStok { get; set; }

        public decimal MinimumStok { get; set; }

        public decimal EksikMiktar { get; set; }
    }
}