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
using System.Security.Cryptography;

namespace MAPPER
{
    public class MPPEntrenador : IGestor<BEEntrenador>
    {
        Acceso acceso;
        Hashtable parametros = new Hashtable();

        #region ABM
        public void Guardar(BEEntrenador entrenador)
        {
            parametros.Clear();
            string sp = "";

            if(entrenador.Codigo == 0)  // alta
            {
                sp = "sp_insert_entrenador";
                parametros.Add("@Pass", EncriptarPW(entrenador.Contrasenia));
            }
            else    // modificacion
            {
                sp = "sp_update_entrenador";
                parametros.Add("@Codigo", entrenador.Codigo);
            }

            parametros.Add("@Legajo", entrenador.Legajo);
            parametros.Add("@Nombre", entrenador.Nombre);
            parametros.Add("@Apellido", entrenador.Apellido);
            parametros.Add("@DNI", entrenador.DNI);
            parametros.Add("@Telefono", entrenador.Telefono);
            parametros.Add("@Fecha_nac", entrenador.Fecha_nacimiento);
            parametros.Add("@Sueldo", entrenador.Sueldo);
            parametros.Add("@Mail", entrenador.Mail);

            acceso = new Acceso();
            acceso.EjecutarConsulta(sp, parametros);
        }

        public void Baja(BEEntrenador entrenador)
        {
            if (!EntrenadorEstaAsociado(entrenador))
            {
                parametros.Clear();
                parametros.Add("@Codigo", entrenador.Codigo);

                acceso = new Acceso();
                acceso.EjecutarConsulta("sp_delete_entrenador", parametros);
            }
            else
                throw new Exception("El entrenador que desea borrar se encuentra asignado a clases.");
        }
        #endregion ABM

        public List<BEEntrenador> Listar()
        {
            acceso = new Acceso();
            DataTable tabla = acceso.Leer("sp_listar_entrenador", null);

            List<BEEntrenador> lista = new List<BEEntrenador>();

            if (tabla.Rows.Count > 0)  // si trajo filas
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    BEEntrenador entrenador = new BEEntrenador();
                    entrenador.Codigo = Convert.ToInt32(fila[0]);
                    entrenador.Legajo = Convert.ToInt32(fila[1]);
                    entrenador.Nombre = fila[2].ToString();
                    entrenador.Apellido = fila[3].ToString();
                    entrenador.DNI = Convert.ToInt32(fila[4]);
                    entrenador.Telefono = Convert.ToInt32(fila[5]);
                    entrenador.Fecha_nacimiento = Convert.ToDateTime(fila[6]);
                    entrenador.Sueldo = Convert.ToDecimal(fila[7]);
                    entrenador.Mail = fila[8].ToString();
                    entrenador.Contrasenia = fila[9].ToString();
                    entrenador.CalcularEdad();

                    lista.Add(entrenador);
                }
            }
            else
                lista = null;

            return lista;
        }
         
        // metodo para buscar la contraseña a la bd, primero se debe comprobar el mail
        public string BuscarPass(string mail)
        {
            parametros.Clear();
            parametros.Add("@Mail", mail);

            acceso = new Acceso();
            DataTable tabla = acceso.Leer("sp_buscarPass_entrenador", parametros);

            return tabla.Rows[0].ItemArray[0].ToString();
        }

        // metodo para buscar el mail y ver si coincide con el ingresado
        public bool BuscarMail(string mail)
        {
            parametros.Clear();
            parametros.Add("@Mail", mail);

            acceso = new Acceso();
            int resultado = acceso.LeerEscalar("sp_buscarMail_entrenador", parametros);

            if (resultado > 0)  // encontro mail
                return true;
            else
                return false;
        }

        // reviso que el entrenado a borrar no este asociado a clases
        private bool EntrenadorEstaAsociado(BEEntrenador entrenador)
        {
            parametros.Clear();
            parametros.Add("@CodEnt", entrenador.Codigo);

            acceso = new Acceso();
            int resultado = acceso.LeerEscalar("sp_count_entrenadorXclase", parametros);

            if (resultado > 0)  // hay clases asociadas
                return true;
            else                // no hay clases asociadas
                return false;
        }

        public string EncriptarPW(string pw)
        {
            UnicodeEncoding codigo = new UnicodeEncoding();
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] hash = sha.ComputeHash(codigo.GetBytes(pw));

            return Convert.ToBase64String(hash);
        }
    }
}
