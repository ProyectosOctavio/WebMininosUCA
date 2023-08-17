using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Entidades;
using Negocio;
using System;
using System.Text.RegularExpressions;
using Microsoft.Ajax.Utilities;

namespace ProyectoWebMininosUCA
{
    public partial class wfEditarContacto : System.Web.UI.Page
    {
        ContactoNegocio ngc = new ContactoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void RecuperarDatos()
        {
            Contacto contacto = (Contacto)Session["DatosContacto"];

            if (contacto != null)
            {
                this.txtIdEdit.Text = contacto.id.ToString();
                this.txtTelefonoEdit.Text = contacto.telefono;

                if (!string.IsNullOrEmpty(contacto.correo))
                {
                    if (!contacto.correo.Equals("&nbsp;")) 
                    {
                        this.txtCorreoEdit.Text = contacto.correo;
                    }
                }
                if (!string.IsNullOrEmpty(contacto.correo2)) 
                {
                    if (!contacto.correo2.Equals("&nbsp;"))
                    {
                        this.txtCorreo2Edit.Text = contacto.correo2;
                    }
                }
                if (!string.IsNullOrEmpty(contacto.insta))
                {
                    if (!contacto.insta.Equals("&nbsp;"))
                    {
                        this.txtInstaEdit.Text = contacto.insta;
                    }
                }
                if (!string.IsNullOrEmpty(contacto.twitter))
                {
                    if (!contacto.twitter.Equals("&nbsp;"))
                    {
                        this.txtTwitterEdit.Text = contacto.twitter;
                    }
                }
                if (!string.IsNullOrEmpty(contacto.facebook))
                {
                    if (!contacto.facebook.Equals("&nbsp;"))
                    {
                        this.txtFacebookEdit.Text = contacto.facebook;
                    }
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }

        private void MostrarAdvertencia(string mensaje, bool esError)
        {
            pnlAdvertencia.Visible = true;
            lblAdvertencia.InnerText = mensaje;

            if (esError)
            {
                pnlAdvertencia.Attributes["class"] = "alert alert-danger";
            }
            else
            {
                pnlAdvertencia.Attributes["class"] = "alert alert-success";
            }
        }

        protected void btnEditarContacto_Click(object sender, EventArgs e)
        {
            string telefono = txtTelefonoEdit.Text.Trim();
            string correo1 = txtCorreoEdit.Text.Trim();
            string correo2 = txtCorreo2Edit.Text.Trim();

            if (!string.IsNullOrEmpty(telefono))
            {
                if (!ValidarTelefono(telefono))
                {
                    MostrarAdvertencia("El número de teléfono ingresado no es válido, trate nuevamente.", true);
                    return;
                }
            } else {
                MostrarAdvertencia("Debe de ingresar un teléfono como contacto", true);
                return;
            }

            if (!string.IsNullOrEmpty(correo1))
            {
                if (!ValidarCorreo(correo1))
                {
                    MostrarAdvertencia("El número de teléfono ingresado no es válido, trate nuevamente.", true);
                    return;
                }
            } 
            else if (!string.IsNullOrEmpty(correo2))
            {
                if (!ValidarCorreo(correo2))
                {
                    MostrarAdvertencia("El número de teléfono ingresado no es válido, trate nuevamente.", true);
                    return;
                }
            }
            else
            {
                MostrarAdvertencia("Debe de ingresar al menos un correo como contacto", true);
                return;
            }

            Contacto nuevoContacto = new Contacto();
            if (int.TryParse(this.txtIdEdit.Text, out int id))
            {
                nuevoContacto.id = id;
                nuevoContacto.telefono = telefono;
                nuevoContacto.correo = correo1;
                nuevoContacto.correo2 = correo2;
                nuevoContacto.twitter = this.txtTwitterEdit.Text.Trim();
                nuevoContacto.insta = this.txtInstaEdit.Text.Trim();
                nuevoContacto.facebook = this.txtFacebookEdit.Text.Trim();

                ngc.EditarContactoNegocio(nuevoContacto);
                Response.Redirect("~/wfGestionarContacto.aspx");
            }
        
        }

        private bool ValidarTelefono(string telefono)
        {
            // Validar el formato del número de teléfono utilizando una expresión regular
            string pattern = @"^\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,9}$";
            return Regex.IsMatch(telefono, pattern);
        }

        private bool ValidarCorreo(string correo)
        {
            // Validar el formato del correo utilizando una expresión regular
            string pattern = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";
            return Regex.IsMatch(correo, pattern);
        }
    }
}