using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LiteDB;
using MiQueridoEnfermito.COMMON.Entidades;
using MiQueridoEnfermito.COMMON.Interfaces;

namespace MiQueridoEnfermito.DAL
{
    public class RepositoriodeVentas : IRepositorio<Venta>
    {
        private string DBName = "MiQueridoEnfermito.db";
        private string TableName = "Ventas";

        public bool GenerarTicket(string empleado, string nombreCliente, double precio, string productos, double TotalPagar, string ruta)
        {
            StreamWriter archivo = new StreamWriter(ruta);
            archivo.WriteLine("\tMi Querido Enfermito");
            archivo.WriteLine("Fecha: "+ DateTime.Now.ToLongDateString());
            archivo.WriteLine("Empleado: "+ empleado);
            archivo.WriteLine("Cliente: "+ nombreCliente);
            archivo.WriteLine("\nCantidad\tProducto\tPrecio\tPresentacion");
            archivo.Write("\t\t\t" + productos);
            archivo.WriteLine("\nTotal: $" + TotalPagar);
            archivo.WriteLine("\tGracias por su compra.");
            archivo.Close();
            return true;
        }
        public List<Venta> Leer
        {
            get
            {
                List<Venta> productos = new List<Venta>();
                using (var db = new LiteDatabase(DBName))
                {
                    productos = db.GetCollection<Venta>(TableName).FindAll().ToList();
                }
                return productos;
            }
        }

        public bool Crear(Venta entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Venta>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Editar(Venta entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Venta>(TableName);
                    coleccion.Update(entidadModificada);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Eliminar(string id)
        {
            try
            {
                int r;
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Venta>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
