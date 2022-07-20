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
    public class BLLClase : IGestor<BEClase>
    {
        MPPClase mppClase = new MPPClase();

        public void Guardar(BEClase clase)
        {
            mppClase.Guardar(clase);
        }

        public void Baja(BEClase clase)
        {
            mppClase.Baja(clase);
        }

        public List<BEClase> Listar()
        {
            return mppClase.Listar();
        }

        public void RegistrarClienteClase(BECliente cliente, BEClase clase)
        {
            mppClase.RegistrarClienteClase(cliente, clase);
        }

        public void ElimnarClienteClase(BECliente cliente, BEClase clase)
        {
            mppClase.ElimnarClienteClase(cliente, clase);
        }
    }
}
