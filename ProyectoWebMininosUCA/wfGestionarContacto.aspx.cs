using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Entidades;
using Negocio;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestionarContacto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AgregarColumnas();



            }

            listarContactos();

        }

        public object SqlHelper { get; private set; }


        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "ContactoId", HeaderText = "Id" },
            new { NombreCampo = "ContactoTelefono", HeaderText = "Telefono" },
            new { NombreCampo = "ContactoCorreo", HeaderText = "Primer correo" },
            new { NombreCampo = "ContactoCorreo2", HeaderText = "Segundo correo" },
            new { NombreCampo = "ContactoTwitter", HeaderText = "Twitter" },
            new { NombreCampo = "ContactoInsta", HeaderText = "Instagram" },
            new { NombreCampo = "ContactoFacebook", HeaderText = "Facebook" },

        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvContacto.Columns.Add(boundField);

                }

                GridViewColumnsAdded = true;
            }
        }

        private void listarContactos()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from contacto in mininosDatabaseEntities.Contacto
                            select new
                            {
                                ContactoId = contacto.id,
                                ContactoTelefono = contacto.telefono,
                                ContactoCorreo = contacto.correo,
                                ContactoCorreo2 = contacto.correo2,
                                ContactoTwitter = contacto.twitter,
                                ContactoInsta = contacto.insta,
                                ContactoFacebook = contacto.facebook,


                            };

                var result = query.ToList();

                gvContacto.AutoGenerateColumns = false;
                gvContacto.DataSource = result;
                gvContacto.DataBind();
            }
        }

        protected void gvContacto_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (gvContacto.HeaderRow != null)
            {
                gvContacto.HeaderRow.Cells[1].Style.Add("display", "none");


                foreach (GridViewRow row in gvContacto.Rows)
                {
                    row.Cells[1].Style.Add("display", "none");

                }


            }
        }

        protected void btnEditarContacto_Click(object sender, EventArgs e)
        {

            string telefono, correo, correo2, twitter, insta, facebook;



            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {

                this.txtId.Text = id.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            telefono = selectedRow.Cells[2].Text;
            correo = selectedRow.Cells[3].Text;
            correo2 = selectedRow.Cells[4].Text;
            twitter = selectedRow.Cells[5].Text;
            insta = selectedRow.Cells[6].Text;
            facebook = selectedRow.Cells[7].Text;




            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtTelefono.Text = telefono;
            this.txtCorreo.Text = correo;
            this.txtCorreo2.Text = correo2;
            this.txtTwitter.Text = twitter;
            this.txtInsta.Text = insta;
            this.txtFacebook.Text = facebook;





            Contacto contactoEdit = new Contacto()
            {
                id = Int32.Parse(this.txtId.Text),
                telefono = this.txtTelefono.Text,
                correo = this.txtCorreo.Text,
                correo2 = this.txtCorreo2.Text,
                twitter = this.txtTwitter.Text,
                insta = this.txtInsta.Text,
                facebook = this.txtFacebook.Text,


            };


            Session["DatosContacto"] = contactoEdit;
            Response.Redirect("~/wfEditarContacto.aspx");
        }
    }
}