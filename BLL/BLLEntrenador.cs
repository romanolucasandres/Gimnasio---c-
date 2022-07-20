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
    public class BLLEntrenador : IGestor<BEEntrenador>
    {
        MPPEntrenador mppEntrenador = new MPPEntrenador();

        public void Guardar(BEEntrenador entrenador)
        {
            if (entrenador.Codigo == 0 && mppEntrenador.BuscarMail(entrenador.Mail))
                throw new Exception("Ya existe un usuario con ese mail.");

            mppEntrenador.Guardar(entrenador);
        }

        public void Baja(BEEntrenador entrenador)
        {
            mppEntrenador.Baja(entrenador);
        }

        public List<BEEntrenador> Listar()
        {
            return mppEntrenador.Listar();
        }

        private string BuscarPass(string mail)
        {
            // valido que no se ingrese un mail que no existe en la bd
            if (!(mppEntrenador.BuscarMail(mail)))
                throw new Exception("No se encontraron coincidencias con el correo ingresado.");

            return mppEntrenador.BuscarPass(mail);
        }

        // asi no tengo que pasar la contraseña original a la capa de presentacion
        public bool PassCorrecta(string mail, string pass)
        {
            string pw = BuscarPass(mail);
            string pwIngresada = mppEntrenador.EncriptarPW(pass);

            if (pw == pwIngresada)
                return true;
            else
                return false;
        }
    }
}
