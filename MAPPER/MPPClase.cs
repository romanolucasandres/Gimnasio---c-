using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml.Linq;
using Abstraccion;
using DAL;
using BE;
using System.Data;

namespace MAPPER
{
    public class MPPClase : IGestor<BEClase>
    {
        Acceso acceso;
        Hashtable parametros = new Hashtable();

        #region ABM
        public void Guardar(BEClase clase)
        {
            parametros.Clear();
            string sp = "";

            if(clase.Codigo == 0)   // alta
            {
                sp = "sp_insert_clase";
            }
            else     // modificacion
            {
                sp = "sp_update_clase";
                parametros.Add("@Cod", clase.Codigo);
            }

            parametros.Add("@Cod_actividad", clase.Actividad.Codigo);
            parametros.Add("@Aula", clase.Aula);
            parametros.Add("@Cod_entrenador", clase.Entrenador.Codigo);
            parametros.Add("@Fecha_hora", clase.Fecha_hora);
            parametros.Add("@Cupo_alumnos", clase.Cupo);

            acceso = new Acceso();
            acceso.EjecutarConsulta(sp, parametros);
        }

        public void Baja(BEClase clase)
        {
            if (!ClaseTieneClientes(clase))
            {
                parametros.Clear();
                parametros.Add("@Cod", clase.Codigo);

                acceso = new Acceso();
                acceso.EjecutarConsulta("sp_delete_clase", parametros);
            }
            else
                throw new Exception("La clase que está intentando borrar contiene clientes registrados.");
        }
        #endregion ABM

        public List<BEClase> Listar()
        {
            acceso = new Acceso();
            DataTable tabla = acceso.Leer("sp_listar_clase", null);

            List<BEClase> lista = new List<BEClase>();

            if (tabla.Rows.Count > 0)    // si trajo algo
            {
                foreach(DataRow fila in tabla.Rows)
                {
                    BEClase clase = new BEClase();

                    clase.Codigo = Convert.ToInt32(fila["ClaseCod"]);
                    clase.Aula = Convert.ToInt32(fila["Aula"]);
                    clase.Fecha_hora = Convert.ToDateTime(fila["Fecha_hora"]);
                    clase.Cupo = Convert.ToInt32(fila["Cupo_alumnos"]);

                    // le agrego el objeto actividad
                    clase.Actividad = new BEActividad(Convert.ToInt32(fila["ActCod"]), fila["ActNom"].ToString());

                    // le agrego el objeto entrenador
                    BEEntrenador entrenador = new BEEntrenador();
                    entrenador.Codigo = Convert.ToInt32(fila["Codigo"]);
                    entrenador.Nombre = fila["Nombre"].ToString();
                    entrenador.Apellido = fila["Apellido"].ToString();
                    entrenador.Legajo = Convert.ToInt32(fila["Legajo"]);
                    entrenador.DNI = Convert.ToInt32(fila["DNI"]);
                    entrenador.Telefono = Convert.ToInt32(fila["Telefono"]);
                    entrenador.Fecha_nacimiento = Convert.ToDateTime(fila["Fecha_nacimiento"]);
                    entrenador.Sueldo = Convert.ToDecimal(fila["Sueldo"]);
                    entrenador.Mail = fila["Mail"].ToString();
                    entrenador.Contrasenia = fila["Contrasenia"].ToString();
                    entrenador.CalcularEdad();
                    clase.Entrenador = entrenador;

                    // lleno su lista de clientes
                    parametros.Clear();
                    parametros.Add("@Cod", clase.Codigo);
                    DataTable tabla2 = acceso.Leer("sp_listar_clientesClase", parametros);

                    List <BECliente> clientes = new List<BECliente>();

                    if (tabla2.Rows.Count > 0)  // si recibio algo
                    {
                        foreach (DataRow fila2 in tabla2.Rows)
                        {
                            BECliente cliente = new BECliente();
                            cliente.Codigo = Convert.ToInt32(fila2[0]);
                            cliente.Nombre = fila2[1].ToString();
                            cliente.Apellido = fila2[2].ToString();
                            cliente.DNI = Convert.ToInt32(fila2[3]);
                            cliente.Telefono = Convert.ToInt32(fila2[4]);
                            cliente.Fecha_nacimiento = Convert.ToDateTime(fila2[5]);
                            cliente.CalcularEdad();

                            // busco su rutina
                            if (!(fila2["Cod_rutina"] is DBNull))
                            {
                                if (Convert.ToInt32(fila2["Cod_rutina"]) != 0)
                                {
                                    XDocument doc = XDocument.Load("Rutinas.xml");

                                    var consulta = from rutina in doc.Descendants("rutina")
                                                   where Convert.ToInt32(rutina.Attribute("id").Value) == Convert.ToInt32(fila2["Cod_rutina"])
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

                            clientes.Add(cliente);
                        }
                    }
                    else
                        clientes = null;

                    // la lista recien creada la asigno a la lista de la clase actual
                    clase.ListaClientes = clientes;

                    lista.Add(clase);
                }
            }
            else
                lista = null;

            return lista;
        }

        #region asignacion y desasignacion
        public void RegistrarClienteClase(BECliente cliente, BEClase clase)
        {
            parametros.Clear();
            parametros.Add("@CodCliente", cliente.Codigo);
            parametros.Add("@CodClase", clase.Codigo);

            acceso = new Acceso();
            acceso.EjecutarConsulta("sp_asociar_clienteClase", parametros);
        }

        public void ElimnarClienteClase(BECliente cliente, BEClase clase)
        {
            parametros.Clear();
            parametros.Add("@CodCliente", cliente.Codigo);
            parametros.Add("@CodClase", clase.Codigo);

            acceso = new Acceso();
            acceso.EjecutarConsulta("sp_desasociar_clienteClase", parametros);
        }
        #endregion asignacion y desasignacion

        private bool ClaseTieneClientes(BEClase clase)
        {
            parametros.Clear();
            parametros.Add("@CodClase", clase.Codigo);

            acceso = new Acceso(); 
            int resultado = acceso.LeerEscalar("sp_count_claseXclientes", parametros);

            if (resultado > 0)   // tiene clientes registrados
                return true;
            else
                return false;
        }
    }
}
