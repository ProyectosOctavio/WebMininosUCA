using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;

namespace ProyectoWebMininosUCA
{
    public partial class wfGaleriaPreview : Page
    {
        protected System.Web.UI.HtmlControls.HtmlGenericControl nombreZonaError;
        protected System.Web.UI.HtmlControls.HtmlGenericControl descripcionError;
        protected System.Web.UI.HtmlControls.HtmlGenericControl fechaIngresoError;
        protected System.Web.UI.HtmlControls.HtmlGenericControl fechaNacimientoError;
        protected System.Web.UI.HtmlControls.HtmlGenericControl fotoError;
        protected System.Web.UI.HtmlControls.HtmlGenericControl nombreResidenteError;
        protected System.Web.UI.HtmlControls.HtmlGenericControl descripcionResidenteError;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();

            }

        }

        protected List<Album> CargarDatos()
        {
            List<Album> galeria = new List<Album>();

            using (var JR = new mininosDatabaseEntities())
            {
                try
                {
                    JR.Database.Connection.Open();

                    string query = @"SELECT (SELECT TOP 1 foto FROM dbo.Album WHERE residenteId = dbo.Residente.id) AS foto,
                            dbo.Residente.nombre, dbo.Residente.descripcion, dbo.Residente.fechaIngreso, dbo.Zona.nombre AS 'Zona'
                            FROM dbo.Residente
                            INNER JOIN dbo.Zona ON dbo.Residente.zonaId = dbo.Zona.id";

                    SqlCommand command = new SqlCommand(query, (SqlConnection)JR.Database.Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Album item = new Album();
                        item.Zona = new Zona();
                        item.Residente = new Residente();

                        try
                        {
                            item.Zona.nombre = (string)reader["Zona"];
                        }
                        catch (Exception ex)
                        {
                            nombreZonaError.InnerText = "Error al obtener NombreZona: " + ex.Message;
                        }

                        try
                        {
                            item.Residente.fechaIngreso = (DateTime)reader["fechaIngreso"];
                        }
                        catch (Exception ex)
                        {
                            fechaNacimientoError.InnerText = "Error al obtener FechaNacimiento: " + ex.Message;
                        }

                        try
                        {
                            item.Residente.nombre = (string)reader["nombre"];
                        }
                        catch (Exception ex)
                        {
                            nombreResidenteError.InnerText = "Error al obtener NombreResidente: " + ex.Message;
                        }

                        try
                        {
                            item.Residente.descripcion = (string)reader["descripcion"];
                        }
                        catch (Exception ex)
                        {
                            descripcionResidenteError.InnerText = "Error al obtener DescripcionResidente: " + ex.Message;
                        }

                        try
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("foto")))
                            {
                                long size = reader.GetBytes(reader.GetOrdinal("foto"), 0, null, 0, 0);
                                byte[] buffer = new byte[size];
                                reader.GetBytes(reader.GetOrdinal("foto"), 0, buffer, 0, buffer.Length);
                                item.foto = buffer;
                            }
                            else
                            {
                                item.foto = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            fotoError.InnerText = "Error al obtener Foto: " + ex.Message;
                        }

                        galeria.Add(item);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    ShowMessageBox("Error al cargar los datos" + ex, false);
                }
                finally
                {
                    JR.Database.Connection.Close();
                }
            }

            return galeria;
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
        }
    }
}