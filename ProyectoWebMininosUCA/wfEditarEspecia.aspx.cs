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
    public partial class wfEditarEspecia : System.Web.UI.Page
    { 
        DonacionNegocio ngu = new DonacionNegocio();
    
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RecuperarDatos()
        {
            DonacionEspecies data_especie = (DonacionEspecies)Session["DatosEspecie"];



            this.txtIdEdit.Text = data_especie.id.ToString();
            this.txtCantidadEdit.Text = data_especie.cantidad.ToString();
            this.txtTipoEspecieEdit.Text = data_especie.tipoEspecie.ToString();
            this.txtUnidadMedidaEdit.Text = data_especie.unidadMedida.ToString();


            Donante data_donante = (Donante)Session["DatosDonanteEspecie"];

            this.txtIdDonanteEdit.Text = data_donante.id.ToString();
            this.txtNombreEdit.Text = data_donante.nombre.ToString();
            this.txtApellidoEdit.Text = data_donante.apellido.ToString();
            this.txtAliasEdit.Text = data_donante.alias.ToString();

        }




        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);

        }


        protected void btnEditarEspecia_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtCantidadEdit.Text) || string.IsNullOrWhiteSpace(txtTipoEspecieEdit.Text) || string.IsNullOrWhiteSpace(txtUnidadMedidaEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return;
            }


            if (!string.IsNullOrEmpty(txtCantidadEdit.Text))
            {
                if (!double.TryParse(txtCantidadEdit.Text, out _))
                {
                    MostrarAdvertencia("El campo Cantidad debe contener un valor numérico válido.", true);
                    return;
                }
            }

            DateTime fecha_creacion = DateTime.Today;
            DonacionEspecies especie_editar = new DonacionEspecies();
            especie_editar.id = Int32.Parse(this.txtIdEdit.Text);
            especie_editar.cantidad = Double.Parse(this.txtCantidadEdit.Text);
            especie_editar.tipoEspecie = this.txtTipoEspecieEdit.Text;
            especie_editar.unidadMedida = this.txtUnidadMedidaEdit.Text;
            especie_editar.fecha = fecha_creacion;
            especie_editar.estado = 1;


            try
            {
                ngu.NG_EditarDonacionEspecies(especie_editar);
            }
            catch (Exception)
            {
                throw;
            }

            MostrarAdvertencia("Datos editados correctamente.", false);
            Response.Redirect("~/wfGestionarEspecia.aspx");


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
