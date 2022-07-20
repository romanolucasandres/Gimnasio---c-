using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEActividad : Entidad
    {
        #region constructores sobrecargados
        // si se llama a este, las propiedades se cargan manualmente luego de instanciado el objeto
        public BEActividad()
        { }

        // si se llama a este, se encarga de llenar las propiedades
        public BEActividad(int cod, string nombre)
        {
            Codigo = cod;
            Nombre = nombre;
        }
        #endregion constructores sobrecargados

        public string Nombre { get; set; }

        // override a ToString para poder mostrar la actividad en los dgv
        public override string ToString()
        {
            return Nombre;
        }
    }
}
