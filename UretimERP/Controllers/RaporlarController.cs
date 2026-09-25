using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class RaporlarController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            ViewBag.ToplamUrun = db.Urunler.Count();

            ViewBag.ToplamSiparis = db.Siparisler.Count();

            ViewBag.ToplamUretimEmri = db.UretimEmirleri.Count();

            ViewBag.ToplamTedarikci = db.Tedarikciler.Count();
            var siparisDurumlari = db.Siparisler
    .GroupBy(s => s.Durum)
    .Select(g => new SiparisDurumRaporViewModel
    {
        Durum = g.Key,
        Adet = g.Count()
    })
    .OrderByDescending(x => x.Adet)
    .ToList();

            ViewBag.SiparisDurumlari = siparisDurumlari;
            var uretimDurumlari = db.UretimEmirleri
    .GroupBy(u => u.Durum)
    .Select(g => new UretimDurumRaporViewModel
    {
        Durum = g.Key,
        Adet = g.Count()
    })
    .OrderByDescending(x => x.Adet)
    .ToList();

            ViewBag.UretimDurumlari = uretimDurumlari;
            var kritikStoklar = db.Urunler
    .Where(u => u.MinimumStok > 0)
    .Select(u => new KritikStokRaporViewModel
    {
        UrunKodu = u.UrunKodu,
        UrunAdi = u.UrunAdi,
        MinimumStok = u.MinimumStok,

        KullanilabilirStok =
            db.Stoklar
                .Where(s => s.UrunID == u.UrunID)
                .Sum(s => (decimal?)(s.Miktar - s.RezerveMiktar))
            ?? 0,

        EksikMiktar =
            u.MinimumStok -
            (
                db.Stoklar
                    .Where(s => s.UrunID == u.UrunID)
                    .Sum(s => (decimal?)(s.Miktar - s.RezerveMiktar))
                ?? 0
            )
    })
    .Where(x => x.KullanilabilirStok <= x.MinimumStok)
    .OrderBy(x => x.KullanilabilirStok)
    .ToList();

            ViewBag.KritikStoklar = kritikStoklar;
            var satinAlmaDurumlari = db.SatinAlmaSiparisleri
    .GroupBy(s => s.Durum)
    .Select(g => new SatinAlmaDurumRaporViewModel
    {
        Durum = g.Key,
        Adet = g.Count()
    })
    .OrderByDescending(x => x.Adet)
    .ToList();

            ViewBag.SatinAlmaDurumlari = satinAlmaDurumlari;
            var finansOzeti = new FinansOzetViewModel
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

            ViewBag.FinansOzeti = finansOzeti;
            var depoStokDagilimi = db.Depolar
    .Where(d => d.AktifMi)
    .Select(d => new DepoStokRaporViewModel
    {
        DepoID = d.DepoID,
        DepoAdi = d.DepoAdi,

        StokKaydiSayisi = db.Stoklar
            .Count(s => s.DepoID == d.DepoID)
    })
    .OrderByDescending(d => d.StokKaydiSayisi)
    .ToList();

            ViewBag.DepoStokDagilimi = depoStokDagilimi;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}