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
    public partial class wfEditarInfoBancaria : System.Web.UI.Page
    {

        InfoBancariaNegocio ngi = new InfoBancariaNegocio();

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
            InfoBancaria data_infoBancaria = (InfoBancaria)Session["DatosInfoBancaria"];
            this.txtId_InfoBancariaEdit.Text = data_infoBancaria.id.ToString();
            this.txtBancoEdit.Text = data_infoBancaria.banco.ToString();
            this.txtNumeroCuentaEdit.Text = data_infoBancaria.numeroCuenta.ToString();
            this.txtMonedaEdit.Text = data_infoBancaria.moneda.ToString();
        }

        protected void btnEditarInfoBancaria_Click(object sender, EventArgs e)
        {
            //Validaciones
            if (string.IsNullOrWhiteSpace(txtBancoEdit.Text) || string.IsNullOrWhiteSpace(txtNumeroCuentaEdit.Text) || string.IsNullOrWhiteSpace(txtMonedaEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (ngi.ValidarInfoBancariaExistente(txtNumeroCuentaEdit.Text))
            {
                MostrarAdvertencia("La informacion bancaria ya existe, por favor, ingrese otra descripcion.", true);
                return;
            }

            InfoBancaria infoBancaria_editar = new InfoBancaria();
            infoBancaria_editar.id = Int32.Parse(this.txtId_InfoBancariaEdit.Text);
            infoBancaria_editar.banco = this.txtBancoEdit.Text;
            infoBancaria_editar.numeroCuenta = this.txtNumeroCuentaEdit.Text;
            infoBancaria_editar.moneda = this.txtMonedaEdit.Text;
            infoBancaria_editar.estado = 2;

            try
            {
                ngi.NG_EditarInfoBancaria(infoBancaria_editar);
            }
            catch (Exception)
            {

                throw;
            }
            MostrarAdvertencia("Datos editados correctamente.", false);
            Response.Redirect("~/wfGestInfoBancaria.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestInfoBancaria.aspx");
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