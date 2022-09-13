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
    public class PedidoController : Controller
    {
        private NorthWindTuneadoDbContext db = new NorthWindTuneadoDbContext();

        // GET: Pedido
        public ActionResult Index(int? id, bool? cliente)
        {
            var pedidos = db.Pedido.Include(p => p.Cliente).Include(p => p.Empleado).Include(p => p.Naviera);
            if (id != null && id >0)
            {
                if (cliente != null)
                {
                    if (cliente == true)
                    {
                        pedidos = pedidos.Where(x => x.CustomerID == id);
                        ViewBag.Message = "Pedidos del Cliente: " + pedidos.FirstOrDefault().Cliente.CustomerName;
                    }
                    else
                    {
                        pedidos = pedidos.Where(x => x.EmployeeID == id);
                        ViewBag.Message = "Pedidos del Empleado: " + pedidos.FirstOrDefault().Empleado.FirstName + " " + pedidos.FirstOrDefault().Empleado.LastName;
                    }
                }
            }
            
            return View(pedidos.ToList());
        }

        // GET: Pedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedido/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Cliente, "CustomerID", "CustomerName");
            ViewBag.EmployeeID = new SelectList(db.Empleado, "EmployeeID", "LastName");
            ViewBag.shipperID = new SelectList(db.Naviera, "shipperID", "shipperName");
            return View();
        }

        // POST: Pedido/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustomerID,EmployeeID,OrderDate,shipperID")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedido.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Cliente, "CustomerID", "CustomerName", pedido.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Empleado, "EmployeeID", "LastName", pedido.EmployeeID);
            ViewBag.shipperID = new SelectList(db.Naviera, "shipperID", "shipperName", pedido.shipperID);
            return View(pedido);
        }

        // GET: Pedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Cliente, "CustomerID", "CustomerName", pedido.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Empleado, "EmployeeID", "LastName", pedido.EmployeeID);
            ViewBag.shipperID = new SelectList(db.Naviera, "shipperID", "shipperName", pedido.shipperID);
            return View(pedido);
        }

        // POST: Pedido/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,EmployeeID,OrderDate,shipperID")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Cliente, "CustomerID", "CustomerName", pedido.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Empleado, "EmployeeID", "LastName", pedido.EmployeeID);
            ViewBag.shipperID = new SelectList(db.Naviera, "shipperID", "shipperName", pedido.shipperID);
            return View(pedido);
        }

        // GET: Pedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedido.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedido.Find(id);
            db.Pedido.Remove(pedido);
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
