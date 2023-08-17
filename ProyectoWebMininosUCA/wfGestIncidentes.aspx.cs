using System;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestIncidentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session.Add("LoggedIn", 1);
                CargarIncidentes();
                CargarHistorialIncidentes();
            }
        }

        protected void CargarIncidentes()
        {
            try
            {
                List<Entidades.Incidente> incidentes = new Negocio.IncidenteNegocio().ListaIncidentesActivos();
                if (incidentes.Count > 0)
                {
                    incidentes = incidentes.OrderByDescending(x => x.fechaHora).ToList();
                    List<InfoGRID> dtGRID = new List<InfoGRID>();

                    incidentes.ForEach(x => dtGRID.Add(new InfoGRID { ID = x.id, NIVEL_RIESGO = x.NivelDeRiesgo.descripcion, DESCRIPCION = x.descripcion, RESIDENTE = (x.residenteId != null) ? x.Residente.nombre : "NINGUN AFECTADO", USUARIO = (x.usuarioId != null) ? x.Usuario.nombre : "DESCONOCIDO", FECHA = x.fechaHora.ToShortDateString() }));

                    gvDatos.DataSource = dtGRID;
                    gvDatos.DataBind();

                    Session.Add("sIncidentes", incidentes);
                }



            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en cargar Incidentes: " + e.Message);
            }
        }


        protected void CargarHistorialIncidentes()
        {
            try
            {
                List<Entidades.Incidente> incidentes = new Negocio.IncidenteNegocio().ListaIncidentesResueltos();
                if (incidentes.Count > 0)
                {
                    incidentes = incidentes.OrderByDescending(x => x.fechaHora).ToList();
                    List<InfoGRID> dtGRID = new List<InfoGRID>();

                    incidentes.ForEach(x => dtGRID.Add(new InfoGRID { ID = x.id, NIVEL_RIESGO = x.NivelDeRiesgo.descripcion, DESCRIPCION = x.descripcion, RESIDENTE = (x.residenteId != null) ? x.Residente.nombre : "NINGUN AFECTADO", USUARIO = (x.usuarioId != null) ? x.Usuario.nombre : "DESCONOCIDO", FECHA = x.fechaHora.ToShortDateString() }));

                    gvHistorial.DataSource = dtGRID;
                    gvHistorial.DataBind();
                    Session.Add("sHistorial", incidentes);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private class InfoGRID
        {
            public int ID { get; set; }
            public string NIVEL_RIESGO { get; set; }
            public string DESCRIPCION { get; set; }
            public string RESIDENTE { get; set; }
            public string USUARIO { get; set; }
            public string FECHA { get; set; }
        }

        protected void gvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[1].Visible = true;
        }

        protected void gvHistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[1].Visible = true;
        }

        protected void btnResolver_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entidades.Incidente> sIncidentes = (List<Entidades.Incidente>)Session["sIncidentes"];
                List<Entidades.Incidente> incidentesSeleccionados = new List<Entidades.Incidente>();

                for (int i = 0; i < gvDatos.Rows.Count; i++)
                {
                    GridViewRow row = gvDatos.Rows[i];
                    CheckBox chb = (CheckBox)row.FindControl("Sel");
                    bool sel = chb.Checked;

                    if (sel)
                    {
                        Entidades.Incidente incidenteSeleccionado = sIncidentes[i];
                        incidentesSeleccionados.Add(incidenteSeleccionado);
                    }
                }

                if (incidentesSeleccionados.Count > 0)
                {
                    foreach (Entidades.Incidente incidenteSeleccionado in incidentesSeleccionados)
                    {
                        // Realiza la acción deseada para cada incidente seleccionado                       
                         incidenteSeleccionado.estado = 2; //resuelto...
                         new Negocio.IncidenteNegocio().Actualizar(incidenteSeleccionado);
                    }

                    CargarIncidentes();
                    CargarHistorialIncidentes();
                }
                else
                {
                    cvDatos.IsValid = false;
                    cvDatos.ErrorMessage = "Debe seleccionar al menos un incidente.";
                }
            }
            catch (Exception err)
            {
                cvDatos.IsValid = false;
                cvDatos.ErrorMessage = "Error al resolver incidentes: " + err.Message;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Entidades.Incidente> sIncidentes = (List<Entidades.Incidente>)Session["sHistorial"];
                List<Entidades.Incidente> incidentesSeleccionados = new List<Entidades.Incidente>();

                for (int i = 0; i < gvHistorial.Rows.Count; i++)
                {
                    GridViewRow row = gvHistorial.Rows[i];
                    CheckBox chb = (CheckBox)row.FindControl("SelH");
                    bool sel = chb.Checked;

                    if (sel)
                    {
                        Entidades.Incidente incidenteSeleccionado = sIncidentes[i];
                        incidentesSeleccionados.Add(incidenteSeleccionado);
                    }
                }

                if (incidentesSeleccionados.Count > 0)
                {
                    foreach (Entidades.Incidente incidenteSeleccionado in incidentesSeleccionados)
                    {
                        // Realiza la acción deseada para cada incidente seleccionado
                         incidenteSeleccionado.estado = 3; //eliminado de historial...
                         new Negocio.IncidenteNegocio().Actualizar(incidenteSeleccionado);
                    }

                    CargarIncidentes();
                    CargarHistorialIncidentes();
                }
                else
                {
                    cvDatos.IsValid = false;
                    cvDatos.ErrorMessage = "Debe seleccionar al menos un incidente.";
                }
            }
            catch (Exception err)
            {
                cvDatos.IsValid = false;
                cvDatos.ErrorMessage = "Error al eliminar incidentes: " + err.Message;
            }
        }

        protected void btnBuscarActivas_Click(object sender, EventArgs e)
        {
            string filtro = txtBusquedaActivas.Text;

            try
            {
                List<Incidente> incidentes = new Negocio.IncidenteNegocio().ListaIncidentesActivos();
                if (incidentes.Count > 0)
                {
                    incidentes = incidentes.OrderByDescending(x => x.fechaHora).ToList();
                    List<InfoGRID> dtGRID = new List<InfoGRID>();

                    // Filtrar los incidentes según el criterio de búsqueda
                    incidentes = incidentes.Where(x => x.NivelDeRiesgo.descripcion.Contains(filtro) || x.descripcion.Contains(filtro) || (x.residenteId != null && (x.Residente != null && x.Residente.nombre.Contains(filtro))) || (x.usuarioId != null && (x.Usuario != null && x.Usuario.nombre.Contains(filtro))) || (filtro.Contains("NINGUN") && (x.Residente == null || (x.Residente != null && x.Residente.nombre == "NINGUN AFECTADO")))).ToList();

                    incidentes.ForEach(x => dtGRID.Add(new InfoGRID { ID = x.id, NIVEL_RIESGO = x.NivelDeRiesgo.descripcion, DESCRIPCION = x.descripcion, RESIDENTE = (x.residenteId != null) ? x.Residente.nombre : "NINGUN AFECTADO", USUARIO = (x.usuarioId != null) ? x.Usuario.nombre : "DESCONOCIDO", FECHA = x.fechaHora.ToShortDateString() }));

                    gvDatos.DataSource = dtGRID;
                    gvDatos.DataBind();

                    Session.Add("sIncidentes", incidentes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en cargar Incidentes: " + ex.Message);
            }
        }

        protected void btnBuscarHistorial_Click(object sender, EventArgs e)
        {
            string filtro = txtBusquedaHistorial.Text;

            try
            {
                List<Incidente> incidentes = new Negocio.IncidenteNegocio().ListaIncidentesResueltos();
                if (incidentes.Count > 0)
                {
                    incidentes = incidentes.OrderByDescending(x => x.fechaHora).ToList();
                    List<InfoGRID> dtGRID = new List<InfoGRID>();

                    // Filtrar los incidentes según el criterio de búsqueda
                    incidentes = incidentes.Where(x => x.NivelDeRiesgo.descripcion.Contains(filtro) || x.descripcion.Contains(filtro) || (x.residenteId != null && (x.Residente != null && x.Residente.nombre.Contains(filtro))) || (x.usuarioId != null && (x.Usuario != null && x.Usuario.nombre.Contains(filtro))) || (filtro.Contains("NINGUN") && (x.Residente == null || (x.Residente != null && x.Residente.nombre == "NINGUN AFECTADO")))).ToList();

                    incidentes.ForEach(x => dtGRID.Add(new InfoGRID { ID = x.id, NIVEL_RIESGO = x.NivelDeRiesgo.descripcion, DESCRIPCION = x.descripcion, RESIDENTE = (x.residenteId != null) ? x.Residente.nombre : "NINGUN AFECTADO", USUARIO = (x.usuarioId != null) ? x.Usuario.nombre : "DESCONOCIDO", FECHA = x.fechaHora.ToShortDateString() }));

                    gvHistorial.DataSource = dtGRID;
                    gvHistorial.DataBind();

                    Session.Add("sHistorial", incidentes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en cargar Incidentes: " + ex.Message);
            }
        }

        protected void gvHistorial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHistorial.PageIndex = e.NewPageIndex;
            CargarHistorialIncidentes();
        }

        protected void gvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDatos.PageIndex = e.NewPageIndex;
            CargarIncidentes();           
        }
    }
}