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
    public partial class wfGestInfoBancaria : System.Web.UI.Page
    {
        InfoBancariaNegocio ngi = new InfoBancariaNegocio();
        public object SqlHelper { get; private set; }
        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "InfoBancariaId", HeaderText = "Id" },
            new { NombreCampo = "InfoBancariaBanco", HeaderText = "Banco" },
            new { NombreCampo = "InfoBancariaNumeroCuenta", HeaderText = "Numero Cuenta" },
            new { NombreCampo = "InfoBancariaMoneda", HeaderText = "Moneda" },
            new { NombreCampo = "PatologiaEstado", HeaderText = "estado" },
        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvInfoBancaria.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }

        private void listarInfoBancarias()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from infoBancaria in mininosDatabaseEntities.InfoBancaria
                            where infoBancaria.estado != 3
                            select new
                            {
                                InfoBancariaId = infoBancaria.id,
                                InfoBancariaBanco = infoBancaria.banco,
                                InfoBancariaNumeroCuenta = infoBancaria.numeroCuenta,
                                InfoBancariaMoneda = infoBancaria.moneda,
                                PatologiaEstado = infoBancaria.estado,
                            };

                var result = query.ToList();

                gvInfoBancaria.AutoGenerateColumns = false;
                gvInfoBancaria.DataSource = result;
                gvInfoBancaria.DataBind();
            }
        }

        private void GuardarInfoBancaria()
        {
            //Obtener la cantidad de cuentas bancarias guardadas en la base de datos
            int numeroFilas = ObtenerNumeroFilasInfoBanco();
            //validaciones
            if (string.IsNullOrWhiteSpace(txtBanco.Text) || string.IsNullOrWhiteSpace(txtNumeroCuenta.Text) || string.IsNullOrWhiteSpace(txtMoneda.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (ngi.ValidarInfoBancariaExistente(txtNumeroCuenta.Text))
            {
                MostrarAdvertencia("La informacion bancaria ya existe, por favor, ingrese otra descripcion.", true);
                return;
            }
            if (numeroFilas >= 3)
            {
                MostrarAdvertencia("Ya se han alcanzado el maximo de cuentas bancarias!!", true);
                return;
            }

            InfoBancaria modelo = new InfoBancaria()
            {
                banco = txtBanco.Text,
                numeroCuenta = txtNumeroCuenta.Text,
                moneda = txtMoneda.Text,
                estado = 1
            };

            ngi.GuardarInfoBancaria(modelo);
            listarInfoBancarias();
            MostrarAdvertencia("Datos guardados correctamente.", false);

        }

        private int ObtenerNumeroFilasInfoBanco()
        {
            using (var context = new mininosDatabaseEntities()) 
            {
                return context.InfoBancaria.Count();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AgregarColumnas();

            }

            listarInfoBancarias();

        }

        protected void btnGuardarInfoBancaria_Click(object sender, EventArgs e)
        {
            GuardarInfoBancaria();
        }

        protected void gvInfoBancaria_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[5].Visible = false;
        }

        protected void btnEditarInfoBancaria_Click(object sender, EventArgs e)
        {


            string banco, numeroCuenta, moneda;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_InfoBancaria.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            banco = selectedRow.Cells[2].Text;
            numeroCuenta = selectedRow.Cells[3].Text;
            moneda = selectedRow.Cells[4].Text;


            //Mandando datos a los campos
            this.txtId_InfoBancaria.Text = id.ToString();
            this.txtBanco.Text = banco;
            this.txtNumeroCuenta.Text = numeroCuenta;
            this.txtMoneda.Text = moneda;

            InfoBancaria infoBancariaEdit = new InfoBancaria()
            {

                id = Int32.Parse(this.txtId_InfoBancaria.Text),
                banco = this.txtBanco.Text,
                numeroCuenta = this.txtNumeroCuenta.Text,
                moneda = this.txtMoneda.Text,
                estado = 2
            };
            Session["DatosInfoBancaria"] = infoBancariaEdit;
            Response.Redirect("~/wfEditarInfoBancaria.aspx");
        }

        protected void btnEliminarInfoBancaria_Click(object sender, EventArgs e)
        {

            string banco, numeroCuenta, moneda;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_InfoBancaria.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            banco = selectedRow.Cells[2].Text;
            numeroCuenta = selectedRow.Cells[3].Text;
            moneda = selectedRow.Cells[4].Text;


            //Mandando datos a los campos
            this.txtId_InfoBancaria.Text = id.ToString();
            this.txtBanco.Text = banco;
            this.txtNumeroCuenta.Text = numeroCuenta;
            this.txtMoneda.Text = moneda;

            InfoBancaria infoBancariaEliminar = new InfoBancaria()
            {

                id = Int32.Parse(this.txtId_InfoBancaria.Text),
                banco = this.txtBanco.Text,
                numeroCuenta = this.txtNumeroCuenta.Text,
                moneda = this.txtMoneda.Text,
                estado = 2
            };
            Session["DatosInfoBancaria"] = infoBancariaEliminar;
            Response.Redirect("~/wfEliminarInfoBancaria.aspx");
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