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
    public partial class wfEliminarRol : System.Web.UI.Page
    {
        RolNegocio ngr = new RolNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }
        protected void RecuperarDatos()
        {
            Rol data_Rol = (Rol)Session["DatosRolEliminar"];

            this.txtIdRolEliminar.Text = data_Rol.id.ToString();
            this.txtDescripcionEliminar.Text = data_Rol.descripcion.ToString();
            this.txtNombreEliminar.Text = data_Rol.nombre.ToString();
            

        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Rol Rol_eliminar = new Rol();
            Rol_eliminar.id = Int32.Parse(this.txtIdRolEliminar.Text);
            Rol_eliminar.estado = 3;

            ngr.NG_EliminarRol(Rol_eliminar);
            Response.Redirect("~/wfGestionarRol.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarRol.aspx");
        }
    }
}