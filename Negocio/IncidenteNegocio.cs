using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class IncidenteNegocio
    {
        IncidenteDatos dtu = new IncidenteDatos();


        public List<NivelDeRiesgo> ListaNivelRiesgo()
        {
            List<NivelDeRiesgo> nivelDeRiesgos = dtu.listarNivelRiesgo();
            return nivelDeRiesgos;
        }

        public List<Incidente> ListaIncidentesActivos()
        {
            return new IncidenteDatos().GetIncidentesT().Where(x => x.estado == 1).ToList();
        }

        public List<Incidente> ListaIncidentesResueltos()
        {
            return new IncidenteDatos().GetIncidentesT().Where(x => x.estado == 2).ToList();
        }

        public void Actualizar(Incidente infoG)
        {
            new IncidenteDatos().actualizarIncidente(infoG);
        }

        public void GuardarIndicente(Incidente modelo)
        {
            try
            {
                dtu.guardarIncidente(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Incidente: " + e.StackTrace);
            }
        }
    }
}