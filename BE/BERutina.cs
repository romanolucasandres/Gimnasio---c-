using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BERutina : Entidad
    {
        public string Ejercicio { get; set; }
        public int Series { get; set; }
        public int Descanso { get; set; }

        public override string ToString()
        {
            return Codigo + ", " + Ejercicio;
        }
    }
}
