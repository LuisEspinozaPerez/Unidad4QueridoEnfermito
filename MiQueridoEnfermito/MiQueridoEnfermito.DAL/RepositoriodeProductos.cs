using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;
using MiQueridoEnfermito.COMMON.Entidades;
using MiQueridoEnfermito.COMMON.Interfaces;

namespace MiQueridoEnfermito.DAL
{
    public class RepositoriodeProductos : IRepositorio<Producto>
    {
        private string DBName = "MiQueridoEnfermito.db";
        private string TableName = "Productos";

        public List<Producto> Leer
        {
            get
            {
                List<Producto> productos = new List<Producto>();
                using (var db = new LiteDatabase(DBName))
                {
                    productos = db.GetCollection<Producto>(TableName).FindAll().ToList();
                }
                return productos;
            }
        }

        public bool Crear(Producto entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Producto>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Editar(Producto entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Producto>(TableName);
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
                    var coleccion = db.GetCollection<Producto>(TableName);
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
