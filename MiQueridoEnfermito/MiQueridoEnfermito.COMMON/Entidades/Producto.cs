using System;
using System.Collections.Generic;
using System.Text;

namespace MiQueridoEnfermito.COMMON.Entidades
{
    public class Producto:Base
    {
        
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string Presentacion { get; set; }
        public string Categoria { get; set; }
        public override string ToString()
        {
            return string.Format("{0}",NombreProducto);
        }
    }
}
