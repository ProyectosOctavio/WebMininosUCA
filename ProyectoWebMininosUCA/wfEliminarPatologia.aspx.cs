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
    public partial class wfEliminarPatologia : System.Web.UI.Page
    {
        PatologiaNegocio ngp = new PatologiaNegocio();
        
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
            Patologia data_patologia = (Patologia)Session["DatosPatologiaEliminar"];
            this.txtId_patologiaEliminar.Text = data_patologia.id.ToString();
            this.txtDescripcionEliminar.Text = data_patologia.descripcion.ToString();
        }

        protected void btnEliminarPatologia_Click(object sender, EventArgs e)
        {
            Patologia patologia_delete = new Patologia();
            patologia_delete.id = Int32.Parse(this.txtId_patologiaEliminar.Text);
            patologia_delete.estado = 3;

            ngp.NG_EliminarPatologia(patologia_delete);
            Response.Redirect("~/wfGestPatologias.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestPatologias.aspx");
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