using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ExamenPractico.Models;
using System.Configuration;
using System.Globalization;

namespace ExamenPractico.Service
{
    public class ServicePizza
    {
        public void GuardarArchivoPedido(Pizza p)
        {
            var tamanio = p.Tamanio;
            var ingrediente = p.Ingredientes;
            int numPizza = p.NumeroPizza;
            double sub = p.Subtotal;
            var pina = p.Pina;
            var jamon = p.Jamon;
            var champinio = p.Champinion;

            p.PrecioChica = 40;
            double precioC = p.PrecioChica;
            p.PrecioMediana = 80;
            double precioM = p.PrecioMediana;
            p.PrecioGrande = 120;
            double precioG = p.PrecioGrande;

            p.PrecioIngrediente = 10;
            double preciIngre = p.PrecioIngrediente;


            var lista = p.TodosIngrediente;

            if (!String.IsNullOrEmpty(pina))
            {
                lista.Add(pina);
            }
            if (!String.IsNullOrEmpty(jamon))
            {
                lista.Add(jamon);
            }
            if (!String.IsNullOrEmpty(champinio))
            {
                lista.Add(champinio);
            }
  
            

            if (tamanio == "Chica")
            {

                sub = (precioC + (lista.Count * preciIngre)) * (numPizza);
            }
            else if (tamanio == "Mediana")
            {
                sub = (precioM +(lista.Count * preciIngre) ) * (numPizza);
            }
            else
            {
                sub = (precioG + (lista.Count * preciIngre)) * (numPizza);
            }

            string ingredientesLista = string.Join("-", lista);
            var res = tamanio + "," + ingredientesLista + "," + numPizza + "," + sub + Environment.NewLine;
            var archivoPedido = HttpContext.Current.Server.MapPath("~/App_Data/pedidos.txt");
            File.AppendAllText(@archivoPedido, res);
        }


        public Array LeerArchivoPedidos()
        {
            Array userData = null;
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/pedidos.txt");
            if (File.Exists(dataFile))
            {
                userData = File.ReadAllLines(dataFile);

            }
            return userData;
        }

        public double TotalPrecioPizzas()
        {
            StreamReader leer;
            leer = File.OpenText(@"C:\Users\Christian Alexis\source\repos\ExamenPractico\ExamenPractico\App_Data\pedidos.txt");
            string cadena;
            string[] arreglo = new string[6];
            cadena = leer.ReadLine();
            double suma = 0;
            Pizza p = new Pizza();
            while (cadena != null)
            {
                arreglo = cadena.Split(',');
                double num = Convert.ToDouble(arreglo[3]);
                suma += num;
                p.Total = suma;
                cadena = leer.ReadLine();
            }
            leer.Close();
            return p.Total;

        }


        public double TotalPrecioDetalle()
        {
            StreamReader leer;
            leer = File.OpenText(@"C:\Users\Christian Alexis\source\repos\ExamenPractico\ExamenPractico\App_Data\detallePedidos.txt");
            string cadena;
            string[] arreglo = new string[6];
            cadena = leer.ReadLine();
            double suma = 0;
            Pizza p = new Pizza();
            while (cadena != null)
            {
                arreglo = cadena.Split(',');
                double num = Convert.ToDouble(arreglo[3]);
                suma += num;
                p.Total = suma;
                cadena = leer.ReadLine();
            }
            leer.Close();
            return p.Total;

        }


        public Array BorrarPedidos(Array data)
        {
            var archivo = HttpContext.Current.Server.MapPath("~/App_Data/detallePedidos.txt");
            var elimina = HttpContext.Current.Server.MapPath("~/App_Data/pedidos.txt");
            foreach (var registro in data)
            {
                File.AppendAllText(archivo, registro.ToString() + Environment.NewLine);
            }
            File.Delete(elimina);
            return data;

        }


        public void GuardarArchivoPersona(Persona per)
        {
            var nombre = per.Nombre;
            double subtotal = per.Subtotal;
            string dir = per.Domicilio;
            string tel = per.Telefono;


       
            per.Fecha = DateTime.Now;

            double total = TotalPrecioPizzas();
            var res = nombre + "," + per.Fecha + ","+dir+","+tel+","+ total + Environment.NewLine;
            var archivoPedido = HttpContext.Current.Server.MapPath("~/App_Data/personas.txt");
          

            File.AppendAllText(@archivoPedido, res);

        }


        public Array LeerArchivoPedidosTotal()
        {
            Array userData = null;
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/pedidos.txt");
            if (File.Exists(dataFile))
            {
                userData = File.ReadAllLines(dataFile);

            }
            return userData;
        }

        public Array LeerArchivoPersonas()
        {
            Array userData = null;
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/personas.txt");
            if (File.Exists(dataFile))
            {
                userData = File.ReadAllLines(dataFile);

            }
            return userData;
        }



        public Array LeerArchivoDetallesPedidos()
        {
            Array userData = null;
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/detallePedidos.txt");
            if (File.Exists(dataFile))
            {
                userData = File.ReadAllLines(dataFile);

            }
            return userData;
        }



        public void BorrarDetallesPedidos()
        {
            var elimina = HttpContext.Current.Server.MapPath("~/App_Data/detallePedidos.txt");
       
            File.Delete(elimina);
    

        }

        public Array DiasSemana(string v1, bool verificador=true)
        {
            Array arreglo = null; //este arreglo va contener el archivo
            Array arregloFiltro = null;
            var archivo = HttpContext.Current.Server.MapPath("~/App_Data/personas.txt");



            if (File.Exists(archivo))// Verificamos si existe el archivo
            {
                arreglo = File.ReadAllLines(archivo);//toma lectura de las lineas
                if (verificador)// si el verificador esta en true
                {
                    List<string> listas = new List<string>(); //
                    foreach (var i in arreglo)
                    {
                        string[] arreglo2 = i.ToString().Split(',');// lo igualamos al arreglo, lo que contenga i
                        string fechaString = arreglo2[1];
                        DateTime fechaDate = Convert.ToDateTime(fechaString);
                        string f = fechaDate.ToString("dddd");//obtenemos los dias
                        if (f.Equals(v1))
                        {
                            listas.Add(i.ToString());
                        }
                    }
                    arregloFiltro = listas.ToArray();

                }
                else
                {
                    List<string> lista = new List<string>();
                    foreach (var j in arreglo)
                    {
                        string[] aux = j.ToString().Split(',');
                        string fechatxt = aux[1];
                        DateTime fehaConvert = Convert.ToDateTime(fechatxt);
                        if (fehaConvert.ToString("MMMM", new CultureInfo("es-MX")).Equals(v1))
                        {
                            lista.Add(j.ToString());
                        }
                    }
                    arregloFiltro = lista.ToArray();
                }

            }
            return arregloFiltro;
        }





        public int TodoElFiltrado(string v1, bool v2)
        {
            int suma = 0;
            Array userDataArch = null;
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/personas.txt");
          

            if (File.Exists(dataFile))
            {

                userDataArch = File.ReadAllLines(dataFile);
                if (v2)
                {


                    foreach (var registro in userDataArch)
                    {
                        string[] auxiliar = registro.ToString().Split(',');
                        string fechaString = auxiliar[1];
                        DateTime fechaConvert = Convert.ToDateTime(fechaString);
                        string f = fechaConvert.ToString("dddd", new CultureInfo("es-MX"));
                        if (f.Equals(v1))
                        {
                            suma += Convert.ToInt32(auxiliar[4]);
                        }
                    }

                }
                else
                {
    

                    foreach (var k in userDataArch)
                    {


                        string[] auxliar = k.ToString().Split(',');
                        string fechatxt = auxliar[1];

                        DateTime fechap = Convert.ToDateTime(fechatxt);



                        if (fechap.ToString("MMMM", new CultureInfo("es-MX")).Equals(v1))
                        {
                            suma += Convert.ToInt32(auxliar[4]);
                        }
                    }
                }
            }
            return suma;
        }

        public int TotalPedidosClientes()
        {
            Array userData = null;
            var dataFile = HttpContext.Current.Server.MapPath("~/App_Data/personas.txt");
            int total = 0;
            if (File.Exists(dataFile))
            {
                userData = File.ReadAllLines(dataFile);
            }
            foreach (var data in userData)
            {
                string[] aux = data.ToString().Split(',');
                total += Convert.ToInt32(aux[4]);
            }
            return total;
        }

    }
}