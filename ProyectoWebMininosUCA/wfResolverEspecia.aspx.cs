using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using Entidades;
using Negocio;

namespace ProyectoWebMininosUCA
{
    public partial class wfResolverEspecia : System.Web.UI.Page
    {
        DonacionNegocio ngu = new DonacionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RecuperarDatos()
        {
            DonacionEspecies data_especie = (DonacionEspecies)Session["DatosEspecieResolver"];



            this.txtIdResolver.Text = data_especie.id.ToString();
            this.txtCantidadResolver.Text = data_especie.cantidad.ToString();
            this.txtTipoEspecieResolver.Text = data_especie.tipoEspecie.ToString();
            this.txtUnidadMedidaResolver.Text = data_especie.unidadMedida.ToString();


            Donante data_donante = (Donante)Session["DatosDonanteEspecieResolver"];

            this.txtIdDonanteResolver.Text = data_donante.id.ToString();
            this.txtNombreResolver.Text = data_donante.nombre.ToString();
            this.txtApellidoResolver.Text = data_donante.apellido.ToString();
            this.txtAliasResolver.Text = data_donante.alias.ToString();

        }

        protected void btnResolver_Click(object sender, EventArgs e)
        {
            DonacionEspecies especie_resolver = new DonacionEspecies();
            especie_resolver.id = Int32.Parse(this.txtIdResolver.Text);
            especie_resolver.estado = 2;

            ngu.NG_ResolverDonacionEspecies(especie_resolver);
            Response.Redirect("~/wfGestionarEspecia.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarEspecia.aspx");
        }




        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);

        }

    }


}
