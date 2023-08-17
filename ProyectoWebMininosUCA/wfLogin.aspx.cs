using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace ProyectoWebMininosUCA
{
    public partial class wfLogin : System.Web.UI.Page
    {
        UsuarioNegocio ngu = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return;
            }
            string user = this.txtUsername.Text;
            string pwd = this.txtPassword.Text;

            bool entrar = ngu.ValidarCredencialesNegocio(user, pwd);
            if (entrar)
            {
                Session["LoggedIn"] = true;
                Usuario usuario = GetUsuarioPorUsername(user);
                bool isAdmin = VerificarAdminUsuario(user);

                if (usuario != null)
                {
                    Session["CurrentLoggedOnUser"] = usuario;
                    Session["IsCurrentLoggedOnUserAdmin"] = isAdmin;
                    Response.Redirect("~/wfInicioAdmin");
                }
                else
                {
                    MostrarAdvertencia("Hubo un error al iniciar sesion", true);
                }
            }
            else
            {
                MostrarAdvertencia("Las credenciales son incorrectas", true);
            }
        }

        private Usuario GetUsuarioPorUsername(String user)
        {
            return ngu.GetUsuarioPorUsernameNegocio(user);
        }

        private bool VerificarAdminUsuario(String user)
        {
            return ngu.VerificarAdminUsuarioNegocio(user);
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
    }
}
