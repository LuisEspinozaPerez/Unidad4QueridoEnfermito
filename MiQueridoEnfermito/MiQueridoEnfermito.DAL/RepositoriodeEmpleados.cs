using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;
using MiQueridoEnfermito.COMMON.Entidades;
using MiQueridoEnfermito.COMMON.Interfaces;

namespace MiQueridoEnfermito.DAL
{
    public class RepositoriodeEmpleados : IRepositorio<Empleado>
    {
        private string DBName = "MiQueridoEnfermito.db";
        private string TableName = "Empleados";

        public List<Empleado> Leer
        {
            get
            {
                List<Empleado> empleados = new List<Empleado>();
                using(var db = new LiteDatabase(DBName))
                {
                    empleados = db.GetCollection<Empleado>(TableName).FindAll().ToList();
                }
                return empleados;
            }
        }

        public bool Crear(Empleado entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Empleado>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Editar(Empleado entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Empleado>(TableName);
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
                    var coleccion = db.GetCollection<Empleado>(TableName);
                    r= coleccion.Delete(e=> e.Id==id);
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
