using System;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Negocio;

namespace ProyectoWebMininosUCA
{
    public partial class Site_Admin : System.Web.UI.MasterPage
    {
        ContactoNegocio cng = new ContactoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] == null)
            {
                Response.Redirect("~/");
            }
            getUserAvatar();
        }

        protected Contacto CargarContacto()
        {
            return cng.ObtenerContactoNegocio();
        }

        protected bool IsUserAdmin()
        {
            return (bool)Session["IsCurrentLoggedOnUserAdmin"];
        }

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            Session["IsCurrentUserEdit"] = true;
            Session["DatosUsuario"] = (Usuario)Session["CurrentLoggedOnUser"];
            Response.Redirect("~/wfEditarUsuario");
        }

        protected void btnLogoff_Click(object sender, EventArgs e)
        {
            Session["LoggedIn"] = null;
            Response.Redirect("~/");
        }

        protected String getUserNombre()
        {
            Usuario user = (Usuario)Session["CurrentLoggedOnUser"];

            return user.nombre + " " + user.apellido;
        }

        protected string getUserAvatar()
        {
            Usuario user = (Usuario)Session["CurrentLoggedOnUser"];
            byte[] fotoBytes = user.fotoId;


            // Convertir los bytes de la foto a una imagen y asignarla al control Image
            if (fotoBytes != null && fotoBytes.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(fotoBytes))
                {
                    System.Drawing.Image imagen = System.Drawing.Image.FromStream(ms);
                    string base64String = Convert.ToBase64String(fotoBytes);
                    string formatoImagen = ObtenerFormatoImagen(fotoBytes);
                    string imageUrl = $"data:image/{formatoImagen.ToLower()};base64,{base64String}";
                    return imageUrl;
                }
            }
            else
            {
                return "images/avatar_placeholder.png";
            }
        }

        public string ObtenerFormatoImagen(byte[] imagenBytes)
        {
            using (MemoryStream ms = new MemoryStream(imagenBytes))
            {
                // Leer los primeros bytes del archivo para identificar el formato
                byte[] header = new byte[4];
                ms.Read(header, 0, 4);

                // Verificar el formato basado en los bytes del encabezado
                if (EsFormatoJPEG(header))
                {
                    return "jpeg";
                }
                else if (EsFormatoPNG(header))
                {
                    return "png";
                }
                else if (EsFormatoGIF(header))
                {
                    return "gif";
                }
                else
                {
                    // Otros formatos de imagen no reconocidos
                    return string.Empty;
                }
            }
        }

        public bool EsFormatoJPEG(byte[] header)
        {
            return header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF && header[3] == 0xE0;
        }

        public bool EsFormatoPNG(byte[] header)
        {
            return header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47;
        }

        public bool EsFormatoGIF(byte[] header)
        {
            return header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38;
        }
    }
}
