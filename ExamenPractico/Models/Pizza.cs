using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenPractico.Models
{
    public class Pizza
    {
        public string Tamanio { get; set; }
        public string Ingredientes { get;set; }
        public int NumeroPizza { get; set; }
        public double Subtotal { get; set; }
        public double Result { get; set; }
        public double PrecioChica { get; set; }
        public double PrecioMediana { get; set; }
        public double PrecioGrande { get; set; }
        public double PrecioIngrediente { get; set; }
        public double Total { get; set; }
        public List<string> TodosIngrediente { get; set; }
        public string Pina { get; set; }
        public string Jamon { get; set; }
        public string Champinion { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
     


    }

    public class Persona
    {
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
    }
}