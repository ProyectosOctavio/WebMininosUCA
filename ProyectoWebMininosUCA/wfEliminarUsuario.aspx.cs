using Datos;
using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfEliminarUsuario : System.Web.UI.Page
    {
        UsuarioNegocio ngu = new UsuarioNegocio();

        RolNegocio rolnegocio = new RolNegocio();

        protected void CargarRol()
        {
            try
            {
                var roles = rolnegocio.ListaRol();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var rolesConNumeros = roles.Select((r, index) => new
                {
                    NumeroRol = index + 1,
                    RolId = r.id,
                    RolNombre = r.nombre

                });

                ddlRolEliminar.DataSource = rolesConNumeros;
                ddlRolEliminar.DataValueField = "RolId";
                ddlRolEliminar.DataTextField = "RolNombre";
                ddlRolEliminar.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarRol();


            }


        }
        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }


        protected void RecuperarDatos()
        {
            Usuario data_usuario = (Usuario)Session["DatosUsuarioEliminar"];

            this.txtIdUsuarioEliminar.Text = data_usuario.id.ToString();
            this.txtNombreEliminar.Text = data_usuario.nombre.ToString();
            this.txtApellidoEliminar.Text = data_usuario.apellido.ToString();
            this.txtEmailEliminar.Text = data_usuario.email.ToString();
            this.txtTelefonoEliminar.Text = data_usuario.telefonoCel.ToString();
            this.txtNombreUsuarioEliminar.Text = data_usuario.username.ToString();
            this.txtClaveEliminar.Text = data_usuario.pw.ToString();
            this.ddlRolEliminar.Text = data_usuario.rolId.ToString();


            using (var contexto = new mininosDatabaseEntities())
            {

                var usuario = contexto.Usuario.Include("AES").FirstOrDefault(u => u.id == data_usuario.id);
                if (usuario != null)
                {
                    var primerAES = usuario.AES.FirstOrDefault();
                    AesDatos aesDatos = new AesDatos();
                    string token = primerAES.token;
                    string iv = primerAES.iv;

                    string claveEncriptada = data_usuario.pw.ToString();
                    string claveDesencriptada = aesDatos.Decrypt_Aes(claveEncriptada, token, iv);
                    this.txtClaveEliminar.Text = claveDesencriptada;
                }             

            }
            // Obtener la foto del usuario en fotobytes
            byte[] fotoBytes = ObtenerFotoDeBaseDeDatosPorId(data_usuario.id);


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

        public byte[] ObtenerFotoDeBaseDeDatosPorId(int usuarioId)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                // Buscar el usuario por el ID en la base de datos
                Usuario usuario = contexto.Usuario.FirstOrDefault(u => u.id == usuarioId && u.estado != 3);
                if (usuario != null)
                {
                    return usuario.fotoId;
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



        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Usuario usuario_delete = new Usuario();
            usuario_delete.id = Int32.Parse(this.txtIdUsuarioEliminar.Text);
            usuario_delete.estado = 3;

            ngu.NG_EliminarUsuario(usuario_delete);
            Response.Redirect("~/wfGestionarUsuario.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarUsuario.aspx");
        }
    }
}
