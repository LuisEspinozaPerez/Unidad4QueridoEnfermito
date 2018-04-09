using System;
using System.Collections.Generic;
using System.Text;

namespace MiQueridoEnfermito.COMMON.Entidades
{
    public class Venta:Base
    {
        
        public DateTime Fecha { get; set; }
        public Empleado Encargado { get; set; }
        public Cliente Comprador { get; set; }
        public List<Producto> ProductosVendidos { get; set; }
    }
}
