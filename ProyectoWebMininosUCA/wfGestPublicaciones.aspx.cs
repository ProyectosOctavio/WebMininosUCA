using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestPublicaciones : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlGenericControl Foto;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Titulo;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Type;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Descripcion;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Fecha;

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarPublicaciones();
            if (!IsPostBack)
            {               
                if (Request.QueryString["mensajeGuardar"] != null && Request.QueryString["esErrorGuardar"] != null)
                {
                    string mensajeGuardar = Server.UrlDecode(Request.QueryString["mensajeGuardar"]);
                    bool esErrorGuardar = Convert.ToBoolean(Request.QueryString["esErrorGuardar"]);

                    MostrarAdvertenciaGuardado(mensajeGuardar, esErrorGuardar);                   
                }
                else
                {
                    pnlGuardarPublicacion.Visible = false;                                       
                }
                if (Request.QueryString["mensajeEditar"] != null && Request.QueryString["esErrorEditar"] != null)
                {
                    string mensajeEditar = Server.UrlDecode(Request.QueryString["mensajeEditar"]);
                    bool esErrorEditar = Convert.ToBoolean(Request.QueryString["esErrorEditar"]);

                    MostrarAdvertenciaEditado(mensajeEditar, esErrorEditar);
                }
                else
                {
                    pnlEditarPublicacion.Visible = false;
                }
                if (Request.QueryString["mensajeEliminar"] != null && Request.QueryString["esErrorEliminar"] != null)
                {
                    string mensajeEliminar = Server.UrlDecode(Request.QueryString["mensajeEliminar"]);
                    bool esErrorEliminar = Convert.ToBoolean(Request.QueryString["esErrorEliminar"]);

                    MostrarAdvertenciaEliminado(mensajeEliminar, esErrorEliminar);
                }
                else
                {
                    pnlEliminarPublicacion.Visible = false;
                }
            }
        }

        protected void CargarPublicaciones()
        {
            try
            {
                List<Publicacion> publicaciones = new Negocio.PublicacionNegocio().ListaPublicaciones();
                if (publicaciones.Count > 0)
                {
                    publicaciones = publicaciones.OrderByDescending(x => x.fecha).ToList();
                    List<InfoGRID> dtGRID = new List<InfoGRID>();

                    publicaciones.ForEach(x => dtGRID.Add(new InfoGRID { ID = x.id, TITULO = x.titulo, TIPO = x.tipo, DESCRIPCION = x.contenido, FECHA = x.fecha.ToShortDateString() }));

                    gvPublicaciones.DataSource = dtGRID;
                    gvPublicaciones.DataBind();

                    Session.Add("sIncidentes", publicaciones);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error al cargar las publicaciones: " + ex.Message);
            }
        }

        //Columnas del GridView.
        private class InfoGRID
        {
            public int ID { get; set; }
            public string TITULO { get; set; }
            public string TIPO { get; set; }
            public string DESCRIPCION { get; set; }
            public string FECHA { get; set; }            
        }

        protected void btnEditarPublicacion_Click(object sender, EventArgs e)
        {
            string titulo, tipo, descripcion;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtIdPublicacion.Text = id.ToString();
            }
            else
            {
                //Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            titulo = selectedRow.Cells[2].Text;
            tipo = selectedRow.Cells[3].Text;
            descripcion = selectedRow.Cells[4].Text;

            //Mandando datos a los campos
            this.txtIdPublicacion.Text = id.ToString();
            this.txtTituloPublicacion.Text = titulo;
            this.txtTipoPublicacion.Text = tipo;
            this.txtContenido.Text = descripcion;

            byte[] fotoBytes = null;
            if (archivoPublicacion.HasFile)
            {
                using (BinaryReader br = new BinaryReader(archivoPublicacion.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(archivoPublicacion.PostedFile.ContentLength);
                }
            }

            Publicacion publicacionEdit = new Publicacion()
            {

                id = Int32.Parse(this.txtIdPublicacion.Text),
                titulo = this.txtTituloPublicacion.Text,
                tipo = this.txtTipoPublicacion.Text,
                contenido = this.txtContenido.Text,
                fotoPublicacion = fotoBytes,
                estado = 2
            };
            Session["DatosPublicacion"] = publicacionEdit;
            Response.Redirect("~/wfEditarPublicacion.aspx");
        }

        protected void gvPublicaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        protected void btnEliminarPublicacion_Click(object sender, EventArgs e)
        {
            string titulo, tipo, descripcion;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtIdPublicacion.Text = id.ToString();
            }
            else
            {
                //Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            titulo = selectedRow.Cells[2].Text;
            tipo = selectedRow.Cells[3].Text;
            descripcion = selectedRow.Cells[4].Text;

            //Mandando datos a los campos
            this.txtIdPublicacion.Text = id.ToString();
            this.txtTituloPublicacion.Text = titulo;
            this.txtTipoPublicacion.Text = tipo;
            this.txtContenido.Text = descripcion;

            byte[] fotoBytes = null;
            if (archivoPublicacion.HasFile)
            {
                using (BinaryReader br = new BinaryReader(archivoPublicacion.PostedFile.InputStream))
                {
                    fotoBytes = br.ReadBytes(archivoPublicacion.PostedFile.ContentLength);
                }
            }

            Publicacion publicacionEliminar = new Publicacion()
            {

                id = Int32.Parse(this.txtIdPublicacion.Text),
                titulo = this.txtTituloPublicacion.Text,
                tipo = this.txtTipoPublicacion.Text,
                contenido = this.txtContenido.Text,
                fotoPublicacion = fotoBytes,
                estado = 2
            };
            Session["DatosPublicacion"] = publicacionEliminar;
            Response.Redirect("~/wfEliminarPublicacion.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBusqueda.Text;

            try
            {
                List<Publicacion> publicaciones = new Negocio.PublicacionNegocio().ListaPublicaciones();
                if (publicaciones.Count > 0)
                {
                    publicaciones = publicaciones.OrderByDescending(x => x.fecha).ToList();
                    List<InfoGRID> dtGRID = new List<InfoGRID>();

                    // Filtrar las publicaciones según el criterio de búsqueda
                    publicaciones = publicaciones.Where(x => x.titulo.Contains(filtro) || x.tipo.Contains(filtro) || x.contenido.Contains(filtro)).ToList();

                    publicaciones.ForEach(x => dtGRID.Add(new InfoGRID { ID = x.id, TITULO = x.titulo, TIPO = x.tipo, DESCRIPCION = x.contenido, FECHA = x.fecha.ToShortDateString() }));

                    gvPublicaciones.DataSource = dtGRID;
                    gvPublicaciones.DataBind();

                    Session.Add("sIncidentes", publicaciones);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void gvPublicaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPublicaciones.PageIndex = e.NewPageIndex;
            CargarPublicaciones();
        }

        private void MostrarAdvertenciaGuardado(string mensaje, bool esError)//bool
        {
            pnlGuardarPublicacion.Visible = true;
            //lblAdvertencia.Text = mensaje;

            if (esError)
            {
                pnlGuardarPublicacion.Attributes["class"] = "alert alert-success";
            }
            else
            {
                pnlGuardarPublicacion.Attributes["class"] = "alert alert-success";
            }
        }

        private void MostrarAdvertenciaEditado(string mensaje, bool esError)//bool
        {
            pnlEditarPublicacion.Visible = true;
            //lblAdvertencia.Text = mensaje;

            if (esError)
            {
                pnlEditarPublicacion.Attributes["class"] = "alert alert-success";
            }
            else
            {
                pnlEditarPublicacion.Attributes["class"] = "alert alert-success";
            }
        }

        private void MostrarAdvertenciaEliminado(string mensaje, bool esError)//bool
        {
            pnlEliminarPublicacion.Visible = true;
            //lblAdvertencia.Text = mensaje;

            if (esError)
            {
                pnlEliminarPublicacion.Attributes["class"] = "alert alert-success";
            }
            else
            {
                pnlEliminarPublicacion.Attributes["class"] = "alert alert-success";
            }
        }
    }
}