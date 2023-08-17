using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Runtime.Remoting.Contexts;

namespace Negocio
{
    public class ContactoNegocio
    {
        ContactoDatos dtc = new ContactoDatos();

        public List<Contacto> ListaContacto()
        {
            try
            {
                List<Contacto> contactos = dtc.listarContacto();
                return contactos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer la base de datos", ex);
            }
        }

        public void EditarContactoNegocio(Contacto modelo)
        {
            try
            {
                dtc.EditarContacto(modelo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Contacto ObtenerContactoNegocio() 
        {
            try
            { 
                return dtc.getContacto();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}