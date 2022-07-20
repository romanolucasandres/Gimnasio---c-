using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEClase : Entidad
    {
        public BEActividad Actividad { get; set; }  //la actividad es parte de la clase
        public int Aula { get; set; }
        public DateTime Fecha_hora { get; set; }
        public BEEntrenador Entrenador { get; set; }    // el entrenador asignado a la clase
        public int Cupo { get; set; }

        // lista de clientes registrados
        public List<BECliente> ListaClientes { get; set; }
    }
}
