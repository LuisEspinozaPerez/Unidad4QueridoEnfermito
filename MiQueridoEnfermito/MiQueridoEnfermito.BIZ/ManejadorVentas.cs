using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiQueridoEnfermito.COMMON.Entidades;
using MiQueridoEnfermito.COMMON.Interfaces;

namespace MiQueridoEnfermito.BIZ
{
    public class ManejadorVentas : IManejadorVentas
    {
        IRepositorio<Venta> repositorio;
        public ManejadorVentas(IRepositorio<Venta> repo)
        {
            repositorio = repo;
        }
        public List<Venta> Listar => repositorio.Leer;

        public bool Agregar(Venta Entidad)
        {
            return repositorio.Crear(Entidad);
        }

        public Venta BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Venta Entidad)
        {
            return repositorio.Editar(Entidad);
        }
    }
}
