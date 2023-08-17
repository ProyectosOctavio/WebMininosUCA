using Entidades;
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
    public partial class wfEliminarInfoBancaria : System.Web.UI.Page
    {
        InfoBancariaNegocio ngi = new InfoBancariaNegocio();
        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }

        protected void RecuperarDatos()
        {
            InfoBancaria data_infoBancaria = (InfoBancaria)Session["DatosInfoBancaria"];
            this.txtId_InfoBancariaEliminar.Text = data_infoBancaria.id.ToString();
            this.txtBancoEliminar.Text = data_infoBancaria.banco.ToString();
            this.txtNumeroCuentaEliminar.Text = data_infoBancaria.numeroCuenta.ToString();
            this.txtMonedaEliminar.Text = data_infoBancaria.moneda.ToString();
        }

        protected void btnEliminarInfoBancaria_Click(object sender, EventArgs e)
        {
            //Obtener la cantidad de cuentas bancarias guardadas en la base de datos
            int numeroFilas = ObtenerNumeroFilasInfoBanco();
            if (numeroFilas >= 3)
            {
                MostrarAdvertencia("No se puede eliminar el unico registro.", true);
                return;
            }
            InfoBancaria infoBancaria_delete = new InfoBancaria();
            infoBancaria_delete.id = Int32.Parse(this.txtId_InfoBancariaEliminar.Text);
            infoBancaria_delete.estado = 3;

            ngi.NG_EliminarInfoBancaria(infoBancaria_delete);
            Response.Redirect("~/wfGestInfoBancaria.aspx");
        }

        private int ObtenerNumeroFilasInfoBanco()
        {
            using (var context = new mininosDatabaseEntities())
            {
                return context.InfoBancaria.Count();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestInfoBancaria.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

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