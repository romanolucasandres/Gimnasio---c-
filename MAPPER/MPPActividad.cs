using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Abstraccion;
using BE;
using DAL;
using System.Data;

namespace MAPPER
{
    public class MPPActividad : IGestor<BEActividad>
    {
        Acceso acceso;
        Hashtable parametros = new Hashtable();

        #region ABM
        // alta y modificacion
        public void Guardar(BEActividad actividad)
        {
            parametros.Clear();
            string sp = "";

            if(actividad.Codigo == 0)   // alta
            {
                sp = "sp_insert_actividad";
            }
            else   // modificacion
            {
                sp = "sp_update_actividad";
                parametros.Add("@Cod", actividad.Codigo);
            }

            parametros.Add("@Nombre", actividad.Nombre);

            acceso = new Acceso();
            acceso.EjecutarConsulta(sp, parametros);
        }

        public void Baja(BEActividad actividad)
        {
            if (!ActividadEstaAsociada(actividad))
            {
                parametros.Clear();
                parametros.Add("@Cod", actividad.Codigo);

                acceso = new Acceso();
                acceso.EjecutarConsulta("sp_delete_actividad", parametros);
            }
            else
                throw new Exception("La actividad que está intentando borrar se encuentra asociada a clases, " +
                    "por favor borre las clases primero.");
        }
        #endregion ABM

        public List<BEActividad> Listar()
        {
            acceso = new Acceso();
            DataTable tabla = acceso.Leer("sp_listar_actividad", null);

            List<BEActividad> lista = new List<BEActividad>();

            if (tabla.Rows.Count > 0)    // si tiene filas
            { 
                foreach(DataRow fila in tabla.Rows)
                {
                    // aca uso el constructor sobrecargado
                    BEActividad actividad = new BEActividad(Convert.ToInt32(fila[0]), fila[1].ToString());
                    lista.Add(actividad);
                }
            }
            else
                lista = null;

            return lista;
        }

        // reviso que la actividad a borrar no este asociada a clases
        private bool ActividadEstaAsociada(BEActividad actividad)
        {
            // cuento la cantidad de clases que tienen asociado el codigo de la actividad a borrar

            parametros.Clear();
            parametros.Add("@CodAct", actividad.Codigo);

            acceso = new Acceso();
            int resultado = acceso.LeerEscalar("sp_count_actividadXclase", parametros);

            if (resultado > 0)  // hay clases asociadas a actividaes
                return true;
            else                // no hay clases asociadas a actividades
                return false;
        } 
    }
}
