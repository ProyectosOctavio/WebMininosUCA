using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using Entidades;

namespace RecuperarContraseñaMininosUca
{
    public partial class wfRecuperarContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviarSoli_Click(object sender, EventArgs e)
        {
            string correo = txtIdCorreo.Text.Trim(); // Trim the input to remove leading/trailing spaces
            bool ingresoCorreo = string.IsNullOrEmpty(correo);

            if (ingresoCorreo)
            {
                ShowMessageBox("No ha ingresado un correo", true);
                return;
            }
            else
            {
                if (!ValidarCorreoElectronico(correo))
                {
                    ShowMessageBox("El correo electrónico ingresado no es válido, intente nuevamente.", true);
                    return;
                }
            }

            using (var contexto = new mininosDatabaseEntities())
            {
                string query = "SELECT dbo.Usuario.email FROM dbo.Usuario WHERE dbo.Usuario.email = @correo";
                SqlCommand command = new SqlCommand(query, (SqlConnection)contexto.Database.Connection);
                command.Parameters.AddWithValue("@correo", correo);
                contexto.Database.Connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string email = reader.GetString(0);
                            EnviarCorreoElectronico(email);

                        }
                    }
                    else
                    {
                        ShowMessageBox("El correo ingresado no existe, intente de nuevo.", true);
                        return;
                    }
                }


                contexto.Database.Connection.Close();
            }


        }

        private bool ValidarCorreoElectronico(string correo)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(correo);
        }

        public bool EnviarCorreoElectronico(string correoRecibido)
        {
            bool enviado = false;
            SmtpClient smtp = null;
            MailMessage email = null;
            // Enlace de verificación de correo
            string linkHR = "http://localhost:50331/wfNuevaContrase%c3%b1a";

            try
            {
                using (smtp = new SmtpClient())
                {
                    using (email = new MailMessage())
                    {


                        smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587; //465
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential("jdelarocha265@gmail.com", "uechkrclnizdktve");

                        // Generar un nuevo token
                        string token = GenerarToken();

                        // Cuerpo del correo  
                        string myMsg = "<strong>PROCESO DE RESTABLECIMIENTO DE CONTRASEÑA</strong><br><br>";
                        myMsg += "<br>";
                        myMsg += "Estimado usuario, usted ha solicitado el restablecimiento de contraseña";
                        myMsg += "<br>";
                        myMsg += "Haga click en el siguiente enlance: " + GenerarEnlaceRestablecimiento(token) + "<br>";

                        email = new MailMessage();
                        email.To.Add(new MailAddress(correoRecibido));
                        email.From = new MailAddress("jdelarocha265@gmail.com", "MININOS UCA");
                        email.Subject = "Asunto Prueba";
                        email.Priority = System.Net.Mail.MailPriority.Normal;
                        email.Body = myMsg;
                        email.IsBodyHtml = true;
                        smtp.Send(email);
                        enviado = true;
                        ShowMessageBox("Restablecimiento de contraseña en proceso, favor revisar el correo", false);

                    }
                }
            }
            catch (Exception exception)
            {
                ShowMessageBox("Error al restablecer la contraseña" + exception, false);
            }
            return enviado;
        }


        private string GenerarToken()
        {
            string correo = txtIdCorreo.Text;
            string token = Guid.NewGuid().ToString();
            DateTime fechaToken = DateTime.Now;

            using (var contexto = new mininosDatabaseEntities())
            {
                string query = "UPDATE dbo.Usuario SET TokenRestablecimiento = @token, FechaCreacionToken = @fecha WHERE email = @correo;";
                SqlCommand command = new SqlCommand(query, (SqlConnection)contexto.Database.Connection);
                command.Parameters.AddWithValue("@token", token);
                command.Parameters.AddWithValue("@fecha", fechaToken);
                command.Parameters.AddWithValue("@correo", correo);
                contexto.Database.Connection.Open();
                command.ExecuteNonQuery();
                contexto.Database.Connection.Close();
            }

            return token;
        }


        private string GenerarEnlaceRestablecimiento(string token)
        {
            string linkHR = "http://localhost:50331/wfNuevaContrase%c3%b1a?token=" + HttpUtility.UrlEncode(token);
            return linkHR;
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
    }
}