using Datos;
using Entidades;
using Negocio;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProyectoWebMininosUCA
{
    public partial class wfEditarUsuario : System.Web.UI.Page
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

                ddlRolEdit.DataSource = rolesConNumeros;
                ddlRolEdit.DataValueField = "RolId";
                ddlRolEdit.DataTextField = "RolNombre";
                ddlRolEdit.DataBind();
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

            txtEmailEdit.Attributes.Add("onkeyup", "ValidarCorreo(this.value);");
        }


        private void RecuperarDatos()
        {
            Usuario data_usuario = (Usuario)Session["DatosUsuario"];

            this.txtIdUsuarioEdit.Text = data_usuario.id.ToString();
            this.txtNombreEdit.Text = data_usuario.nombre.ToString();
            this.txtApellidoEdit.Text = data_usuario.apellido.ToString();
            this.txtEmailEdit.Text = data_usuario.email.ToString();
            this.txtTelefonoEdit.Text = data_usuario.telefonoCel.ToString();
            this.txtNombreUsuarioEdit.Text = data_usuario.username.ToString();

            using (var contexto = new mininosDatabaseEntities())
            {
                var usuario = contexto.Usuario.Include("AES").FirstOrDefault(u => u.id == data_usuario.id);
                if (usuario != null)
                {
                    var primerAES = usuario.AES.FirstOrDefault(); // Obtener el primer objeto AES asociado al usuario

                    if (primerAES != null)
                    {
                        // Desencriptar la contraseña
                        AesDatos aes = new AesDatos();
                        string token = primerAES.token;
                        string iv = primerAES.iv;

                        string claveEncriptada = data_usuario.pw.ToString();
                        string claveDesencriptada = aes.Decrypt_Aes(claveEncriptada, token, iv);
                        this.txtClaveEdit.Text = claveDesencriptada;
                    }

                    this.ddlRolEdit.Text = data_usuario.rolId.ToString();
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
                    imgFotoEdit.ImageUrl = imageUrl;
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



        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNombreEdit.Text) || string.IsNullOrWhiteSpace(txtApellidoEdit.Text) || string.IsNullOrWhiteSpace(txtEmailEdit.Text) || string.IsNullOrWhiteSpace(txtClaveEdit.Text) || string.IsNullOrWhiteSpace(txtNombreUsuarioEdit.Text) || string.IsNullOrWhiteSpace(ddlRolEdit.SelectedValue) || string.IsNullOrWhiteSpace(txtTelefonoEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }

            if (!ValidarCorreoElectronico(txtEmailEdit.Text))
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

            DateTime fecha_creacion = DateTime.Today;
            Usuario usuario_editar = new Usuario();
            usuario_editar.id = Int32.Parse(this.txtIdUsuarioEdit.Text);
            usuario_editar.nombre = this.txtNombreEdit.Text;
            usuario_editar.apellido = this.txtApellidoEdit.Text;
            usuario_editar.email = this.txtEmailEdit.Text;
            usuario_editar.telefonoCel = this.txtTelefonoEdit.Text;
            usuario_editar.username = this.txtNombreUsuarioEdit.Text;
            
            using (var contexto = new mininosDatabaseEntities())
            {
                var usuario = contexto.Usuario.Include("AES").FirstOrDefault(u => u.id == usuario_editar.id);
                if (usuario != null)
                {
                    var primerAES = usuario.AES.FirstOrDefault(); // Obtener el primer objeto AES asociado al usuario

                    if (primerAES != null)
                    {
                        //Encriptar la contraseña
                        AesDatos aes = new AesDatos();
                        string token = primerAES.token;
                        string iv = primerAES.iv;

                        string claveDesencriptada = this.txtClaveEdit.Text;
                        string claveEncriptada = aes.Encrypt_Aes(claveDesencriptada, token, iv);
                        usuario_editar.pw = claveEncriptada;
                    }
                }
            }
            
            usuario_editar.fechaCreacion = fecha_creacion;
            usuario_editar.estado = 2;
            usuario_editar.rolId = Int32.Parse(this.ddlRolEdit.Text);

            byte[] fotoBytes = null;
            byte[] fotoImagen = null;

            if (fuFotoEdit.HasFile)
            {
                using (BinaryReader br = new BinaryReader(fuFotoEdit.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(fuFotoEdit.PostedFile.ContentLength);
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
                fotoBytes = usuario_editar.fotoId;
            }

            usuario_editar.fotoId = fotoBytes;

            try
            {
                ngu.NG_EditarUsuario(usuario_editar);
            }
            catch (Exception)
            {
                throw;
            }

            MostrarAdvertencia("Datos editados correctamente.", false);
            if (Session["IsCurrentUserEdit"] != null) {
                if ((bool)Session["IsCurrentUserEdit"]) {
                    if (!fuFotoEdit.HasFile)
                    {
                        usuario_editar.fotoId = ObtenerFotoDeBaseDeDatosPorId(usuario_editar.id);
                    }
                    Session["CurrentLoggedOnUser"] = usuario_editar;
                    Session["IsCurrentUserEdit"] = null;
                }
            }
            Response.Redirect("~/wfGestionarUsuario.aspx");


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

        private bool ValidarCorreoElectronico(string correo)
        {
            string pattern = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(correo);
        }
    }
}
