using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECliente : BEPersona
    {
        public BERutina RutinaPersonalizada { get; set; }

        public override string ToString()
        {
            return Apellido + ", " + Nombre;
        }
    }
}
