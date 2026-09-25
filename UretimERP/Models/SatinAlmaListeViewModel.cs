using System;

namespace UretimERP.Models
{
    public class SatinAlmaListeViewModel
    {
        public int SatinAlmaSiparisID { get; set; }

        public string SiparisNo { get; set; }

        public string TedarikciAdi { get; set; }

        public DateTime SiparisTarihi { get; set; }

        public DateTime? TerminTarihi { get; set; }

        public string Durum { get; set; }
    }
}