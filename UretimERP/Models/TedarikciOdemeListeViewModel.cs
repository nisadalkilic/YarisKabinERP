using System;

namespace UretimERP.Models
{
    public class TedarikciOdemeListeViewModel
    {
        public int OdemeID { get; set; }

        public string TedarikciAdi { get; set; }

        public DateTime OdemeTarihi { get; set; }

        public decimal Tutar { get; set; }

        public string ParaBirimi { get; set; }

        public string OdemeYontemi { get; set; }

        public string BelgeNo { get; set; }

        public string Aciklama { get; set; }
    }
}
