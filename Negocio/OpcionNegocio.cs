using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Negocio
{
   public class OpcionNegocio
    {
        OpcionDatos dto = new OpcionDatos();
        public List<Opcion> ListaOpcion()
        {
            List<Opcion> Opcion = dto.listarOpcion();
            return Opcion;
        }



        public void GuardarOpcion(Opcion modelo)
        {
            try
            {
                dto.guardarOpcion(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Opcion: " + e.StackTrace);
            }
        }

        public void NG_EditarOpcion(Opcion modelo)
        {
            try
            {
                dto.EditarOpcion(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Opcion - Editar : " + e.StackTrace);
            }
        }

        public void NG_EliminarOpcion(Opcion modelo)
        {
            try
            {
                dto.EliminarOpcion(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Opcion - Eliminar : " + e.StackTrace);
            }
        }

        public bool ValidarAccionExistente(string Accion)
        {
            bool existe = false;
            try
            {
                existe = dto.ExisteAccion(Accion);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return existe;
        }

        public bool ValidarURLExistente(string URL)
        {
            bool existe = false;
            try
            {
                existe = dto.ExisteURL(URL);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return existe;
        }


    }
}

