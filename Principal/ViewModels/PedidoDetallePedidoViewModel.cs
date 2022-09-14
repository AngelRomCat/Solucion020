using Datos.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Principal.ViewModels
{
    public class PedidoDetallePedidoViewModel
    {
        public int OrderID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> shipperID { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual Naviera Naviera { get; set; }

        public virtual ICollection<DetallePedido> DetallePedido { get; set; }
    }
}