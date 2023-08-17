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
    public partial class wfEliminarResidentePatologia : System.Web.UI.Page
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

                ddlResidenteIdEliminar.DataSource = residentesConNumeros;
                ddlResidenteIdEliminar.DataValueField = "ResidenteId";
                ddlResidenteIdEliminar.DataTextField = "ResidenteNombre";
                ddlResidenteIdEliminar.DataBind();
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

                ddlPatologiaIdEliminar.DataSource = patologiasConNumeros;
                ddlPatologiaIdEliminar.DataValueField = "PatologiaId";
                ddlPatologiaIdEliminar.DataTextField = "PatolofiaDescripcion";
                ddlPatologiaIdEliminar.DataBind();
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
            ResidentePatologia data_residentePatologia = (ResidentePatologia)Session["DatosResidentePatologiaEliminar"];



            this.txtId_residentePatologiaEliminar.Text = data_residentePatologia.id.ToString();
            this.ddlPatologiaIdEliminar.SelectedValue = data_residentePatologia.patologiaId.ToString();
            this.ddlResidenteIdEliminar.SelectedValue = data_residentePatologia.residenteId.ToString();
        }

        protected void btnEliminarResidentePatologia_Click(object sender, EventArgs e)
        {
            ResidentePatologia residentePatologia_delete = new ResidentePatologia();
            residentePatologia_delete.id = Int32.Parse(this.txtId_residentePatologiaEliminar.Text);
            residentePatologia_delete.estado = 3;

            ngrp.NG_EliminarResidentePatologia(residentePatologia_delete);
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