using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraccion;
using BE;
using MAPPER;

namespace BLL
{
    public class BLLRutina : IGestor<BERutina>
    {
        MPPRutina mppRutina;


        public void Guardar(BERutina rutina)
        {
            mppRutina = new MPPRutina();
            mppRutina.Guardar(rutina);
        }

        public void Baja(BERutina rutina)
        {
            mppRutina = new MPPRutina();
            mppRutina.Baja(rutina);
        }

        public List<BERutina> Listar()
        {
            mppRutina = new MPPRutina();
            return mppRutina.Listar();
        }

        public List<BERutina> Buscar(string criterio)
        {
            mppRutina = new MPPRutina();
            List<BERutina> lista = mppRutina.Buscar(criterio);

            if (lista.Count != 0)
                return lista;
            else
                throw new Exception("No se encontraron coincidencias.");
        }
    }
}
