using System;

namespace UretimERP.Models
{
    public class KullaniciListeViewModel
    {
        public int KullaniciID { get; set; }

        public string KullaniciAdi { get; set; }

        public string AdSoyad { get; set; }

        public string Email { get; set; }

        public string Roller { get; set; }

        public bool AktifMi { get; set; }

        public DateTime OlusturmaTarihi { get; set; }

        public DateTime? SonGirisTarihi { get; set; }
    }
}