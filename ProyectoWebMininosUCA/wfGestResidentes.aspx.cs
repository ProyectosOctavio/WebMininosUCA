using Datos;
using Entidades;
using Negocio;
using System;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestResidentes : System.Web.UI.Page
    {
        PatologiaDatos ptd = new PatologiaDatos();
        ResidenteNegocio ngr = new ResidenteNegocio();
        ZonaNegocio ngz = new ZonaNegocio();
 
        public object SqlHelper { get; private set; }
        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "ResidenteId", HeaderText = "Id" },
            new { NombreCampo = "ResidenteNombre", HeaderText = "Nombre del Residente" },
            new { NombreCampo = "ZonaNombre", HeaderText = "Zona" },
            new { NombreCampo = "ResidenteDescripcion", HeaderText = "Descripcion" },
            new { NombreCampo = "ResidenteSexo", HeaderText = "Sexo"},
            new { NombreCampo = "ResidenteEsterilizado", HeaderText = "Esterilizacion"},
            new { NombreCampo = "ResidenteFechaCreacion", HeaderText = "Fecha de creacion" },
            new { NombreCampo = "ResidenteFechaNacimiento", HeaderText = "Fecha de Nacimiento" },
            new { NombreCampo = "ResidenteFechaDesaparicion", HeaderText = "Fecha de Desaparicion" },
            new { NombreCampo = "ResidenteFechaDefuncion", HeaderText = "Fecha de Defuncion" },
            new { NombreCampo = "ResidenteEstado", HeaderText = "Estado" },
            new { NombreCampo = "ResidenteResidenteFoto", HeaderText = "Foto" },
            new { NombreCampo = "ZonaId", HeaderText = "Id de Zona" }
        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvResidente.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }


        private void ListarPatologias()
        {
            PatologiaDatos ptd = new PatologiaDatos();
            List<Patologia> patologias = ptd.listarPatologia();
            List<string> descripciones = patologias.Select(p => p.descripcion).ToList();

            cblPatologias.DataSource = descripciones;
            cblPatologias.DataBind();
        }

        private void listarResidentes()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from residente in mininosDatabaseEntities.Residente
                        join zona in mininosDatabaseEntities.Zona on residente.zonaId equals zona.id
                        where residente.estado != 3
                            orderby residente.fechaIngreso descending
                            select new
                        {
                            ResidenteId = residente.id,
                            ResidenteNombre = residente.nombre,
                            ZonaNombre = zona.nombre,
                            ResidenteDescripcion = residente.descripcion,
                            ResidenteSexo = residente.sexo.HasValue ? (residente.sexo.Value ? "Femenino" : "Masculino") : "Desconocido",
                            ResidenteEsterilizado = residente.esterilizado ? "Si" : "No",
                            ResidenteFechaCreacion = residente.fechaIngreso,
                            ResidenteFechaNacimiento = residente.fechaNacimiento,
                            ResidenteFechaDesaparicion = residente.fechaDesaparicion,
                            ResidenteFechaDefuncion = residente.fechaDefuncion,
                            ResidenteEstado = residente.estado,
                            ResidenteResidenteFoto = residente.fotoId,
                            ZonaId = residente.zonaId,
                        };

            var result = query.ToList();

            gvResidente.AutoGenerateColumns = false;
            gvResidente.DataSource = result;
            gvResidente.DataBind();
        }
    }
        protected void CargarZona()
        {
            try
            {
                var zonas = ngz.LlenarDropDownListZonaNeg();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var zonasConNumeros = zonas.Select((z, index) => new
                {
                    NumeroZona = index + 1,
                    ZonaId = z.id,
                    ZonaNombre = z.nombre

                });

                ddlZona.DataSource = zonasConNumeros;
                ddlZona.DataValueField = "ZonaId";
                ddlZona.DataTextField = "ZonaNombre";
                ddlZona.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }

        private void GuardarResidente()
        {
            // Obtener el valor del TextBox
            string valor = txtNombre.Text;
            //Fechas (creditos a Alejandro Junior de la rocha primero)
            string fechaNacimiento = txtFechaNacimiento.Value;

            DateTime FechaNacimientoSeleccionada;

            if (!DateTime.TryParse(fechaNacimiento, out FechaNacimientoSeleccionada))
            {

                MostrarAdvertencia("Las fechas seleccionadas no son validas.", true);

                return;
            }
            //validaciones
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text) || string.IsNullOrWhiteSpace(ddlSexo.SelectedValue) || string.IsNullOrWhiteSpace(ddlZona.SelectedValue) || string.IsNullOrWhiteSpace(ddlEsterilizacion.SelectedValue))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (ngr.ValidarResidenteExistente(txtNombre.Text))
            {
                MostrarAdvertencia("El residente ya existe, por favor, elija otro nombre de residente.", true);
                return;
            }
            if (valor.Length >= 15) 
            {
                MostrarAdvertencia("El nombre es demasiado largo.", true);
                return;
            }

            int zonaIdSeleccionado = Convert.ToInt32(ddlZona.SelectedValue);

            //Fecha de ingreso del registro
            DateTime fecha_ingreso = DateTime.Today;
            //sexo y esterilizacion
            bool sexoSeleccionado = Convert.ToBoolean(ddlSexo.SelectedValue);
            bool esterilizadoSeleccionado = Convert.ToBoolean(ddlEsterilizacion.SelectedValue);
            //Foto
            byte[] fotoImagen = null;
            byte[] fotoBytes = null;
            if (fuResidente.HasFile) //fotobytes es igual a lo que hay en fileupload
            {

                using (BinaryReader br = new BinaryReader(fuResidente.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(fuResidente.PostedFile.ContentLength);
                }
            }
                // Validar si se seleccionó una foto
                if (fotoBytes == null || fotoBytes.Length == 0)
                {
                    MostrarAdvertencia("Debe seleccionar una foto.", true);
                    return;
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
                        imgFoto.ImageUrl = imageUrl;
                    }
                }


          


            Residente modelo = new Residente()
            {
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                fechaIngreso = fecha_ingreso,
                fechaNacimiento = FechaNacimientoSeleccionada,
                sexo = sexoSeleccionado,
                zonaId = zonaIdSeleccionado,
                esterilizado = esterilizadoSeleccionado,
                fotoId = fotoBytes,
                estado = 1
            };

            // Verificar si el gato tiene alguna patología seleccionada
            bool tienePatologias = false;
            foreach (ListItem item in cblPatologias.Items)
            {
                if (item.Selected)
                {
                    tienePatologias = true;
                    break;
                }
            }

            if (tienePatologias)
            {
                // Verificar si las patologías existen en la base de datos
                List<Patologia> patologiasSeleccionadas = new List<Patologia>();
                foreach (ListItem item in cblPatologias.Items)
                {
                    if (item.Selected)
                    {
                        string descripcionPatologia = item.Text;
                        Patologia patologiaExistente = ptd.ObtenerPatologiaPorDescripcion(descripcionPatologia);
                        if (patologiaExistente == null)
                        {
                            MostrarAdvertencia($"La patología '{descripcionPatologia}' no existe.", true);
                            return;
                        }

                        patologiasSeleccionadas.Add(patologiaExistente);
                    }
                }

                // Asociar las patologías seleccionadas al gato
                foreach (Patologia patologia in patologiasSeleccionadas)
                {
                    ResidentePatologia residentePatologia = new ResidentePatologia()
                    {
                        patologiaId = patologia.id,
                        residenteId = modelo.id,
                        estado = 1
                    };

                    modelo.ResidentePatologia.Add(residentePatologia);
                }
            }

            // Guardar el residente en la base de datos
            ngr.GuardarResidente(modelo);
            listarResidentes();
            MostrarAdvertencia("Datos guardados correctamente.", false);
            LimpiarCampos();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AgregarColumnas();
                CargarZona();
                ListarPatologias(); 
            }
            listarResidentes();
        }

        protected void btnGuardarResidente_Click(object sender, EventArgs e)
        {
            GuardarResidente();
        }

        protected void gvResidente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (gvResidente.HeaderRow != null)
            {
                gvResidente.HeaderRow.Cells[1].Style.Add("display", "none");
                gvResidente.HeaderRow.Cells[11].Style.Add("display", "none");
                gvResidente.HeaderRow.Cells[12].Style.Add("display", "none");
                gvResidente.HeaderRow.Cells[13].Style.Add("display", "none");
            }

            foreach (GridViewRow row in gvResidente.Rows)
            {
                row.Cells[1].Style.Add("display", "none");
                row.Cells[11].Style.Add("display", "none");
                row.Cells[12].Style.Add("display", "none");
                row.Cells[13].Style.Add("display", "none");
            }
        }

        protected void btnEditarResidente_Click(object sender, EventArgs e)
        {


            string nombre, descripcion, fotoId, fechaNacimientoText, sexo, esterilizado, fechaDefuncionText, fechaDesaparicionText;
            DateTime fechaNacimiento;

            Button btnConsultar = (Button)sender;

            int zonaIdSeleccionado = 0;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_residente.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }
            if (int.TryParse(selectedRow.Cells[13].Text, out int ZonaId))
            {
                zonaIdSeleccionado = ZonaId;
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[13].Text no es un número entero válido.");
            }

            nombre = selectedRow.Cells[2].Text;
            descripcion = selectedRow.Cells[4].Text;
            sexo = selectedRow.Cells[5].Text;
            esterilizado = selectedRow.Cells[6].Text;
            fechaNacimientoText = selectedRow.Cells[8].Text;
            fechaDesaparicionText = selectedRow.Cells[9].Text;
            fechaDefuncionText = selectedRow.Cells[10].Text;
            fotoId = selectedRow.Cells[12].Text;

            //Mandando datos a los campos
            if (DateTime.TryParse(fechaNacimientoText, out fechaNacimiento))
            {
                txtFechaNacimiento.Value = fechaNacimiento.ToString("dd/MM/yyyy"); // Asignas la fecha de nacimiento como cadena de texto al TextBox
            }
            else
            {
                // La conversión falló, maneja el escenario apropiadamente
                txtFechaNacimiento.Value = string.Empty;
            }
            this.txtId_residente.Text = id.ToString();
            this.txtDescripcion.Text = descripcion;
            //this.ddlEsterilizacion.SelectedValue = esterilizado;
            this.txtNombre.Text = nombre;
            //this.ddlSexo.SelectedValue = sexo;
            try
            {


                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlZona.Items.FindByValue(zonaIdSeleccionado.ToString()) != null)
                {
                    ddlZona.SelectedValue = zonaIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlZona.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado

                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar la zona: " + ex.Message, true); // Mostrar mensaje de error
            }

            //foto
            byte[] fotoBytes = null;
            if (fuResidente.HasFile)
            {
                using (BinaryReader br = new BinaryReader(fuResidente.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(fuResidente.PostedFile.ContentLength);
                }
            }
            bool sexoBool;
            bool esterilizadoBool;

            if (sexo == "Femenino")
            {
                sexoBool = true;
            }
            else
            {
                sexoBool = false;
            }
            if (esterilizado == "Si")
            {
                esterilizadoBool = true;
            }
            else
            {
                esterilizadoBool = false;
            }
            DateTime fechaNacimientoSeleccionada;
            DateTime fechaDefuncionSeleccionada;
            DateTime fechaDesaparicionSeleccionada;
            if (!DateTime.TryParse(txtFechaNacimiento.Value, out fechaNacimientoSeleccionada))
            {
                LimpiarCampos();
                MostrarAdvertencia("Las fechas seleccionadas no son validas.", true);
                return;
            }
            

            Residente residenteEdit = new Residente()
            {

                id = Int32.Parse(this.txtId_residente.Text),
                descripcion = this.txtDescripcion.Text,
                esterilizado = esterilizadoBool,
                fechaNacimiento = fechaNacimientoSeleccionada,
                nombre = this.txtNombre.Text,
                sexo = sexoBool,
                fotoId = fotoBytes,
                zonaId = Int32.Parse(this.ddlZona.SelectedValue),
                estado = 2
            };
            if (!DateTime.TryParse(fechaDesaparicionText, out fechaDesaparicionSeleccionada))
            {
            }
            else
            {
                residenteEdit.fechaDesaparicion = fechaDesaparicionSeleccionada;
            }
            if (!DateTime.TryParse(fechaDefuncionText, out fechaDefuncionSeleccionada))
            {
            }
            else
            {
                residenteEdit.fechaDefuncion = fechaDefuncionSeleccionada;
            }
            Session["DatosResidente"] = residenteEdit;
            Response.Redirect("~/wfEditarResidente.aspx");
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtFechaNacimiento.Value = string.Empty;
            ddlSexo.SelectedIndex = 0;
            ddlZona.SelectedIndex = 0;
            ddlEsterilizacion.SelectedIndex = 0;
            imgFoto.ImageUrl = string.Empty; // Limpiar la URL de la imagen
            pnlAdvertencia.Visible = false;
        }

        protected void gv_Residente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResidente.PageIndex = e.NewPageIndex;
            listarResidentes();
            pnlAdvertencia.Visible = false;
        }

        protected void limpiarCampos_Click(object sender, EventArgs e)
        {

            LimpiarCampos();
        }

        protected void btnEliminarResidente_Click(object sender, EventArgs e)
        {


            string nombre, descripcion, fotoId, fechaNacimiento, sexo, esterilizado;

            Button btnConsultar = (Button)sender;

            int zonaIdSeleccionado = 0;


            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_residente.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }
            if (int.TryParse(selectedRow.Cells[13].Text, out int zonaId))
            {
                zonaIdSeleccionado = zonaId;
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[13].Text no es un número entero válido.");
            }

            nombre = selectedRow.Cells[2].Text;
            descripcion = selectedRow.Cells[4].Text;
            sexo = selectedRow.Cells[5].Text;
            esterilizado = selectedRow.Cells[6].Text;
            fechaNacimiento = selectedRow.Cells[8].Text;
            fotoId = selectedRow.Cells[12].Text;



            //Mandando datos a los campos
            this.txtId_residente.Text = id.ToString();
            this.txtDescripcion.Text = descripcion;
            //this.ddlEsterilizacion.Text = esterilizado;
            this.txtFechaNacimiento.Value = fechaNacimiento;
            this.txtNombre.Text = nombre;
            //this.ddlSexo.Text = sexo;
            try
            {


                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlZona.Items.FindByValue(zonaIdSeleccionado.ToString()) != null)
                {
                    ddlZona.SelectedValue = zonaIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlZona.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado

                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar la zona: " + ex.Message, true); // Mostrar mensaje de error
            }

            //foto
            byte[] fotoBytes = null;
            if (fuResidente.HasFile)
            {
                using (BinaryReader br = new BinaryReader(fuResidente.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(fuResidente.PostedFile.ContentLength);
                }
            }
            bool sexoBool;
            bool esterilizadoBool;

            if (sexo == "Femenino")
            {
                sexoBool = true;
            }
            else
            {
                sexoBool = false;
            }
            if (esterilizado == "Si")
            {
                esterilizadoBool = true;
            }
            else
            {
                esterilizadoBool = false;
            }


            Residente residenteEliminar = new Residente()
            {

                id = Int32.Parse(this.txtId_residente.Text),
                descripcion = this.txtDescripcion.Text,
                esterilizado = esterilizadoBool,
                fechaNacimiento = DateTime.Parse(this.txtFechaNacimiento.Value),
                nombre = this.txtNombre.Text,
                sexo = sexoBool,
                fotoId = fotoBytes,
                zonaId = Int32.Parse(this.ddlZona.SelectedValue),
                estado = 2
            }; 
            Session["DatosResidenteEliminar"] = residenteEliminar;
            Response.Redirect("~/wfEliminarResidente.aspx");
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string terminoBusqueda = txtBusqueda.Text.Trim();
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from residente in mininosDatabaseEntities.Residente
                            join zona in mininosDatabaseEntities.Zona on residente.zonaId equals zona.id
                            where residente.estado != 3 &&
                                  (residente.nombre.ToLower().Contains(terminoBusqueda)||
                       zona.nombre.ToLower().Contains(terminoBusqueda) ||
                       residente.descripcion.ToLower().Contains(terminoBusqueda)||
                       residente.fechaNacimiento.ToString().Contains(terminoBusqueda)||
                       residente.fechaIngreso.ToString().Contains(terminoBusqueda) ||
                       residente.fechaDesaparicion.ToString().Contains(terminoBusqueda)||
                       residente.fechaDefuncion.ToString().Contains(terminoBusqueda)||
                       (residente.esterilizado && terminoBusqueda.StartsWith("si")) ||
                       (!residente.esterilizado && terminoBusqueda.StartsWith("no")) ||
                       (residente.sexo.HasValue && residente.sexo.Value && terminoBusqueda.StartsWith("Femenino")) ||
                       (residente.sexo.HasValue && !residente.sexo.Value && terminoBusqueda.StartsWith("Masculino")))
                            orderby residente.fechaIngreso descending
                            select new
                            {
                                ResidenteId = residente.id,
                                ResidenteNombre = residente.nombre,
                                ZonaNombre = zona.nombre,
                                ResidenteDescripcion = residente.descripcion,
                                ResidenteSexo = residente.sexo.HasValue ? (residente.sexo.Value ? "Femenino" : "Masculino") : "Desconocido",
                                ResidenteEsterilizado = residente.esterilizado ? "Si" : "No",
                                ResidenteFechaCreacion = residente.fechaIngreso,
                                ResidenteFechaNacimiento = residente.fechaNacimiento,
                                ResidenteFechaDesaparicion = residente.fechaDesaparicion,
                                ResidenteFechaDefuncion = residente.fechaDefuncion,
                                ResidenteEstado = residente.estado,
                                ResidenteResidenteFoto = residente.fotoId,
                                ZonaId = residente.zonaId,
                            };

                var result = query.ToList();

                gvResidente.AutoGenerateColumns = false;
                gvResidente.DataSource = result;
                gvResidente.DataBind();
            }
        }

    };

}
