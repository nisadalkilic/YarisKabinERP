using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;
using System.Collections.Generic;

namespace UretimERP.Controllers
{
    public class AramaController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index(string q)
        {
            var sonuclar = new List<AramaSonucViewModel>();

            if (string.IsNullOrWhiteSpace(q))
            {
                ViewBag.AramaMetni = "";
                return View(sonuclar);
            }

            q = q.Trim();
            ViewBag.AramaMetni = q;

            var urunler = db.Urunler
                .Where(u =>
                    u.UrunKodu.Contains(q) ||
                    u.UrunAdi.Contains(q))
                .Select(u => new AramaSonucViewModel
                {
                    ID = u.UrunID,
                    Tur = "Ürün",
                    Kod = u.UrunKodu,
                    Baslik = u.UrunAdi,
                    Aciklama = "Ürün kartı",
                    Controller = "Urunler"
                })
                .Take(10)
                .ToList();

            sonuclar.AddRange(urunler);

            return View(sonuclar);
        }
    }
}