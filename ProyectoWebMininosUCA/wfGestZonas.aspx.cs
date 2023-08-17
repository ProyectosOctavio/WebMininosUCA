using Datos;
using Entidades;
using Negocio;
using System;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestZonas : System.Web.UI.Page
    {
        ZonaNegocio ngz = new ZonaNegocio();
        public object SqlHelper { get; private set; }
        private bool GridViewColumnsAdded = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AgregarColumnas();

            }

            listarZona();
        }

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "ZonaId", HeaderText = "Id" },
            new { NombreCampo = "ZonaNombre", HeaderText = "Nombre" },
            new { NombreCampo = "ZonaEstado", HeaderText = "estado" },
        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvZona.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }
        private void listarZona()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from Zona in mininosDatabaseEntities.Zona
                            where Zona.estado != 3
                            select new
                            {
                                ZonaId = Zona.id,
                                ZonaNombre = Zona.nombre,
                                ZonaEstado = Zona.estado,
                            };

                var result = query.ToList();

                gvZona.AutoGenerateColumns = false;
                gvZona.DataSource = result;
                gvZona.DataBind();
            }
        }

        private void GuardarZona()
        {
            // Obtener el valor del TextBox
            string valor = txtNombre.Text;
            //validaciones
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (ngz.ValidarZonaExistente(txtNombre.Text))
            {
                MostrarAdvertencia("La Zona ya existe, por favor, ingrese otro Nombre.", true);
                return;
            }
            if (valor.Length >= 20)
            {
                MostrarAdvertencia("El Nombre es demasiado largo.", true);
                return;
            }
            Zona modelo = new Zona()
            {
                nombre = txtNombre.Text,
                estado = 1
            };

            ngz.GuardarZona(modelo);
            listarZona();
            MostrarAdvertencia("Datos guardados correctamente.", false);

        }

        protected void btnGuardarPatologia_Click(object sender, EventArgs e)
        {
            GuardarZona();
        }

        protected void gvZona_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            pnlAdvertencia.Visible = false;
        }

        protected void limpiarCampos_Click(object sender, EventArgs e)
        {

            LimpiarCampos();
        }


        protected void btnEditarZona_Click(object sender, EventArgs e)
        {


            string nombre;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_zona.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            nombre = selectedRow.Cells[2].Text;




            //Mandando datos a los campos
            this.txtId_zona.Text = id.ToString();
            this.txtNombre.Text = nombre;


            Zona ZonaEdit = new Zona()
            {

                id = Int32.Parse(this.txtId_zona.Text),
                nombre = this.txtNombre.Text,
                estado = 2
            };
            Session["DatosZona"] = ZonaEdit;
            Response.Redirect("~/wfEditarZona.aspx");
        }

        protected void btnEliminarPatologia_Click(object sender, EventArgs e)
        {


            string nombre;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_zona.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            nombre = selectedRow.Cells[2].Text;




            //Mandando datos a los campos
            this.txtId_zona.Text = id.ToString();
            this.txtNombre.Text = nombre;


            Zona ZonaEliminar = new Zona()
            {

                id = Int32.Parse(this.txtId_zona.Text),
                nombre = this.txtNombre.Text,
                estado = 2
            };
            Session["DatosZonaEliminar"] = ZonaEliminar;
            Response.Redirect("~/wfEliminarZona.aspx");
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
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string terminoBusqueda = txtBusqueda.Text.Trim();
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from Zona in mininosDatabaseEntities.Zona
                            where Zona.estado != 3 &&
                                  (Zona.nombre.Contains(terminoBusqueda))
                            select new
                            {
                                PatologiaId = Zona.id,
                                PatologiaDescripcion = Zona.nombre,
                                PatologiaEstado = Zona.estado,
                            };

                var result = query.ToList();

                gvZona.AutoGenerateColumns = false;
                gvZona.DataSource = result;
                gvZona.DataBind();
            }
        }
    }
}