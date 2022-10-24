using ExamenPractico.Models;
using ExamenPractico.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenPractico.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pedidos()
        {
      
         
            return View();
        }

        [HttpPost]
        public ActionResult Pedidos(Pizza p)
        {
            
            var ope = new ServicePizza();
            p.TodosIngrediente = new List<string>();
            ope.GuardarArchivoPedido(p);
            var tabla = ope.LeerArchivoPedidos();
            ViewBag.tablaPedidos = tabla;
         

            return View(p);
        }


        public ActionResult DetallePedido()
        {
            var arch = new ServicePizza();
            var p = new Persona();
            
            ViewBag.Nombre = p.Nombre;
            ViewBag.Fecha = p.Fecha;
            ViewBag.Total = arch.TotalPrecioDetalle();
            ViewBag.temp = arch.LeerArchivoDetallesPedidos();
            return View();

        }

     

        public ActionResult Clientes()
        {
            var regis = new ServicePizza();
            ViewBag.tablaCliente = regis.LeerArchivoPersonas();
            ViewBag.Totalidad = regis.TotalPedidosClientes();
            return View();
        }

        [HttpPost]
        public ActionResult Clientes(String per)
        {
            var regis = new ServicePizza();
            bool s = false;
            if (per.Equals("lunes") || per.Equals("martes") || per.Equals("miércoles") || per.Equals("jueves") || per.Equals("viernes") || per.Equals("sábado") || per.Equals("domingo"))
            {
                s = true;
                ViewBag.tablaCliente = regis.DiasSemana(per, s);
                ViewBag.Totalidad = regis.TodoElFiltrado(per, s);
            }
            else if (per.Equals("enero") || per.Equals("febrero") || per.Equals("marzo") || per.Equals("abril") || per.Equals("mayo") || per.Equals("junio") || per.Equals("julio") || per.Equals("agosto") || per.Equals("septiembre") || per.Equals("octubre") || per.Equals("noviembre") || per.Equals("diciembre"))
            {
                ViewBag.tablaCliente = regis.DiasSemana(per, s);
                ViewBag.Totalidad = regis.TodoElFiltrado(per, s);
            }
            else
            {
                ViewBag.tablaCliente = null;
                ViewBag.Totalidad = 0;
            }
            return View();
            
        }

        public ActionResult Mensaje(Persona per)
        {
            var obj = new ServicePizza();
            obj.GuardarArchivoPersona(per);
            ViewBag.or = obj.BorrarPedidos(obj.LeerArchivoPedidos());
            return View();
        }

        public ActionResult MensajeImpreso()
        {
            var obj = new ServicePizza();
            obj.BorrarDetallesPedidos();

            return View();


        }


        public ActionResult Filtrado()
        {

            return View();
        }

     
    }
}
