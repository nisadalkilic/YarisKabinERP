using System;

namespace UretimERP.Models
{
    public class MusteriTahsilatListeViewModel
    {
        public int TahsilatID { get; set; }

        public string MusteriAdi { get; set; }

        public DateTime TahsilatTarihi { get; set; }

        public decimal Tutar { get; set; }

        public string ParaBirimi { get; set; }

        public string OdemeYontemi { get; set; }

        public string Aciklama { get; set; }
    }
}
