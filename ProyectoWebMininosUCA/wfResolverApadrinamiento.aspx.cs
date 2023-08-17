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
    public partial class wfResolverApadrinamiento : System.Web.UI.Page
    {
        DonacionNegocio ngu = new DonacionNegocio();

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }


        protected void RecuperarDatos()
        {
            ResidenteDonante data_rd = (ResidenteDonante)Session["DatosApadrinarResolver"];



            this.txtId.Text = data_rd.id.ToString();
            this.txtDonanteId.Text = data_rd.donanteId.ToString();
            this.txtResidenteId.Text = data_rd.residenteId.ToString();



            Donante data_donante = (Donante)Session["DatosDonanteResolver"];

            this.txtDonanteId.Text = data_donante.id.ToString();
            this.txtNombreDonante.Text = data_donante.nombre.ToString();
            this.txtApellido.Text = data_donante.apellido.ToString();
            this.txtAliasDonante.Text = data_donante.alias.ToString();

            Residente data_residente = (Residente)Session["DatosResidenteResolver"];

            this.txtResidenteId.Text = data_residente.id.ToString();
            this.txtResidente.Text = data_residente.nombre.ToString();


        }

        protected void btnResolver_Click(object sender, EventArgs e)
        {
            ResidenteDonante rd_resolver = new ResidenteDonante();
            rd_resolver.id = Int32.Parse(this.txtId.Text);
            rd_resolver.estado = 2;

            ngu.NG_ResolverApadrinamiento(rd_resolver);
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
