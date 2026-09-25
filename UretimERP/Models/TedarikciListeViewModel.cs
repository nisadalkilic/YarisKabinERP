using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UretimERP.Models
{
    public class TedarikciListeViewModel
    {
        public int TedarikciID { get; set; }

        public string CariKod { get; set; }

        public string FirmaAdi { get; set; }

        public string YetkiliAdi { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public bool AktifMi { get; set; }
    }
}
