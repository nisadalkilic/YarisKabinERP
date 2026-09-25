using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UretimERP.Models;

namespace UretimERP.Controllers
{
    public class UrunlerController : Controller
    {
        private YarisKabinERPEntities db = new YarisKabinERPEntities();

        public ActionResult Index(string ara)
        {
            var urunler = db.Urunler
                .Include(u => u.Birimler)
                .Include(u => u.Kategoriler)
                .Include(u => u.UrunTipleri)
                .AsQueryable();

            ViewBag.ToplamUrun = urunler.Count();

            ViewBag.AktifUrun = urunler.Count(u => u.AktifMi);

            ViewBag.PasifUrun = urunler.Count(u => !u.AktifMi);

            if (!string.IsNullOrWhiteSpace(ara))
            {
                urunler = urunler.Where(u =>
                    u.UrunKodu.Contains(ara) ||
                    u.UrunAdi.Contains(ara));
            }

            ViewBag.Ara = ara;
            ViewBag.BulunanUrun = urunler.Count();

            return View(
                urunler
                    .OrderBy(u => u.UrunKodu)
                    .ToList()
            );
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

    
        public ActionResult Create()
        {
            ViewBag.BirimID = new SelectList(db.Birimler, "BirimID", "BirimAdi");
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi");
            ViewBag.UrunTipiID = new SelectList(db.UrunTipleri, "UrunTipiID", "TipAdi");
            return View();
        }

       
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunID,UrunKodu,UrunAdi,KategoriID,UrunTipiID,BirimID,Aciklama,MinimumStok,MaksimumStok,AktifMi")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urunler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BirimID = new SelectList(db.Birimler, "BirimID", "BirimAdi", urunler.BirimID);
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", urunler.KategoriID);
            ViewBag.UrunTipiID = new SelectList(db.UrunTipleri, "UrunTipiID", "TipAdi", urunler.UrunTipiID);
            return View(urunler);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirimID = new SelectList(db.Birimler, "BirimID", "BirimAdi", urunler.BirimID);
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", urunler.KategoriID);
            ViewBag.UrunTipiID = new SelectList(db.UrunTipleri, "UrunTipiID", "TipAdi", urunler.UrunTipiID);
            return View(urunler);
        }

       
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,UrunKodu,UrunAdi,KategoriID,UrunTipiID,BirimID,Aciklama,MinimumStok,MaksimumStok,AktifMi")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirimID = new SelectList(db.Birimler, "BirimID", "BirimAdi", urunler.BirimID);
            ViewBag.KategoriID = new SelectList(db.Kategoriler, "KategoriID", "KategoriAdi", urunler.KategoriID);
            ViewBag.UrunTipiID = new SelectList(db.UrunTipleri, "UrunTipiID", "TipAdi", urunler.UrunTipiID);
            return View(urunler);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
