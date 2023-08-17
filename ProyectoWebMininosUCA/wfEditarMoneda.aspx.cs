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
    public partial class wfEditarMoneda : System.Web.UI.Page
    {
        DonacionNegocio ngu = new DonacionNegocio();
        

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RecuperarDatos()
        {
            DonacionMonetaria data_moneda = (DonacionMonetaria)Session["DatosMonetario"];



            this.txtIdMonedaEdit.Text = data_moneda.id.ToString();
            this.txtMontoEdit.Text = data_moneda.montoNi.ToString();

            byte[] fotoBytes = ObtenerFotoDeBaseDeDatosPorId(data_moneda.id);

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


            Donante data_donante = (Donante)Session["DatosDonante"];

            this.txtIdDonanteEdit.Text = data_donante.id.ToString();
            this.txtNombreEdit.Text = data_donante.nombre.ToString();
            this.txtApellidoEdit.Text = data_donante.apellido.ToString();
            this.txtAliasEdit.Text = data_donante.alias.ToString();

        }


        public byte[] ObtenerFotoDeBaseDeDatosPorId(int monedaId)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                
                DonacionMonetaria moneda = contexto.DonacionMonetaria.FirstOrDefault(m => m.id == monedaId && m.estado != 3);
                if (moneda != null)
                {
                    return moneda.voucher;
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





        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);

        }


        protected void btnEditarMoneda_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtMontoEdit.Text))
            {
                MostrarAdvertencia("Por favor, complete el monto obligatorios.", true);
                return; 
            }

            

            if (!string.IsNullOrEmpty(txtMontoEdit.Text))
            {
                if (!double.TryParse(txtMontoEdit.Text, out _))
                {
                    MostrarAdvertencia("El campo Monto debe contener un valor numérico válido.", true);
                    return;
                }
            }

            DateTime fecha_creacion = DateTime.Today;
            DonacionMonetaria moneda_editar = new DonacionMonetaria();
            moneda_editar.id = Int32.Parse(this.txtIdMonedaEdit.Text);
            moneda_editar.montoNi = Double.Parse(this.txtMontoEdit.Text);
            
            moneda_editar.fecha = fecha_creacion;
            moneda_editar.estado = 1;

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
                fotoBytes = moneda_editar.voucher;
            }

            moneda_editar.voucher= fotoBytes;


            try
            {
                ngu.NG_EditarDonacionMonetaria(moneda_editar);
            }
            catch (Exception)
            {
                throw;
            }

            MostrarAdvertencia("Datos editados correctamente.", false);
            Response.Redirect("~/wfGestionarMoneda.aspx");


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