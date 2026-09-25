using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class SiparisController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            var siparisler = db.Siparisler
                .OrderByDescending(s => s.SiparisTarihi)
                .Select(s => new SiparisListeViewModel
                {
                    SiparisID = s.SiparisID,
                    SiparisNo = s.SiparisNo,

                    MusteriAdi = s.Musteriler.FirmaAdi,

                    SiparisTarihi = s.SiparisTarihi,

                    TerminTarihi = s.TerminTarihi,

                    Durum = s.Durum
                })
                .ToList();
            ViewBag.ToplamSiparis = siparisler.Count;

            ViewBag.BekleyenSiparis = siparisler
                .Count(x => x.Durum == "Bekliyor");

            ViewBag.UretimdeSiparis = siparisler
                .Count(x => x.Durum == "Üretimde");

            ViewBag.TamamlananSiparis = siparisler
                .Count(x => x.Durum == "Tamamlandı");
            return View(siparisler);
        }
        public ActionResult Detay(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var siparis = db.Siparisler
                .FirstOrDefault(s => s.SiparisID == id.Value);

            if (siparis == null)
            {
                return HttpNotFound();
            }

            ViewBag.Siparis = siparis;

            var detaylar = db.SiparisDetaylari
                .Where(d => d.SiparisID == id.Value)
                .ToList();
            ViewBag.ToplamTutar = detaylar
    .Sum(d => d.Miktar * d.BirimFiyat);
            return View(detaylar);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}