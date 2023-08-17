using Datos;
using Entidades;
using Negocio;
using System;
using Entidades;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace ProyectoWebMininosUCA
{
    public partial class wfOpciones : System.Web.UI.Page
    {
      OpcionNegocio ngo = new OpcionNegocio();

        public object SqlHelper { get; private set; }

        private void listarOpcion()
        {
            gvOpcion.AutoGenerateColumns = true;
            gvOpcion.DataSource = ngo.ListaOpcion();

            gvOpcion.DataBind();

        }




        private void GuardarOpcion()
        {
            if (string.IsNullOrWhiteSpace(txtAccion.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text) || string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }

            if (!ValidarURL(txtUrl.Text))
            {
                MostrarAdvertencia("La URL ingresada no es válida, trate nuevamente.", true);
                return;
            }       

            if (ngo.ValidarAccionExistente(txtAccion.Text))
            {
                MostrarAdvertencia("La Accion ya existe, por favor, elija otra Accion.", true);
                return;
            }

            if (ngo.ValidarURLExistente(txtUrl.Text))
            {
                MostrarAdvertencia("La URL  ya existe, por favor, elija otra URL.", true);
                return;
            }



            Opcion modelo = new Opcion()
            {
                accion = txtAccion.Text,
                descripcion = txtDescripcion.Text,
                url = txtUrl.Text,
                estado = 1,

            };
            ngo.GuardarOpcion(modelo);
            listarOpcion();
            LimpiarOpcion();
            MostrarAdvertencia("Datos guardados correctamente.", false);
        }

        private void MostrarAdvertencia(string mensaje, bool esError)
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
        private bool ValidarURL(string url)
        {
            string pattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(url);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            listarOpcion();
        }

        protected void guardarOpcion_Click(object sender, EventArgs e)
        {
            GuardarOpcion();
        }

        protected void gvOpcion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[5].Visible = false;




        }

        protected void btnEditarOpcion_Click(object sender, EventArgs e)
        {
            string accion, descripcion, url;


            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId.Text = id.ToString();

            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            accion = selectedRow.Cells[3].Text;
            descripcion = selectedRow.Cells[2].Text;
            url = selectedRow.Cells[4].Text;


            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtAccion.Text = accion;
            this.txtDescripcion.Text = descripcion;
            this.txtUrl.Text = url;



            Opcion OpcionEdit = new Opcion()
            {
                id = Int32.Parse(this.txtId.Text),
                accion = this.txtAccion.Text,
                descripcion = this.txtDescripcion.Text,
                url = this.txtUrl.Text,
                estado = 2,
            };

            Session["DatosOpcion"] = OpcionEdit;
            Response.Redirect("~/wfEditarOpcion.aspx");
        }


        protected void btnEliminarOpcion_Click(object sender, EventArgs e)
        {

            string accion, descripcion, url;


            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId.Text = id.ToString();

            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            accion = selectedRow.Cells[3].Text;
            descripcion = selectedRow.Cells[2].Text;
            url = selectedRow.Cells[4].Text;


            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtAccion.Text = accion;
            this.txtDescripcion.Text = descripcion;
            this.txtUrl.Text = url;



            Opcion OpcionEliminar = new Opcion()
            {
                id = Int32.Parse(this.txtId.Text),
                accion = this.txtAccion.Text,
                descripcion = this.txtDescripcion.Text,
                url = this.txtUrl.Text,
                estado = 2,
            };

            Session["DatosOpcionEliminar"] = OpcionEliminar;
            Response.Redirect("~/wfEliminarOpcion.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBusqueda = txtBusqueda.Text.Trim();
            List<Opcion> opciones = ngo.ListaOpcion();
            List<Opcion> opcionesFiltradas = opciones.Where(o => o.accion.Contains(textoBusqueda) || o.descripcion.Contains(textoBusqueda)).ToList();

            gvOpcion.DataSource = opcionesFiltradas;
            gvOpcion.DataBind();
        }

        protected void LimpiarOpcion_Click(object sender, EventArgs e)
        {
            txtId.Text = string.Empty;
            txtAccion.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtUrl.Text = string.Empty;
        }

        private void LimpiarOpcion()
        {
            txtId.Text = string.Empty;
            txtAccion.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtUrl.Text = string.Empty;
        }
    }
}