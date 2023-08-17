using Datos;
using Entidades;
using Negocio;
using System;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestResidentePatologia : System.Web.UI.Page
    {
        PatologiaNegocio ngp = new PatologiaNegocio();
        ResidenteNegocio ngr = new ResidenteNegocio();
        ResidentePatologiaNegocio ngrp = new ResidentePatologiaNegocio();
        public object SqlHelper { get; private set; }
        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "ResidentePatologiaId", HeaderText = "Id" },
            new { NombreCampo = "ResidenteNombre", HeaderText = "Residente" },
            new { NombreCampo = "PatologiaDescripcion", HeaderText = "Descripcion" },
            new { NombreCampo = "ResidenteId", HeaderText = "ResidenteId" },
            new { NombreCampo = "PatologiaId", HeaderText = "PatologiaId" },
            new { NombreCampo = "ResodentePatologiaEstado", HeaderText = "Estado" },
        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvResidentePatologia.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }
        private void listarResidentePatologias()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from ResidentePatologia in mininosDatabaseEntities.ResidentePatologia
                            join residente in mininosDatabaseEntities.Residente on ResidentePatologia.residenteId equals residente.id
                            join patologia in mininosDatabaseEntities.Patologia on ResidentePatologia.patologiaId equals patologia.id
                            where ResidentePatologia.estado != 3
                            select new
                            {
                                ResidentePatologiaId = ResidentePatologia.id,
                                ResidenteNombre = residente.nombre,
                                PatologiaDescripcion = patologia.descripcion,
                                ResidenteId = ResidentePatologia.residenteId,
                                PatologiaId = ResidentePatologia.patologiaId,
                                ResodentePatologiaEstado = ResidentePatologia.estado
                            };

                var result = query.ToList();

                gvResidentePatologia.AutoGenerateColumns = false;
                gvResidentePatologia.DataSource = result;
                gvResidentePatologia.DataBind();
            }
        }

        protected void CargarResidentes()
        {
            try
            {
                var residentes = ngr.ListaResidente();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var residentesConNumeros = residentes.Select((r, index) => new
                {
                    NumeroResidente = index + 1,
                    ResidenteId = r.id,
                    ResidenteNombre= r.nombre

                });

                ddlResidenteId.DataSource = residentesConNumeros;
                ddlResidenteId.DataValueField = "ResidenteId";
                ddlResidenteId.DataTextField = "ResidenteNombre";
                ddlResidenteId.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }

        protected void CargarPatologias()
        {
            try
            {
                var patologias = ngp.ListaPatologia();

                // Agregar una columna "NumeroRol" con números secuenciales a los roles
                var patologiasConNumeros = patologias.Select((p, index) => new
                {
                    NumeroPatologia = index + 1,
                    PatologiaId = p.id,
                    PatolofiaDescripcion = p.descripcion

                });

                ddlPatologiaId.DataSource = patologiasConNumeros;
                ddlPatologiaId.DataValueField = "PatologiaId";
                ddlPatologiaId.DataTextField = "PatolofiaDescripcion";
                ddlPatologiaId.DataBind();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NO FUNCIONA PORQUE: " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }

        }

        private void LimpiarCampos()
        {
            ddlResidenteId.SelectedIndex = 0;
            ddlPatologiaId.SelectedIndex = 0;
            pnlAdvertencia.Visible = false;
        }

        protected void limpiarCampos_Click(object sender, EventArgs e)
        {

            LimpiarCampos();
        }

        private void GuardarResidentePatologia()
        {
            //validaciones
            if (string.IsNullOrWhiteSpace(ddlResidenteId.SelectedValue) || string.IsNullOrWhiteSpace(ddlPatologiaId.SelectedValue))
            {
                MostrarAdvertencia("Por favor, complete todos los campos obligatorios.", true);
                return; // Salir del método sin guardar los datos
            }
            if (ngrp.ValidarResidentePatologiaExistente(ddlResidenteId.SelectedIndex, ddlPatologiaId.SelectedIndex))
            {
                MostrarAdvertencia("El residente y la patologia ya estan relacionados, por favor, elija otras opciones.", true);
                return;
            }
            int patologiaIdSeleccionado = Convert.ToInt32(ddlPatologiaId.SelectedValue);
            int residenteIdSeleccionado = Convert.ToInt32(ddlResidenteId.SelectedValue);

            ResidentePatologia modelo = new ResidentePatologia()
            {
                patologiaId = patologiaIdSeleccionado,
                residenteId = residenteIdSeleccionado,
                estado = 1
            };

            ngrp.GuardarResidentePatologia(modelo);
            listarResidentePatologias();
            MostrarAdvertencia("Datos guardados correctamente.", false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AgregarColumnas();
                CargarPatologias();
                CargarResidentes();

            }



            listarResidentePatologias();

        }

        protected void btnGuardarResidentePatologia_Click(object sender, EventArgs e)
        {

            GuardarResidentePatologia();
        }

        protected void gvResidentePatologia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
        }

        protected void btnEditarResidentePatologia_Click(object sender, EventArgs e)
        {
            int residenteIdSeleccionado = 0;
            int patologiaIdSeleccionado = 0;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_residentePatologia.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }
            if (int.TryParse(selectedRow.Cells[4].Text, out int residenteId))
            {
                residenteIdSeleccionado = residenteId;
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[4].Text no es un número entero válido.");
            }
            if (int.TryParse(selectedRow.Cells[5].Text, out int patologiaId))
            {
                patologiaIdSeleccionado = patologiaId;
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[5].Text no es un número entero válido.");
            }




            //Mandando datos a los campos
            this.txtId_residentePatologia.Text = id.ToString();
            try
            {


                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlResidenteId.Items.FindByValue(residenteIdSeleccionado.ToString()) != null)
                {
                    ddlResidenteId.SelectedValue = residenteIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlResidenteId.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado

                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar el residente: " + ex.Message, true); // Mostrar mensaje de error
            }
            try
            {


                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlPatologiaId.Items.FindByValue(patologiaIdSeleccionado.ToString()) != null)
                {
                    ddlPatologiaId.SelectedValue = patologiaIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlPatologiaId.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado

                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar la patologia: " + ex.Message, true); // Mostrar mensaje de error
            }


            ResidentePatologia ResidentePatologiaEdit = new ResidentePatologia()
            {

                id = Int32.Parse(this.txtId_residentePatologia.Text),
                residenteId = Int32.Parse(this.ddlResidenteId.Text),
                patologiaId = Int32.Parse(this.ddlPatologiaId.Text),
                estado = 2
            };
            Session["DatosResidentePatologia"] = ResidentePatologiaEdit;
            Response.Redirect("~/wfEditarResidentePatologia.aspx");
        }

        protected void btnEliminarResidentePatologia_Click(object sender, EventArgs e)
        {
            int residenteIdSeleccionado = 0;
            int patologiaIdSeleccionado = 0;

            Button btnConsultar = (Button)sender;

            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;
            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {
                // Asignar el valor de id a txtId.Text
                this.txtId_residentePatologia.Text = id.ToString();
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }
            if (int.TryParse(selectedRow.Cells[4].Text, out int residenteId))
            {
                residenteIdSeleccionado = residenteId;
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[4].Text no es un número entero válido.");
            }
            if (int.TryParse(selectedRow.Cells[5].Text, out int patologiaId))
            {
                patologiaIdSeleccionado = patologiaId;
            }
            else
            {
                // Manejar el error de formato incorrecto
                // (por ejemplo, mostrar un mensaje de error al usuario)
                Console.WriteLine("El valor en selectedRow.Cells[5].Text no es un número entero válido.");
            }




            //Mandando datos a los campos
            this.txtId_residentePatologia.Text = id.ToString();
            try
            {


                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlResidenteId.Items.FindByValue(residenteIdSeleccionado.ToString()) != null)
                {
                    ddlResidenteId.SelectedValue = residenteIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlResidenteId.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado

                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar el residente: " + ex.Message, true); // Mostrar mensaje de error
            }
            try
            {


                // Verificar si el valor seleccionado existe en la lista de elementos del DropDownList
                if (ddlPatologiaId.Items.FindByValue(patologiaIdSeleccionado.ToString()) != null)
                {
                    ddlPatologiaId.SelectedValue = patologiaIdSeleccionado.ToString();
                }
                else
                {
                    // El valor seleccionado no existe en la lista, realizar acción deseada
                    ddlPatologiaId.SelectedValue = string.Empty; // Asignar un valor vacío o predeterminado

                }
            }
            catch (Exception ex)
            {
                MostrarAdvertencia("Error al seleccionar la patologia: " + ex.Message, true); // Mostrar mensaje de error
            }


            ResidentePatologia ResidentePatologiaEliminar = new ResidentePatologia()
            {

                id = Int32.Parse(this.txtId_residentePatologia.Text),
                residenteId = Int32.Parse(this.ddlResidenteId.Text),
                patologiaId = Int32.Parse(this.ddlPatologiaId.Text),
                estado = 2
            };
            Session["DatosResidentePatologiaEliminar"] = ResidentePatologiaEliminar;
            Response.Redirect("~/wfEliminarResidentePatologia.aspx");
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string terminoBusqueda = txtBusqueda.Text.Trim();
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from ResidentePatologia in mininosDatabaseEntities.ResidentePatologia
                            join residente in mininosDatabaseEntities.Residente on ResidentePatologia.residenteId equals residente.id
                            join patologia in mininosDatabaseEntities.Patologia on ResidentePatologia.patologiaId equals patologia.id
                            where ResidentePatologia.estado != 3 &&
                                  (residente.nombre.Contains(terminoBusqueda) ||
                                   patologia.descripcion.Contains(terminoBusqueda))
                            select new
                            {
                                ResidentePatologiaId = ResidentePatologia.id,
                                ResidenteNombre = residente.nombre,
                                PatologiaDescripcion = patologia.descripcion,
                                ResidenteId = ResidentePatologia.residenteId,
                                PatologiaId = ResidentePatologia.patologiaId,
                                ResodentePatologiaEstado = ResidentePatologia.estado,
                            };

                var result = query.ToList();

                gvResidentePatologia.AutoGenerateColumns = false;
                gvResidentePatologia.DataSource = result;
                gvResidentePatologia.DataBind();
            }
        }
    }
    
}