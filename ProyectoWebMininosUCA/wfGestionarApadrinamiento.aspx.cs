using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Entidades;
using Negocio;


namespace ProyectoWebMininosUCA
{
    public partial class wfGestionarApadrinamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                AgregarColumnas();



            }

            listarApadrinados();
            listarApadrinadosHistorial();

        }

        public object SqlHelper { get; private set; }


        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "ApadrinadoId", HeaderText = "Id" },
            new { NombreCampo = "ApadrinadoDonanteNombre", HeaderText = "Nombre" },
            new { NombreCampo = "ApadrinadoDonanteApellido", HeaderText = "Apellido" },
            new { NombreCampo = "ApadrinadoDonanteAlias", HeaderText = "Alias" },
            new { NombreCampo = "ApadrinadoResidenteNombre", HeaderText = "Residente" },
             new { NombreCampo = "ApadrinadoEstado", HeaderText = "Estado" },
             new { NombreCampo = "ApadrinadoDonanteId", HeaderText = "id del donante"},
             new { NombreCampo = "ApadrinadoResidenteId", HeaderText = "id del residente"},

        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvApadrinarActiva.Columns.Add(boundField);
                    gvApadrinarHistorial.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }

        private void listarApadrinados()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from residenteDonante in mininosDatabaseEntities.ResidenteDonante
                            join donante in mininosDatabaseEntities.Donante on residenteDonante.donanteId equals donante.id
                            join residente in mininosDatabaseEntities.Residente on residenteDonante.residenteId equals residente.id
                            where residenteDonante.estado != 2 && residenteDonante.estado != 3
                            orderby residenteDonante.donanteId
                            select new
                            {
                                ApadrinadoId = residenteDonante.id,
                                ApadrinadoDonanteNombre = donante.nombre,
                                ApadrinadoDonanteApellido = donante.apellido,
                                ApadrinadoDonanteAlias = donante.alias,
                                ApadrinadoResidenteNombre = residente.nombre,
                                ApadrinadoEstado = residenteDonante.estado,
                                ApadrinadoDonanteId = residenteDonante.donanteId,
                                ApadrinadoResidenteId = residenteDonante.residenteId
                            };

                var result = query.ToList();

                gvApadrinarActiva.AutoGenerateColumns = false;
                gvApadrinarActiva.DataSource = result;
                gvApadrinarActiva.DataBind();
            }
        }


        protected void btnBuscarActiva_Click(object sender, EventArgs e)
        {
            string valorBusqueda = txtBusquedaActiva.Text.Trim();

            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from residenteDonante in mininosDatabaseEntities.ResidenteDonante
                            join donante in mininosDatabaseEntities.Donante on residenteDonante.donanteId equals donante.id
                            join residente in mininosDatabaseEntities.Residente on residenteDonante.residenteId equals residente.id
                            where residenteDonante.estado != 2 && residenteDonante.estado != 3 &&
                                  (donante.nombre.Contains(valorBusqueda) ||
                                   donante.apellido.Contains(valorBusqueda) ||
                                   donante.alias.Contains(valorBusqueda) ||
                                   residente.nombre.Contains(valorBusqueda))
                            orderby residenteDonante.donanteId
                            select new
                            {
                                ApadrinadoId = residenteDonante.id,
                                ApadrinadoDonanteNombre = donante.nombre,
                                ApadrinadoDonanteApellido = donante.apellido,
                                ApadrinadoDonanteAlias = donante.alias,
                                ApadrinadoResidenteNombre = residente.nombre,
                                ApadrinadoEstado = residenteDonante.estado,
                                ApadrinadoDonanteId = residenteDonante.donanteId,
                                ApadrinadoResidenteId = residenteDonante.residenteId
                            };

                var result = query.ToList();

                gvApadrinarActiva.AutoGenerateColumns = false;
                gvApadrinarActiva.DataSource = result;
                gvApadrinarActiva.DataBind();
            }
        }


        private void listarApadrinadosHistorial()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from residenteDonante in mininosDatabaseEntities.ResidenteDonante
                            join donante in mininosDatabaseEntities.Donante on residenteDonante.donanteId equals donante.id
                            join residente in mininosDatabaseEntities.Residente on residenteDonante.residenteId equals residente.id
                            where residenteDonante.estado == 2
                            orderby residenteDonante.donanteId
                            select new
                            {
                                ApadrinadoId = residenteDonante.id,
                                ApadrinadoDonanteNombre = donante.nombre,
                                ApadrinadoDonanteApellido = donante.apellido,
                                ApadrinadoDonanteAlias = donante.alias,
                                ApadrinadoResidenteNombre = residente.nombre,
                                ApadrinadoEstado = residenteDonante.estado,
                                ApadrinadoDonanteId = residenteDonante.donanteId,
                                ApadrinadoResidenteId = residenteDonante.residenteId
                            };

                var result = query.ToList();

                gvApadrinarHistorial.AutoGenerateColumns = false;
                gvApadrinarHistorial.DataSource = result;
                gvApadrinarHistorial.DataBind();
            }
        }

        protected void btnBuscarHistorial_Click(object sender, EventArgs e)
        {
            string valorBusqueda = txtBuscarHistorial.Text.Trim();

            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from residenteDonante in mininosDatabaseEntities.ResidenteDonante
                            join donante in mininosDatabaseEntities.Donante on residenteDonante.donanteId equals donante.id
                            join residente in mininosDatabaseEntities.Residente on residenteDonante.residenteId equals residente.id
                            where residenteDonante.estado == 2 &&
                                  (donante.nombre.Contains(valorBusqueda) ||
                                   donante.apellido.Contains(valorBusqueda) ||
                                   donante.alias.Contains(valorBusqueda) ||
                                   residente.nombre.Contains(valorBusqueda))
                            orderby residenteDonante.donanteId
                            select new
                            {
                                ApadrinadoId = residenteDonante.id,
                                ApadrinadoDonanteNombre = donante.nombre,
                                ApadrinadoDonanteApellido = donante.apellido,
                                ApadrinadoDonanteAlias = donante.alias,
                                ApadrinadoResidenteNombre = residente.nombre,
                                ApadrinadoEstado = residenteDonante.estado,
                                ApadrinadoDonanteId = residenteDonante.donanteId,
                                ApadrinadoResidenteId = residenteDonante.residenteId
                            };

                var result = query.ToList();

                gvApadrinarHistorial.AutoGenerateColumns = false;
                gvApadrinarHistorial.DataSource = result;
                gvApadrinarHistorial.DataBind();
            }
        }


        protected void gvApadrinar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (gvApadrinarActiva.HeaderRow != null)
            {
                gvApadrinarActiva.HeaderRow.Cells[1].Style.Add("display", "none");
                gvApadrinarActiva.HeaderRow.Cells[6].Style.Add("display", "none");
                gvApadrinarActiva.HeaderRow.Cells[7].Style.Add("display", "none");
                gvApadrinarActiva.HeaderRow.Cells[8].Style.Add("display", "none");
            }

            foreach (GridViewRow row in gvApadrinarActiva.Rows)
            {
                row.Cells[1].Style.Add("display", "none");
                row.Cells[6].Style.Add("display", "none");
                row.Cells[7].Style.Add("display", "none");
                row.Cells[8].Style.Add("display", "none");
            }



        }

        protected void gvApadrinarH_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (gvApadrinarHistorial.HeaderRow != null)
            {
                gvApadrinarHistorial.HeaderRow.Cells[0].Style.Add("display", "none");
                gvApadrinarHistorial.HeaderRow.Cells[5].Style.Add("display", "none");
                gvApadrinarHistorial.HeaderRow.Cells[6].Style.Add("display", "none");
                gvApadrinarHistorial.HeaderRow.Cells[7].Style.Add("display", "none");
            }

            foreach (GridViewRow row in gvApadrinarHistorial.Rows)
            {
                row.Cells[0].Style.Add("display", "none");
                row.Cells[5].Style.Add("display", "none");
                row.Cells[6].Style.Add("display", "none");
                row.Cells[7].Style.Add("display", "none");
            }



        }

        protected void gv_Activa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApadrinarActiva.PageIndex = e.NewPageIndex;
            listarApadrinados();

        }

        protected void gv_Historial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           gvApadrinarHistorial.PageIndex = e.NewPageIndex;
            listarApadrinados();

        }


        protected void btnEliminarApadrinar_Click(object sender, EventArgs e)
        {

            string residente, nombre, apellido, alias;



            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {

                this.txtId.Text = id.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            if (int.TryParse(selectedRow.Cells[7].Text, out int idDonante))
            {

                this.txtDonanteId.Text = idDonante.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[7].Text no es un número entero válido.");
            }

            if (int.TryParse(selectedRow.Cells[8].Text, out int idResidente))
            {

                this.txtResidenteId.Text = idResidente.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[8].Text no es un número entero válido.");
            }


            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            alias = selectedRow.Cells[4].Text;
           residente = selectedRow.Cells[5].Text;



            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtAlias.Text = alias;
            this.txtResidente.Text = residente;
            this.txtDonanteId.Text = idDonante.ToString();
            this.txtResidenteId.Text = idResidente.ToString();




            ResidenteDonante ApadrinarEliminar = new ResidenteDonante()
            {
                id = Int32.Parse(this.txtId.Text),
                donanteId= Int32.Parse(this.txtDonanteId.Text),
                residenteId = Int32.Parse(this.txtResidenteId.Text),
                estado = 1


            };

            Donante donanteEliminar = new Donante()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                alias = this.txtAlias.Text,
                estado = 1


            };

            Residente residenteEliminar = new Residente()
            {
                id = Int32.Parse(this.txtId.Text),
              nombre = this.txtResidente.Text,
                estado = 1


            };

            Session["DatosApadrinarEliminar"] = ApadrinarEliminar;
            Session["DatosDonanteEliminar"] = donanteEliminar;
            Session["DatosResidenteEliminar"] = residenteEliminar;
            Response.Redirect("~/wfEliminarApadrinamiento.aspx");

        }

        protected void btnEditarApadrinar_Click(object sender, EventArgs e)
        {
            string residente, nombre, apellido, alias;



            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {

                this.txtId.Text = id.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            if (int.TryParse(selectedRow.Cells[7].Text, out int idDonante))
            {

                this.txtDonanteId.Text = idDonante.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[7].Text no es un número entero válido.");
            }

            if (int.TryParse(selectedRow.Cells[8].Text, out int idResidente))
            {

                this.txtResidenteId.Text = idResidente.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[8].Text no es un número entero válido.");
            }


            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            alias = selectedRow.Cells[4].Text;
            residente = selectedRow.Cells[5].Text;



            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtAlias.Text = alias;
            this.txtResidente.Text = residente;
            this.txtDonanteId.Text = idDonante.ToString();
            this.txtResidenteId.Text = idResidente.ToString();




            ResidenteDonante ApadrinarEditar = new ResidenteDonante()
            {
                id = Int32.Parse(this.txtId.Text),
                donanteId = Int32.Parse(this.txtDonanteId.Text),
                residenteId = Int32.Parse(this.txtResidenteId.Text),
                estado = 1


            };

            Donante donanteEditar = new Donante()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                alias = this.txtAlias.Text,
                estado = 1


            };

            Residente residenteEditar = new Residente()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtResidente.Text,
                estado = 1


            };

            Session["DatosApadrinarEditar"] = ApadrinarEditar;
            Session["DatosDonanteEditar"] = donanteEditar;
            Session["DatosResidenteEditar"] = residenteEditar;
            Response.Redirect("~/wfEditarApadrinamiento.aspx");

        }

        protected void btnResolverApadrinar_Click(object sender, EventArgs e)
        {
            string residente, nombre, apellido, alias;



            Button btnConsultar = (Button)sender;
            GridViewRow selectedRow = (GridViewRow)btnConsultar.NamingContainer;

            if (int.TryParse(selectedRow.Cells[1].Text, out int id))
            {

                this.txtId.Text = id.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[1].Text no es un número entero válido.");
            }

            if (int.TryParse(selectedRow.Cells[7].Text, out int idDonante))
            {

                this.txtDonanteId.Text = idDonante.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[7].Text no es un número entero válido.");
            }

            if (int.TryParse(selectedRow.Cells[8].Text, out int idResidente))
            {

                this.txtResidenteId.Text = idResidente.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[8].Text no es un número entero válido.");
            }


            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            alias = selectedRow.Cells[4].Text;
            residente = selectedRow.Cells[5].Text;



            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtAlias.Text = alias;
            this.txtResidente.Text = residente;
            this.txtDonanteId.Text = idDonante.ToString();
            this.txtResidenteId.Text = idResidente.ToString();




            ResidenteDonante ApadrinarResolver = new ResidenteDonante()
            {
                id = Int32.Parse(this.txtId.Text),
                donanteId = Int32.Parse(this.txtDonanteId.Text),
                residenteId = Int32.Parse(this.txtResidenteId.Text),
                estado = 1


            };

            Donante donanteResolver = new Donante()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                alias = this.txtAlias.Text,
                estado = 1


            };

            Residente residenteResolver = new Residente()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtResidente.Text,
                estado = 1


            };

            Session["DatosApadrinarResolver"] = ApadrinarResolver;
            Session["DatosDonanteResolver"] = donanteResolver;
            Session["DatosResidenteResolver"] = residenteResolver;
            Response.Redirect("~/wfResolverApadrinamiento.aspx");

        }
    }

}