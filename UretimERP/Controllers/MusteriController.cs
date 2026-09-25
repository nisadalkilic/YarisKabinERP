using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class MusteriController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            var musteriler = db.Musteriler
                .OrderBy(m => m.FirmaAdi)
                .Select(m => new MusteriListeViewModel
                {
                    MusteriID = m.MusteriID,
                    CariKod = m.CariKod,
                    FirmaAdi = m.FirmaAdi,
                    YetkiliAdi = m.YetkiliAdi,
                    Telefon = m.Telefon,
                    Email = m.Email,
                    AktifMi = m.AktifMi
                })
                .ToList();

            return View(musteriler);
        }
        public ActionResult Detay(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var musteri = db.Musteriler
                .FirstOrDefault(m => m.MusteriID == id.Value);

            if (musteri == null)
            {
                return HttpNotFound();
            }

            var model = new MusteriDetayViewModel
            {
                MusteriID = musteri.MusteriID,
                CariKod = musteri.CariKod,
                FirmaAdi = musteri.FirmaAdi,
                YetkiliAdi = musteri.YetkiliAdi,
                Telefon = musteri.Telefon,
                Email = musteri.Email,
                Adres = musteri.Adres,
                AktifMi = musteri.AktifMi,

                SiparisSayisi = db.Siparisler
                    .Count(s => s.MusteriID == id.Value),

                ToplamAlacak = db.MusteriAlacaklari
                    .Where(a => a.MusteriID == id.Value)
                    .Select(a => (decimal?)a.Tutar)
                    .Sum() ?? 0,

                ToplamTahsilat = db.MusteriTahsilatlari
                    .Where(t => t.MusteriID == id.Value)
                    .Select(t => (decimal?)t.Tutar)
                    .Sum() ?? 0,

                Siparisler = db.Siparisler
    .Where(s => s.MusteriID == id.Value)
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
    .ToList()
            };

            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}