using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class SatinAlmaController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {

            var satinAlmalar = db.SatinAlmaSiparisleri
                .OrderByDescending(s => s.SatinAlmaSiparisID)
                .Select(s => new SatinAlmaListeViewModel
                {
                    SatinAlmaSiparisID = s.SatinAlmaSiparisID,

                    SiparisNo = s.SatinAlmaSiparisNo,

                    TedarikciAdi = s.Tedarikciler.FirmaAdi,

                    SiparisTarihi = s.SiparisTarihi,

                    TerminTarihi = s.BeklenenTeslimTarihi,

                    Durum = s.Durum
                })
                .ToList();
            ViewBag.ToplamSatinAlma = satinAlmalar.Count;

            ViewBag.Bekleyen = satinAlmalar.Count(x => x.Durum == "Taslak");

            ViewBag.Tamamlanan = satinAlmalar
                .Count(x => x.Durum == "Tamamlandı");

            ViewBag.AcikSatinAlma = satinAlmalar
                .Count(x => x.Durum != "Tamamlandı" &&
                            x.Durum != "İptal");

            return View(satinAlmalar);
        }
        public ActionResult Detay(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var satinAlma = db.SatinAlmaSiparisleri
                .FirstOrDefault(s => s.SatinAlmaSiparisID == id.Value);

            if (satinAlma == null)
            {
                return HttpNotFound();
            }

            ViewBag.SatinAlma = satinAlma;

            var detaylar = db.SatinAlmaDetaylari
                .Where(d => d.SatinAlmaSiparisID == id.Value)
                .ToList();

            return View(detaylar);
        }
    }
}