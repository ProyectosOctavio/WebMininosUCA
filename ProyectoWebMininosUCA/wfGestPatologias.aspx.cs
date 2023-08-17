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
    public partial class wfGestPatologias : System.Web.UI.Page
    {
        PatologiaNegocio ngp = new PatologiaNegocio();
        public object SqlHelper { get; private set; }
        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "PatologiaId", HeaderText = "Id" },
            new { NombreCampo = "PatologiaDescripcion", HeaderText = "Descripcion" },
            new { NombreCampo = "PatologiaEstado", HeaderText = "estado" },
        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvPatologia.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }
        private void listarPatologias()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from patologia in mininosDatabaseEntities.Patologia
                            where patologia.estado != 3
                            select new
                            {
                                PatologiaId = patologia.id,
                                PatologiaDescripcion = patologia.descripcion,
                                PatologiaEstado = patologia.estado,
                            };

                var result = query.ToList();

                gvPatologia.AutoGenerateColumns = false;
                gvPatologia.DataSource = result;
                gvPatologia.DataBind();
            }
        }

        private void GuardarPatologia()
        {
            // Obtener el valor del TextBox
            string valor = txtDescripcion.Text;
            //validaciones
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (ngp.ValidarPatologiaExistente(txtDescripcion.Text))
            {
                MostrarAdvertencia("La patologia ya existe, por favor, ingrese otra descripcion.", true);
                return;
            }
            if (valor.Length >= 20) 
            {
                MostrarAdvertencia("La descripcion es demasiado larga.", true);
                return;
            }
            Patologia modelo = new Patologia()
            {
                descripcion = txtDescripcion.Text,
                estado = 1
            };

            ngp.GuardarPatologia(modelo);
            listarPatologias();
            MostrarAdvertencia("Datos guardados correctamente.", false);

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AgregarColumnas();

            }

            listarPatologias();
        }

        protected void btnGuardarPatologia_Click(object sender, EventArgs e)
        {
            GuardarPatologia();
        }

        protected void gvPatologia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
        }

        private void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            pnlAdvertencia.Visible = false;
        }

        protected void limpiarCampos_Click(object sender, EventArgs e)
        {

            LimpiarCampos();
        }


        protected void btnEditarPatologia_Click(object sender, EventArgs e)
        {


            string descripcion;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_patologia.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            descripcion = selectedRow.Cells[2].Text;




            //Mandando datos a los campos
            this.txtId_patologia.Text = id.ToString();
            this.txtDescripcion.Text = descripcion;


            Patologia patologiaEdit = new Patologia()
            {

                id = Int32.Parse(this.txtId_patologia.Text),
                descripcion = this.txtDescripcion.Text,
                estado = 2
            };
            Session["DatosPatologia"] = patologiaEdit;
            Response.Redirect("~/wfEditarPatologia.aspx");
        }

        protected void btnEliminarPatologia_Click(object sender, EventArgs e)
        {


            string descripcion;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_patologia.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            descripcion = selectedRow.Cells[2].Text;




            //Mandando datos a los campos
            this.txtId_patologia.Text = id.ToString();
            this.txtDescripcion.Text = descripcion;


            Patologia patologiaEliminar = new Patologia()
            {

                id = Int32.Parse(this.txtId_patologia.Text),
                descripcion = this.txtDescripcion.Text,
                estado = 2
            };
            Session["DatosPatologiaEliminar"] = patologiaEliminar;
            Response.Redirect("~/wfEliminarPatologia.aspx");
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
                var query = from patologia in mininosDatabaseEntities.Patologia
                            where patologia.estado != 3 &&
                                  (patologia.descripcion.Contains(terminoBusqueda))
                            select new
                            {
                                PatologiaId = patologia.id,
                                PatologiaDescripcion = patologia.descripcion,
                                PatologiaEstado = patologia.estado,
                            };

                var result = query.ToList();

                gvPatologia.AutoGenerateColumns = false;
                gvPatologia.DataSource = result;
                gvPatologia.DataBind();
            }
        }
    }
}