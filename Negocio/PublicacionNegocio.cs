using System;
using Datos;
using Entidades;
using ProyectoWebMininosUCA;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PublicacionNegocio
    {
        PublicacionDatos dc = new PublicacionDatos();

        public void GuardarPublicacion(Publicacion publicacion)
        {
            try
            {
                dc.guardarPublicacion(publicacion);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Publicacion: " + e.StackTrace);
            }
        }

        public void EditarPublicacion(Publicacion modelo)
        {
            try
            {
                dc.EditarPublicacion(modelo);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en NG_Publicacion - Editar: " + e.StackTrace);
            }
        }

        public void EliminarPublicacion(Publicacion modelo)
        {
            try
            {
                dc.EliminarPublicacion(modelo);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en NG_Publicacion - Eliminar: " + e.StackTrace);
            }
        }

        public List<Publicacion> ListaPublicaciones()
        {
            return new PublicacionDatos().getPublicaciones().Where(x => x.estado != 3).ToList();
        }
    }
}