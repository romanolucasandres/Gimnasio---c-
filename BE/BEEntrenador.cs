using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEEntrenador : BEPersona
    {
        public int Legajo { get; set; }
        public decimal Sueldo { get; set; }
        public string Mail { get; set; }
        public string Contrasenia { get; set; }

        public override string ToString()
        {
            return Codigo + ", " + Nombre + " " + Apellido;
        }
    }
}
