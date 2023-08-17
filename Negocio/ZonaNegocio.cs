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
    public class ZonaNegocio
    {
        ZonaDatos dtz = new ZonaDatos();

        public List<Zona> ListaZona()
        {
            List<Zona> Zona = dtz.listarZona();
            return Zona;
        }

        public void GuardarZona(Zona modelo)
        {
            try
            {
                dtz.guardarZona(modelo);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en NG_Zona: " + e.StackTrace);
            }
        }

        public void NG_EditarZona(Zona modelo)
        {
            try
            {
                dtz.EditarZona(modelo);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en NG_Zona - Editar : " + e.StackTrace);
            }
        }

        public void NG_EliminarZona(Zona modelo)
        {
            try
            {
                dtz.EliminarZona(modelo);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en NG_Zona - Eliminar : " + e.StackTrace);
            }
        }

        public bool ValidarZonaExistente(string zona)
        {
            bool existe = false;
            try
            {
                existe = dtz.ExisteZona(zona);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return existe;
        }

        public List<Zona> LlenarDropDownListZonaNeg()
        {
            List<Zona> listaZona = new List<Zona>();
            try
            {
                listaZona = dtz.LlenarDropDownListZona();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return listaZona;
        }
    }
}
