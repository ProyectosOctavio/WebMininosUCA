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


namespace ProyectoWebMininosUCA
{
    public partial class wfEliminarResidente : System.Web.UI.Page
    {
        ResidenteNegocio ngr = new ResidenteNegocio();
        ZonaNegocio ngz = new ZonaNegocio();

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

                ddlZonaEliminar.DataSource = zonasConNumeros;
                ddlZonaEliminar.DataValueField = "ZonaId";
                ddlZonaEliminar.DataTextField = "ZonaNombre";
                ddlZonaEliminar.DataBind();
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
            Residente data_residente = (Residente)Session["DatosResidenteEliminar"];

            this.txtId_residenteEliminar.Text = data_residente.id.ToString();
            this.txtNombreEliminar.Text = data_residente.nombre.ToString();
            this.txtDescripcionEliminar.Text = data_residente.descripcion.ToString();
            //this.txtFechaNacimientoEliminar.Value = data_residente.fechaNacimiento.ToString();
            //this.txtFechaDefuncionEliminar.Value = data_residente.fechaDefuncion.ToString();
            //this.txtFechaDesaparicionEliminar.Value = data_residente.fechaDesaparicion.ToString();
            //this.ddlSexoEliminar.Text = data_residente.sexo.ToString();
            this.ddlZonaEliminar.Text = data_residente.zonaId.ToString();
            //this.ddlEsterilizacionEliminar.Text = data_residente.esterilizado.ToString();
            byte[] fotoBytes = ObtenerFotoDeBaseDeDatosPorId(data_residente.id);

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

        protected void btnEliminarResidente_Click(object sender, EventArgs e)
        {
            Residente residente_delete = new Residente();
            residente_delete.id = Int32.Parse(this.txtId_residenteEliminar.Text);
            residente_delete.estado = 3;

            ngr.NG_EliminarResidente(residente_delete);
            Response.Redirect("~/wfGestResidentes.aspx");
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
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