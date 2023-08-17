using Datos;
using Entidades;
using System;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ProyectoWebMininosUCA
{
    public partial class wfNuevaContraseña : System.Web.UI.Page
    {


        AES aes = new AES();
        AesDatos aesDatos = new AesDatos();

        protected void Page_Load(object sender, EventArgs e)
        {

            btnGuardarPw.Click += btnGuardarPw_Click;
        }



        protected void btnGuardarPw_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text;


            // Obtener el token y la fecha de creación desde la base de datos
            string tokenFromDataBase = ObtenerTokenDesdeLaBaseDeDatos(correo);
            DateTime fechaCreacion = ObtenerFechaCreacionTokenDesdeLaBaseDeDatos(correo); // Modificado: se obtiene como string

            // Comparar la fecha de creación del token con la fecha y hora actual
            TimeSpan diferencia = DateTime.Now - fechaCreacion;

            if (diferencia.TotalHours >= 24)
            {
                // El enlace de restablecimiento ha expirado, mostrar un mensaje al usuario
                ShowMessageBox("El enlace de restablecimiento ha expirado. Por favor, solicite un nuevo restablecimiento de contraseña", true);

                // Ocultar los botones y campos de ingreso
                txtCorreo.Visible = false;
                txtNuevapPw.Visible = false;
                txtCorfirmacionPw.Visible = false;
                btnGuardarPw.Visible = false;
            }
            else
            {
                // El token es válido, permitir al usuario restablecer la contraseña
                txtCorreo.Visible = true;
                txtNuevapPw.Visible = true;
                txtCorfirmacionPw.Visible = true;
                btnGuardarPw.Visible = true;
                CambioPW();
            }

        }

        protected void CambioPW()
        {
            string correo = txtCorreo.Text;
            string pw = txtNuevapPw.Text;
            string pwConfir = txtCorfirmacionPw.Text;

            bool correoIngresado = string.IsNullOrEmpty(correo);
            bool pwIngresado = string.IsNullOrEmpty(pw);
            bool pwConfirB = string.IsNullOrEmpty(pwConfir);

            if (correoIngresado || pwIngresado || pwConfirB)
            {
                ShowMessageBox("Complete todos los campos", true);
                return;
            }

            if (!ValidarCorreoElectronico(correo))
            {
                ShowMessageBox("Introduzca un correo válido", true);
                return;
            }

            if (pw != pwConfir)
            {
                ShowMessageBox("Las contraseñas no coinciden", true);
                return;
            }

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var usuario = contexto.Usuario.FirstOrDefault(u => u.email == correo);
                if (usuario != null)
                {
                    // Obtener el vector de inicialización almacenado en la tabla dbo.AES para el usuario
                    var aesExistente = usuario.AES.FirstOrDefault();
                    if (aesExistente != null)
                    {
                        string privateKey = aesExistente.token;
                        string myIV = aesExistente.iv;

                        // Encriptar la nueva contraseña utilizando el mismo vector de inicialización (myIV)
                        string contraseñaEncriptada = aesDatos.Encrypt_Aes(pw, privateKey, myIV);

                        usuario.pw = contraseñaEncriptada;

                        try
                        {
                            contexto.SaveChanges();
                            ShowMessageBox("La contraseña se ha actualizado exitosamente", false);
                        }
                        catch (DbEntityValidationException ex)
                        {
                            // Recorrer los errores de validación y mostrar los mensajes de error
                            foreach (var entityValidationError in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationError.ValidationErrors)
                                {
                                    Response.Write($"Error de validación: {validationError.ErrorMessage}");
                                }
                            }

                            ShowMessageBox("Se produjo un error al guardar los cambios", true);
                        }
                    }
                    else
                    {
                        ShowMessageBox("No se encontró el vector de inicialización para el usuario", true);
                    }
                }
                else
                {
                    ShowMessageBox("No se pudo encontrar el usuario con el correo electrónico especificado", true);
                }
            }
        }




        public string ObtenerTokenDesdeLaBaseDeDatos(string correo)
        {
            string resToke = "";

            using (var contexto = new mininosDatabaseEntities())
            {
                string query = "SELECT dbo.Usuario.TokenRestablecimiento FROM dbo.Usuario WHERE email = @correo;";
                SqlCommand command = new SqlCommand(query, (SqlConnection)contexto.Database.Connection);
                command.Parameters.AddWithValue("@correo", correo);

                contexto.Database.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        resToke = (string)reader["TokenRestablecimiento"];


                    }

                }
            }

            return resToke;
        }

        public DateTime ObtenerFechaCreacionTokenDesdeLaBaseDeDatos(string correo)
        {
            DateTime fecha = DateTime.MinValue;
            using (var contexto = new mininosDatabaseEntities())
            {
                string query = "SELECT dbo.Usuario.FechaCreacionToken FROM dbo.Usuario WHERE email = @Correo;";
                SqlCommand command = new SqlCommand(query, (SqlConnection)contexto.Database.Connection);
                command.Parameters.AddWithValue("@Correo", correo);
                contexto.Database.Connection.Open();


                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fecha = (DateTime)reader["FechaCreacionToken"];
                    }

                }
                else
                {

                }



                contexto.Database.Connection.Close();
            }

            return fecha;
        }

        private void ShowMessageBox(string mensaje, bool esError)
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
            if (string.IsNullOrEmpty(correo))
                return false;

            try
            {
                // Utilizar el método de .NET para validar el formato del correo electrónico
                var addr = new System.Net.Mail.MailAddress(correo);
                return addr.Address == correo;
            }
            catch
            {
                return false;
            }
        }
    }
}
