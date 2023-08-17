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
    public partial class wfEliminarZona : System.Web.UI.Page
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
            Zona data_Zona = (Zona)Session["DatosZonaEliminar"];
            this.txtId_ZonaEliminar.Text = data_Zona.id.ToString();
            this.txtNombreEliminar.Text = data_Zona.nombre.ToString();
        }

        protected void btnEliminarPatologia_Click(object sender, EventArgs e)
        {
            Zona zona_delete = new Zona();
            zona_delete.id = Int32.Parse(this.txtId_ZonaEliminar.Text);
            zona_delete.estado = 3;

            ngz.NG_EliminarZona(zona_delete);
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