using Entidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace Registro
{
    public partial class wfInformeIncidentes : System.Web.UI.Page
    {
        DonacionNegocio ngd = new DonacionNegocio();

        List<Incidente> incidente = new List<Incidente>();
        List<Donante> donaciones = new List<Donante>();
        List<DonacionEspecies> donacionesEspec = new List<DonacionEspecies>();
        List<Residente> residentes = new List<Residente>();
        List<Donante> donantes = new List<Donante>();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void ShowMessageBox(string mensaje, bool esError)
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
            // Establecer un temporizador para ocultar la advertencia después de 5 segundos (5000 milisegundos)
            string script = @"<script type='text/javascript'>
                        setTimeout(function() {
                            document.getElementById('pnlAdvertencia').style.display = 'none';
                        }, 5000);
                    </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "HideAlertScript", script);
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string fechaInicio = txtFechaInicio.Value;
            string fechaFin = txtFechaFin.Value;

            bool seleccionoFechasInicio = !string.IsNullOrEmpty(fechaInicio);
            bool seleccionoFechaFin = !string.IsNullOrEmpty(fechaFin);

            if (!seleccionoFechasInicio || !seleccionoFechaFin)
            {
                ShowMessageBox("Seleccione una fecha de inicio y una fecha de fin.", false);
                return;
            }

            DateTime fechaInicioSelecionada, fechaFinSeleccionada;

            if (!DateTime.TryParse(fechaInicio, out fechaInicioSelecionada) || !DateTime.TryParse(fechaFin, out fechaFinSeleccionada))
            {
                ShowMessageBox("Las fechas seleccionadas no son  validas", false);
                return;
            }
            if (fechaFinSeleccionada > DateTime.Now)
            {
                ShowMessageBox("La fecha de fin no puede ser posterior a la fecha actual.", false);
                return;
            }
            if (fechaInicioSelecionada >= fechaFinSeleccionada.AddDays(-1))
            {
                ShowMessageBox("La fecha de inicio y la fecha de fin no pueden ser consecutivas.", false);
                return;
            }
            if (fechaInicioSelecionada > fechaFinSeleccionada)
            {
                ShowMessageBox("La fecha de inicio no puede ser posterior a la fecha de fin.", false);
                return;
            }

            string informe = ddlInforme.SelectedValue;

            switch (informe)
            {
                case "1": // Si se seleccionó "Residentes"
                    GenerarInformeResidentes(); // !=3
                    break;
                case "2": // Si se seleccionó "Donaciones"
                    GenerarInformeDonacionMonetaria(); //Estado 2
                    break;
                case "3": // Si se seleccionó "Incidentes"
                    GenerarInformeDonacionEspecie();  //Estado 2
                    break;
                case "4": // Si se seleccionó "Incidentes"
                    GenerarInformeIncidentes();  //Estado 2 
                    break;
                case "5": // Si se seleccionó "Donantes"
                    GenerarInformeDonantes(); //!=3
                    break;
                default:
                    ShowMessageBox("Debe seleccionar un informe válido", false);
                    break;
            }
        }

        private void GenerarInformeDonacionEspecie()
        {
            string fechaInicio = txtFechaInicio.Value;
            string fechaFin = txtFechaFin.Value;

            bool seleccionoFechasInicio = !string.IsNullOrEmpty(fechaInicio);
            bool seleccionoFechaFin = !string.IsNullOrEmpty(fechaFin);

            if (!seleccionoFechasInicio || !seleccionoFechaFin)
            {

                ShowMessageBox("Seleccione una fecha de inicio y una fecha de fin.", false);

                ShowMessageBox("Seleccione una fecha de inicio y una fecha de fin.", false);

                return;
            }
            DateTime fechaInicioSelecionada, fechaFinSeleccionada;
            if (!DateTime.TryParse(fechaInicio, out fechaInicioSelecionada) || !DateTime.TryParse(fechaFin, out fechaFinSeleccionada))
            {

                ShowMessageBox("Las fechas seleccionadas no son  validas", false);

                ShowMessageBox("Las fechas seleccionadas no son  validas", false);

                return;
            }

            if (fechaFinSeleccionada > DateTime.Now)
            {
                ShowMessageBox("La fecha de fin no puede ser posterior a la fecha actual.", false);
                return;
            }
            else if (fechaInicioSelecionada >= fechaFinSeleccionada.AddDays(-1))
            {
                ShowMessageBox("La fecha de inicio y la fecha de fin no pueden ser consecutivas.", false);
                return;
            }
            else if (fechaInicioSelecionada > fechaFinSeleccionada)
            {
                ShowMessageBox("La fecha de inicio no puede ser posterior a la fecha de fin.", false);
                return;
            }
            else
            {
                string query = @"SELECT dbo.Donante.nombre,
         dbo.Donante.apellido,
         dbo.Donante.alias,
		 dbo.DonacionEspecies.tipoEspecie,
		 dbo.DonacionEspecies.cantidad,
		 dbo.DonacionEspecies.unidadMedida,
		 dbo.DonacionEspecies.fecha
		FROM Donante 
		INNER JOIN DonacionEspecies ON Donante.id = DonacionEspecies.donanteID
		WHERE fecha >=@fechaInicio AND fecha <=@fechaFin AND dbo.DonacionEspecies.estado =2;";

                using (var donacion = new mininosDatabaseEntities())
                {
                    SqlCommand command = new SqlCommand(query, (SqlConnection)donacion.Database.Connection);
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicioSelecionada);
                    command.Parameters.AddWithValue("@fechaFin", fechaFinSeleccionada);
                    donacion.Database.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Donante dn = new Donante();
                            dn.nombre = reader.GetString(0);
                            dn.apellido = reader.GetString(1);
                            dn.alias = reader.GetString(2);


                            DonacionEspecies mone = new DonacionEspecies();
                            mone.tipoEspecie = reader.GetString(3);
                            mone.cantidad = reader.GetDouble(4);
                            mone.unidadMedida = reader.GetString(5);
                            mone.fecha = reader.GetDateTime(6);

                            dn.DonacionEspecies.Add(mone);
                            donantes.Add(dn);
                        }


                    }
                    reader.Close();
                    GenerarPDF("DonacionEspecie");
                }

            }
        }

        private void GenerarInformeDonantes()
        {
            string fechaInicio = txtFechaInicio.Value;
            string fechaFinal = txtFechaFin.Value;

            bool seleccionoFechaInicio = !string.IsNullOrEmpty(fechaInicio);
            bool seleccionoFechaFin = !string.IsNullOrEmpty(fechaFinal);

            if (!seleccionoFechaInicio || !seleccionoFechaFin)
            {

                ShowMessageBox("Seleccione una fecha de inicio y de fin", false);

                ShowMessageBox("Seleccione una fecha de inicio y de fin", false);

                return;
            }
            DateTime fechaInicioSelecionada, fechaFinSeleccionada;
            if (!DateTime.TryParse(fechaInicio, out fechaInicioSelecionada) || !DateTime.TryParse(fechaFinal, out fechaFinSeleccionada))
            {

                ShowMessageBox("Las fechas seleccionada no son validas", false);

                ShowMessageBox("Las fechas seleccionada no son validas", false);

                return;
            }


            if (fechaFinSeleccionada > DateTime.Now)
            {
                ShowMessageBox("La fecha de fin no puede ser posterior a la fecha actual.", false);
                return;
            }
            else if (fechaInicioSelecionada >= fechaFinSeleccionada.AddDays(-1))
            {
                ShowMessageBox("La fecha de inicio y la fecha de fin no pueden ser consecutivas.", false);
                return;
            }
            else if (fechaInicioSelecionada > fechaFinSeleccionada)
            {
                ShowMessageBox("La fecha de inicio no puede ser posterior a la fecha de fin.", false);
                return;
            }
            else
            {
                string query = @"SELECT DISTINCT dbo.Donante.nombre,
                            dbo.Donante.apellido,
                            dbo.Donante.ciudad,
                            dbo.Donante.alias,
                            dbo.Donante.correo,
                            dbo.Donante.numTelefono,
                            dbo.Donante.pais FROM dbo.Donante
                            WHERE fechaIngreso >=@fechaInicio AND fechaIngreso<=@fechaFin AND dbo.Donante.estado!=3;";

                using (var Donante = new mininosDatabaseEntities())
                {
                    SqlCommand command = new SqlCommand(query, (SqlConnection)Donante.Database.Connection);
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicioSelecionada);
                    command.Parameters.AddWithValue("@fechaFin", fechaFinSeleccionada);

                    Donante.Database.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Donante dn = new Donante();
                            dn.nombre = reader.GetString(0);
                            dn.apellido = reader.GetString(1);
                            dn.ciudad = reader.GetString(2);
                            dn.alias = reader.GetString(3);
                            dn.correo = reader.GetString(4);
                            dn.numTelefono = reader.GetString(5);
                            dn.pais = reader.GetString(6);
                            donantes.Add(dn);
                        }
                    }
                    reader.Close();
                    GenerarPDF("Donantes");

                }

            }

        }

        private void GenerarInformeResidentes()
        {
            string fechaInicio = txtFechaInicio.Value;
            string fechaFin = txtFechaFin.Value;

            bool seleccionoFechaInicio = !string.IsNullOrEmpty(fechaInicio);
            bool seleccionoFechaFin = !string.IsNullOrEmpty(fechaFin);

            if (!seleccionoFechaFin || !seleccionoFechaInicio)
            {
                ShowMessageBox("Seleccione una fecha de inicio y de fin.", false);
                return;
            }

            DateTime fechaInicioSeleccionado, fechaFinSeleccionado;
            if (!DateTime.TryParse(fechaInicio, out fechaInicioSeleccionado) || !DateTime.TryParse(fechaFin, out fechaFinSeleccionado))
            {
                ShowMessageBox("Las fechas seleccionadas no son válidas", false);
                return;
            }

            if (fechaFinSeleccionado > DateTime.Now)
            {
                ShowMessageBox("La fecha de fin no puede ser posterior a la fecha actual.", false);
                return;
            }
            else if (fechaInicioSeleccionado >= fechaFinSeleccionado.AddDays(-1))
            {
                ShowMessageBox("La fecha de inicio y la fecha de fin no pueden ser consecutivas.", false);
                return;
            }
            else if (fechaInicioSeleccionado > fechaFinSeleccionado)
            {
                ShowMessageBox("La fecha de inicio no puede ser posterior a la fecha de fin.", false);
                return;
            }
            else
            {
                string query = @"SELECT 
    dbo.Residente.nombre,
    CASE WHEN dbo.Residente.sexo = 1 THEN 'Macho' ELSE 'Hembra' END AS sexo_convertido,
    dbo.Residente.descripcion,
    dbo.Zona.nombre,
    dbo.Residente.fechaIngreso,
    dbo.Residente.fechaDesaparicion,
    dbo.Residente.fechaDefuncion,
    dbo.Residente.fechaNacimiento,
    CASE WHEN dbo.Residente.esterilizado = 1 THEN 'Sí' ELSE 'No' END AS esterilizado_convertido
FROM dbo.Residente
INNER JOIN dbo.Zona ON dbo.Residente.zonaId = dbo.Zona.id
WHERE 
    (
        (fechaIngreso >= @fechaInicio AND fechaIngreso <= @fechaFin)
        OR (fechaNacimiento >= @fechaInicio AND fechaNacimiento <= @fechaFin)
        OR (fechaDesaparicion >= @fechaInicio AND fechaDesaparicion <= @fechaFin)
        OR (fechaDefuncion >= @fechaInicio AND fechaDefuncion <= @fechaFin)
    )
    AND dbo.Residente.estado != 3;";


                using (var residente = new mininosDatabaseEntities())
                {
                    SqlCommand command = new SqlCommand(query, (SqlConnection)residente.Database.Connection);
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicioSeleccionado);
                    command.Parameters.AddWithValue("@fechaFin", fechaFinSeleccionado);
                    

                    residente.Database.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Residente rs = new Residente();
                            rs.Zona = new Zona();
                            rs.nombre = reader.GetString(0);
                            rs.sexo = reader.GetString(1) == "Macho" ? true : false;
                            rs.descripcion = reader.GetString(2);
                            rs.Zona.nombre = reader.GetString(3);
                            rs.fechaIngreso = reader.GetDateTime(4);
                            if (!reader.IsDBNull(5))
                            {
                                rs.fechaDesaparicion = reader.GetDateTime(5);

                                if (!reader.IsDBNull(6))
                                {
                                    rs.fechaDefuncion = reader.GetDateTime(6);

                                    if (!reader.IsDBNull(7))
                                    {
                                        rs.fechaNacimiento = reader.GetDateTime(7);
                                    }
                                }
                            }

                            rs.esterilizado = reader.GetString(8) == "Sí" ? true : false;

                            residentes.Add(rs);
                        }
                    }
                    reader.Close();
                    GenerarPDF("Residentes");
                }

            }


        }

        private void GenerarInformeDonacionMonetaria()
        {
            string fechaInicio = txtFechaInicio.Value;
            string fechaFin = txtFechaFin.Value;

            bool seleccionoFechasInicio = !string.IsNullOrEmpty(fechaInicio);
            bool seleccionoFechaFin = !string.IsNullOrEmpty(fechaFin);

            if (!seleccionoFechasInicio || !seleccionoFechaFin)
            {

                ShowMessageBox("Seleccione una fecha de inicio y una fecha de fin.", false);

                ShowMessageBox("Seleccione una fecha de inicio y una fecha de fin.", false);

                return;
            }
            DateTime fechaInicioSelecionada, fechaFinSeleccionada;
            if (!DateTime.TryParse(fechaInicio, out fechaInicioSelecionada) || !DateTime.TryParse(fechaFin, out fechaFinSeleccionada))
            {

                ShowMessageBox("Las fechas seleccionadas no son  validas", false);

                ShowMessageBox("Las fechas seleccionadas no son  validas", false);

                return;
            }

            if (fechaFinSeleccionada > DateTime.Now)
            {
                ShowMessageBox("La fecha de fin no puede ser posterior a la fecha actual.", false);
                return;
            }
            else if (fechaInicioSelecionada >= fechaFinSeleccionada.AddDays(-1))
            {
                ShowMessageBox("La fecha de inicio y la fecha de fin no pueden ser consecutivas.", false);
                return;
            }
            else if (fechaInicioSelecionada > fechaFinSeleccionada)
            {
                ShowMessageBox("La fecha de inicio no puede ser posterior a la fecha de fin.", false);
                return;
            }
            else
            {
                string query = @"SELECT dbo.Donante.nombre,
         dbo.Donante.apellido,
         dbo.Donante.alias,
		 dbo.DonacionMonetaria.montoNi,
		 dbo.DonacionMonetaria.fecha
		FROM Donante 
		INNER JOIN DonacionMonetaria ON Donante.id = DonacionMonetaria.donanteID
		WHERE fecha >=@fechaInicio AND fecha <=@fechaFin AND dbo.DonacionMonetaria.estado = 2;";

                using (var donacion = new mininosDatabaseEntities())
                {
                    SqlCommand command = new SqlCommand(query, (SqlConnection)donacion.Database.Connection);
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicioSelecionada);
                    command.Parameters.AddWithValue("@fechaFin", fechaFinSeleccionada);
                    donacion.Database.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Donante dn = new Donante();
                            dn.nombre = reader.GetString(0);
                            dn.apellido = reader.GetString(1);
                            dn.alias = reader.GetString(2);


                            DonacionMonetaria mone = new DonacionMonetaria();
                            mone.montoNi = reader.GetDouble(3);
                            mone.fecha = reader.GetDateTime(4);
                        

                            dn.DonacionMonetaria.Add(mone);
                            donaciones.Add(dn);
                        }


                    }
                    reader.Close();
                    GenerarPDF("DonacionMonetaria");
                }
            }

        }
        protected void GenerarInformeIncidentes()
        {
            string fechaInicio = txtFechaInicio.Value;
            string fechaFin = txtFechaFin.Value;

            bool seleccionoFechasInicio = !string.IsNullOrEmpty(fechaInicio);
            bool seleccionoFechaFin = !string.IsNullOrEmpty(fechaFin);

            if (!seleccionoFechasInicio || !seleccionoFechaFin)
            {

                ShowMessageBox("Seleccione una fecha de inicio y una fecha de fin.", false);

                ShowMessageBox("Seleccione una fecha de inicio y una fecha de fin.", false);

                return;
            }

            DateTime fechaInicioSeleccionada, fechaFinSeleccionada;

            if (!DateTime.TryParse(fechaInicio, out fechaInicioSeleccionada) || !DateTime.TryParse(fechaFin, out fechaFinSeleccionada))
            {

                ShowMessageBox("Las fechas seleccionadas no son válidas.", false);

                ShowMessageBox("Las fechas seleccionadas no son válidas.", false);

                return;
            }



            if (fechaFinSeleccionada > DateTime.Now)
            {
                ShowMessageBox("La fecha de fin no puede ser posterior a la fecha actual.", false);
                return;
            }
            else if (fechaInicioSeleccionada >= fechaFinSeleccionada.AddDays(-1))
            {
                ShowMessageBox("La fecha de inicio y la fecha de fin no pueden ser consecutivas.", false);
                return;
            }
            else if (fechaInicioSeleccionada > fechaFinSeleccionada)
            {
                ShowMessageBox("La fecha de inicio no puede ser posterior a la fecha de fin.", false);
                return;
            }
            else
            {
                // Resto del código para generar el informe basado en fechas
                string query = @"SELECT dbo.Usuario.nombre,
                                dbo.Incidente.descripcion,
                                dbo.Incidente.fechaHora,
                                dbo.Incidente.estado,
                                dbo.Residente.nombre,
                                dbo.NivelDeRiesgo.descripcion                               
                                FROM dbo.Incidente
                                INNER JOIN dbo.Usuario ON dbo.Incidente.usuarioId = Usuario.id
                                INNER JOIN dbo.NivelDeRiesgo ON dbo.Incidente.nivelRiesgoId = NivelDeRiesgo.id
                                INNER JOIN dbo.Residente ON dbo.Incidente.residenteId = Residente.id
                                WHERE fechaHora >= @fechaInicio AND fechaHora <= @fechaFin AND dbo.Incidente.estado =2;";

                using (var cont = new mininosDatabaseEntities())
                {
                    SqlCommand command = new SqlCommand(query, (SqlConnection)cont.Database.Connection);
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicioSeleccionada);
                    command.Parameters.AddWithValue("@fechaFin", fechaFinSeleccionada);

                    cont.Database.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Usuario user = new Usuario();
                            user.nombre = reader.GetString(0);

                            Incidente ic = new Incidente();
                            ic.descripcion = reader.GetString(1);
                            ic.fechaHora = reader.GetDateTime(2);
                            ic.estado = reader.GetInt32(3);

                            Residente res = new Residente();
                            res.nombre = reader.GetString(4);

                            NivelDeRiesgo nl = new NivelDeRiesgo();
                            nl.descripcion = reader.GetString(5);

                            ic.Usuario = user;
                            ic.Residente = res;
                            ic.NivelDeRiesgo = nl;

                            incidente.Add(ic);
                        }
                    }

                    reader.Close();
                    GenerarPDF("Incidente");
                }
            }

               
        }

        protected void GenerarPDF(string tipoInforme)
        {
            try
            {
                // Crear un nuevo documento PDF
                Document document = new Document();

                // Crear un objeto PdfWriter para escribir el documento en un flujo de salida
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Registrar el evento de pie de página personalizado
                    CustomPdfPageEventHelper pageEventHandler = new CustomPdfPageEventHelper();
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    writer.PageEvent = pageEventHandler;

                    // Abrir el documento
                    document.Open();

                    // Crear una tabla para los datos del informe
                    PdfPTable tablaIncidente = new PdfPTable(5); // 6 columnas
                    PdfPTable tablaDonaciones = new PdfPTable(6);
                    tablaDonaciones.SpacingBefore = 20f;

                    PdfPTable tablaDonacionEspecie = new PdfPTable(7);
                    tablaDonacionEspecie.SpacingBefore = 20f;

                    PdfPTable tablaResidentes = new PdfPTable(9);
                    tablaResidentes.SpacingBefore = 20f;


                    PdfPTable tablaDonante = new PdfPTable(7);
                    tablaDonante.SpacingBefore = 20f;

                    switch (tipoInforme)
                    {
                        case "Incidente":


                            //Logo tipo
                            string logo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "mininos_logo.png");
                            Image imageLogo = Image.GetInstance(logo);

                            imageLogo.ScaleToFit(70f, 70f);
                            imageLogo.SetAbsolutePosition(50f, 760f);
                            Paragraph foto = new Paragraph();
                            foto.Alignment = Element.ALIGN_LEFT;
                            foto.Add(imageLogo);
                            Chunk espacioF = new Chunk("\n");

                            //Encabezado 
                            Font encabezadoFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                            Paragraph encabezado = new Paragraph("INFORME INCIDENTE", encabezadoFont);
                            encabezado.Alignment = Element.ALIGN_CENTER;
                            Chunk espacio = new Chunk("\n");


                            // Encabezados de columna
                            var columnaI = new string[] {
                                "Nombre",
                                "Descripcion",
                                "FechaHora",
                                "Nombre reidente",
                                "Nivel de riesgo" };

                            // Establecer el ancho de la tabla en modo automático
                            tablaIncidente.TotalWidth = PageSize.A4.Width - document.LeftMargin - document.RightMargin;
                            tablaIncidente.LockedWidth = true;
                            // Escribir los encabezados en la tabla
                            foreach (var header in columnaI)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(header));
                                tablaIncidente.AddCell(cell);
                            }


                            foreach (var incidenteItem in incidente)
                            {
                                Usuario user = incidenteItem.Usuario;
                                Residente residente = incidenteItem.Residente;
                                NivelDeRiesgo nivel = incidenteItem.NivelDeRiesgo;

                                tablaIncidente.AddCell(user.nombre.ToString());
                                tablaIncidente.AddCell(incidenteItem.descripcion.ToString());
                                tablaIncidente.AddCell(incidenteItem.fechaHora.ToString("dd/MM/yyyy HH:mm:ss"));
                                tablaIncidente.AddCell(residente.nombre.ToString());
                                tablaIncidente.AddCell(nivel.descripcion.ToString());
                            }

                            document.Add(imageLogo);
                            document.Add(encabezado);
                            document.Add(espacio);
                            break;

                        case "DonacionMonetaria":


                            //Logo tipo
                            string logoDonacion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "mininos_logo.png");
                            Image imageLogoDonacion = Image.GetInstance(logoDonacion);

                            imageLogoDonacion.ScaleToFit(70f, 70f);
                            imageLogoDonacion.SetAbsolutePosition(50f, 760f);
                            Paragraph fotoDonacion = new Paragraph();
                            fotoDonacion.Alignment = Element.ALIGN_LEFT;
                            fotoDonacion.Add(imageLogoDonacion);
                            Chunk espacioD = new Chunk("\n");

                            //Encabezado 
                            Font encabezadoFontDonacion = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                            Paragraph encabezadoDonacion = new Paragraph("DONACION MONETARIA INFORME", encabezadoFontDonacion);
                            encabezadoDonacion.Alignment = Element.ALIGN_CENTER;
                            Chunk espacioD2 = new Chunk("\n");


                            var columnaD = new string[] {
                                "Nombre",
                                "Apellido",
                                "Alias",
                                "Monto",
                                "Fecha"
                             
                                };
                            // Establecer el ancho automático
                            tablaDonaciones.WidthPercentage = 100f;
                           




                            foreach (var header in columnaD)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(header));
                                tablaDonaciones.AddCell(cell);
                            }
                            foreach (var donacion in donaciones)
                            {

                                tablaDonaciones.AddCell(donacion.nombre.ToString());
                                tablaDonaciones.AddCell(donacion.apellido.ToString());
                                tablaDonaciones.AddCell(donacion.alias.ToString());


                                foreach (var donacionMonetaria in donacion.DonacionMonetaria)
                                {
                                    tablaDonaciones.AddCell(donacionMonetaria.montoNi.ToString());
                                    tablaDonaciones.AddCell(donacionMonetaria.fecha.ToString("dd/MM/yyyy"));
                                   
                                }


                            }

                            document.Add(imageLogoDonacion);
                            document.Add(encabezadoDonacion);
                            document.Add(espacioD2);

                            break;

                        case "DonacionEspecie":

                            //Logo tipo
                            string logoDonacionEspecie = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "mininos_logo.png");
                            Image imageLogoDonacionEspecie = Image.GetInstance(logoDonacionEspecie);

                            imageLogoDonacionEspecie.ScaleToFit(70f, 70f);
                            imageLogoDonacionEspecie.SetAbsolutePosition(50f, 760f);
                            Paragraph fotoDonacionEspecie = new Paragraph();
                            fotoDonacionEspecie.Alignment = Element.ALIGN_LEFT;
                            fotoDonacionEspecie.Add(imageLogoDonacionEspecie);
                            Chunk espacioD5 = new Chunk("\n");

                            //Encabezado 
                            Font encabezadoFontDonacionEspecie = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                            Paragraph encabezadoDonacionEspecie = new Paragraph("DONACION ESPECIE INFORME", encabezadoFontDonacionEspecie);
                            encabezadoDonacionEspecie.Alignment = Element.ALIGN_CENTER;
                            Chunk espacioD3 = new Chunk("\n");

                            var columnaE = new string[] {
                                "Nombre",
                                "Apellido",
                                "Alias",
                                "Tipo especie",
                                "Cantidad",
                                "Unidad de medida",
                                "Fecha",

                                };
                            // Establecer el ancho automático
                            tablaDonacionEspecie.WidthPercentage = 100f;
                            tablaDonacionEspecie.SetWidths(new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f });




                            foreach (var header in columnaE)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(header));
                                tablaDonacionEspecie.AddCell(cell);
                            }
                            foreach (var donacion in donantes)
                            {

                                tablaDonacionEspecie.AddCell(donacion.nombre.ToString());
                                tablaDonacionEspecie.AddCell(donacion.apellido.ToString());
                                tablaDonacionEspecie.AddCell(donacion.alias.ToString());


                                foreach (var donacionEspecie in donacion.DonacionEspecies)
                            {
                                    tablaDonacionEspecie.AddCell(donacionEspecie.tipoEspecie.ToString());
                                    tablaDonacionEspecie.AddCell(donacionEspecie.cantidad.ToString());
                                    tablaDonacionEspecie.AddCell(donacionEspecie.unidadMedida.ToString());
                                    tablaDonacionEspecie.AddCell(donacionEspecie.fecha.ToString("dd/MM/yyyy"));

                                }


                            }

                            document.Add(imageLogoDonacionEspecie);
                            document.Add(encabezadoDonacionEspecie);
                            document.Add(espacioD3);

                            break;

                        case "Residentes":

                            string logoResidente = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "mininos_logo.png");
                            Image imagenResidente = Image.GetInstance(logoResidente);

                            imagenResidente.ScaleToFit(70f, 70f);
                            imagenResidente.SetAbsolutePosition(50f, 760f);
                            Paragraph fotoResidente = new Paragraph();
                            fotoResidente.Alignment = Element.ALIGN_LEFT;
                            fotoResidente.Add(imagenResidente);
                            Chunk espacioR = new Chunk("\n");

                            Font encabezadoFontResidente = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                            Paragraph encabezadoResidente = new Paragraph("INFORME RESIDENTE", encabezadoFontResidente);
                            encabezadoResidente.Alignment = Element.ALIGN_CENTER;
                            encabezadoResidente.Add(espacioR);



                            var columnaR = new string[] {
                                "Nombre",
                                "Sexo",
                                "Descripcion",
                                "Zona",
                                "Fecha de ingreso",
                                "Fecha de desaparicion",
                                "Fecha de defuncion",
                                "Fecha de nacimiento",
                                "Esterilizado" };

                            // Establecer el ancho automático
                            tablaResidentes.WidthPercentage = 112f;
                            tablaResidentes.SetWidths(new float[] { 2.5f, 2f, 3.2f, 2.7f, 2.8f, 3.5f, 2.8f, 3f, 3f });



                            foreach (var header in columnaR)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(header));
                                tablaResidentes.AddCell(cell);
                            }
                            foreach (var residente in residentes)
                            {
                                string sexoTexto = residente.sexo.HasValue ? (residente.sexo.Value ? "Macho" : "Hembra") : string.Empty;
                                string esterilizadoTexto = residente.esterilizado ? (residente.esterilizado ? "Sí" : "No") : string.Empty;

                                tablaResidentes.AddCell(residente.nombre.ToString());
                                tablaResidentes.AddCell(sexoTexto);
                                tablaResidentes.AddCell(residente.descripcion.ToString());
                                tablaResidentes.AddCell(residente.Zona.nombre.ToString());
                                tablaResidentes.AddCell(residente.fechaIngreso.ToString("dd/MM/yyyy"));
                                tablaResidentes.AddCell(residente.fechaDesaparicion.HasValue ? residente.fechaDesaparicion.Value.ToString("dd/MM/yyyy") : "");
                                tablaResidentes.AddCell(residente.fechaDefuncion.HasValue ? residente.fechaDefuncion.Value.ToString("dd/MM/yyyy") : "");
                                tablaResidentes.AddCell(residente.fechaNacimiento.HasValue ? residente.fechaNacimiento.Value.ToString("dd/MM/yyyy") : "");
                                tablaResidentes.AddCell(esterilizadoTexto);
                            }





                            document.Add(imagenResidente);
                            document.Add(encabezadoResidente);
                            document.Add(espacioR);
                            break;

                        case "Donantes":

                            string logoDonante = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "mininos_logo.png");
                            Image imagenDonante = Image.GetInstance(logoDonante);



                            imagenDonante.ScaleToFit(70f, 70f);

                            imagenDonante.ScaleToFit(70f, 70f);

                            imagenDonante.SetAbsolutePosition(50f, 760f);

                            Paragraph fotoDonante = new Paragraph();
                            fotoDonante.Alignment = Element.ALIGN_LEFT;
                            fotoDonante.Add(imagenDonante);
                            Chunk espacioDD = new Chunk("\n");


                            Font encabezadoFontDonante = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                            Paragraph encabezadoDonante = new Paragraph("INFORME DONANTES", encabezadoFontDonante);
                            encabezadoDonante.Alignment = Element.ALIGN_CENTER;
                            encabezadoDonante.Add(espacioDD);




                            var columnaDn = new string[] { "Nombre", "Apellido", "Ciudad", "Alias", "Correo", "Numero", "Pais" };

                            //Establecer el ancho automatico
                            tablaDonante.WidthPercentage = 112f;
                            tablaDonante.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1.3f, 1f, 1f });

                            foreach (var header in columnaDn)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(header));
                                tablaDonante.AddCell(cell);
                            }
                            foreach (var donante in donantes)
                            {
                                tablaDonante.AddCell(donante.nombre.ToString());
                                tablaDonante.AddCell(donante.apellido.ToString());
                                tablaDonante.AddCell(donante.ciudad.ToString());
                                tablaDonante.AddCell(donante.alias.ToString());
                                tablaDonante.AddCell(donante.correo.ToString());
                                tablaDonante.AddCell(donante.numTelefono.ToString());
                                tablaDonante.AddCell(donante.pais.ToString());
                            }
                            document.Add(imagenDonante);
                            document.Add(encabezadoDonante);
                            document.Add(espacioDD);
                            break;

                        default:
                            // Tipo de informe no válido
                            throw new ArgumentException("Tipo de informe no válido.");
                    }

                    // Agregar la tabla al documento
                    document.Add(tablaIncidente);
                    document.Add(tablaDonaciones);
                    document.Add(tablaResidentes);
                    document.Add(tablaDonante);
                    document.Add(tablaDonacionEspecie);
                    // Enviar el archivo como respuesta al navegador

                    document.Close();
                    writer.Close();

                    Response.Clear();
                    Response.ContentType = "application/pdf";

                    string filename;

                    switch (tipoInforme)
                    {
                        case "Incidente":
                            filename = "IncidenteInforme.pdf";
                            break;

                        case "DonacionMonetaria":
                            filename = "DonacionMonetariaInforme.pdf";
                            break;
                        case "DonacionEspecie":
                            filename = "DonacionEspecieInforme.pdf";
                            break;
                        case "Donantes":
                            filename = "DonantesInforme.pdf";
                            break;

                        case "Residentes":
                            filename = "ResidenteInforme.pdf";
                            break;
                        default:
                            filename = "informe.pdf";
                            break;
                    }

                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.BinaryWrite(memoryStream.ToArray());
                    Response.Flush();
                    Response.End();

                }
            }
            catch (SqlException ex)
            {
                Response.Write("Ocurrió un error al generar el archivo: " + ex.Message);
            }
        }

    }
}