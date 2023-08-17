using System;
using Entidades;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using System.Diagnostics;
using System.IO;

namespace ProyectoWebMininosUCA
{
    public partial class GestionarDonaciones : System.Web.UI.Page
    {
        IncidenteNegocio ngd = new IncidenteNegocio();
        ResidenteNegocio ngr = new ResidenteNegocio();


        public object SqlHelper { get; private set; }

        protected void CargarResidente()
        {
            try
            {
                var residentes = ngr.ListaResidente();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var residentesConIndex = residentes.Select((r, index) => new
                {
                    NumeroResidente = index + 1,
                    ResidenteId = r.id,
                    ResidenteNombre = r.nombre
                });

                cblMininos.DataSource = residentesConIndex;
                cblMininos.DataValueField = "ResidenteId";
                cblMininos.DataTextField = "ResidenteNombre";
                cblMininos.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }

        protected void CargarNivelRiesgos()
        {
            try
            {
                var nivelesDeRiesgos = ngd.ListaNivelRiesgo();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var riesgosConIndex = nivelesDeRiesgos.Select((r, index) => new
                {
                    NumeroRiesgo = index + 1,
                    NivelDeRiesgoid = r.id,
                    NivelDeRiesgodesc = r.descripcion

                });

                ddlNivelRiesgo.DataSource = riesgosConIndex;
                ddlNivelRiesgo.DataValueField = "NivelDeRiesgoid";
                ddlNivelRiesgo.DataTextField = "NivelDeRiesgodesc";
                ddlNivelRiesgo.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }


        private void GuardarIncidente()
        {
            Usuario user = (Usuario)Session["CurrentLoggedOnUser"];

            int idNivelRiesgoSelecionado = Convert.ToInt32(ddlNivelRiesgo.SelectedValue);

            DateTime fecha_registro = DateTime.Now;

            byte[] fotoImagen = null;
            byte[] fotoBytes = null;

            if (fuFoto.HasFile)
            {
                using (BinaryReader br = new BinaryReader(fuFoto.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(fuFoto.PostedFile.ContentLength);
                }
            }

            // Null Check para verificar si se seleccionó una foto
            if (fotoBytes == null || fotoBytes.Length == 0)
            {
                MostrarAdvertencia("Debe seleccionar una foto para el incidente.", true);
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

            bool isSeleccionarResidenteChecked = chkSeleccionarResidente.Checked;

            if (isSeleccionarResidenteChecked)
            {
                int seleccionadosCount = 0;
                int residenteIdSeleccionado = 0;

                foreach (ListItem item in cblMininos.Items)
                {
                    if (item.Selected)
                    {
                        seleccionadosCount++;
                        residenteIdSeleccionado = int.Parse(item.Value);
                    }
                }

                if (seleccionadosCount != 1)
                {
                    MostrarAdvertencia("Por favor, seleccione un solo gato afectado", true);
                    return;
                }

                Incidente incidente = new Incidente()
                {
                    descripcion = txtDescripcion.Text,
                    fechaHora = fecha_registro,
                    nivelRiesgoId = idNivelRiesgoSelecionado,
                    foto = fotoBytes,
                    usuarioId = user.id,
                    estado = 1,
                    residenteId = residenteIdSeleccionado
                };
                ngd.GuardarIndicente(incidente);

                MostrarAdvertencia("Datos guardados correctamente.", false);
            }
            else
            {
                Incidente incidente = new Incidente()
                {
                    descripcion = txtDescripcion.Text,
                    fechaHora = fecha_registro,
                    nivelRiesgoId = idNivelRiesgoSelecionado,
                    foto = fotoBytes,
                    usuarioId = user.id,
                    estado = 1,
                    residenteId = null
                };
                ngd.GuardarIndicente(incidente);

                MostrarAdvertencia("Datos guardados correctamente.", false);
            }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Datos que se cargan para el registrar
                CargarResidente();
                CargarNivelRiesgos();
                lblDateTime.Text = "Se registrará con esta fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + " Hora: " + DateTime.Now.ToString("HH:mm");
            }
        }

        protected void chkSeleccionarResidente_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkSeleccionarResidente.Checked;

            for (int i = 0; i < cblMininos.Items.Count; i++)
            {
                cblMininos.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
            }

            divGatoAfectado.Visible = isChecked;
        }

        protected void guardarIncidente_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            else if (ddlNivelRiesgo.SelectedValue == "")
            {
                MostrarAdvertencia("Por favor, debe seleccionar un nivel de riesgo", true);
                return; //vuelve al principio del metodo donar
            }

            GuardarIncidente();
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
