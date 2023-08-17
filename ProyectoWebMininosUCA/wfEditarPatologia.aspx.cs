using Entidades;
using Datos;
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
    public partial class wfEditarPatologia : System.Web.UI.Page
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
            Patologia data_patologia = (Patologia)Session["DatosPatologia"];
            this.txtId_patologiaEdit.Text = data_patologia.id.ToString();
            this.txtDescripcionEdit.Text = data_patologia.descripcion.ToString();
        }

        protected void btnEditarPatologia_Click(object sender, EventArgs e)
        {
            // Obtener el valor del TextBox
            string valor = txtDescripcionEdit.Text;
            //validaciones
            if (string.IsNullOrWhiteSpace(txtDescripcionEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (ngp.ValidarPatologiaExistente(txtDescripcionEdit.Text))
            {
                MostrarAdvertencia("La patologia ya existe, por favor, ingrese otra descripcion.", true);
                return;
            }
            if (valor.Length >= 20)
            {
                MostrarAdvertencia("La descripcion es demasiado larga.", true);
                return;
            }

            Patologia patologia_editar = new Patologia();
            patologia_editar.id = Int32.Parse(this.txtId_patologiaEdit.Text);
            patologia_editar.descripcion = this.txtDescripcionEdit.Text;
            patologia_editar.estado = 2;

            try
            {
                ngp.NG_EditarPatologia(patologia_editar);
            }
            catch (Exception)
            {

                throw;
            }
            MostrarAdvertencia("Datos editados correctamente.", false);
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