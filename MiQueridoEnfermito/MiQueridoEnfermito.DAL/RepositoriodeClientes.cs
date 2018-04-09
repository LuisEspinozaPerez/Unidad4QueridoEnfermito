using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;
using MiQueridoEnfermito.COMMON.Entidades;
using MiQueridoEnfermito.COMMON.Interfaces;

namespace MiQueridoEnfermito.DAL
{
    public class RepositoriodeClientes : IRepositorio<Cliente>
    {
        private string DBName = "MiQueridoEnfermito.db";
        private string TableName = "Clientes";

        public List<Cliente> Leer
        {
            get
            {
                List<Cliente> clientes = new List<Cliente>();
                using (var db = new LiteDatabase(DBName))
                {
                    clientes = db.GetCollection<Cliente>(TableName).FindAll().ToList();
                }
                return clientes;
            }
        }

        public bool Crear(Cliente entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Cliente>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Editar(Cliente entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Cliente>(TableName);
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
                    var coleccion = db.GetCollection<Cliente>(TableName);
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
