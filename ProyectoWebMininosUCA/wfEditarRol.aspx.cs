using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProyectoWebMininosUCA
{
    public partial class wfEditarRol : System.Web.UI.Page
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
            Rol data_Rol = (Rol)Session["DatosRol"];

            this.txtIdRolEdit.Text = data_Rol.id.ToString();
            this.txtNombreEdit.Text = data_Rol.nombre.ToString();
            this.txtDescripcionEdit.Text = data_Rol.descripcion.ToString();
        }

        protected void btnEditarRol_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreEdit.Text) || string.IsNullOrWhiteSpace(txtDescripcionEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }

            Rol Rol_editar = new Rol();
            Rol_editar.id = Int32.Parse(this.txtIdRolEdit.Text);
            Rol_editar.nombre = this.txtNombreEdit.Text;
            Rol_editar.descripcion = this.txtDescripcionEdit.Text;
            Rol_editar.estado = 2;

            try
            {
                ngr.NG_EditarRol(Rol_editar);
            }
            catch (Exception)
            {
                throw;
            }
            MostrarAdvertencia("Datos editados correctamente.", false);
            Response.Redirect("~/wfGestionarRol.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarRol.aspx");
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
    }
}
