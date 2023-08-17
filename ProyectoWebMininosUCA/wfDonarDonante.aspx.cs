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
using System.Text.RegularExpressions;
using System.IO;

namespace ProyectoWebMininosUCA
{
    public partial class wfDonarDonante : System.Web.UI.Page
    {
        DonacionNegocio ngd = new DonacionNegocio();
        DonacionDatos dtg = new DonacionDatos();
        InfoBancariaNegocio ngi = new InfoBancariaNegocio();
        ResidenteNegocio residentenegocio = new ResidenteNegocio();
        private bool GridViewColumnsAdded = false;

        int nuevoIdDonante;

        public object SqlHelper { get; private set; }

        protected void CargarResidente()
        {
            try
            {
                var residentes = residentenegocio.ListaResidente();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var residentesConNumeros = residentes.Select((r, index) => new
                {
                    NumeroResidente = index + 1,
                    ResidenteId = r.id,
                    ResidenteNombre = r.nombre

                });

                cblApadrinacion.DataSource = residentesConNumeros;
                cblApadrinacion.DataValueField = "ResidenteId";
                cblApadrinacion.DataTextField = "ResidenteNombre";
                cblApadrinacion.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "InfoBancariaId", HeaderText = "Id" },
            new { NombreCampo = "InfoBancariaBanco", HeaderText = "Banco" },
            new { NombreCampo = "InfoBancariaNumeroCuenta", HeaderText = "Numero Cuenta" },
            new { NombreCampo = "InfoBancariaMoneda", HeaderText = "Moneda" },
            new { NombreCampo = "PatologiaEstado", HeaderText = "estado" },
        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvInfoBancaria.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }

        private void listarInfoBancarias()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from infoBancaria in mininosDatabaseEntities.InfoBancaria
                            where infoBancaria.estado != 3
                            select new
                            {
                                InfoBancariaId = infoBancaria.id,
                                InfoBancariaBanco = infoBancaria.banco,
                                InfoBancariaNumeroCuenta = infoBancaria.numeroCuenta,
                                InfoBancariaMoneda = infoBancaria.moneda,
                                PatologiaEstado = infoBancaria.estado,
                            };

                var result = query.ToList();

                gvInfoBancaria.AutoGenerateColumns = false;
                gvInfoBancaria.DataSource = result;
                gvInfoBancaria.DataBind();
            }
        }


        protected void gvInfoBancaria_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[4].Visible = false;
        }



        private void GuardarDonacionMonetaria()
        {


            try
            {

                DateTime fechaCreacion = DateTime.Today;

                if (txtCorreoDonante.Text.Contains("Anonimo") || txtCorreoDonante.Text.Contains("Anónimo"))
                {
                    // Verificar si el alias y el teléfono existen en la base de datos
                    if (ngd.ValidarAiasExistente(txtAliasDonante.Text) && ngd.ValidarTelefonoExistente(txtTelefonoDonante.Text))
                    {
                        // Se encontró un donante existente con el alias y el teléfono proporcionados
                        nuevoIdDonante = dtg.ObtenerIdDonanteExistentePorAliasYTelefono(txtAliasDonante.Text, txtTelefonoDonante.Text);
                    }
                    else
                    {
                        // No se encontró un donante existente con el alias y el teléfono proporcionados, crear un nuevo donante anónimo
                        GuardarDonante();
                        nuevoIdDonante = dtg.ObtenerUltimoIdDonante();
                    }
                }
                else if (ngd.ValidarcorreoExistente(txtCorreoDonante.Text))
                {
                    // Se encontró un donante existente con el correo electrónico proporcionado
                    int idDonanteExistente = dtg.ObtenerIdDonanteExistentePorCorreo(txtCorreoDonante.Text);
                    nuevoIdDonante = idDonanteExistente;
                }
                else
                {
                    // No se encontró un donante existente, crear un nuevo donante regular
                    GuardarDonante();
                    nuevoIdDonante = dtg.ObtenerUltimoIdDonante();
                }

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
                    MostrarAdvertencia("Debe seleccionar una foto del voucher.", true);
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


                DonacionMonetaria modelo = new DonacionMonetaria()
                {
                    montoNi = double.Parse(txtCantDonacionMonetaria.Text),
                    donanteId = nuevoIdDonante,
                    voucher = fotoBytes,
                    fecha = fechaCreacion,
                    estado = 1
                };

                ngd.GuardarDonacionMonetaria(modelo);
                MostrarAdvertencia("Donacion enviada con exito, puede regresar a la pagina principal", false);

            }
            catch (Exception ex)
            {
                MostrarAdvertencia(" Vuelva a ingresar un nuevo alias o teléfono.", true);
            }
            return;

        }

        private void GuardarDonacionEspecies()
        {
            try
            {

                DateTime fechaCreacion = DateTime.Today;

                if (txtCorreoDonante.Text.Contains("Anonimo") || txtCorreoDonante.Text.Contains("Anónimo"))
                {
                    // Verificar si el alias y el teléfono existen en la base de datos
                    if (ngd.ValidarAiasExistente(txtAliasDonante.Text) && ngd.ValidarTelefonoExistente(txtTelefonoDonante.Text))
                    {
                        // Se encontró un donante existente con el alias y el teléfono proporcionados
                        nuevoIdDonante = dtg.ObtenerIdDonanteExistentePorAliasYTelefono(txtAliasDonante.Text, txtTelefonoDonante.Text);
                    }
                    else
                    {
                        // No se encontró un donante existente con el alias y el teléfono proporcionados, crear un nuevo donante anónimo
                        GuardarDonante();
                        nuevoIdDonante = dtg.ObtenerUltimoIdDonante();
                    }
                }
                else if (ngd.ValidarcorreoExistente(txtCorreoDonante.Text))
                {
                    // Se encontró un donante existente con el correo electrónico proporcionado
                    int idDonanteExistente = dtg.ObtenerIdDonanteExistentePorCorreo(txtCorreoDonante.Text);
                    nuevoIdDonante = idDonanteExistente;
                }
                else
                {
                    // No se encontró un donante existente, crear un nuevo donante regular
                    GuardarDonante();
                    nuevoIdDonante = dtg.ObtenerUltimoIdDonante();
                }
                DonacionEspecies modelo2 = new DonacionEspecies()
                {
                    cantidad = Double.Parse(txtCantDonacionEspecias.Text),
                    donanteId = nuevoIdDonante,
                    tipoEspecie = ddlNombreDonacionEspecias.SelectedItem.Text,
                    unidadMedida = ddlMedidaDonacion.SelectedItem.Text,
                    fecha = fechaCreacion,
                    estado = 1,
                };

                ngd.GuardarDonacionEspecies(modelo2);
                MostrarAdvertencia("Donacion enviada con exito, puede regresar a la pagina principal", false);

            }
            catch (Exception ex)
            {
                MostrarAdvertencia(" Vuelva a ingresar un nuevo alias o teléfono.", true);
            }
            return;
        }

        private void GuardarDonante()
        {




            Donante modelo3 = new Donante()
            {

                alias = txtAliasDonante.Text,
                apellido = txtApellidoDonante.Text,
                ciudad = txtCiudadDonante.Text,
                correo = txtCorreoDonante.Text,
                nombre = txtNombreDonante.Text,
                numTelefono = txtTelefonoDonante.Text,
                pais = txtPaisDonante.Text,
                estado = 1


            };
            ngd.GuardarDonante(modelo3);

        }



        private void GuardarResidenteDonante()
        {
            try
            {
                if (txtCorreoDonante.Text.Contains("Anonimo") || txtCorreoDonante.Text.Contains("Anónimo"))
                {
                    // Verificar si el alias y el teléfono existen en la base de datos
                    if (ngd.ValidarAiasExistente(txtAliasDonante.Text) && ngd.ValidarTelefonoExistente(txtTelefonoDonante.Text))
                    {
                        // Se encontró un donante existente con el alias y el teléfono proporcionados
                        nuevoIdDonante = dtg.ObtenerIdDonanteExistentePorAliasYTelefono(txtAliasDonante.Text, txtTelefonoDonante.Text);
                    }
                    else
                    {
                        // No se encontró un donante existente con el alias y el teléfono proporcionados, crear un nuevo donante anónimo
                       
                        nuevoIdDonante = dtg.ObtenerUltimoIdDonante();
                    }
                }
                else if (ngd.ValidarcorreoExistente(txtCorreoDonante.Text))
                {
                    // Se encontró un donante existente con el correo electrónico proporcionado
                    int idDonanteExistente = dtg.ObtenerIdDonanteExistentePorCorreo(txtCorreoDonante.Text);
                    nuevoIdDonante = idDonanteExistente;
                }
                else
                {
                    // No se encontró un donante existente, crear un nuevo donante regular
                   
                    nuevoIdDonante = dtg.ObtenerUltimoIdDonante();
                }

                // Recorrer los elementos seleccionados del CheckBoxList
                foreach (ListItem item in cblApadrinacion.Items)
                {
                    if (item.Selected)
                    {
                        int residenteIdSeleccionado;
                        if (int.TryParse(item.Value, out residenteIdSeleccionado))
                        {
                            // Crear el objeto ResidenteDonante con los IDs obtenidos
                            ResidenteDonante modelo4 = new ResidenteDonante()
                            {
                                donanteId = nuevoIdDonante,
                                residenteId = residenteIdSeleccionado,
                                estado = 1
                            };

                            // Guardar el modelo en la capa de negocio correspondiente
                            ngd.GuardarResidenteDonante(modelo4);
                        }
                    }
                }

                
                MostrarAdvertencia("Donacion enviada con exito, puede regresar a la pagina principal", false);
            }
            catch (Exception ex)
            {
                MostrarAdvertencia(" Vuelva a ingresar un nuevo alias o teléfono.", true);
            }
            return;
        }






        protected void Page_Load(object sender, EventArgs e)

        {

            if (!IsPostBack)
            {

                CargarResidente();
                AgregarColumnas();


            }
            listarInfoBancarias();

            if (ddlFormaDonacion.SelectedValue == "")
            {
                txtNombreDonante.Visible = false;
                lblNombreDonante.Visible = false;
                txtApellidoDonante.Visible = false;
                lblApellidoDonante.Visible = false;
                txtPaisDonante.Visible = false;
                lblPaisDonante.Visible = false;
                txtCiudadDonante.Visible = false;
                lblCiudadDonante.Visible = false;
                txtCorreoDonante.Visible = false;
                lblCorreoDonante.Visible = false;
                txtAliasDonante.Visible = false;
                lblAliasDonante.Visible = false;
                txtTelefonoDonante.Visible = false;
                lblTelefonoDonante.Visible = false;
            }

            if (ddlTipoDonacion.SelectedValue == "")
            {
                ddlNombreDonacionEspecias.Visible = false; // bloquear los campos de la donación en especie
                txtCantDonacionEspecias.Visible = false;
                ddlMedidaDonacion.Visible = false;
                lblNombreEspecias.Visible = false;
                lblCantidadEspecias.Visible = false;
                lblMedidaEspecias.Visible = false;

                txtCantDonacionMonetaria.Visible = false;
                lblDonacionMonto.Visible = false;
                fuFoto.Visible = false;
                lblFoto.Visible = false;
                imgFoto.Visible = false;
                lblNota.Visible = false;
                lblSeleccion.Visible = false;

                gvInfoBancaria.Visible = false;
                lblInfoBancaria.Visible = false;
            }


            // Agregar el evento "onkeyup" al textbox para realizar la validación en tiempo real
            txtCorreoDonante.Attributes.Add("onkeyup", "ValidarCorreo(this.value);");

           

        }

        protected void ddlFormaDonacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (ddlFormaDonacion.SelectedValue == "2") // si se selecciona Anónimo
            {
                // Ocultar los TextBox y Labels asociados
                txtNombreDonante.Visible = false;
                lblNombreDonante.Visible = false;
                txtApellidoDonante.Visible = false;
                lblApellidoDonante.Visible = false;
                txtPaisDonante.Visible = false;
                lblPaisDonante.Visible = false;
                txtCiudadDonante.Visible = false;
                lblCiudadDonante.Visible = false;
                txtCorreoDonante.Visible = false;
                lblCorreoDonante.Visible = false;
                pnlAdvertencia.Visible = false;


                txtNombreDonante.Text = "Anónimo";
                txtApellidoDonante.Text = "Anónimo";
                txtCiudadDonante.Text = "Anónimo";
                txtCorreoDonante.Text = "Anónimo";
                txtPaisDonante.Text = "Anónimo";
                txtAliasDonante.Text = "";
                txtTelefonoDonante.Text = "";

                txtAliasDonante.Visible = true;
                txtTelefonoDonante.Visible = true;
            }
            else if (ddlFormaDonacion.SelectedValue == "1") // si se selecciona Reconocido
            {

                txtNombreDonante.Text = "";
                txtApellidoDonante.Text = "";
                txtCiudadDonante.Text = "";
                txtCorreoDonante.Text = "";
                txtPaisDonante.Text = "";
                txtAliasDonante.Text = "";
                txtTelefonoDonante.Text = "";

                pnlAdvertencia.Visible = false;
                txtNombreDonante.Visible = true;
                lblNombreDonante.Visible = true;
                txtApellidoDonante.Visible = true;
                lblApellidoDonante.Visible = true;
                txtPaisDonante.Visible = true;
                lblPaisDonante.Visible = true;
                txtCiudadDonante.Visible = true;
                lblCiudadDonante.Visible = true;
                txtCorreoDonante.Visible = true;
                lblCorreoDonante.Visible = true;
                txtTelefonoDonante.Visible = true;
                lblTelefonoDonante.Visible = true;
                txtAliasDonante.Visible = true;
                lblAliasDonante.Visible = true;


            }
        }


        protected void ddlTipoDonacion_SelectedIndexChanged(object sender, EventArgs e)
        {



            if (ddlTipoDonacion.SelectedValue == "1") // si se selecciona Monetaria
            {
                
               
                ddlMedidaDonacion.SelectedIndex = -1;
                ddlNombreDonacionEspecias.Text = "";
                txtCantDonacionEspecias.Text = "";
                txtCantDonacionMonetaria.Text = "";
                imgFoto.ImageUrl = string.Empty;

                pnlAdvertencia.Visible = false;
                ddlNombreDonacionEspecias.Visible = false; // bloquear los campos de la donación en especie
                txtCantDonacionEspecias.Visible = false;
                ddlMedidaDonacion.Visible = false;
                lblNombreEspecias.Visible = false;
                lblCantidadEspecias.Visible = false;
                lblMedidaEspecias.Visible = false;

                txtCantDonacionMonetaria.Visible = true;
                lblDonacionMonto.Visible = true;              
                fuFoto.Visible = true;
                lblFoto.Visible = true;
                imgFoto.Visible = true;
                lblNota.Visible = true;
                lblSeleccion.Visible = true;

                gvInfoBancaria.Visible = true;
                lblInfoBancaria.Visible = true;
            }
            else if (ddlTipoDonacion.SelectedValue == "2") // si se selecciona Unitaria
            {

                
                ddlMedidaDonacion.SelectedIndex = -1;
                ddlNombreDonacionEspecias.Text = "";
                txtCantDonacionEspecias.Text = "";
                txtCantDonacionMonetaria.Text = "";
                imgFoto.ImageUrl = string.Empty;



                pnlAdvertencia.Visible = false;
                txtCantDonacionMonetaria.Visible = false;
                lblDonacionMonto.Visible = false;
                fuFoto.Visible = false;
                lblFoto.Visible = false;
                imgFoto.Visible = false;
                lblNota.Visible = false;
                lblSeleccion.Visible = false;

                gvInfoBancaria.Visible = false;
                lblInfoBancaria.Visible = false;

                ddlNombreDonacionEspecias.Visible = true; // bloquear los campos de la donación en especie
                txtCantDonacionEspecias.Visible = true;
                ddlMedidaDonacion.Visible = true;
                lblNombreEspecias.Visible = true;
                lblCantidadEspecias.Visible = true;
                lblMedidaEspecias.Visible = true;
                // habilitar los campos de la donación en especie
            }
        }

        




        protected void btnDonar_Click(object sender, EventArgs e)
        {
            if (ddlFormaDonacion.SelectedValue == "")
            {
                MostrarAdvertencia("Por favor, debe seleccionar una forma de donacion", true);
                return; //vuelve al principio del metodo donar
            }

                if (ddlFormaDonacion.SelectedValue == "2")
            { 
               

                if ( string.IsNullOrEmpty(txtAliasDonante.Text) || string.IsNullOrEmpty(txtTelefonoDonante.Text)) // si almenos uno se cumple
                {
                    MostrarAdvertencia("Por favor, proporcione un Alias y un Telefono.", true);
                    return; //vuelve al principio del metodo donar
                }

                string telefono = txtTelefonoDonante.Text.Trim();
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


            }

            else if (ddlFormaDonacion.SelectedValue == "1")
            {
                if (string.IsNullOrEmpty(txtNombreDonante.Text) || string.IsNullOrEmpty(txtApellidoDonante.Text) || string.IsNullOrEmpty(txtPaisDonante.Text) || string.IsNullOrEmpty(txtCiudadDonante.Text) || string.IsNullOrEmpty(txtCorreoDonante.Text) || string.IsNullOrEmpty(txtAliasDonante.Text) || string.IsNullOrEmpty(txtTelefonoDonante.Text)) // si almenos uno se cumple
                {
                    MostrarAdvertencia("Por favor, complete todos los campos con sus datos personales", true);
                    return;
                }

                if (!ValidarCorreoElectronico(txtCorreoDonante.Text))
                {
                    MostrarAdvertencia("El correo electrónico ingresado no es válido, trate nuevamente.", true);
                    return;
                }

                string telefono = txtTelefonoDonante.Text.Trim();
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


            }


            if (ddlTipoDonacion.SelectedValue == "")
            {
                MostrarAdvertencia("Por favor, debe seleccionar una tipo de donacion", true);
                return; //vuelve al principio del metodo donar
            }




            if (ddlTipoDonacion.SelectedValue == "1") // si se selecciona Monetaria
            {
                if (string.IsNullOrEmpty(txtCantDonacionMonetaria.Text))
                {
                    MostrarAdvertencia("Por favor, complete con el monto", true);
                    return;
                }
                if (fuFoto == null || fuFoto.PostedFile == null || fuFoto.PostedFile.ContentLength == 0)
                {
                    MostrarAdvertencia("Debe seleccionar una foto del voucher.", true);
                    return;
                }






                if (!string.IsNullOrEmpty(txtCantDonacionMonetaria.Text))
                {
                    if (!double.TryParse(txtCantDonacionMonetaria.Text, out _))
                    {
                        MostrarAdvertencia("El campo Monto debe contener un valor numérico válido.", true);
                        return;
                    }
                }
               
                GuardarDonacionMonetaria();
                GuardarResidenteDonante();


            }
            else if (ddlTipoDonacion.SelectedValue == "2") // si se selecciona Unitaria
            {
                if (ddlNombreDonacionEspecias.SelectedValue == "" || string.IsNullOrEmpty(txtCantDonacionEspecias.Text) || ddlMedidaDonacion.SelectedValue == "")
                {
                    MostrarAdvertencia("Por favor, complete todos los campos de la donación en especie.", true);
                    return;
                }

               
                GuardarDonacionEspecies();
                GuardarResidenteDonante();

            }

            ddlFormaDonacion.ClearSelection();
            ddlTipoDonacion.SelectedIndex = -1;
            cblApadrinacion.ClearSelection();
           
            ddlMedidaDonacion.SelectedIndex = -1;
            txtNombreDonante.Text = "";
            txtApellidoDonante.Text = "";
            txtCiudadDonante.Text = "";
            txtCorreoDonante.Text = "";
            txtPaisDonante.Text = "";
            txtAliasDonante.Text = "";
            txtTelefonoDonante.Text = "";
            ddlNombreDonacionEspecias.SelectedIndex = -1;
            txtCantDonacionEspecias.Text = "";
            txtCantDonacionMonetaria.Text = "";

            imgFoto.ImageUrl = string.Empty;

            txtCantDonacionMonetaria.Visible = false;
            lblDonacionMonto.Visible = false;
            fuFoto.Visible = false;
            lblFoto.Visible = false;
            imgFoto.Visible = false;
            lblNota.Visible = false;
            lblSeleccion.Visible = false;

            gvInfoBancaria.Visible = false;
            lblInfoBancaria.Visible = false;

            ddlNombreDonacionEspecias.Visible = false; // bloquear los campos de la donación en especie
            txtCantDonacionEspecias.Visible = false;
            ddlMedidaDonacion.Visible = false;
            lblNombreEspecias.Visible = false;
            lblCantidadEspecias.Visible = false;
            lblMedidaEspecias.Visible = false;
            // habilitar los campos de la donación en especie

            txtNombreDonante.Visible = false;
            lblNombreDonante.Visible = false;
            txtApellidoDonante.Visible = false;
            lblApellidoDonante.Visible = false;
            txtPaisDonante.Visible = false;
            lblPaisDonante.Visible = false;
            txtCiudadDonante.Visible = false;
            lblCiudadDonante.Visible = false;
            txtCorreoDonante.Visible = false;
            lblCorreoDonante.Visible = false;
            txtAliasDonante.Visible = false;
            lblAliasDonante.Visible = false;
            txtTelefonoDonante.Visible = false;
            lblTelefonoDonante.Visible = false;





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




    }
}