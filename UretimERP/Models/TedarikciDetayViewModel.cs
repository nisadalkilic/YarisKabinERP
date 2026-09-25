using System;
using System.Collections.Generic;

namespace UretimERP.Models
{
    public class TedarikciDetayViewModel
    {
        public int TedarikciID { get; set; }

        public string CariKod { get; set; }

        public string FirmaAdi { get; set; }

        public string YetkiliAdi { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public string Adres { get; set; }

        public bool AktifMi { get; set; }

        public int SatinAlmaSayisi { get; set; }

        public decimal ToplamBorc { get; set; }

        public decimal ToplamOdeme { get; set; }

        public List<SatinAlmaListeViewModel> SatinAlmalar { get; set; }
    }
}