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
    public class ResidenteNegocio
    {
        ResidenteDatos dtr = new ResidenteDatos();
        public List<Residente> ListaResidente()
        {
            List<Residente> residentes = dtr.listarResidente();
            return residentes;
        }

        public void GuardarResidente(Residente modelo)
        {
            try
            {
                dtr.guardarResidente(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Residente: " + e.StackTrace);
            }
        }

        public void NG_EditarResidente(Residente modelo)
        {
            try
            {
                dtr.EditarResidente(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Residente - Editar : " + e.StackTrace);
            }
        }

        public void NG_EliminarResidente(Residente modelo)
        {
            try
            {
                dtr.EliminarResidente(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Residente - Eliminar : " + e.StackTrace);
            }
        }


        public bool ValidarResidenteExistente(string residente)
        {
            bool existe = false;
            try
            {
                existe = dtr.ExisteResidente(residente);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return existe;
        }

        public List<Patologia> ObtenerPatologiasDeResidente(object id)
        {
            try
            {
                int residenteId = Convert.ToInt32(id);
                return dtr.ObtenerPatologiasDeResidente(residenteId);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
                return new List<Patologia>();
            }
        }

    }
}
