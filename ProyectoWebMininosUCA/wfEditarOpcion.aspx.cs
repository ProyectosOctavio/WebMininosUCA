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
    public partial class wfEditarOpcion : System.Web.UI.Page
    {
        OpcionNegocio ngo = new OpcionNegocio();
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
            Opcion data_Opcion = (Opcion)Session["DatosOpcion"];



            this.txtIdOpcionEdit.Text = data_Opcion.id.ToString();
            this.txtAccionEdit.Text = data_Opcion.accion.ToString();
            this.txtDescripcionEdit.Text = data_Opcion.descripcion.ToString();
            this.txtUrlEdit.Text = data_Opcion.url.ToString();

        }
        protected void btnEditarOpcion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAccionEdit.Text) || string.IsNullOrWhiteSpace(txtDescripcionEdit.Text) || string.IsNullOrWhiteSpace(txtUrlEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }



            Opcion Opcion_editar = new Opcion();
            Opcion_editar.id = Int32.Parse(this.txtIdOpcionEdit.Text);
            Opcion_editar.accion = this.txtAccionEdit.Text;
            Opcion_editar.descripcion = this.txtDescripcionEdit.Text;
            Opcion_editar.url = this.txtUrlEdit.Text;
            Opcion_editar.estado = 2;





            try
            {
                ngo.NG_EditarOpcion(Opcion_editar);
            }
            catch (Exception)
            {

                throw;
            }
            MostrarAdvertencia("Datos editados correctamente.", false);
            Response.Redirect("~/wfGestionarOpcion.aspx");


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
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarOpcion.aspx");
        }
    }
}