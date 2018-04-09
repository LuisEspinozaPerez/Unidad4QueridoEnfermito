using System;
using System.Collections.Generic;
using System.Text;
using MiQueridoEnfermito.COMMON.Entidades;

namespace MiQueridoEnfermito.COMMON.Interfaces
{
    public interface IManejadorProductos:IManejadorGenerico<Producto>
    {
        List<Producto> ProductosPorCategoria(string categoria);
    }
}
