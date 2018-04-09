using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiQueridoEnfermito.COMMON.Entidades;
using MiQueridoEnfermito.COMMON.Interfaces;

namespace MiQueridoEnfermito.BIZ
{
    public class ManejadorProductos : IManejadorProductos
    {
        IRepositorio<Producto> repositorio;
        public ManejadorProductos(IRepositorio<Producto> repo)
        {
            repositorio = repo;
        }
        public List<Producto> Listar => repositorio.Leer;

        public bool Agregar(Producto Entidad)
        {
            return repositorio.Crear(Entidad);
        }

        public Producto BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Producto Entidad)
        {
            return repositorio.Editar(Entidad);
        }

        public List<Producto> ProductosPorCategoria(string categoria)
        {
            return Listar.Where(e => e.Categoria == categoria).ToList();
        }
    }
}
