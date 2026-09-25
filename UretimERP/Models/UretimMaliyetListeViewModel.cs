using System;

namespace UretimERP.Models
{
    public class UretimMaliyetListeViewModel
    {
        public int UretimMaliyetID { get; set; }

        public string UretimEmriNo { get; set; }

        public decimal MalzemeMaliyeti { get; set; }

        public decimal IscilikMaliyeti { get; set; }

        public decimal GenelUretimGideri { get; set; }

        public decimal DigerMaliyet { get; set; }

        public decimal ToplamMaliyet { get; set; }

        public DateTime HesaplamaTarihi { get; set; }

        public string Aciklama { get; set; }
    }
}