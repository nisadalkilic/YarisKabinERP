using System;
using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class HomeController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            ViewBag.ToplamSiparis = db.Siparisler.Count();

            ViewBag.UretimdekiEmir = db.UretimEmirleri
                .Count(x => x.Durum == "Üretimde");

            ViewBag.AcikSatinAlma = db.SatinAlmaSiparisleri
                .Count(x => x.Durum != "Tamamlandı" &&
                            x.Durum != "İptal");

            ViewBag.KritikStok = db.Urunler
                .Count(u =>
                    u.MinimumStok > 0 &&
                    (
                        db.Stoklar
                            .Where(s => s.UrunID == u.UrunID)
                            .Sum(s => (decimal?)(s.Miktar - s.RezerveMiktar))
                        ?? 0
                    ) <= u.MinimumStok
                );

            ViewBag.SonSiparisler = db.Siparisler
                .OrderByDescending(x => x.SiparisTarihi)
                .Take(5)
                .ToList();

            var kritikStoklar = db.Urunler
     .Where(u => u.MinimumStok > 0)
     .Select(u => new KritikStokViewModel
     {
         UrunKodu = u.UrunKodu,
         UrunAdi = u.UrunAdi,
         MinimumStok = u.MinimumStok,

         MevcutStok =
             db.Stoklar
                 .Where(s => s.UrunID == u.UrunID)
                 .Sum(s => (decimal?)(s.Miktar - s.RezerveMiktar))
             ?? 0
     })
     .Where(x => x.MevcutStok <= x.MinimumStok)
     .OrderBy(x => x.MevcutStok)
     .Take(5)
     .ToList();

            ViewBag.KritikStoklar = kritikStoklar;
            var uretimDurumlari = db.UretimEmirleri
    .GroupBy(x => x.Durum)
    .Select(g => new UretimDurumViewModel
    {
        Durum = g.Key,
        Adet = g.Count()
    })
    .ToList();

            ViewBag.UretimDurumlari = uretimDurumlari;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}