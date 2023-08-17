using System;
using Entidades;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfEliminarApadrinamiento : System.Web.UI.Page
    {
        DonacionNegocio ngu = new DonacionNegocio();

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }


        protected void RecuperarDatos()
        {
            ResidenteDonante data_rd = (ResidenteDonante)Session["DatosApadrinarEliminar"];



            this.txtId.Text = data_rd.id.ToString();
            this.txtDonanteId.Text = data_rd.donanteId.ToString();
            this.txtResidenteId.Text = data_rd.residenteId.ToString();



            Donante data_donante = (Donante)Session["DatosDonanteEliminar"];

            this.txtDonanteId.Text = data_donante.id.ToString();
            this.txtNombreDonante.Text = data_donante.nombre.ToString();
            this.txtApellido.Text = data_donante.apellido.ToString();
            this.txtAliasDonante.Text = data_donante.alias.ToString();

            Residente data_residente = (Residente)Session["DatosResidenteEliminar"];

            this.txtResidenteId.Text = data_residente.id.ToString();
            this.txtResidente.Text = data_residente.nombre.ToString();
            

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ResidenteDonante rd_delete = new ResidenteDonante();
            rd_delete.id = Int32.Parse(this.txtId.Text);
            rd_delete.estado = 3;

            ngu.NG_EliminarApadrinamiento(rd_delete);
            Response.Redirect("~/wfGestionarApadrinamiento.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarApadrinamiento.aspx");
        }



        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}