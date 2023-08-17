using Entidades;
using Negocio;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using Datos;

namespace ProyectoWebMininosUCA
{
    public partial class wfEditarResidente : System.Web.UI.Page
    {
        ResidenteNegocio ngr = new ResidenteNegocio();
        ZonaNegocio ngz = new ZonaNegocio();
        PatologiaDatos ptd = new PatologiaDatos();




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

                ddlZonaEdit.DataSource = zonasConNumeros;
                ddlZonaEdit.DataValueField = "ZonaId";
                ddlZonaEdit.DataTextField = "ZonaNombre";
                ddlZonaEdit.DataBind();
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

                CargarZona();

            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }

        protected void RecuperarDatos()
        {


            Residente data_residente = (Residente)Session["DatosResidente"];
            string fechaNacimientoString = data_residente.fechaNacimiento.ToString();
            string fechaDefuncionString = data_residente.fechaDefuncion.ToString();
            string fechaDesaparicionString = data_residente.fechaDesaparicion.ToString();

            DateTime fechaNacimiento;
            DateTime fechaDefuncion;
            DateTime fechaDesaparicion;



            this.txtId_residenteEdit.Text = data_residente.id.ToString();
            this.txtNombreEdit.Text = data_residente.nombre.ToString();
            this.txtDescripcionEdit.Text = data_residente.descripcion.ToString();
            if (DateTime.TryParse(fechaNacimientoString, out fechaNacimiento))
            {
                this.txtFechaNacimientoEdit.Value = fechaNacimiento.ToString("yyyy-MM-dd");
            }
            if (DateTime.TryParse(fechaDefuncionString, out fechaDefuncion))
            {
                this.txtFechaDefuncionEdit.Value = fechaDefuncion.ToString("yyyy-MM-dd");
            }
            if (DateTime.TryParse(fechaDesaparicionString, out fechaDesaparicion))
            {
                this.txtFechaDesaparicionEdit.Value = fechaDesaparicion.ToString("yyyy-MM-dd");
            }
            this.ddlSexoEdit.SelectedValue = data_residente.sexo.ToString().ToLower();
            this.ddlZonaEdit.Text = data_residente.zonaId.ToString();
            this.ddlEsterilizacionEdit.SelectedValue = data_residente.esterilizado.ToString().ToLower();

            // Obtener la foto del usuario en fotobytes
            byte[] fotoBytes = ObtenerFotoDeBaseDeDatosPorId(data_residente.id);


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

            List<Patologia> todasLasPatologias = ptd.listarPatologia();

            // Obtener las patologías asociadas al residente
            List<Patologia> patologiasAsociadas = ngr.ObtenerPatologiasDeResidente(data_residente.id);

            // Verificar si el residente tiene patologías asociadas
            if (patologiasAsociadas.Count > 0)
            {
                // Asignar las patologías disponibles al CheckBoxList
                cblPatologiasEditar.DataSource = todasLasPatologias;
                cblPatologiasEditar.DataTextField = "descripcion";
                cblPatologiasEditar.DataBind();

                foreach (ListItem item in cblPatologiasEditar.Items)
                {
                    int patologiaId;
                    if (int.TryParse(item.Value, out patologiaId))
                    {
                        if (patologiasAsociadas.Any(p => p.id == patologiaId))
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
            else
            {
                // Si el residente no tiene patologías asociadas, ocultar el CheckBoxList y mostrar un mensaje
                cblPatologiasEditar.Visible = false;
                lblMensajePatologiasEditar.Visible = true;
                lblMensajePatologiasEditar.Text = "Este residente no contiene patologías.";
            }



        }

        protected void btnEditarResidente_Click(object sender, EventArgs e)
        {

            int zonaIdSeleccionado = Convert.ToInt32(ddlZonaEdit.SelectedValue);
            // Obtener el valor del TextBox
            string valor = txtNombreEdit.Text;
            //Fechas
            string fechaNacimiento = txtFechaNacimientoEdit.Value;
            string fechaDesaparicion = txtFechaDesaparicionEdit.Value;
            string fechaDefuncion = txtFechaDefuncionEdit.Value;
            DateTime FechaNacimientoSeleccionada;
            DateTime FechaDesaparicionSeleccionada;
            DateTime fechaDefuncionSeleccionada;

            //validaciones
            if (string.IsNullOrWhiteSpace(txtNombreEdit.Text) || string.IsNullOrWhiteSpace(txtDescripcionEdit.Text) || string.IsNullOrWhiteSpace(ddlSexoEdit.SelectedValue) || string.IsNullOrWhiteSpace(ddlZonaEdit.SelectedValue) || string.IsNullOrWhiteSpace(ddlEsterilizacionEdit.SelectedValue))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (valor.Length >= 15)
            {
                MostrarAdvertencia("El nombre es demasiado largo.", true);
                return;
            }
            //sexo y esterilizacion
            bool sexoSeleccionado = Convert.ToBoolean(ddlSexoEdit.SelectedValue);
            bool esterilizadoSeleccionado = Convert.ToBoolean(ddlEsterilizacionEdit.SelectedValue);

            Residente residente_editar = new Residente();
            residente_editar.id = Int32.Parse(this.txtId_residenteEdit.Text);
            residente_editar.descripcion = this.txtDescripcionEdit.Text;
            residente_editar.esterilizado = esterilizadoSeleccionado;

            if (!DateTime.TryParse(fechaNacimiento, out FechaNacimientoSeleccionada))
            {
            }
            else
            {
                residente_editar.fechaNacimiento = FechaNacimientoSeleccionada;
            }
            if (!DateTime.TryParse(fechaDesaparicion, out FechaDesaparicionSeleccionada))
            {
            }
            else if (FechaDesaparicionSeleccionada < FechaNacimientoSeleccionada)
            {
                MostrarAdvertencia("La fecha de desaparición no puede ser anterior a la fecha de nacimiento.", true);
                return;
            }
            else
            {
                residente_editar.fechaDesaparicion = FechaDesaparicionSeleccionada;
            }
            if (!DateTime.TryParse(fechaDefuncion, out fechaDefuncionSeleccionada))
            {
            }
            else if (fechaDefuncionSeleccionada < FechaDesaparicionSeleccionada)
            {
                MostrarAdvertencia("La fecha de defunción no puede ser anterior a la fecha de desaparición.", true);
                return;
            }
            else if (fechaDefuncionSeleccionada < FechaNacimientoSeleccionada)
            {
                MostrarAdvertencia("La fecha de defunción no puede ser anterior a la fecha de nacimiento.", true);
                return;
            }
            else
            {
                residente_editar.fechaDefuncion = fechaDefuncionSeleccionada;
            }
            residente_editar.nombre = this.txtNombreEdit.Text;
            residente_editar.sexo = sexoSeleccionado;
            residente_editar.zonaId = zonaIdSeleccionado;
            residente_editar.estado = 2;
            byte[] fotoBytes = null;
            byte[] fotoImagen = null;
            if (fuResidenteEdit.HasFile)
            {
                using (BinaryReader br = new BinaryReader(fuResidenteEdit.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(fuResidenteEdit.PostedFile.ContentLength);
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
                fotoBytes = residente_editar.fotoId;
            }

            residente_editar.fotoId = fotoBytes;


            // Obtener las patologías seleccionadas
            List<ResidentePatologia> patologiasSeleccionadas = new List<ResidentePatologia>();
            foreach (ListItem item in cblPatologiasEditar.Items)
            {
                if (item.Selected)
                {
                    string descripcionPatologia = item.Text;
                    patologiasSeleccionadas.Add(new ResidentePatologia { Patologia = new Patologia { descripcion = descripcionPatologia }, estado = residente_editar.estado });
                }
            }
            residente_editar.ResidentePatologia = patologiasSeleccionadas;


            try
            {
                ngr.NG_EditarResidente(residente_editar);
            }
            catch (Exception)
            {
                throw;
            }

            MostrarAdvertencia("Datos editados correctamente.", false);
            Response.Redirect("~/wfGestResidentes.aspx");
        }
        public byte[] ObtenerFotoDeBaseDeDatosPorId(int residenteId)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                // Buscar el usuario por el ID en la base de datos
                Residente residente = contexto.Residente.FirstOrDefault(r => r.id == residenteId && r.estado != 3);
                if (residente != null)
                {
                    return residente.fotoId;
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestResidentes.aspx");
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