using System;

namespace UretimERP.Models
{
    public class MusteriAlacakListeViewModel
    {
        public int AlacakID { get; set; }

        public string MusteriAdi { get; set; }

        public string BelgeNo { get; set; }

        public DateTime AlacakTarihi { get; set; }

        public DateTime? VadeTarihi { get; set; }

        public decimal Tutar { get; set; }

        public string ParaBirimi { get; set; }

        public string Durum { get; set; }
    }
}