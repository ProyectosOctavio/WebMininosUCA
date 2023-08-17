using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ProyectoWebMininosUCA
{
    public partial class wfEditarDonante : System.Web.UI.Page
    {
        DonanteNegocio ngd = new DonanteNegocio();

        

        protected void Page_Load(object sender, EventArgs e)
        {
       

        }

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);

            txtCorreoEdit.Attributes.Add("onkeyup", "ValidarCorreo(this.value);");
        }


        protected void RecuperarDatos()
        {
            Donante data_donante = (Donante)Session["DonanteDatos"];



            this.txtIdEdit.Text = data_donante.id.ToString();
            this.txtNombreEdit.Text = data_donante.nombre.ToString();
            this.txtApellidoEdit.Text = data_donante.apellido.ToString();
            this.txtCorreoEdit.Text = data_donante.correo.ToString();
            this.txtTelefonoEdit.Text = data_donante.numTelefono.ToString();
            this.txtAliasEdit.Text = data_donante.alias.ToString();
            this.txtPaisEdit.Text = data_donante.pais.ToString();
            this.txtCiudadEdit.Text = data_donante.ciudad.ToString();
           
        }




        protected void btnEditarDonante_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNombreEdit.Text) || string.IsNullOrWhiteSpace(txtApellidoEdit.Text) || string.IsNullOrWhiteSpace(txtCorreoEdit.Text) || string.IsNullOrWhiteSpace(txtTelefonoEdit.Text) || string.IsNullOrWhiteSpace(txtPaisEdit.Text) || string.IsNullOrWhiteSpace(txtCiudadEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }

            if (!ValidarCorreoElectronico(txtCorreoEdit.Text))
            {
                MostrarAdvertencia("El correo electrónico ingresado no es válido, trate nuevamente.", true);
                return;
            }

            string telefono = txtTelefonoEdit.Text.Trim();
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

            
            Donante donante_editar = new Donante();

            donante_editar.id = Int32.Parse(this.txtIdEdit.Text);
            donante_editar.nombre = this.txtNombreEdit.Text;
            donante_editar.apellido = this.txtApellidoEdit.Text;
            donante_editar.correo = this.txtCorreoEdit.Text;
            donante_editar.numTelefono = this.txtTelefonoEdit.Text;
            donante_editar.alias = this.txtAliasEdit.Text;
            donante_editar.pais = this.txtPaisEdit.Text;
            donante_editar.ciudad = this.txtCiudadEdit.Text;
            donante_editar.estado = 2;
             

           try
            {
                ngd.NG_EditarDonante(donante_editar);
            }
            catch (Exception)
            {
                throw;
            }

            MostrarAdvertencia("Datos editados correctamente.", false);
            Response.Redirect("~/wfGestDonantes.aspx");


        }


        private void MostrarAdvertencia(string mensaje, bool esError)//bool
        {
            pnlAdvertenciaEdit.Visible = true;
            lblAdvertenciaEdit.InnerText = mensaje;

            if (esError)
            {
                pnlAdvertenciaEdit.Attributes["class"] = "alert alert-danger";
            }
            else
            {
                pnlAdvertenciaEdit.Attributes["class"] = "alert alert-success";
            }
        }

        private bool ValidarCorreoElectronico(string correo)
        {
            string pattern = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(correo);
        }
    }
}