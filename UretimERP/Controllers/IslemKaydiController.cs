using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class IslemKaydiController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            var islemKayitlari = db.IslemKayitlari
                .OrderByDescending(i => i.IslemTarihi)
                .Select(i => new IslemKaydiListeViewModel
                {
                    IslemKayitID = i.IslemKayitID,
                    KullaniciAdi = i.Kullanicilar.KullaniciAdi,
                    IslemTuru = i.IslemTuru,
                    TabloAdi = i.TabloAdi,
                    KayitID = i.KayitID,
                    IslemTarihi = i.IslemTarihi,
                    Aciklama = i.Aciklama
                })
                .ToList();

            ViewBag.ToplamIslem = islemKayitlari.Count;

            return View(islemKayitlari);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
