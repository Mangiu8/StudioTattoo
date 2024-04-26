using Octopink2.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Octopink2.Controllers
{
    public class ArtistController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        public ActionResult Index()
        {
            return View(db.Artist.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artist.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistID,Nome,Aka,Descrizione,InstaLink,Foto1,Foto2,Foto3,Foto4,Foto5,Foto6,Foto7,Foto8,Foto9,Foto10")] Artist artist,
            HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, HttpPostedFileBase file5)
        {
            try
            {
                // Gestione della prima foto (Foto1)
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/Content/img")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/img"));
                    }
                    file.SaveAs(path);
                    artist.Foto1 = "/Content/img/" + fileName;
                }
                else
                {
                    artist.Foto1 = "/Content/img/default.jpg";
                }

                // Gestione della seconda foto (Foto2)
                if (file2 != null && file2.ContentLength > 0)
                {
                    var fileName2 = Path.GetFileName(file2.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/Content/img"), fileName2);
                    file2.SaveAs(path2);
                    artist.Foto2 = "/Content/img/" + fileName2;
                }

                // Gestione della terza foto (Foto3)
                if (file3 != null && file3.ContentLength > 0)
                {
                    var fileName3 = Path.GetFileName(file3.FileName);
                    var path3 = Path.Combine(Server.MapPath("~/Content/img"), fileName3);
                    file3.SaveAs(path3);
                    artist.Foto3 = "/Content/img/" + fileName3;
                }

                // Gestione della quarta foto (Foto4)
                if (file4 != null && file4.ContentLength > 0)
                {
                    var fileName4 = Path.GetFileName(file4.FileName);
                    var path4 = Path.Combine(Server.MapPath("~/Content/img"), fileName4);
                    file4.SaveAs(path4);
                    artist.Foto4 = "/Content/img/" + fileName4;
                }

                // Gestione della quinta foto (Foto5)
                if (file5 != null && file5.ContentLength > 0)
                {
                    var fileName5 = Path.GetFileName(file5.FileName);
                    var path5 = Path.Combine(Server.MapPath("~/Content/img"), fileName5);
                    file5.SaveAs(path5);
                    artist.Foto5 = "/Content/img/" + fileName5;
                }

                if (ModelState.IsValid)
                {
                    db.Artist.Add(artist);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Errore nella creazione del prodotto");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return View(artist);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artist.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID, [Bind(Include = "ArtistID,Nome,Aka,Descrizione,InstaLink,Foto1,Foto2,Foto3,Foto4,Foto5,Foto6,Foto7,Foto8,Foto9,Foto10")] Artist artist,
             HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, HttpPostedFileBase file5)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldProduct = db.Artist.AsNoTracking().FirstOrDefault(p => p.ArtistID == ID);

                    // Gestione della prima foto (Foto)
                    if (file != null && file.ContentLength > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(oldProduct.Foto1))
                        {
                            var existingImagePath = Path.Combine(Server.MapPath("~/Content/img/"), oldProduct.Foto1);
                            if (System.IO.File.Exists(existingImagePath))
                            {
                                System.IO.File.Delete(existingImagePath);
                            }
                        }

                        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                        file.SaveAs(path);

                        artist.Foto1 = "/Content/img/" + fileName;
                    }
                    else
                    {
                        artist.Foto1 = oldProduct.Foto1;
                    }

                    // Gestione della seconda foto (Foto2)
                    if (file2 != null && file2.ContentLength > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(oldProduct.Foto2))
                        {
                            var existingImagePath = Path.Combine(Server.MapPath("~/Content/img/"), oldProduct.Foto2);
                            if (System.IO.File.Exists(existingImagePath))
                            {
                                System.IO.File.Delete(existingImagePath);
                            }
                        }

                        var fileName2 = Path.GetFileNameWithoutExtension(file2.FileName) + DateTime.Now.Ticks + Path.GetExtension(file2.FileName);
                        var path2 = Path.Combine(Server.MapPath("~/Content/img/"), fileName2);
                        file2.SaveAs(path2);

                        artist.Foto2 = "/Content/img/" + fileName2;
                    }
                    else
                    {
                        artist.Foto2 = oldProduct.Foto2;
                    }

                    // Gestione della terza foto (Foto3)
                    if (file3 != null && file3.ContentLength > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(oldProduct.Foto3))
                        {
                            var existingImagePath = Path.Combine(Server.MapPath("~/Content/img/"), oldProduct.Foto3);
                            if (System.IO.File.Exists(existingImagePath))
                            {
                                System.IO.File.Delete(existingImagePath);
                            }
                        }

                        var fileName3 = Path.GetFileNameWithoutExtension(file3.FileName) + DateTime.Now.Ticks + Path.GetExtension(file3.FileName);
                        var path3 = Path.Combine(Server.MapPath("~/Content/img/"), fileName3);
                        file3.SaveAs(path3);

                        artist.Foto3 = "/Content/img/" + fileName3;
                    }
                    else
                    {
                        artist.Foto3 = oldProduct.Foto3;
                    }

                    // Gestione della quarta foto (Foto4)
                    if (file4 != null && file4.ContentLength > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(oldProduct.Foto4))
                        {
                            var existingImagePath = Path.Combine(Server.MapPath("~/Content/img/"), oldProduct.Foto4);
                            if (System.IO.File.Exists(existingImagePath))
                            {
                                System.IO.File.Delete(existingImagePath);
                            }
                        }

                        var fileName4 = Path.GetFileNameWithoutExtension(file4.FileName) + DateTime.Now.Ticks + Path.GetExtension(file4.FileName);
                        var path4 = Path.Combine(Server.MapPath("~/Content/img/"), fileName4);
                        file4.SaveAs(path4);

                        artist.Foto4 = "/Content/img/" + fileName4;
                    }
                    else
                    {
                        artist.Foto4 = oldProduct.Foto4;
                    }

                    if (file5 != null && file5.ContentLength > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(oldProduct.Foto5))
                        {
                            var existingImagePath = Path.Combine(Server.MapPath("~/Content/img/"), oldProduct.Foto5);
                            if (System.IO.File.Exists(existingImagePath))
                            {
                                System.IO.File.Delete(existingImagePath);
                            }
                        }

                        var fileName5 = Path.GetFileNameWithoutExtension(file5.FileName) + DateTime.Now.Ticks + Path.GetExtension(file5.FileName);
                        var path5 = Path.Combine(Server.MapPath("~/Content/img/"), fileName5);
                        file5.SaveAs(path5);

                        artist.Foto5 = "/Content/img/" + fileName5;
                    }
                    else
                    {
                        artist.Foto5 = oldProduct.Foto5;
                    }

                    db.Entry(artist).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["EditMess"] = "Prodotto modificato correttamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return View(artist);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artist.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artist.Find(id);
            db.Artist.Remove(artist);
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
