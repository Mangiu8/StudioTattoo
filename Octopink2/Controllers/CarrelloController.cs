using Octopink2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Octopink2.Controllers
{
    public class CarrelloController : Controller
    {
        public ActionResult Index()
        {
            var cart = Session["cart"] as List<Product>;
            if (cart == null || !cart.Any())
            {
                TempData["CartEmpty"] = "Il carrello è vuoto";
                return RedirectToAction("Index", "Products");
            }
            return View(cart);
        }

        // Rimuove un prodotto dal carrello
        // Se la quantità del prodotto è maggiore di 1, decrementa la quantità
        public ActionResult Delete(int? id)
        {
            var cart = Session["cart"] as List<Product>;
            if (cart != null)
            {
                var productToRemove = cart.FirstOrDefault(p => p.ProductID == id);
                if (productToRemove != null)
                {
                    if (productToRemove.Quantita > 1)
                    {
                        productToRemove.Quantita--;
                    }
                    else
                    {
                        cart.Remove(productToRemove);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        // Svuota il carrello
        public ActionResult CartClear()
        {
            var cart = Session["cart"] as List<Product>;
            if (cart != null)
            {
                cart.Clear();
            }
            TempData["ClearMess"] = "Il carrello è stato svuotato";
            return RedirectToAction("Index", "Products");
        }
        [HttpPost]
        public ActionResult Ordina(string note, string indirizzo)
        {
            ModelDBContext db = new ModelDBContext();
            var userId = db.User.FirstOrDefault(u => u.Email == User.Identity.Name).UserID;

            var cart = Session["cart"] as List<Product>;
            if (cart != null && cart.Any())
            {
                Orders newOrder = new Orders();
                newOrder.Date = DateTime.Now;
                newOrder.Shipped = false;
                newOrder.UserID_FK = userId;
                newOrder.Address = indirizzo;
                newOrder.Total = cart.Sum(p => p.Price);
                newOrder.Note = note;

                db.Orders.Add(newOrder);
                db.SaveChanges();

                foreach (var product in cart)
                {
                    Details newDetail = new Details();
                    newDetail.OrderID_FK = newOrder.OrderID;
                    newDetail.ProductID_FK = product.ProductID;
                    newDetail.Quantity = Convert.ToInt32(product.Quantita);
                    db.Details.Add(newDetail);
                    db.SaveChanges();
                }
                cart.Clear();
            }
            TempData["Ordine"] = "L'ordine è stato inviato correttamente";
            return RedirectToAction("Index", "Products");
        }
    }
}