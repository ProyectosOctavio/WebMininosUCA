using Datos;
using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfRoles : System.Web.UI.Page
    {
        RolNegocio ngr = new RolNegocio();

        public object SqlHelper { get; private set; }

        private void listarRol()
        {
            gvRol.AutoGenerateColumns = true;
            gvRol.DataSource = ngr.ListaRol();

            gvRol.DataBind();

        }



        private void GuardarRol()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }

            if (ngr.ValidarNombreExistente(txtNombre.Text))
            {
                MostrarAdvertencia("El Nombre ya existe, por favor, elija otra Nombre.", true);
                return;
            }

            Rol modelo = new Rol()
            {
                descripcion = txtDescripcion.Text,
                nombre = txtNombre.Text,
                estado = 1,

            };
            ngr.GuardarRol(modelo);
            listarRol();
            limpiarRol();
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                listarRol();
            }
        }

        protected void gvRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRol.PageIndex = e.NewPageIndex;
            listarRol();
            pnlAdvertencia.Visible = false;
        }

        protected void guardarRol_Click(object sender, EventArgs e)
        {
            GuardarRol();
        }

        protected void gvRol_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (gvRol.HeaderRow != null)
            {
                gvRol.HeaderRow.Cells[1].Style.Add("display", "none");
                gvRol.HeaderRow.Cells[4].Style.Add("display", "none");
                
            }

            foreach (GridViewRow row in gvRol.Rows)
            {
                row.Cells[1].Style.Add("display", "none");
                row.Cells[4].Style.Add("display", "none");
            
            }
        }

        protected void btnEditarRol_Click(object sender, EventArgs e)
        {

            string nombre, descripcion;
            

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

            descripcion = selectedRow.Cells[3].Text;
            nombre = selectedRow.Cells[2].Text;


            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtDescripcion.Text = descripcion;
            this.txtNombre.Text = nombre;
            


            Rol RolEdit = new Rol()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                descripcion = this.txtDescripcion.Text,           
                estado = 2,                
            };

            Session["DatosRol"] = RolEdit;
            Response.Redirect("~/wfEditarRol.aspx");
        }


        protected void btnEliminarRol_Click(object sender, EventArgs e)
        {

            string nombre, descripcion;


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

            descripcion = selectedRow.Cells[3].Text;
            nombre = selectedRow.Cells[2].Text;


            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtDescripcion.Text = descripcion;
            this.txtNombre.Text = nombre;



            Rol RolEliminar = new Rol()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                descripcion = this.txtDescripcion.Text,
                estado = 2,
            };

            Session["DatosRolEliminar"] = RolEliminar;
            Response.Redirect("~/wfEliminarRol.aspx");
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBusqueda = txtBusqueda.Text.Trim();
            List<Rol> roles = ngr.ListaRol();
            List<Rol> rolesFiltrados = roles.Where(r => r.nombre.Contains(textoBusqueda)).ToList();

            gvRol.DataSource = rolesFiltrados;
            gvRol.DataBind();
        }

        protected void LimpiarRol_Click(object sender, EventArgs e)
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            pnlAdvertencia.Visible = false; // Ocultar el panel de advertencia
            lblAdvertencia.InnerText = string.Empty; // Limpiar el mensaje de advertencia
        }

        private void limpiarRol()
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            pnlAdvertencia.Visible = false; // Ocultar el panel de advertencia
            lblAdvertencia.InnerText = string.Empty; // Limpiar el mensaje de advertencia
        }


    }
}