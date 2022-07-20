using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml.Linq;
using Abstraccion;
using BE;
using DAL;
using System.Data;

namespace MAPPER
{
    public class MPPCliente : IGestor<BECliente>
    {
        Acceso acceso;
        Hashtable parametros = new Hashtable();

        #region ABM
        // metodo para dar de alta o modificar un cliente, recibe un objeto de la BE
        public void Guardar(BECliente cliente)
        {
            parametros.Clear();
            string sp = "";

            if(cliente.Codigo == 0) // si es 0, es un alta (el cliente aun no existe en el sistema)
            {
                sp = "sp_insert_cliente";
            }
            else  // si no es 0, es una modificacion
            {
                sp = "sp_update_cliente";
                parametros.Add("@Codigo", cliente.Codigo);
            }

            parametros.Add("@Nombre", cliente.Nombre);
            parametros.Add("@Apellido", cliente.Apellido);
            parametros.Add("@DNI", cliente.DNI);
            parametros.Add("@Telefono", cliente.Telefono);
            parametros.Add("@Fecha_nac", cliente.Fecha_nacimiento);
            parametros.Add("@Edad", cliente.Edad);

            acceso = new Acceso();
            acceso.EjecutarConsulta(sp, parametros);
        }

        public void Baja(BECliente cliente)
        {
            if (!ClienteEstaEnClases(cliente))
            {
                parametros.Clear();
                parametros.Add("@Cod", cliente.Codigo);

                acceso = new Acceso();
                acceso.EjecutarConsulta("sp_delete_cliente", parametros);

                // elimino su rutina del sistema, ya que al ser personalizadas pertenecen completamente al cliente
                if (cliente.RutinaPersonalizada != null)
                {
                    MPPRutina mppRutina = new MPPRutina();
                    mppRutina.Baja(cliente.RutinaPersonalizada);
                }
            }
            else
                throw new Exception("El cliente que desea borrar se encuentra registrado en clases");
        }
        #endregion ABM
         
        // recibe un DataTable y lo mappea a lista
        public List<BECliente> Listar()
        {
            acceso = new Acceso();
            DataTable tabla = acceso.Leer("sp_listar_cliente", null);

            // mappeo la tabla a lista
            List<BECliente> lista = new List<BECliente>();

            if (tabla.Rows.Count > 0)    // si trajo una tabla llena o vacia
            {
                foreach (DataRow fila in tabla.Rows)
                {
                    BECliente cliente = new BECliente();

                    cliente.Codigo = Convert.ToInt32(fila[0]);
                    cliente.Nombre = fila[1].ToString();
                    cliente.Apellido = fila[2].ToString();
                    cliente.DNI = Convert.ToInt32(fila[3]);
                    cliente.Telefono = Convert.ToInt32(fila[4]);
                    cliente.Fecha_nacimiento = Convert.ToDateTime(fila[5]);
                    cliente.Edad = Convert.ToInt32(fila[6]);

                    // busco su rutina y se la agrego
                    if (!(fila["Cod_rutina"] is DBNull))
                    {
                        if(Convert.ToInt32(fila["Cod_rutina"]) != 0)
                        {
                            XDocument doc = XDocument.Load("Rutinas.xml");

                            var consulta = from rutina in doc.Descendants("rutina")
                                           where Convert.ToInt32(rutina.Attribute("id").Value) == Convert.ToInt32(fila["Cod_rutina"])
                                           select new BERutina
                                           {
                                               Codigo = Convert.ToInt32(rutina.Attribute("id").Value),
                                               Ejercicio = rutina.Element("ejercicio").Value,
                                               Series = Convert.ToInt32(rutina.Element("series").Value),
                                               Descanso = Convert.ToInt32(rutina.Element("descanso").Value)
                                           };

                            List<BERutina> r = consulta.ToList<BERutina>();
                            cliente.RutinaPersonalizada = r.First();
                        }
                    }

                    lista.Add(cliente);
                }
            }
            else
                lista = null;

            return lista;
        }

        #region agregar y eliminar
        public void AgregarRutina(BECliente cliente, BERutina rutina)
        {
            // verifico que la rutina a asignar no pertenezca a otro cliente
            if (!(RutinaEstaAsignada(rutina)))
            {
                parametros.Clear();
                parametros.Add("@Cod_rutina", rutina.Codigo);
                parametros.Add("@Cod_cli", cliente.Codigo);

                acceso = new Acceso();
                acceso.EjecutarConsulta("sp_agregar_rutinaCliente", parametros);
            }
            else
                throw new Exception("La rutina ya pertenece a un cliente.");
        }

        public void EliminarRutina(BECliente cliente)
        {
            // elimino la rutina del cliente y ademas la borro del sistema ya que no se puede asignar a otro
            parametros.Clear();
            parametros.Add("@Cod", cliente.Codigo);

            acceso = new Acceso();
            acceso.EjecutarConsulta("sp_eliminar_rutinaCliente", parametros);

            MPPRutina mppRutina = new MPPRutina();
            mppRutina.Baja(cliente.RutinaPersonalizada);
        }
        #endregion agregar y eliminar

        private bool ClienteEstaEnClases(BECliente cliente)
        {
            parametros.Clear();
            parametros.Add("@CodCli", cliente.Codigo);

            acceso = new Acceso();
            int resultado = acceso.LeerEscalar("sp_count_clienteXclase", parametros);

            if (resultado > 0)   // el cliente esta registrado por lo menos en una clase
                return true;
            else
                return false;
        }

        internal bool RutinaEstaAsignada(BERutina rutina)
        {
            parametros.Clear();
            parametros.Add("@Cod", rutina.Codigo);

            acceso = new Acceso();
            int resultado = acceso.LeerEscalar("sp_count_rutinaXcliente", parametros);

            if (resultado > 0)  // la rutina ya pertenece a un cliente
                return true;
            else
                return false;
        }
    }
}
