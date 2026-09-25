using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class BildirimController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            var bildirimler = db.Bildirimler
                .OrderByDescending(b => b.OlusturmaTarihi)
                .Select(b => new BildirimListeViewModel
                {
                    BildirimID = b.BildirimID,
                    KullaniciAdi = b.Kullanicilar.KullaniciAdi,
                    Baslik = b.Baslik,
                    Mesaj = b.Mesaj,
                    OkunduMu = b.OkunduMu,
                    OkunmaTarihi = b.OkunmaTarihi,
                    OlusturmaTarihi = b.OlusturmaTarihi,
                    OnemSeviyesi = b.OnemSeviyesi,
                    ReferansTuru = b.ReferansTuru,
                    ReferansID = b.ReferansID
                })
                .ToList();

            ViewBag.ToplamBildirim = bildirimler.Count;
            ViewBag.OkunmamisBildirim = bildirimler.Count(b => !b.OkunduMu);

            return View(bildirimler);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OkunduYap(int id)
        {
            var bildirim = db.Bildirimler
                .FirstOrDefault(b => b.BildirimID == id);

            if (bildirim == null)
            {
                return HttpNotFound();
            }

            bildirim.OkunduMu = true;
            bildirim.OkunmaTarihi = System.DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}