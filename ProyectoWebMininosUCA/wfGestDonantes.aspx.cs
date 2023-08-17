using Entidades;
using Negocio;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestDonante : System.Web.UI.Page
    {
        DonanteNegocio ngd = new DonanteNegocio();

        public object SqlHelper { get; private set; }

        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "DonanteId", HeaderText = "Id" },
            new { NombreCampo = "DonanteNombre", HeaderText = "Nombre" },
            new { NombreCampo = "DonanteApellido", HeaderText = "Apellido" },
            new { NombreCampo = "DonanteCorreo", HeaderText = "Correo"},
            new { NombreCampo = "DonanteTelefono", HeaderText = "Telefono"},
            new { NombreCampo = "DonanteAlias", HeaderText = "Alias" },
            new { NombreCampo = "DonantePais", HeaderText = "Pais" },
            new { NombreCampo = "DonanteCiudad", HeaderText = "Ciudad" },
            new { NombreCampo = "DonanteEstado", HeaderText = "Estado" },
        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvDonante.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }

        private void listarDonantes()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from donante in mininosDatabaseEntities.Donante
                            where donante.estado != 3
                            orderby donante.id descending
                            select new
                            {
                                DonanteId = donante.id,
                                DonanteNombre = donante.nombre,
                                DonanteApellido = donante.apellido,
                                DonanteCorreo = donante.correo,
                                DonanteTelefono = donante.numTelefono,
                                DonanteAlias = donante.alias,
                                DonantePais = donante.pais,
                                DonanteCiudad = donante.ciudad,
                                DonanteEstado = donante.estado,
                            };

                var result = query.ToList();

                gvDonante.AutoGenerateColumns = false;
                gvDonante.DataSource = result;
                gvDonante.DataBind();
            }
        }

        private void GuardarDonante()
        {


            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text) || string.IsNullOrWhiteSpace(txtPais.Text) || string.IsNullOrWhiteSpace(txtCiudad.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }

            if (!ValidarCorreoElectronico(txtCorreo.Text))
            {
                MostrarAdvertencia("El correo electrónico ingresado no es válido, trate nuevamente.", true);
                return;
            }

            string telefono = txtTelefono.Text.Trim();
            if (!string.IsNullOrEmpty(telefono))
            {
                // Validar el formato del número de teléfono utilizando una expresión regular
                string pattern = @"^\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}[-.\s]?\d{1,9}$";
                if (!Regex.IsMatch(telefono, pattern))
                {
                    MostrarAdvertencia("El número de teléfono ingresado no es válido, trate nuevamente.", true);
                    return;
                }
            }


            Donante modelo = new Donante()
            {
                alias = txtAlias.Text,
                apellido = txtApellido.Text,
                ciudad = txtCiudad.Text,
                correo = txtCorreo.Text,
                nombre = txtNombre.Text,
                numTelefono = txtTelefono.Text,
                pais = txtPais.Text,
                estado = 1
            };
            ngd.GuardarDonante(modelo);

            listarDonantes();
            MostrarAdvertencia("Datos guardados correctamente.", false);

            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtAlias.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtPais.Text = string.Empty;
            txtTelefono.Text = string.Empty;

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

        private bool ValidarCorreoElectronico(string correo)
        {
            string pattern = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(correo);
        }



        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtAlias.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtPais.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            pnlAdvertencia.Visible = false;
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AgregarColumnas();


            }



            listarDonantes();
        }

        protected void gv_Donante_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDonante.PageIndex = e.NewPageIndex;
            listarDonantes();
            pnlAdvertencia.Visible = false;

        }

        protected void gvDonante_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (gvDonante.HeaderRow != null)
            {
                gvDonante.HeaderRow.Cells[1].Style.Add("display", "none");
                gvDonante.HeaderRow.Cells[9].Style.Add("display", "none");
            }
            foreach (GridViewRow row in gvDonante.Rows)
            {
                row.Cells[1].Style.Add("display", "none");
                row.Cells[9].Style.Add("display", "none");

            }
        }
        protected void guardarDonante_Click(object sender, EventArgs e)
        {

            GuardarDonante();
        }

        protected void limpiarDonante_Click(object sender, EventArgs e)
        {

            LimpiarCampos();
        }


        protected void btnEditarDonante_Click(object sender, EventArgs e)
        {

            string nombre, apellido, correo, alias, ciudad, pais, telefono;

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
                Console.WriteLine("El valor en selectedRow.Cells[11].Text no es un número entero válido.");
            }

            
            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            correo = selectedRow.Cells[4].Text;
            telefono = selectedRow.Cells[5].Text;
            alias = selectedRow.Cells[6].Text;
            pais = selectedRow.Cells[7].Text;
            ciudad = selectedRow.Cells[8].Text;


            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtCorreo.Text = correo;
            this.txtTelefono.Text = telefono;
            this.txtAlias.Text = alias;
            this.txtPais.Text = pais;
            this.txtCiudad.Text = ciudad;


            Donante donanteEdit = new Donante()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                correo = this.txtCorreo.Text,
                numTelefono = this.txtTelefono.Text,
                alias = this.txtAlias.Text,
                pais = this.txtPais.Text,
                ciudad = this.txtCiudad.Text,
                estado = 2,
            };

            Session["DonanteDatos"] = donanteEdit;
            Response.Redirect("~/wfEditarDonante.aspx");
        }


        protected void btnEliminarDonante_Click(object sender, EventArgs e)
        {

            string nombre, apellido, correo, alias, ciudad, pais, telefono;

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
                Console.WriteLine("El valor en selectedRow.Cells[11].Text no es un número entero válido.");
            }

            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            correo = selectedRow.Cells[4].Text;
            telefono = selectedRow.Cells[5].Text;
            alias = selectedRow.Cells[6].Text;
            pais = selectedRow.Cells[7].Text;
            ciudad = selectedRow.Cells[8].Text;


            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtCorreo.Text = correo;
            this.txtTelefono.Text = telefono;
            this.txtAlias.Text = alias;
            this.txtPais.Text = pais;
            this.txtCiudad.Text = ciudad;


            Donante donanteEliminar = new Donante()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                correo = this.txtCorreo.Text,
                numTelefono = this.txtTelefono.Text,
                alias = this.txtAlias.Text,
                pais = this.txtPais.Text,
                ciudad = this.txtCiudad.Text,
                estado = 2,
            };


            Session["DonanteDatos"] = donanteEliminar;
            Response.Redirect("~/wfEliminarDonante.aspx");
        }

    }
}