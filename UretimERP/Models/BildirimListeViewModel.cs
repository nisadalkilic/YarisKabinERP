using System;

namespace UretimERP.Models
{
    public class BildirimListeViewModel
    {
        public int BildirimID { get; set; }

        public string KullaniciAdi { get; set; }

        public string Baslik { get; set; }

        public string Mesaj { get; set; }

        public bool OkunduMu { get; set; }

        public DateTime? OkunmaTarihi { get; set; }

        public DateTime OlusturmaTarihi { get; set; }

        public string OnemSeviyesi { get; set; }

        public string ReferansTuru { get; set; }

        public int? ReferansID { get; set; }
    }
}