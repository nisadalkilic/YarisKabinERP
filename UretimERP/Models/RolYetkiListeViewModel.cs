using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UretimERP.Models
{
    public class RolYetkiListeViewModel
    {
        public int RolID { get; set; }

        public string RolAdi { get; set; }

        public string Aciklama { get; set; }

        public bool AktifMi { get; set; }

        public string Yetkiler { get; set; }
    }
}