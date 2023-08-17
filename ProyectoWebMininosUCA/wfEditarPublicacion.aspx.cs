using Entidades;
using System;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;

namespace ProyectoWebMininosUCA
{
    public partial class wfEditarPublicacion : System.Web.UI.Page
    {
        PublicacionNegocio dc = new PublicacionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEditarPublicacion_Click(object sender, EventArgs e)
        {
            // Obtener el valor del TextBox
            //string valor = txtNombreEdit.Text;
            //validaciones
            if (string.IsNullOrWhiteSpace(txtTituloPublicacionEdit.Text) || string.IsNullOrWhiteSpace(txtContenidoEdit.Text) || string.IsNullOrWhiteSpace(txtTipoPublicacionEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }

            //Fecha de edicion de la publicacion
            DateTime fecha_ingreso = DateTime.Today;

            Publicacion publicacion_editar = new Publicacion();
            publicacion_editar.id = Int32.Parse(this.txtIdPublicacionEdit.Text);
            publicacion_editar.titulo = this.txtTituloPublicacionEdit.Text;
            publicacion_editar.tipo = txtTipoPublicacionEdit.Text;
            publicacion_editar.contenido = this.txtContenidoEdit.Text;
            publicacion_editar.fecha = fecha_ingreso;
            publicacion_editar.estado = 2;
            byte[] fotoBytes = null;
            byte[] fotoImagen = null;
            if (archivoPublicacionEdit.HasFile)
            {
                using (BinaryReader br = new BinaryReader(archivoPublicacionEdit.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(archivoPublicacionEdit.PostedFile.ContentLength);
                }

                // Convertir los bytes de la foto a una imagen y asignarla al control Image
                if (fotoBytes != null && fotoBytes.Length > 0)
                {
                    fotoImagen = fotoBytes;
                    using (MemoryStream ms = new MemoryStream(fotoImagen))
                    {
                        System.Drawing.Image imagen = System.Drawing.Image.FromStream(ms);
                        string base64String = Convert.ToBase64String(fotoBytes);
                        string formatoImagen = ObtenerFormatoImagen(fotoBytes);
                        string imageUrl = $"data:image/{formatoImagen.ToLower()};base64,{base64String}";
                        imgFotoEdit.ImageUrl = imageUrl;
                    }
                }
            }
            else
            {
                // Si no se carga una nueva imagen, mantener la imagen existente sin cambios
                fotoBytes = publicacion_editar.fotoPublicacion;
            }

            publicacion_editar.fotoPublicacion = fotoBytes;

            try
            {
                dc.EditarPublicacion(publicacion_editar);
            }
            catch (Exception)
            {

                throw;
            }

            //MostrarAdvertencia("Publicacion editada correctamente.", false);
            string mensajeEditar = "La imagen se ha editado correctamente.";
            bool esErrorEditar = false;
            Response.Redirect("wfGestPublicaciones.aspx?mensajeEditar=" + Server.UrlEncode(mensajeEditar) + "&esErrorEditar=" + esErrorEditar);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }

        protected void RecuperarDatos()
        {
            Publicacion data_publicacion = (Publicacion)Session["DatosPublicacion"];

            this.txtIdPublicacionEdit.Text = data_publicacion.id.ToString();
            this.txtTituloPublicacionEdit.Text = data_publicacion.titulo.ToString();
            this.txtTipoPublicacionEdit.Text = data_publicacion.tipo.ToString();
            this.txtContenidoEdit.Text = data_publicacion.contenido.ToString();

            // Obtener la foto del usuario en fotobytes
            byte[] fotoBytes = ObtenerFotoDeBaseDeDatosPorId(data_publicacion.id);


            // Convertir los bytes de la foto a una imagen y asignarla al control Image
            if (fotoBytes != null && fotoBytes.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(fotoBytes))
                {
                    System.Drawing.Image imagen = System.Drawing.Image.FromStream(ms);
                    string base64String = Convert.ToBase64String(fotoBytes);
                    string formatoImagen = ObtenerFormatoImagen(fotoBytes);
                    string imageUrl = $"data:image/{formatoImagen.ToLower()};base64,{base64String}";
                    imgFotoEdit.ImageUrl = imageUrl;
                }
            }
        }
    


        public byte[] ObtenerFotoDeBaseDeDatosPorId(int publicacionId)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                // Buscar el usuario por el ID en la base de datos
                Publicacion publicacion = contexto.Publicacion.FirstOrDefault(r => r.id == publicacionId && r.estado != 3);
                if (publicacion != null)
                {
                    return publicacion.fotoPublicacion;
                }

                return null;
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
