using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class KullaniciController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {

                                                            var kullanicilar = db.Kullanicilar
                .OrderBy(k => k.AdSoyad)
                .ToList()
                .Select(k => new KullaniciListeViewModel
                {
                    KullaniciID = k.KullaniciID,
                    KullaniciAdi = k.KullaniciAdi,
                    AdSoyad = k.AdSoyad,
                    Email = k.Email,

                    Roller = string.Join(", ",
                        k.KullaniciRolleri
                            .Where(kr => kr.Roller.AktifMi)
                            .Select(kr => kr.Roller.RolAdi)),

                    AktifMi = k.AktifMi,
                    OlusturmaTarihi = k.OlusturmaTarihi,
                    SonGirisTarihi = k.SonGirisTarihi
                })
                .ToList();

            return View(kullanicilar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}