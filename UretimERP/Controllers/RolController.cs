using System.Linq;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class RolController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index()
        {
            var roller = db.Roller
                .OrderBy(r => r.RolAdi)
                .ToList()
                .Select(r => new RolYetkiListeViewModel
                {
                    RolID = r.RolID,
                    RolAdi = r.RolAdi,
                    Aciklama = r.Aciklama,
                    AktifMi = r.AktifMi,

                    Yetkiler = string.Join(", ",
                        r.RolYetkileri
                            .Where(ry => ry.Yetkiler.AktifMi)
                            .Select(ry => ry.Yetkiler.YetkiKodu))
                })
                .ToList();

            return View(roller);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}