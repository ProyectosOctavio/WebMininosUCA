﻿using System;
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
    public partial class wfResolverMoneda : System.Web.UI.Page
    {
        DonacionNegocio ngu = new DonacionNegocio();

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }


        protected void RecuperarDatos()
        {
            DonacionMonetaria data_moneda = (DonacionMonetaria)Session["DatosMonetarioResolver"];



            this.txtIdMonedaResolver.Text = data_moneda.id.ToString();
            this.txtMontoResolver.Text = data_moneda.montoNi.ToString();

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
                    imgFotoResolver.ImageUrl = imageUrl;
                }
            }



            Donante data_donante = (Donante)Session["DatosDonanteResolver"];

            this.txtIdDonanteResolver.Text = data_donante.id.ToString();
            this.txtNombreResolver.Text = data_donante.nombre.ToString();
            this.txtApellidoResolver.Text = data_donante.apellido.ToString();
            this.txtAliasResolver.Text = data_donante.alias.ToString();


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


        protected void btnResolver_Click(object sender, EventArgs e)
        {
            DonacionMonetaria monetaria_resolver= new DonacionMonetaria();
            monetaria_resolver.id = Int32.Parse(this.txtIdMonedaResolver.Text);
            monetaria_resolver.estado = 2;

            ngu.NG_ResolverDonacionMonetaria(monetaria_resolver);
            Response.Redirect("~/wfGestionarMoneda.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarMoneda.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}