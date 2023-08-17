using Entidades;
using Datos;
using Negocio;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;

namespace ProyectoWebMininosUCA
{
    public partial class wfEditarZona : System.Web.UI.Page
    {
        ZonaNegocio ngz = new ZonaNegocio();
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
            Zona data_Zona = (Zona)Session["DatosZona"];
            this.txtId_ZonaEdit.Text = data_Zona.id.ToString();
            this.txtNombreEdit.Text = data_Zona.nombre.ToString();
        }

        protected void btnEditarZona_Click(object sender, EventArgs e)
        {
            // Obtener el valor del TextBox
            string valor = txtNombreEdit.Text;
            //validaciones
            if (string.IsNullOrWhiteSpace(txtNombreEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (ngz.ValidarZonaExistente(txtNombreEdit.Text))
            {
                MostrarAdvertencia("La Zona ya existe, por favor, ingrese otro Nombre.", true);
                return;
            }
            if (valor.Length >= 20)
            {
                MostrarAdvertencia("El nombre es demasiado largo.", true);
                return;
            }

            Zona Zona_editar = new Zona();
            Zona_editar.id = Int32.Parse(this.txtId_ZonaEdit.Text);
            Zona_editar.nombre = this.txtNombreEdit.Text;
            Zona_editar.estado = 2;

            try
            {
                ngz.NG_EditarZona(Zona_editar);
            }
            catch (Exception)
            {

                throw;
            }
            MostrarAdvertencia("Datos editados correctamente.", false);
            Response.Redirect("~/wfGestZonas.aspx");

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestZonas.aspx");
        }

        private void MostrarAdvertencia(string mensaje, bool esError)//bool
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