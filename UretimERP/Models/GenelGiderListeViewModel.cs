using System;

namespace UretimERP.Models
{
    public class GenelGiderListeViewModel
    {
        public int GiderID { get; set; }

        public DateTime GiderTarihi { get; set; }

        public string GiderTuru { get; set; }

        public string BelgeNo { get; set; }

        public decimal Tutar { get; set; }

        public string ParaBirimi { get; set; }

        public string Aciklama { get; set; }
    }
}