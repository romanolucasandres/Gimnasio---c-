using System;
using System.Collections;
//referencias necesarias para utilizar los objetos de ADO
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Acceso
    {
        private SqlConnection conexion; // declaro el objeto connection
        private SqlCommand comando; // declaro el objeto command
        private SqlTransaction transaccion; // declaro el objeto transaction

        private string cadena = @"Data Source = DESKTOP-OKGHPOB\SQLEXPRESS; Initial Catalog = Gimnasio; Integrated Security = True";

        public Acceso()
        {
            // lo instancio pasandole el connectionString
            conexion = new SqlConnection(cadena);
        }

        // método para ejecutar las consultas en la base de datos, sirve para todo el ABM
        public void EjecutarConsulta(string storedP, Hashtable parametros)
        {
            conexion.Open();

            try
            {
                // creo el objeto transaccion
                transaccion = conexion.BeginTransaction();

                // instancio el objeto command, le paso todo lo necesario
                comando = new SqlCommand(storedP, conexion, transaccion);
                comando.CommandType = CommandType.StoredProcedure;

                // reviso si la consulta tiene parametros 
                if (parametros != null)
                {
                    // voy agregando los parametros a la coleccion Parameters del comando
                    foreach (string parametro in parametros.Keys)
                        comando.Parameters.AddWithValue(parametro, parametros[parametro]);
                }

                // la tabla es bloqueada y se libera una vez que se termina de ejecutar el Commit
                comando.ExecuteNonQuery();
                transaccion.Commit();
            }
            catch (SqlException ex) // atrapo excepcion sql
            {
                // si algo sale mal, no queda nada guardado en la base
                transaccion.Rollback();
                throw ex;
            }
            catch(Exception ex) // atrapo cualquier otra excepcion que pueda ocurrir
            {
                throw ex;
            }
            finally
            {
                // cierro la conexion en el finally para asegurarme que se cierre si ocurre una excepcion
                conexion.Close();
            }
        } 

        #region metodos LEER
        // metodo para leer datos de la base, llena un objeto DataTable y se lo pasa a MAPPER
        public DataTable Leer(string storedP, Hashtable parametros)
        {
            DataTable tabla = new DataTable();

            try
            {
                comando = new SqlCommand(storedP, conexion);
                comando.CommandType = CommandType.StoredProcedure;

                // reviso si la consulta tiene parametros 
                if (parametros != null)
                {
                    foreach (string parametro in parametros.Keys)
                        comando.Parameters.AddWithValue(parametro, parametros[parametro]);
                }

                // instancio el DataAdapter y con eso lleno la tabla
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                adapter.Fill(tabla);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            // devuelvo la tabla para que sea mappeada
            return tabla;
        }

        // para las asignaciones, devuelve la cantidad de elementos asignados a otro
        public int LeerEscalar(string storedP, Hashtable parametros)
        {
            conexion.Open();

            comando = new SqlCommand(storedP, conexion);
            comando.CommandType = CommandType.StoredProcedure;

            // reviso si la consulta tiene parametros 
                if (parametros != null)
                {
                    foreach (string parametro in parametros.Keys)
                        comando.Parameters.AddWithValue(parametro, parametros[parametro]);
                }

            try
            {
                return Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        #endregion metodos LEER
    }
}
