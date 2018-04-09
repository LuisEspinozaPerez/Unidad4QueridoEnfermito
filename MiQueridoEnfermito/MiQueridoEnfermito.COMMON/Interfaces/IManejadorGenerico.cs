using System;
using System.Collections.Generic;
using System.Text;
using MiQueridoEnfermito.COMMON.Entidades;

namespace MiQueridoEnfermito.COMMON.Interfaces
{
    public interface IManejadorGenerico<T> where T:Base
    {
        bool Agregar(T Entidad);
        List<T> Listar { get; }
        bool Eliminar(string id);
        bool Modificar(T Entidad);
        T BuscarPorId(string id);
    }
}
