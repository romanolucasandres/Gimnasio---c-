using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MAPPER;
using Abstraccion;

namespace BLL
{
    public class BLLActividad : IGestor<BEActividad>
    {
        MPPActividad mppActividad;

        public void Guardar(BEActividad actividad)
        {
            mppActividad = new MPPActividad();
            mppActividad.Guardar(actividad);
        }

        public void Baja(BEActividad actividad)
        {
            mppActividad = new MPPActividad();
            mppActividad.Baja(actividad);
        }

        public List<BEActividad> Listar()
        {
            mppActividad = new MPPActividad();
            return mppActividad.Listar();
        }
    }
}
