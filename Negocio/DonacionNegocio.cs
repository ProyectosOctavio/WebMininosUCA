using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DonacionNegocio
    {
        DonacionDatos dtu = new DonacionDatos();
        public List<DonacionMonetaria> ListaDonacionMonetaria()
        {
            List<DonacionMonetaria> donacionMonetaria = dtu.listarDonacionMonetaria();
            return donacionMonetaria;
        }

        public List<DonacionEspecies> ListaDonacionEspecies()
        {
            List<DonacionEspecies> donacionEspecies = dtu.listarDonacionEspecies();
            return donacionEspecies;
        }

        public List<DonacionEspecies> getDonacionesEspeciesConFechaNegocio(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return dtu.getDonacionesEspeciesConFechaDatos(fechaInicio, fechaFin);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Donante> ListaDonante()
        {
            List<Donante> donante = dtu.listarDonante();
            return donante;
        }

        public List<ResidenteDonante> ListaResidenteDonante()
        {
            List<ResidenteDonante> residentedonante = dtu.listarResidenteDonante();
            return residentedonante;
        }

        public void GuardarDonacionMonetaria(DonacionMonetaria modelo)
        {
            try
            {
                dtu.guardarDonacionMonetaria(modelo);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en NG_Donacion: " + e.StackTrace);
            }
        }

        public void GuardarDonacionEspecies(DonacionEspecies modelo2)
        {
            try
            {
                dtu.guardarDonacionEspecies(modelo2);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion: " + e.StackTrace);
            }
        }

        public void GuardarDonante(Donante modelo3)
        {
            try
            {
                dtu.guardarDonante(modelo3);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion: " + e.StackTrace);
            }
        }

        public void GuardarResidenteDonante(ResidenteDonante modelo4)
        {
            try
            {
                dtu.guardarResidenteDonante(modelo4);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion: " + e.StackTrace);
            }
        }

        public void NG_EditarDonacionMonetaria(DonacionMonetaria modelo)
        {
            try
            {
                dtu.EditarDonacionMonetaria(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion - Editar : " + e.StackTrace);
            }
        }

        public void NG_EditarDonacionEspecies(DonacionEspecies modelo2)
        {
            try
            {
                dtu.EditarDonacionEspecies(modelo2);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion - Editar : " + e.StackTrace);
            }
        }

        public void NG_EditarApadrinamiento(ResidenteDonante modelo3)
        {
            try
            {
                dtu.EditarApadrinamiento(modelo3);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion - Editar : " + e.StackTrace);
            }
        }



        public void NG_EliminarDonacionMonetaria(DonacionMonetaria modelo)
        {
            try
            {
                dtu.EliminarDonacionMonetaria(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion - Eliminar : " + e.StackTrace);
            }
        }

        public void NG_EliminarDonacionEspecies(DonacionEspecies modelo2)
        {
            try
            {
                dtu.EliminarDonacionEspecies(modelo2);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion - Eliminar : " + e.StackTrace);
            }
        }

        public void NG_EliminarApadrinamiento(ResidenteDonante modelo3)
        {
            try
            {
                dtu.EliminarApadrinamiento(modelo3);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion - Eliminar : " + e.StackTrace);
            }
        }

        public void NG_ResolverDonacionMonetaria(DonacionMonetaria modelo)
        {
            try
            {
                dtu.ResolverDonacionMonetaria(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion - Resolver : " + e.StackTrace);
            }
        }

        public void NG_ResolverDonacionEspecies(DonacionEspecies modelo2)
        {
            try
            {
                dtu.ResolverDonacionEspecies(modelo2);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion - Resolver : " + e.StackTrace);
            }
        }

        public void NG_ResolverApadrinamiento(ResidenteDonante modelo3)
        {
            try
            {
                dtu.ResolverApadrinamiento(modelo3);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donacion - Eliminar : " + e.StackTrace);
            }
        }

        public bool ValidarcorreoExistente(string correo)
        {
            bool existe = false;
            try
            {
                existe = dtu.ExisteCorreo(correo);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return existe;
        }

        public bool ValidarAiasExistente(string alias)
        {
            bool existe = false;
            try
            {
                existe = dtu.ExisteAlias(alias);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return existe;
        }

        public bool ValidarTelefonoExistente(string telefono)
        {
            bool existe = false;
            try
            {
                existe = dtu.ExisteTelefono(telefono);
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