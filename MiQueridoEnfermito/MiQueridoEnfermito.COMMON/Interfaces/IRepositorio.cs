using System;
using System.Collections.Generic;
using System.Text;
using MiQueridoEnfermito.COMMON.Entidades;

namespace MiQueridoEnfermito.COMMON.Interfaces
{
    public interface IRepositorio<T> where T:Base
    {
        bool Crear(T entidad);
        bool Editar(T entidadModificada);
        bool Eliminar(string id);
        List<T> Leer { get; }
    }
}
