using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfEliminarDonante : System.Web.UI.Page
    {
        DonanteNegocio ngd = new DonanteNegocio();

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
            Donante data_donante = (Donante)Session["DonanteDatos"];

            this.txtIdEliminar.Text = data_donante.id.ToString();
            this.txtNombreEliminar.Text = data_donante.nombre.ToString();
            this.txtApellidoEliminar.Text = data_donante.apellido.ToString();
            this.txtCorreoEliminar.Text = data_donante.correo.ToString();
            this.txtTelefonoEliminar.Text = data_donante.numTelefono.ToString();
            this.txtAliasEliminar.Text = data_donante.alias.ToString();
            this.txtPaisEliminar.Text = data_donante.pais.ToString();
            this.txtCiudadEliminar.Text = data_donante.ciudad.ToString();

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Donante donante_delete = new Donante();
            donante_delete.id = Int32.Parse(this.txtIdEliminar.Text);
            donante_delete.estado = 3;

            ngd.NG_EliminarDonante(donante_delete);
            Response.Redirect("~/wfGestDonantes.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestDonantes.aspx");
        }
    }
}
