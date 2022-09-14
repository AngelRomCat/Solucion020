using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Datos.Datos;

namespace Principal.Controllers
{
    public class DetallePedidoController : Controller
    {
        private NorthWindTuneadoDbContext db = new NorthWindTuneadoDbContext();

        // GET: DetallePedido
        public ActionResult Index(int? id, bool? pedido)
        {
            var detallePedidos = db.DetallePedido.Include(d => d.Pedido).Include(d => d.Producto);
            if (id != null && id > 0 && pedido != null)
            {
                if (pedido == true)
                {
                    detallePedidos = detallePedidos.Where(x => x.OrderID == id);
                    if (detallePedidos != null && detallePedidos.Count() > 0)
                    {
                        ViewBag.Message = "Detalles del pedido con fecha: " + detallePedidos.FirstOrDefault().Pedido.OrderDate.Value.ToString("dd/MM/yyyy");
                    }
                }
                else
                {
                    detallePedidos = detallePedidos.Where(x => x.ProductID == id);

                    if (detallePedidos != null && detallePedidos.Count() > 0)
                    {
                        ViewBag.Message = "Detalles del pedido en los que aparece el producto: " + detallePedidos.FirstOrDefault().Producto.ProductName;
                    }
                }
            }     
            
            return View(detallePedidos.ToList());
        }

        // GET: DetallePedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // GET: DetallePedido/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Pedido, "OrderID", "OrderID");
            ViewBag.ProductID = new SelectList(db.Producto, "ProductID", "ProductName");
            return View();
        }

        // POST: DetallePedido/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetailID,OrderID,ProductID,Quantity")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.DetallePedido.Add(detallePedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Pedido, "OrderID", "OrderID", detallePedido.OrderID);
            ViewBag.ProductID = new SelectList(db.Producto, "ProductID", "ProductName", detallePedido.ProductID);
            return View(detallePedido);
        }

        // GET: DetallePedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Pedido, "OrderID", "OrderID", detallePedido.OrderID);
            ViewBag.ProductID = new SelectList(db.Producto, "ProductID", "ProductName", detallePedido.ProductID);
            return View(detallePedido);
        }

        // POST: DetallePedido/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailID,OrderID,ProductID,Quantity")] DetallePedido detallePedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallePedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Pedido, "OrderID", "OrderID", detallePedido.OrderID);
            ViewBag.ProductID = new SelectList(db.Producto, "ProductID", "ProductName", detallePedido.ProductID);
            return View(detallePedido);
        }

        // GET: DetallePedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            if (detallePedido == null)
            {
                return HttpNotFound();
            }
            return View(detallePedido);
        }

        // POST: DetallePedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetallePedido detallePedido = db.DetallePedido.Find(id);
            db.DetallePedido.Remove(detallePedido);
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
