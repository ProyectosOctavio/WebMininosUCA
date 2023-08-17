using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfVerPublicaciones : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlGenericControl Foto;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Titulo;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Type;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Descripcion;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Fecha;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPublicacion();

            }
        }
        protected List<Publicacion> CargarPublicacion()
        {
            List<Publicacion> publicacion = new List<Publicacion>();

            using (var cx = new mininosDatabaseEntities())
            {
                try
                {
                    cx.Database.Connection.Open();

                    string query = "SELECT dbo.Publicacion.fotoPublicacion, dbo.Publicacion.titulo, dbo.Publicacion.tipo, dbo.Publicacion.contenido, dbo.Publicacion.fecha FROM dbo.Publicacion WHERE estado != 3 ORDER BY dbo.Publicacion.fecha DESC;";

                    SqlCommand command = new SqlCommand(query, (SqlConnection)cx.Database.Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Publicacion item = new Publicacion();

                        try
                        {
                            item.fotoPublicacion = (byte[])reader["fotoPublicacion"];
                        }
                        catch (Exception ex)
                        {
                            Foto.InnerText = "Error al obtener fotoPublicacion: " + ex.Message;
                        }

                        try
                        {
                            item.titulo = (string)reader["titulo"];
                        }
                        catch (Exception ex)
                        {
                            Titulo.InnerText = "Error al obtener titulo: " + ex.Message;
                        }

                        try
                        {
                            item.tipo = (string)reader["tipo"];
                        }
                        catch (Exception ex)
                        {
                            Type.InnerText = "Error al obtener tipo: " + ex.Message;
                        }

                        try
                        {
                            item.contenido = (string)reader["contenido"];
                        }
                        catch (Exception ex)
                        {
                            Descripcion.InnerText = "Error al obtener contenido: " + ex.Message;
                        }

                        try
                        {
                            item.fecha = (DateTime)reader["fecha"];
                        }
                        catch (Exception ex)
                        {
                            Fecha.InnerText = "Error al obtener fecha: " + ex.Message;
                        }

                        publicacion.Add(item);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    cx.Database.Connection.Close();
                }
            }

            return publicacion;
        }
    }
}