using Octopink2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Octopink2.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ModelDBContext db = new ModelDBContext();


        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Price,Descrizione,Foto1,Foto2,Foto3,Foto4")] Product product, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)
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
                    product.Foto1 = "/Content/img/" + fileName;
                }
                else
                {
                    product.Foto1 = "/Content/img/default.jpg";
                }

                // Gestione della seconda foto (Foto2)
                if (file2 != null && file2.ContentLength > 0)
                {
                    var fileName2 = Path.GetFileName(file2.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/Content/img"), fileName2);
                    file2.SaveAs(path2);
                    product.Foto2 = "/Content/img/" + fileName2;
                }

                // Gestione della terza foto (Foto3)
                if (file3 != null && file3.ContentLength > 0)
                {
                    var fileName3 = Path.GetFileName(file3.FileName);
                    var path3 = Path.Combine(Server.MapPath("~/Content/img"), fileName3);
                    file3.SaveAs(path3);
                    product.Foto3 = "/Content/img/" + fileName3;
                }

                // Gestione della quarta foto (Foto4)
                if (file4 != null && file4.ContentLength > 0)
                {
                    var fileName4 = Path.GetFileName(file4.FileName);
                    var path4 = Path.Combine(Server.MapPath("~/Content/img"), fileName4);
                    file4.SaveAs(path4);
                    product.Foto4 = "/Content/img/" + fileName4;
                }

                if (ModelState.IsValid)
                {
                    db.Product.Add(product);
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
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ID, [Bind(Include = "ProductID,ProductName,Price,Descrizione,Foto1,Foto2,Foto3,Foto4")] Product product, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldProduct = db.Product.AsNoTracking().FirstOrDefault(p => p.ProductID == ID);

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

                        product.Foto1 = "/Content/img/" + fileName;
                    }
                    else
                    {
                        product.Foto1 = oldProduct.Foto1;
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

                        product.Foto2 = "/Content/img/" + fileName2;
                    }
                    else
                    {
                        product.Foto2 = oldProduct.Foto2;
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

                        product.Foto3 = "/Content/img/" + fileName3;
                    }
                    else
                    {
                        product.Foto3 = oldProduct.Foto3;
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

                        product.Foto4 = "/Content/img/" + fileName4;
                    }
                    else
                    {
                        product.Foto4 = oldProduct.Foto4;
                    }

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["EditMess"] = "Prodotto modificato correttamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return View(product);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
        public ActionResult AddToCart(int id, int quantita)
        {
            var prodotto = db.Product.Find(id);
            if (prodotto != null)
            {
                var cart = Session["cart"] as List<Product> ?? new List<Product>();
                prodotto.Quantita = quantita;
                if (cart.Any(p => p.ProductID == id))
                {
                    var product = cart.FirstOrDefault(p => p.ProductID == id);
                    product.Quantita += quantita;
                }
                else
                    cart.Add(prodotto);
                Session["cart"] = cart;
                TempData["AddCart"] = "Prodotto aggiunto al carrello";
            }
            return RedirectToAction("Index");
        }
    }
}
