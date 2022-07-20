using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Abstraccion;
using BE;

namespace MAPPER
{
    public class MPPRutina : IGestor<BERutina>
    {
        public void Guardar(BERutina rutina)
        {
            // el alta y modificacion se hace de esta forma para poder seguir usando la interfaz IGestor
            // en lugar de hacer metodos individuales (ya que no puedo traer un codigo 0 porque no se 
            // esta usando la propiedad autoincremental de la BD)

            XDocument doc = XDocument.Load("Rutinas.xml");

            if (Listar().Exists(x => x.Codigo == rutina.Codigo)) // modificacion
            {
                var consulta = from r in doc.Descendants("rutina")
                               where Convert.ToInt32(r.Attribute("id").Value) == rutina.Codigo
                               select r;

                foreach (var r in consulta)
                {
                    r.Element("ejercicio").Value = rutina.Ejercicio;
                    r.Element("series").Value = rutina.Series.ToString();
                    r.Element("descanso").Value = rutina.Descanso.ToString();
                }

                doc.Save("Rutinas.xml");
            }
            else  // alta
            {
                // a "rutinas" le agrego el elemento "rutina" que ademas contiene los demas elementos
                doc.Element("rutinas").Add(new XElement("rutina", new XAttribute("id", rutina.Codigo),
                                                                  new XElement("ejercicio", rutina.Ejercicio),
                                                                  new XElement("series", rutina.Series),
                                                                  new XElement("descanso", rutina.Descanso)));
                doc.Save("Rutinas.xml");
            }
        }

        public void Baja(BERutina rutina)
        {
            MPPCliente mppCliente = new MPPCliente();

            // si la rutina se encuentra asignada no la puedo borrar
            if (!(mppCliente.RutinaEstaAsignada(rutina)))
            {
                XDocument doc = XDocument.Load("Rutinas.xml");

                var consulta = from r in doc.Descendants("rutina")
                               where Convert.ToInt32(r.Attribute("id").Value) == rutina.Codigo
                               select r;

                consulta.Remove();

                doc.Save("Rutinas.xml");
            }
            else
                throw new Exception("La rutina se encuentra asignada a un cliente.");
        }

        public List<BERutina> Listar()
        {
            XDocument doc = XDocument.Load("Rutinas.xml");

            var consulta = from rutina in doc.Descendants("rutina")
                           select new BERutina
                           {
                               Codigo = Convert.ToInt32(rutina.Attribute("id").Value),
                               Ejercicio = rutina.Element("ejercicio").Value.ToString(),
                               Series = Convert.ToInt32(rutina.Element("series").Value),
                               Descanso = Convert.ToInt32(rutina.Element("descanso").Value)
                           };

            return consulta.ToList<BERutina>();
        }

        public List<BERutina> Buscar(string criterio)
        {
            // busco la rutina segun el criterio establecido
            var consulta = from rutina in XElement.Load("Rutinas.xml").Elements("rutina")
                           where rutina.Element("ejercicio").Value == criterio
                           select new BERutina
                           {
                               Codigo = Convert.ToInt32(rutina.Attribute("id").Value),
                               Ejercicio = rutina.Element("ejercicio").Value.ToString(),
                               Series = Convert.ToInt32(rutina.Element("series").Value),
                               Descanso = Convert.ToInt32(rutina.Element("descanso").Value)
                           };

            return consulta.ToList<BERutina>();
        }
    }
}
