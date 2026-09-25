using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class FinansController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            var model = new FinansOzetViewModel
            {
                ToplamAlacak = db.MusteriAlacaklari
                    .Sum(x => (decimal?)x.Tutar) ?? 0,

                ToplamTahsilat = db.MusteriTahsilatlari
                    .Sum(x => (decimal?)x.Tutar) ?? 0,

                ToplamTedarikciBorcu = db.TedarikciBorclari
                    .Sum(x => (decimal?)x.Tutar) ?? 0,

                ToplamTedarikciOdeme = db.TedarikciOdemeleri
                    .Sum(x => (decimal?)x.Tutar) ?? 0,

                ToplamGenelGider = db.GenelGiderler
                    .Sum(x => (decimal?)x.Tutar) ?? 0,

                ToplamUretimMaliyeti = db.UretimMaliyetleri
                    .Sum(x => (decimal?)(
                        x.MalzemeMaliyeti +
                        x.IscilikMaliyeti +
                        x.GenelUretimGideri +
                        x.DigerMaliyet
                    )) ?? 0
            };
            var alacaklar = db.MusteriAlacaklari
    .OrderByDescending(a => a.AlacakTarihi)
    .Select(a => new MusteriAlacakListeViewModel
    {
        AlacakID = a.AlacakID,
        MusteriAdi = a.Musteriler.FirmaAdi,
        BelgeNo = a.BelgeNo,
        AlacakTarihi = a.AlacakTarihi,
        VadeTarihi = a.VadeTarihi,
        Tutar = a.Tutar,
        ParaBirimi = a.ParaBirimi,
        Durum = a.Durum
    })
    .Take(10)
    .ToList();

            ViewBag.Alacaklar = alacaklar;
            var borclar = db.TedarikciBorclari
    .OrderByDescending(b => b.BorcTarihi)
    .Select(b => new TedarikciBorcListeViewModel
    {
        BorcID = b.BorcID,
        TedarikciAdi = b.Tedarikciler.FirmaAdi,
        BelgeNo = b.BelgeNo,
        BorcTarihi = b.BorcTarihi,
        VadeTarihi = b.VadeTarihi,
        Tutar = b.Tutar,
        ParaBirimi = b.ParaBirimi,
        Durum = b.Durum
    })
    .Take(10)
    .ToList();

            ViewBag.Borclar = borclar;
            var tahsilatlar = db.MusteriTahsilatlari
    .OrderByDescending(t => t.TahsilatTarihi)
    .Select(t => new MusteriTahsilatListeViewModel
    {
        TahsilatID = t.TahsilatID,
        MusteriAdi = t.Musteriler.FirmaAdi,
        TahsilatTarihi = t.TahsilatTarihi,
        Tutar = t.Tutar,
        ParaBirimi = t.ParaBirimi,
        OdemeYontemi = t.OdemeYontemi,
        Aciklama = t.Aciklama
    })
    .Take(10)
    .ToList();

            ViewBag.Tahsilatlar = tahsilatlar;
            var odemeler = db.TedarikciOdemeleri
    .OrderByDescending(o => o.OdemeTarihi)
    .Select(o => new TedarikciOdemeListeViewModel
    {
        OdemeID = o.OdemeID,
        TedarikciAdi = o.Tedarikciler.FirmaAdi,
        OdemeTarihi = o.OdemeTarihi,
        Tutar = o.Tutar,
        ParaBirimi = o.ParaBirimi,
        OdemeYontemi = o.OdemeYontemi,
        BelgeNo = o.BelgeNo,
        Aciklama = o.Aciklama
    })
    .Take(10)
    .ToList();

            ViewBag.Odemeler = odemeler;
            var genelGiderler = db.GenelGiderler
    .OrderByDescending(g => g.GiderTarihi)
    .Select(g => new GenelGiderListeViewModel
    {
        GiderID = g.GiderID,
        GiderTarihi = g.GiderTarihi,
        GiderTuru = g.GiderTuru,
        BelgeNo = g.BelgeNo,
        Tutar = g.Tutar,
        ParaBirimi = g.ParaBirimi,
        Aciklama = g.Aciklama
    })
    .Take(10)
    .ToList();

            ViewBag.GenelGiderler = genelGiderler;
            var uretimMaliyetleri = db.UretimMaliyetleri
    .OrderByDescending(u => u.HesaplamaTarihi)
    .Select(u => new UretimMaliyetListeViewModel
    {
        UretimMaliyetID = u.UretimMaliyetID,
        UretimEmriNo = u.UretimEmirleri.UretimEmriNo,

        MalzemeMaliyeti = u.MalzemeMaliyeti,
        IscilikMaliyeti = u.IscilikMaliyeti,
        GenelUretimGideri = u.GenelUretimGideri,
        DigerMaliyet = u.DigerMaliyet,

        ToplamMaliyet =
            u.MalzemeMaliyeti +
            u.IscilikMaliyeti +
            u.GenelUretimGideri +
            u.DigerMaliyet,

        HesaplamaTarihi = u.HesaplamaTarihi,
        Aciklama = u.Aciklama
    })
    .Take(10)
    .ToList();

            ViewBag.UretimMaliyetleri = uretimMaliyetleri;
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