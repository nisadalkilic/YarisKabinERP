using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class StokController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index(string ara, int? depoId, string durum)
        {
            var stokListesi = db.Stoklar
                .Select(s => new StokListeViewModel
                {
                    StokID = s.StokID,

                    DepoID = s.DepoID,

                    UrunKodu = s.Urunler.UrunKodu,

                    UrunAdi = s.Urunler.UrunAdi,

                    DepoAdi = s.Depolar.DepoAdi,

                    FizikselStok = s.Miktar,

                    RezerveStok = s.RezerveMiktar,

                    KullanilabilirStok = s.Miktar - s.RezerveMiktar,

                    MinimumStok = s.Urunler.MinimumStok
                })
                .ToList();
    

            ViewBag.ToplamStokKaydi = stokListesi.Count;

            ViewBag.KritikStokSayisi = stokListesi
                .Count(x => x.KullanilabilirStok <= x.MinimumStok);

            ViewBag.NormalStokSayisi = stokListesi
                .Count(x => x.KullanilabilirStok > x.MinimumStok);

            if (!string.IsNullOrWhiteSpace(ara))
            {
                stokListesi = stokListesi
                    .Where(x =>
                        x.UrunKodu.Contains(ara) ||
                        x.UrunAdi.Contains(ara) ||
                        x.DepoAdi.Contains(ara))
                    .ToList();
            }
            if (depoId.HasValue)
            {
                stokListesi = stokListesi
                    .Where(x => x.DepoID == depoId.Value)
                    .ToList();
            }
            ViewBag.Ara = ara;
            ViewBag.Depolar = db.Depolar
    .Where(d => d.AktifMi)
    .OrderBy(d => d.DepoAdi)
    .ToList();
            ViewBag.SeciliDurum = durum;
            ViewBag.SeciliDepoId = depoId;
            ViewBag.BulunanKayit = stokListesi.Count;

            if (durum == "kritik")
            {
                stokListesi = stokListesi
                    .Where(x => x.KullanilabilirStok <= x.MinimumStok)
                    .ToList();
            }
            else if (durum == "normal")
            {
                stokListesi = stokListesi
                    .Where(x => x.KullanilabilirStok > x.MinimumStok)
                    .ToList();
            }
            return View(stokListesi);
        }
        public ActionResult Detay(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            var stok = db.Stoklar
    .FirstOrDefault(s => s.StokID == id.Value);
            if (stok == null)
            {
                return HttpNotFound();
            }

            ViewBag.Stok = stok;

            var hareketler = db.StokHareketleri
                .Where(h =>
                    h.UrunID == stok.UrunID &&
                    h.DepoID == stok.DepoID)
                .OrderByDescending(h => h.Tarih)
                .ToList();

            return View(hareketler);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}