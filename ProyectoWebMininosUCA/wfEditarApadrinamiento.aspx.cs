using System;
using Entidades;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ProyectoWebMininosUCA
{
    public partial class wfEditarApadrinamiento : System.Web.UI.Page
    {
        DonacionNegocio ngu = new DonacionNegocio();
        ResidenteNegocio residentenegocio = new ResidenteNegocio();

        protected void CargarResidente()
        {
            try
            {
                var residentes = residentenegocio.ListaResidente();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var rolesConNumeros = residentes.Select((r, index) => new
                {
                    NumeroRol = index + 1,
                    ResidenteId = r.id,
                    ResidenteNombre = r.nombre

                });

                ddlResidente.DataSource = rolesConNumeros;
                ddlResidente.DataValueField = "ResidenteId";
                ddlResidente.DataTextField = "ResidenteNombre";
                ddlResidente.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }


        protected void RecuperarDatos()
        {
            ResidenteDonante data_rd = (ResidenteDonante)Session["DatosApadrinarEditar"];



            this.txtId.Text = data_rd.id.ToString();
            this.txtDonanteId.Text = data_rd.donanteId.ToString();
            this.txtResidenteId.Text = data_rd.residenteId.ToString();



            Donante data_donante = (Donante)Session["DatosDonanteEditar"];

            this.txtDonanteId.Text = data_donante.id.ToString();
            this.txtNombreDonante.Text = data_donante.nombre.ToString();
            this.txtApellido.Text = data_donante.apellido.ToString();
            this.txtAliasDonante.Text = data_donante.alias.ToString();

            Residente data_residente = (Residente)Session["DatosResidenteEditar"];

            this.txtResidenteId.Text = data_residente.id.ToString();
            this.ddlResidente.Text = data_residente.nombre.ToString();


        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            ResidenteDonante rd_editar = new ResidenteDonante();
            rd_editar.id = Int32.Parse(this.txtId.Text);
            rd_editar.estado = 1;
            rd_editar.residenteId = Int32.Parse(this.ddlResidente.Text);
        

            try
            {
                ngu.NG_EditarApadrinamiento(rd_editar);
            }

            catch (Exception)
            {
                throw;
            }
            Response.Redirect("~/wfGestionarApadrinamiento.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarApadrinamiento.aspx");
        }



        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                CargarResidente();


            }

        }
    }
}
