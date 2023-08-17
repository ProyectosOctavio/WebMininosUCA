using Entidades;
using Negocio;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestionarUsuarios : System.Web.UI.Page
    {
        UsuarioNegocio ngu = new UsuarioNegocio();
     
        RolNegocio rolnegocio = new RolNegocio();

        public object SqlHelper { get; private set; }

        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "UsuarioId", HeaderText = "Id" },
            new { NombreCampo = "RolNombre", HeaderText = "Rol" },
            new { NombreCampo = "UsuarioNombre", HeaderText = "Nombre" },
            new { NombreCampo = "UsuarioApellido", HeaderText = "Apellido" },
            new { NombreCampo = "UsuarioEmail", HeaderText = "Email"},
             new { NombreCampo = "UsuarioTelefono", HeaderText = "Telefono"},
            new { NombreCampo = "UsuarioUsername", HeaderText = "Usuario" },
            new { NombreCampo = "UsuarioPw", HeaderText = "Clave" },
            new { NombreCampo = "UsuarioFecha", HeaderText = "Fecha de creacion" },
            new { NombreCampo = "UsuarioEstado", HeaderText = "Estado" },
            new { NombreCampo = "UsuarioRolId", HeaderText = "RolId" },
             new { NombreCampo = "UsuarioFoto", HeaderText = "Foto" }
        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvUsuario.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }



        private void listarUsuarios()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from usuario in mininosDatabaseEntities.Usuario
                            join rol in mininosDatabaseEntities.Rol on usuario.rolId equals rol.id
                            where usuario.estado != 3
                            orderby usuario.fechaCreacion descending
                            select new
                            {
                                UsuarioApellido = usuario.apellido,
                                UsuarioEstado = usuario.estado,
                                UsuarioFecha = usuario.fechaCreacion,
                                UsuarioNombre = usuario.nombre,
                                UsuarioPw = usuario.pw,
                                UsuarioUsername = usuario.username,
                                UsuarioEmail = usuario.email,
                                UsuarioTelefono = usuario.telefonoCel,
                                UsuarioId = usuario.id,
                                UsuarioRolId = usuario.rolId,
                                UsuarioFoto = usuario.fotoId,
                                RolNombre = rol.nombre,

                            };

                var result = query.ToList();

                gvUsuario.AutoGenerateColumns = false;
                gvUsuario.DataSource = result;
                gvUsuario.DataBind();
            }
        }

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

                ddlRol.DataSource = rolesConNumeros;
                ddlRol.DataValueField = "RolId";
                ddlRol.DataTextField = "RolNombre";
                ddlRol.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }





        private void GuardarUsuario()
        {
           

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtClave.Text) || string.IsNullOrWhiteSpace(txtNombreUsuario.Text) || string.IsNullOrWhiteSpace(ddlRol.SelectedValue) || string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }

            if (!ValidarCorreoElectronico(txtEmail.Text))
            {
                MostrarAdvertencia("El correo electrónico ingresado no es válido, trate nuevamente.", true);
                return;
            }

            string telefono = txtTelefono.Text.Trim();
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

            if (ngu.ValidarUsuarioExistente(txtNombreUsuario.Text))
            {
                MostrarAdvertencia("El usuario ya existe, por favor, elija otro nombre de usuario.", true);
                return;
            }

            // Verificar si el correo electrónico ya existe en la base de datos
            if (ngu.ValidarCorreoElectronicoExistente(txtEmail.Text))
            {
                MostrarAdvertencia("El correo electrónico ya existe, por favor, ingrese otro correo.", true);
                return;
            }


            int rolIdSeleccionado = Convert.ToInt32(ddlRol.SelectedValue);

                DateTime fecha_creacion = DateTime.Today;

                byte[] fotoImagen = null;
                byte[] fotoBytes = null;

            if (fuFoto.HasFile)
            {
                using (BinaryReader br = new BinaryReader(fuFoto.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(fuFoto.PostedFile.ContentLength);
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

            Usuario modelo = new Usuario()
                {
                    nombre = txtNombre.Text,
                    apellido = txtApellido.Text,
                    username = txtNombreUsuario.Text,
                    fechaCreacion = fecha_creacion,
                    pw = txtClave.Text,
                    fotoId = fotoBytes, // Puede quedar vacío
                    estado = 1,
                    email = txtEmail.Text,
                    telefonoCel = txtTelefono.Text, // Puede quedar vacío
                    rolId = rolIdSeleccionado
                };
                ngu.GuardarUsuario(modelo);

                listarUsuarios();
                MostrarAdvertencia("Datos guardados correctamente.", false);

            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtNombreUsuario.Text = string.Empty;
            txtClave.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            ddlRol.SelectedIndex = 0; // Establecer el índice seleccionado a 0 (primer elemento)
            imgFoto.ImageUrl = string.Empty; // Limpiar la URL de la imagen
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

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtNombreUsuario.Text = string.Empty;
            txtClave.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            ddlRol.SelectedIndex = 0; // Establecer el índice seleccionado a 0 (primer elemento)
            imgFoto.ImageUrl = string.Empty; // Limpiar la URL de la imagen
            pnlAdvertencia.Visible = false;
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmail.Attributes.Add("onkeyup", "ValidarCorreo(this.value);");

            if (!IsPostBack)
            {
                AgregarColumnas();
                CargarRol();


            }

           

            listarUsuarios();

        }

        protected void gv_Usuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuario.PageIndex = e.NewPageIndex;
            listarUsuarios();
            pnlAdvertencia.Visible = false;

        }

        protected void guardarUsuario_Click(object sender, EventArgs e)
        {

            GuardarUsuario();
        }

        protected void limpiarUsuario_Click(object sender, EventArgs e)
        {

            LimpiarCampos();
        }
        protected void gvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (gvUsuario.HeaderRow != null)
            {
                gvUsuario.HeaderRow.Cells[1].Style.Add("display", "none");
                gvUsuario.HeaderRow.Cells[8].Style.Add("display", "none");
                gvUsuario.HeaderRow.Cells[10].Style.Add("display", "none");
                gvUsuario.HeaderRow.Cells[11].Style.Add("display", "none");
                gvUsuario.HeaderRow.Cells[12].Style.Add("display", "none");
            }

            foreach (GridViewRow row in gvUsuario.Rows)
            {
                row.Cells[1].Style.Add("display", "none");
                row.Cells[8].Style.Add("display", "none");
                row.Cells[10].Style.Add("display", "none");
                row.Cells[11].Style.Add("display", "none");
                row.Cells[12].Style.Add("display", "none");
            }
        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
           
            string nombre, apellido, username, clave, email, telefono;
            int rolIdSeleccionado = 0;

            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId.Text = id.ToString();

                // Convertir el valor de la celda "Rol" a un entero
                if (int.TryParse(selectedRow.Cells[11].Text, out int rolId))
                {
                    rolIdSeleccionado = rolId;
                }
                else
                {
                    // Manejar el error de formato incorrecto
                    // (por ejemplo, mostrar un mensaje de error al usuario)
                    Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
                }
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[11].Text no es un número entero válido.");
            }

            apellido = selectedRow.Cells[4].Text;
            nombre = selectedRow.Cells[3].Text;
            email = selectedRow.Cells[5].Text;
            telefono = selectedRow.Cells[6].Text;
            username = selectedRow.Cells[7].Text;
            clave = selectedRow.Cells[8].Text;
           

            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtEmail.Text = email;
            this.txtTelefono.Text = telefono;
            this.txtNombreUsuario.Text = username;
            this.txtClave.Text = clave;

            try
            {
              

                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlRol.Items.FindByValue(rolIdSeleccionado.ToString()) != null)
                {
                    ddlRol.SelectedValue = rolIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlRol.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado
                   
                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar el rol: " + ex.Message, true); // Mostrar mensaje de error
            }




            Usuario usuarioEdit = new Usuario()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                telefonoCel = this.txtTelefono.Text,
                email = this.txtEmail.Text,
                username = this.txtNombreUsuario.Text,
                pw = this.txtClave.Text,
                estado = 2,
                rolId = Int32.Parse(this.ddlRol.Text),
            };

            Session["DatosUsuario"] = usuarioEdit;
            Response.Redirect("~/wfEditarUsuario.aspx");
        }


        protected void btnEliminarUsuario_Click(object sender, EventArgs e)
        {

            string nombre, apellido, username, clave, fotoid, email, telefono;
            int rolIdSeleccionado = 0;

            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId.Text = id.ToString();

                // Convertir el valor de la celda "Rol" a un entero
                if (int.TryParse(selectedRow.Cells[11].Text, out int rolId))
                {
                    rolIdSeleccionado = rolId;
                }
                else
                {
                    // Manejar el error de formato incorrecto
                    // (por ejemplo, mostrar un mensaje de error al usuario)
                    Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
                }
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[11].Text no es un número entero válido.");
            }

            apellido = selectedRow.Cells[4].Text;
            nombre = selectedRow.Cells[3].Text;
            email = selectedRow.Cells[5].Text;
            telefono = selectedRow.Cells[6].Text;
            username = selectedRow.Cells[7].Text;
            clave = selectedRow.Cells[8].Text;
            fotoid = selectedRow.Cells[12].Text;

            //Mandando datos a los campos
        this.txtId.Text = id.ToString();
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtEmail.Text = email;
            this.txtTelefono.Text = telefono;
            this.txtNombreUsuario.Text = username;
            this.txtClave.Text = clave;

            try
            {


                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlRol.Items.FindByValue(rolIdSeleccionado.ToString()) != null)
                {
                    ddlRol.SelectedValue = rolIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlRol.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado
                    
                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar el rol: " + ex.Message, true); // Mostrar mensaje de error
            }



            byte[] fotoBytes = null;
            if (fuFoto.HasFile)
            {
                using (BinaryReader br = new BinaryReader(fuFoto.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(fuFoto.PostedFile.ContentLength);
                }
            }

            Usuario usuarioEliminar = new Usuario()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                telefonoCel = this.txtTelefono.Text,
                email = this.txtEmail.Text,
                username = this.txtNombreUsuario.Text,
                pw = this.txtClave.Text,
                fotoId = fotoBytes,
                estado = 2,
                rolId = Int32.Parse(this.ddlRol.Text),
            };

           
            Session["DatosUsuarioEliminar"] = usuarioEliminar;
            Response.Redirect("~/wfEliminarUsuario.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string terminoBusqueda = txtBusqueda.Text.Trim();
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from usuario in mininosDatabaseEntities.Usuario
                            join rol in mininosDatabaseEntities.Rol on usuario.rolId equals rol.id
                            where usuario.estado != 3 &&
                                  (usuario.apellido.Contains(terminoBusqueda) ||
                                   usuario.nombre.Contains(terminoBusqueda) ||
                                   usuario.email.Contains(terminoBusqueda) ||
                                   usuario.username.Contains(terminoBusqueda) ||
                                   usuario.telefonoCel.Contains(terminoBusqueda) ||
                                   rol.nombre.Contains(terminoBusqueda) ||
                                   usuario.fechaCreacion.ToString().Contains(terminoBusqueda))
                            orderby usuario.fechaCreacion descending
                            select new
                            {
                                UsuarioApellido = usuario.apellido,
                                UsuarioEstado = usuario.estado,
                                UsuarioFecha = usuario.fechaCreacion,
                                UsuarioNombre = usuario.nombre,
                                UsuarioPw = usuario.pw,
                                UsuarioUsername = usuario.username,
                                UsuarioEmail = usuario.email,
                                UsuarioTelefono = usuario.telefonoCel,
                                UsuarioId = usuario.id,
                                UsuarioRolId = usuario.rolId,
                                UsuarioFoto = usuario.fotoId,
                                RolNombre = rol.nombre,
                            };

                var result = query.ToList();

                gvUsuario.AutoGenerateColumns = false;
                gvUsuario.DataSource = result;
                gvUsuario.DataBind();
            }
        }


    }
}
    
