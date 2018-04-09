using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiQueridoEnfermito.COMMON.Entidades;
using MiQueridoEnfermito.COMMON.Interfaces;

namespace MiQueridoEnfermito.BIZ
{
    public class ManejadorEmpleados : IManejadorEmpleado
    {
        IRepositorio<Empleado> repositorio;
        public ManejadorEmpleados(IRepositorio<Empleado> repo)
        {
            repositorio = repo;
        }
        public List<Empleado> Listar => repositorio.Leer;

        public bool Agregar(Empleado Entidad)
        {
            return repositorio.Crear(Entidad);
        }

        public Empleado BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Empleado Entidad)
        {
            return repositorio.Editar(Entidad);
        }
    }
}
