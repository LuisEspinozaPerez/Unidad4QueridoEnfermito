using System;
using System.Collections.Generic;
using System.Text;

namespace MiQueridoEnfermito.COMMON.Entidades
{
    public class Empleado:Base
    {
        
        public string NombreEmpleado { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", NombreEmpleado);
        }

    }
}
