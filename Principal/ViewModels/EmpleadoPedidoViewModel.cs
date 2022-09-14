using Datos.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Principal.ViewModels
{
    public class EmpleadoPedidoViewModel
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<System.DateTime> birthDate { get; set; }
        public string Photo { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}