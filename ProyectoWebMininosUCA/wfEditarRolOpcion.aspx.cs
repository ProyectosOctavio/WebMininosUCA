using Entidades;
using Negocio;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;

namespace ProyectoWebMininosUCA
{
    public partial class wfEditarRolOpcion : System.Web.UI.Page
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

                ddlRolEdit.DataSource = rolesConNumeros;
                ddlRolEdit.DataValueField = "RolId";
                ddlRolEdit.DataTextField = "RolNombre";
                ddlRolEdit.DataBind();
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

                ddlOpcionEdit.DataSource = opcionesConNumeros;
                ddlOpcionEdit.DataValueField = "OpcionId";
                ddlOpcionEdit.DataTextField = "OpcionAccion";
                ddlOpcionEdit.DataBind();
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

            this.txtIdEdit.Text = data_RolOpcion.id.ToString();


            this.ddlRolEdit.Text = data_RolOpcion.rolId.ToString();
            this.ddlOpcionEdit.Text = data_RolOpcion.rolId.ToString();
        }


        protected void btnEditarRolOpcion_Click(object sender, EventArgs e)
        {
            RolOpcion RolOpcion_editar = new RolOpcion();
            RolOpcion_editar.id = Int32.Parse(this.txtIdEdit.Text);
            RolOpcion_editar.estado = 2;
            RolOpcion_editar.rolId = Int32.Parse(this.ddlRolEdit.Text);
            RolOpcion_editar.opcionId = Int32.Parse(this.ddlOpcionEdit.Text);

            try
            {
                ngro.EditarRolOpcion(RolOpcion_editar);
            }
            catch (Exception)
            {
                throw;
            }
            Response.Redirect("~/wfGestionarRolOpcion.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfGestionarRolOpcion.aspx");
        }

    }
}
