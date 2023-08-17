using Datos;
using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestionarRolOpcion : System.Web.UI.Page
    {
        private RolOpcionNegocio ngro = new RolOpcionNegocio();
        private RolNegocio rolnegocio = new RolNegocio();
        private OpcionNegocio opcionnegocio = new OpcionNegocio();

        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
                    new { NombreCampo = "RolNombre", HeaderText = "Rol" },
                    new { NombreCampo = "OpcionAccion", HeaderText = "Opcion"},
                    new { NombreCampo = "RolOpcionId", HeaderText = "Id" },
                    new { NombreCampo = "opcionId", HeaderText = "OpcionId" },
                    new { NombreCampo = "rolId", HeaderText = "RolId" },
                    new { NombreCampo = "estado", HeaderText = "Estado" }
                };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvRolOpcion.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }

        private void ListarRolOpcion()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from ro in mininosDatabaseEntities.RolOpcion
                            join rol in mininosDatabaseEntities.Rol on ro.rolId equals rol.id
                            join opcion in mininosDatabaseEntities.Opcion on ro.opcionId equals opcion.id
                            where ro.estado != 3
                            select new
                            {
                                RolOpcionId = ro.id,
                                RolNombre = rol.nombre,
                                OpcionAccion = opcion.accion,
                                RolId = rol.id,
                                OpcionId = opcion.id,
                                Estado = ro.estado
                            };

                var result = query.ToList();

                gvRolOpcion.AutoGenerateColumns = false;
                gvRolOpcion.DataSource = result;
                gvRolOpcion.DataBind();
            }
        }

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
                }).ToList();

                rolesConNumeros.Insert(0, new { NumeroRol = 0, RolId = 0, RolNombre = "Seleccione" });

                ddlRol.DataSource = rolesConNumeros;
                ddlRol.DataValueField = "RolId";
                ddlRol.DataTextField = "RolNombre";
                ddlRol.DataBind();
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
                }).ToList();

                opcionesConNumeros.Insert(0, new { NumeroOpcion = 0, OpcionId = 0, OpcionAccion = "Seleccione" });

                ddlOpcion.DataSource = opcionesConNumeros;
                ddlOpcion.DataValueField = "OpcionId";
                ddlOpcion.DataTextField = "OpcionAccion";
                ddlOpcion.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }
        }


        private void GuardarRolOpcion()
        {
            if (ddlRol.SelectedIndex <= 0 || ddlOpcion.SelectedIndex <= 0)
            {
                MostrarAdvertencia("Por favor, seleccione una opción válida en los campos obligatorios.", true);
                return; // Salir del evento sin guardar los datos
            }

            int rolIdSeleccionado = Convert.ToInt32(ddlRol.SelectedValue);
            int opcionIdSeleccionado = Convert.ToInt32(ddlOpcion.SelectedValue);

            // Restablecer la opción "Seleccione" en el DropDownList
            ddlRol.SelectedIndex = 0;
            ddlOpcion.SelectedIndex = 0;
            ddlRol.ClearSelection();
            ddlOpcion.ClearSelection();
            ddlRol.Items.Insert(0, new ListItem("Seleccione", ""));
            ddlOpcion.Items.Insert(0, new ListItem("Seleccione", ""));

            RolOpcion modelo = new RolOpcion()
            {
                rolId = rolIdSeleccionado,
                opcionId = opcionIdSeleccionado,
                estado = 1
            };
            ngro.GuardarRolOpcion(modelo);
            ListarRolOpcion();
            MostrarAdvertencia("Datos guardados correctamente.", false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AgregarColumnas();
                CargarRol();
                CargarOpcion();
            }

            ListarRolOpcion();
        }

        protected void guardarRolOpcion_Click(object sender, EventArgs e)
        {
            GuardarRolOpcion();
        }

        protected void gvRolOpcion_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
        }

        protected void btnEditarRolOpcion_Click(object sender, EventArgs e)
        {
            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            int rolIdSeleccionado = 0;
            int opcionIdSeleccionado = 0;

            if (int.TryParse(selectedRow.Cells[3].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                txtId.Text = id.ToString();

                // Convertir el valor de la celda "Rol" a un entero
                if (int.TryParse(selectedRow.Cells[5].Text, out int rolId))
                {
                    rolIdSeleccionado = rolId;
                }
                else
                {
                    // Manejar el error de formato incorrecto
                    // (por ejemplo, mostrar un mensaje de error al usuario)
                    Console.WriteLine("El valor en selectedRow.Cells[5].Text no es un número entero válido.");
                }

                // Convertir el valor de la celda "Opcion" a un entero
                if (int.TryParse(selectedRow.Cells[4].Text, out int opcionId))
                {
                    opcionIdSeleccionado = opcionId;
                }
                else
                {
                    // Manejar el error de formato incorrecto
                    // (por ejemplo, mostrar un mensaje de error al usuario)
                    Console.WriteLine("El valor en selectedRow.Cells[4].Text no es un número entero válido.");
                }
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[3].Text no es un número entero válido.");
            }

            // Asignar datos a los campos
            txtId.Text = id.ToString();

            try
            {
                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlOpcion.Items.FindByValue(opcionIdSeleccionado.ToString()) != null)
                {
                    ddlOpcion.SelectedValue = opcionIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlOpcion.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado
                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar la opción: " + ex.Message, true); // Mostrar mensaje de error
            }

            try
            {
                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlRol.Items.FindByValue(rolIdSeleccionado.ToString()) != null)
                {
                    ddlRol.SelectedValue = rolIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlRol.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado
                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar el rol: " + ex.Message, true); // Mostrar mensaje de error
            }

            RolOpcion RolOpcionEdit = new RolOpcion()
            {
                id = Int32.Parse(this.txtId.Text),
                estado = 2,
                rolId = Int32.Parse(this.ddlRol.Text),
                opcionId = Int32.Parse(this.ddlOpcion.SelectedValue)
            };

            Session["DatosRolOpcion"] = RolOpcionEdit;
            Response.Redirect("~/wfEditarRolOpcion.aspx");
        }


        protected void btnEliminarRolOpcion_Click(object sender, EventArgs e)
        {
            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            int rolIdSeleccionado = 0;
            int opcionIdSeleccionado = 0;

            if (int.TryParse(selectedRow.Cells[3].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                txtId.Text = id.ToString();

                // Convertir el valor de la celda "Rol" a un entero
                if (int.TryParse(selectedRow.Cells[5].Text, out int rolId))
                {
                    rolIdSeleccionado = rolId;
                }
                else
                {
                    // Manejar el error de formato incorrecto
                    // (por ejemplo, mostrar un mensaje de error al usuario)
                    Console.WriteLine("El valor en selectedRow.Cells[5].Text no es un número entero válido.");
                }

                // Convertir el valor de la celda "Opcion" a un entero
                if (int.TryParse(selectedRow.Cells[4].Text, out int opcionId))
                {
                    opcionIdSeleccionado = opcionId;
                }
                else
                {
                    // Manejar el error de formato incorrecto
                    // (por ejemplo, mostrar un mensaje de error al usuario)
                    Console.WriteLine("El valor en selectedRow.Cells[4].Text no es un número entero válido.");
                }
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[3].Text no es un número entero válido.");
            }

            // Asignar datos a los campos
            txtId.Text = id.ToString();

            try
            {
                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlOpcion.Items.FindByValue(opcionIdSeleccionado.ToString()) != null)
                {
                    ddlOpcion.SelectedValue = opcionIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlOpcion.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado
                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar la opción: " + ex.Message, true); // Mostrar mensaje de error
            }

            try
            {
                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlRol.Items.FindByValue(rolIdSeleccionado.ToString()) != null)
                {
                    ddlRol.SelectedValue = rolIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlRol.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado
                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar el rol: " + ex.Message, true); // Mostrar mensaje de error
            }

            RolOpcion RolOpcionEliminar = new RolOpcion()
            {
                id = int.Parse(txtId.Text),
                estado = 3,
                rolId = int.Parse(ddlRol.SelectedValue),
                opcionId = int.Parse(ddlOpcion.SelectedValue)
            };

            Session["DatosRolOpcion"] = RolOpcionEliminar;
            Response.Redirect("~/wfEliminarRolOpcion.aspx");

        }

        private void MostrarAdvertencia(string mensaje, bool esError)
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


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBusqueda = txtBusqueda.Text.Trim();

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                using (var mininosDatabaseEntities = new mininosDatabaseEntities())
                {
                    var query = from ro in mininosDatabaseEntities.RolOpcion
                                join rol in mininosDatabaseEntities.Rol on ro.rolId equals rol.id
                                join opcion in mininosDatabaseEntities.Opcion on ro.opcionId equals opcion.id
                                where ro.estado != 3 &&
                                      (rol.nombre.Contains(textoBusqueda) || opcion.accion.Contains(textoBusqueda))
                                select new
                                {
                                    RolOpcionId = ro.id,
                                    RolNombre = rol.nombre,
                                    OpcionAccion = opcion.accion,
                                    RolId = rol.id,
                                    OpcionId = opcion.id,
                                    Estado = ro.estado
                                };

                    var result = query.ToList();

                    gvRolOpcion.DataSource = result;
                    gvRolOpcion.DataBind();
                }
            }
            else
            {
                // Texto de búsqueda vacío, listar todos los registros
                ListarRolOpcion();
            }
        }

    }
}

