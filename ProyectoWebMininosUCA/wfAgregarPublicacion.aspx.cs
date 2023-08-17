using System;
using Entidades;
using Negocio;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfAgregarPublicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregarPublicacion_Click(object sender, EventArgs e)
        {
            GuardarPublicacion();           
        }

        private void GuardarPublicacion()
        {

            PublicacionNegocio np = new PublicacionNegocio();

            // Obtener el valor del TextBox
            string valor = txtTituloPublicacion.Text;
            //validaciones
            if (string.IsNullOrWhiteSpace(txtTituloPublicacion.Text) || string.IsNullOrWhiteSpace(txtTipoPublicacion.Text) || string.IsNullOrWhiteSpace(txtContenido.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }


            //Fecha de ingreso de la publicacion
            DateTime fecha_ingreso = DateTime.Today;

            //Tipo de Publicacion
            string tipo = txtTipoPublicacion.Text;

            //Foto
            byte[] fotoImagen = null;
            byte[] fotoBytes = null;
            if (archivoPublicacion.HasFile)
            {
                using (BinaryReader br = new BinaryReader(archivoPublicacion.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(archivoPublicacion.PostedFile.ContentLength);
                }
            }

            // Validar si se seleccionó una foto
            if (fotoBytes == null || fotoBytes.Length == 0)
            {
                MostrarAdvertencia("Debe seleccionar una foto.", true);
                return;
            }

            // Convertir los bytes de la foto a una imagen y asignarla al control Image
            if (fotoBytes.Length > 0)
            {
                fotoImagen = fotoBytes;
                using (MemoryStream ms = new MemoryStream(fotoImagen))
                {
                    System.Drawing.Image imagen = System.Drawing.Image.FromStream(ms);
                    string base64String = Convert.ToBase64String(fotoBytes);
                    string formatoImagen = ObtenerFormatoImagen(fotoBytes);
                    string imageUrl = $"data:image/{formatoImagen.ToLower()};base64,{base64String}";
                    imgFoto.ImageUrl = imageUrl;
                }
            }


            Publicacion pbct = new Publicacion()
            {
                fotoPublicacion = fotoBytes,
                titulo = txtTituloPublicacion.Text,
                tipo = txtTipoPublicacion.Text,
                contenido = txtContenido.Text,
                fecha = fecha_ingreso,
                estado = 1
            };

            np.GuardarPublicacion(pbct);
            MostrarAdvertencia("Datos guardados correctamente.", false);
            Session["DatosPublicacion"] = pbct;
            string mensajeGuardar = "La imagen se ha guardado correctamente.";
            bool esErrorGuardar = false;
            Response.Redirect("wfGestPublicaciones.aspx?mensajeGuardar=" + Server.UrlEncode(mensajeGuardar) + "&esErrorGuardar=" + esErrorGuardar);
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

        private void LimpiarCampos()
        {
            txtTituloPublicacion.Text = string.Empty;
            txtTipoPublicacion.Text = string.Empty;
            txtContenido.Text = string.Empty;
            pnlAdvertencia.Visible = false;
        }

        protected void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
