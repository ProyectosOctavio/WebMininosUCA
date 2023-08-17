using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class SiteMaster : MasterPage
    {
        ContactoNegocio cng = new ContactoNegocio();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null)
            {
                Response.Redirect("~/wfInicioAdmin");
            }
        }

        protected Contacto CargarContacto() 
        {
            return cng.ObtenerContactoNegocio();
        }
    }
}