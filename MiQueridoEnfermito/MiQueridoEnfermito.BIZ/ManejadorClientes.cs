using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiQueridoEnfermito.COMMON.Entidades;
using MiQueridoEnfermito.COMMON.Interfaces;

namespace MiQueridoEnfermito.BIZ
{
    public class ManejadorClientes : IManejadorCliente
    {
        IRepositorio<Cliente> repositorio;
        public ManejadorClientes(IRepositorio<Cliente> repo)
        {
            repositorio = repo;
        }
        public List<Cliente> Listar => repositorio.Leer;

        public bool Agregar(Cliente Entidad)
        {
            return repositorio.Crear(Entidad);
        }

        public Cliente BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Cliente Entidad)
        {
            return repositorio.Editar(Entidad);
        }
    }
}
