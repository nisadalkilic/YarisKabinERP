using System;

namespace UretimERP.Models
{
    public class SiparisListeViewModel
    {
        public int SiparisID { get; set; }

        public string SiparisNo { get; set; }

        public string MusteriAdi { get; set; }

        public DateTime SiparisTarihi { get; set; }

        public DateTime? TerminTarihi { get; set; }

        public string Durum { get; set; }
    }
} 