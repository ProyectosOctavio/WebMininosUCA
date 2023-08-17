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
    public partial class wfEliminarEspecia : System.Web.UI.Page
    {
        DonacionNegocio ngu = new DonacionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RecuperarDatos()
        {
            DonacionEspecies data_especie = (DonacionEspecies)Session["DatosEspecieEliminar"];



            this.txtIdEliminar.Text = data_especie.id.ToString();
            this.txtCantidadEliminar.Text = data_especie.cantidad.ToString();
            this.txtTipoEspecieEliminar.Text = data_especie.tipoEspecie.ToString();
            this.txtUnidadMedidaEliminar.Text = data_especie.unidadMedida.ToString();


            Donante data_donante = (Donante)Session["DatosDonanteEspecieEliminar"];

            this.txtIdDonanteEliminar.Text = data_donante.id.ToString();
            this.txtNombreEliminar.Text = data_donante.nombre.ToString();
            this.txtApellidoEliminar.Text = data_donante.apellido.ToString();
            this.txtAliasEliminar.Text = data_donante.alias.ToString();

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            DonacionEspecies especie_delete = new DonacionEspecies();
            especie_delete.id = Int32.Parse(this.txtIdEliminar.Text);
            especie_delete.estado = 3;

            ngu.NG_EliminarDonacionEspecies(especie_delete);
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