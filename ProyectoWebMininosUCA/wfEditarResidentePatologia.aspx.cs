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
    public partial class wfEditarResidentePatologia : System.Web.UI.Page
    {
        ResidentePatologiaNegocio ngrp = new ResidentePatologiaNegocio();
        ResidenteNegocio ngr = new ResidenteNegocio();
        PatologiaNegocio ngp = new PatologiaNegocio();

        protected void CargarResidentes()
        {
            try
            {
                var residentes = ngr.ListaResidente();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var residentesConNumeros = residentes.Select((r, index) => new
                {
                    NumeroResidente = index + 1,
                    ResidenteId = r.id,
                    ResidenteNombre = r.nombre

                });

                ddlResidenteIdEdit.DataSource = residentesConNumeros;
                ddlResidenteIdEdit.DataValueField = "ResidenteId";
                ddlResidenteIdEdit.DataTextField = "ResidenteNombre";
                ddlResidenteIdEdit.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }

        protected void CargarPatologias()
        {
            try
            {
                var patologias = ngp.ListaPatologia();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var patologiasConNumeros = patologias.Select((p, index) => new
                {
                    NumeroPatologia = index + 1,
                    PatologiaId = p.id,
                    PatolofiaDescripcion = p.descripcion

                });

                ddlPatologiaIdEdit.DataSource = patologiasConNumeros;
                ddlPatologiaIdEdit.DataValueField = "PatologiaId";
                ddlPatologiaIdEdit.DataTextField = "PatolofiaDescripcion";
                ddlPatologiaIdEdit.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarPatologias();
                CargarResidentes();

            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }
        protected void RecuperarDatos()
        {
            ResidentePatologia data_residentePatologia = (ResidentePatologia)Session["DatosResidentePatologia"];



            this.txtId_residentePatologiaEdit.Text = data_residentePatologia.id.ToString();
            this.ddlPatologiaIdEdit.Text = data_residentePatologia.patologiaId.ToString();
            this.ddlResidenteIdEdit.Text = data_residentePatologia.residenteId.ToString();
        }

        protected void btnEditarResidentePatologia_Click(object sender, EventArgs e)
        {
            //validaciones
            if (string.IsNullOrWhiteSpace(ddlResidenteIdEdit.SelectedValue) || string.IsNullOrWhiteSpace(ddlPatologiaIdEdit.SelectedValue))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (ngrp.ValidarResidentePatologiaExistente(ddlResidenteIdEdit.SelectedIndex, ddlPatologiaIdEdit.SelectedIndex))
            {
                MostrarAdvertencia("El residente y la patologia ya estan relacionados, por favor, elija otras opciones.", true);
                return;
            }
            int patologiaIdSeleccionado = Convert.ToInt32(ddlPatologiaIdEdit.SelectedValue);
            int residenteIdSeleccionado = Convert.ToInt32(ddlResidenteIdEdit.SelectedValue);

            ResidentePatologia residentePatologia_editar = new ResidentePatologia();
            residentePatologia_editar.id = Int32.Parse(this.txtId_residentePatologiaEdit.Text);
            residentePatologia_editar.residenteId = residenteIdSeleccionado;
            residentePatologia_editar.patologiaId = patologiaIdSeleccionado;
            residentePatologia_editar.estado = 2;

            try
            {
                ngrp.NG_EditarResidentePatologia(residentePatologia_editar);
            }
            catch (Exception)
            {

                throw;
            }
            MostrarAdvertencia("Datos editados correctamente.", false);
            Response.Redirect("~/wfGestResidentePatologia.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestResidentePatologia.aspx");
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