using System;

namespace UretimERP.Models
{
    public class TedarikciBorcListeViewModel
    {
        public int BorcID { get; set; }

        public string TedarikciAdi { get; set; }

        public string BelgeNo { get; set; }

        public DateTime BorcTarihi { get; set; }

        public DateTime? VadeTarihi { get; set; }

        public decimal Tutar { get; set; }

        public string ParaBirimi { get; set; }

        public string Durum { get; set; }
    }
}