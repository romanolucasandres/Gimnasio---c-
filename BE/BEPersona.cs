using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    // clase abstracta para que hereden Cliente y Entrenador
    abstract public class BEPersona : Entidad
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        public DateTime Fecha_nacimiento { get; set; }
        public int Edad { get; set; }

        // metodo que calcula la edad y llena la propiedad
        public void CalcularEdad()
        {
            Edad = DateTime.Today.Year - Fecha_nacimiento.Year;
            if (DateTime.Today.Month < Fecha_nacimiento.Month)
                Edad -= 1;
            if (DateTime.Today.Month == Fecha_nacimiento.Month && DateTime.Today.Day < Fecha_nacimiento.Day)
                Edad -= 1;
        }
    }
}
