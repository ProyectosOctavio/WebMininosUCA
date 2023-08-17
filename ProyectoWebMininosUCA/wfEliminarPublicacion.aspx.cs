using System;
using Entidades;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ProyectoWebMininosUCA
{
    public partial class wfEliminarPublicacion : System.Web.UI.Page
    {
        PublicacionNegocio dc = new PublicacionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEliminarPublicacion_Click(object sender, EventArgs e)
        {
            Publicacion publicacion_delete = new Publicacion();
            publicacion_delete.id = Int32.Parse(this.txtIdPublicacionEliminar.Text);
            publicacion_delete.estado = 3;

            dc.EliminarPublicacion(publicacion_delete);

            string mensajeEliminar = "La imagen se ha eliminada correctamente.";
            bool esErrorEliminar = false;
            Response.Redirect("wfGestPublicaciones.aspx?mensajeEliminar=" + Server.UrlEncode(mensajeEliminar) + "&esErrorEliminar=" + esErrorEliminar);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }

        protected void RecuperarDatos()
        {
            Publicacion data_publicacion = (Publicacion)Session["DatosPublicacion"];

            this.txtIdPublicacionEliminar.Text = data_publicacion.id.ToString();
            this.txtTituloPublicacionEliminar.Text = data_publicacion.titulo.ToString();
            this.txtTipoPublicacionEliminar.Text = data_publicacion.tipo.ToString();
            this.txtContenidoEliminar.Text = data_publicacion.contenido.ToString();

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
                    imgFotoEliminar.ImageUrl = imageUrl;
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
    }
}