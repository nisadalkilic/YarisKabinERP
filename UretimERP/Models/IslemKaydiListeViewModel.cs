using System;

namespace UretimERP.Models
{
    public class IslemKaydiListeViewModel
    {
        public int IslemKayitID { get; set; }

        public string KullaniciAdi { get; set; }

        public string IslemTuru { get; set; }

        public string TabloAdi { get; set; }

        public int? KayitID { get; set; }

        public DateTime IslemTarihi { get; set; }

        public string Aciklama { get; set; }
    }
}