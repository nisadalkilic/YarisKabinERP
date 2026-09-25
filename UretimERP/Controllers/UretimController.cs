using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class UretimController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            var uretimEmirleri = db.UretimEmirleri
                .OrderByDescending(u => u.UretimEmriID)
                .Select(u => new UretimEmriListeViewModel
                {
                    UretimEmriID = u.UretimEmriID,
                    UretimEmriNo = u.UretimEmriNo,

                    UrunKodu = u.Urunler.UrunKodu,
                    UrunAdi = u.Urunler.UrunAdi,

                    PlanlananMiktar = u.PlanlananMiktar,

                    PlanlananBaslangicTarihi = u.PlanlananBaslangic,

                    Durum = u.Durum
                })
                .ToList();
            ViewBag.ToplamUretimEmri = uretimEmirleri.Count;

            ViewBag.Planlanan = uretimEmirleri
                .Count(x => x.Durum == "Planlandı");

            ViewBag.Uretimde = uretimEmirleri
                .Count(x => x.Durum == "Üretimde");

            ViewBag.Tamamlanan = uretimEmirleri
                .Count(x => x.Durum == "Tamamlandı");

            return View(uretimEmirleri);
        }
        public ActionResult Detay(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var uretimEmri = db.UretimEmirleri
                .FirstOrDefault(u => u.UretimEmriID == id.Value);

            if (uretimEmri == null)
            {
                return HttpNotFound();
            }

            ViewBag.UretimEmri = uretimEmri;

            var operasyonlar = db.UretimEmriOperasyonlari
                .Where(o => o.UretimEmriID == id.Value)
                .OrderBy(o => o.UretimEmriOperasyonID)
                .ToList();

            return View(operasyonlar);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}