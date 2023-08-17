using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfEliminarRolOpcion : System.Web.UI.Page
    {
        RolOpcionNegocio ngro = new RolOpcionNegocio();
        RolNegocio rolnegocio = new RolNegocio();
        OpcionNegocio opcionnegocio = new OpcionNegocio();




        protected void CargarRol()
        {
            try
            {
                var roles = rolnegocio.ListaRol();

                var rolesConNumeros = roles.Select((r, index) => new
                {
                    NumeroRol = index + 1,
                    RolId = r.id,
                    RolNombre = r.nombre
                });

                ddlRolEliminar.DataSource = rolesConNumeros;
                ddlRolEliminar.DataValueField = "RolId";
                ddlRolEliminar.DataTextField = "RolNombre";
                ddlRolEliminar.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }
        }

        protected void CargarOpcion()
        {
            try
            {
                var opciones = opcionnegocio.ListaOpcion();

                var opcionesConNumeros = opciones.Select((o, index) => new
                {
                    NumeroOpcion = index + 1,
                    OpcionId = o.id,
                    OpcionAccion = o.accion
                });

                ddlOpcionEliminar.DataSource = opcionesConNumeros;
                ddlOpcionEliminar.DataValueField = "OpcionId";
                ddlOpcionEliminar.DataTextField = "OpcionAccion";
                ddlOpcionEliminar.DataBind();
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
                CargarRol();
                CargarOpcion();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            RecuperarDatos();
            base.OnLoadComplete(e);
        }

        protected void RecuperarDatos()
        {
            RolOpcion data_RolOpcion = (RolOpcion)Session["DatosRolOpcion"];

            this.txtIdEliminar.Text = data_RolOpcion.id.ToString();
            this.ddlRolEliminar.Text = data_RolOpcion.rolId.ToString();
            this.ddlOpcionEliminar.Text = data_RolOpcion.opcionId.ToString();


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            RolOpcion rolopcion_delete = new RolOpcion();
            rolopcion_delete.id = Int32.Parse(this.txtIdEliminar.Text);
            rolopcion_delete.estado = 3;

            ngro.EliminarRolOpcion(rolopcion_delete);
            Response.Redirect("~/wfGestionarRolOpcion.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarUsuario.aspx");
        }
    }
}