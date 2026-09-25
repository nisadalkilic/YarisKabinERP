using System;

namespace UretimERP.Models
{
    public class UretimEmriListeViewModel
    {
        public int UretimEmriID { get; set; }

        public string UretimEmriNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public decimal PlanlananMiktar { get; set; }

        public DateTime? PlanlananBaslangicTarihi { get; set; }

        public string Durum { get; set; }
    }
}