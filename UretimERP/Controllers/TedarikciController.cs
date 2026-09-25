using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class TedarikciController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            var tedarikciler = db.Tedarikciler
                .OrderBy(t => t.FirmaAdi)
                .Select(t => new TedarikciListeViewModel
                {
                    TedarikciID = t.TedarikciID,
                    CariKod = t.CariKod,
                    FirmaAdi = t.FirmaAdi,
                    YetkiliAdi = t.YetkiliAdi,
                    Telefon = t.Telefon,
                    Email = t.Email,
                    AktifMi = t.AktifMi
                })
                .ToList();

            return View(tedarikciler);
        }
        public ActionResult Detay(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var tedarikci = db.Tedarikciler
                .FirstOrDefault(t => t.TedarikciID == id.Value);

            if (tedarikci == null)
            {
                return HttpNotFound();
            }

            var model = new TedarikciDetayViewModel
            {
                TedarikciID = tedarikci.TedarikciID,
                CariKod = tedarikci.CariKod,
                FirmaAdi = tedarikci.FirmaAdi,
                YetkiliAdi = tedarikci.YetkiliAdi,
                Telefon = tedarikci.Telefon,
                Email = tedarikci.Email,
                Adres = tedarikci.Adres,
                AktifMi = tedarikci.AktifMi,

                SatinAlmaSayisi = db.SatinAlmaSiparisleri
                    .Count(s => s.TedarikciID == id.Value),

                ToplamBorc = db.TedarikciBorclari
                    .Where(b => b.TedarikciID == id.Value)
                    .Select(b => (decimal?)b.Tutar)
                    .Sum() ?? 0,

                ToplamOdeme = db.TedarikciOdemeleri
                    .Where(o => o.TedarikciID == id.Value)
                    .Select(o => (decimal?)o.Tutar)
                    .Sum() ?? 0,
                SatinAlmalar = db.SatinAlmaSiparisleri
    .Where(s => s.TedarikciID == id.Value)
    .OrderByDescending(s => s.SiparisTarihi)
    .Select(s => new SatinAlmaListeViewModel
    {
        SatinAlmaSiparisID = s.SatinAlmaSiparisID,
        SiparisNo = s.SatinAlmaSiparisNo,
        TedarikciAdi = s.Tedarikciler.FirmaAdi,
        SiparisTarihi = s.SiparisTarihi,
        TerminTarihi = s.BeklenenTeslimTarihi,
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