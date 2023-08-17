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
    public partial class wfEliminarOpcion : System.Web.UI.Page
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
            Opcion data_Opcion = (Opcion)Session["DatosOpcionEliminar"];

            this.txtIdOpcionEliminar.Text = data_Opcion.id.ToString();
            this.txtAccionEliminar.Text = data_Opcion.accion.ToString();
            this.txtDescripcionEliminar.Text = data_Opcion.descripcion.ToString();
            this.txtUrlEliminar.Text = data_Opcion.url.ToString();


        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Opcion Opcion_eliminar = new Opcion();
            Opcion_eliminar.id = Int32.Parse(this.txtIdOpcionEliminar.Text);
            Opcion_eliminar.accion = this.txtAccionEliminar.Text;
            Opcion_eliminar.descripcion = this.txtDescripcionEliminar.Text;
            Opcion_eliminar.url = this.txtUrlEliminar.Text;
            Opcion_eliminar.estado = 3;

            ngo.NG_EliminarOpcion(Opcion_eliminar);
            Response.Redirect("~/wfGestionarOpcion.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarOpcion.aspx");
        }
    }
}