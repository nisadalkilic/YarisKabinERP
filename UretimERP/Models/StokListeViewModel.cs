using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UretimERP.Models
{
    public class StokListeViewModel
    {
        public int StokID { get; set; }

        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }
        
        public int DepoID { get; set; }

        public string DepoAdi { get; set; }

        public decimal FizikselStok { get; set; }

        public decimal RezerveStok { get; set; }

        public decimal KullanilabilirStok { get; set; }

        public decimal MinimumStok { get; set; }
    }
}