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
    public class RolNegocio
    {
        RolDatos dtr = new RolDatos();
        public List<Rol> ListaRol()
        {
            List<Rol> Rol = dtr.listarRol();
            return Rol;
        }



        public void GuardarRol(Rol modelo)
        {
            try
            {
                dtr.guardarRol(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Rol: " + e.StackTrace);
            }
        }

        public void NG_EditarRol(Rol modelo)
        {
            try
            {
                dtr.EditarRol(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Rol - Editar : " + e.StackTrace);
            }
        }

        public void NG_EliminarRol(Rol modelo)
        {
            try
            {
                dtr.EliminarRol(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Rol - Eliminar : " + e.StackTrace);
            }
        }

        public bool ValidarNombreExistente(string Nombre)
        {
            bool existe = false;
            try
            {
                existe = dtr.ExisteNombre(Nombre);
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

