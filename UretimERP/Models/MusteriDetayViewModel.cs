using System;
using System.Collections.Generic;

namespace UretimERP.Models
{
    public class MusteriDetayViewModel
    {
        public int MusteriID { get; set; }

        public string CariKod { get; set; }

        public string FirmaAdi { get; set; }

        public string YetkiliAdi { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public string Adres { get; set; }

        public bool AktifMi { get; set; }

        public int SiparisSayisi { get; set; }

        public decimal ToplamAlacak { get; set; }

        public decimal ToplamTahsilat { get; set; }

        public List<SiparisListeViewModel> Siparisler { get; set; }
    }
}