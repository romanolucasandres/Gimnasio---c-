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
    public class BLLCliente : IGestor<BECliente>
    {
        // declaro un objeto mapper
        MPPCliente mppCliente;

        public BLLCliente()
        {
            // lo instancio
            mppCliente = new MPPCliente();
        }

        public void Guardar(BECliente cliente)
        {
            mppCliente.Guardar(cliente);
        }

        public void Baja(BECliente cliente)
        {
            mppCliente.Baja(cliente);
        }

        // devuelve una lista de clientes ya mappeados
        public List<BECliente> Listar()
        {
            return mppCliente.Listar();
        }

        public void AgregarRutina(BECliente cliente, BERutina rutina)
        {
            // verifico que el cliente no tenga rutina, sino se pisarian
            if(cliente.RutinaPersonalizada == null)
                mppCliente.AgregarRutina(cliente, rutina);
            else
                throw new Exception("El cliente ya tiene una rutina.");
        }

        public void EliminarRutina(BECliente cliente)
        {
            // verifico que el cliente tenga rutina para borrar
            if (cliente.RutinaPersonalizada != null)
                mppCliente.EliminarRutina(cliente);
            else
                throw new Exception("No hay nada que borrar.");
        }
    }
}
