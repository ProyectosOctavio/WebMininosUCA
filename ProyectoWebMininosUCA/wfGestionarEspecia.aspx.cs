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
    public partial class wfGestionarEspecias : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                AgregarColumnas();



            }

            listarDonacionesEspecies();
            listarDonacionesEspeciesHistorial();

        }


        public object SqlHelper { get; private set; }


        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "EspeciesId", HeaderText = "Id" },
            new { NombreCampo = "EspecieDonanteNombre", HeaderText = "Nombre" },
            new { NombreCampo = "EspecieDonanteApellido", HeaderText = "Apellido" },
            new { NombreCampo = "EspecieDonanteAlias", HeaderText = "Alias" },
            new { NombreCampo = "EspecieCantidad", HeaderText = "Cantidad" },
            new { NombreCampo = "EspecieTipo", HeaderText = "Tipo" },
             new { NombreCampo = "EspecieMedida", HeaderText = "Unidad de medida" },
            new { NombreCampo = "EspecieFecha", HeaderText = "Fecha de la donacion"},
             new { NombreCampo = "EspecieEstado", HeaderText = "Estado de la donacion" },
             new { NombreCampo = "EspecieDonanteId", HeaderText = "id del donante"},

        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvEspeciesActiva.Columns.Add(boundField);
                    gvEspeciesHistorial.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }

        private void listarDonacionesEspecies()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from especie in mininosDatabaseEntities.DonacionEspecies
                            join donante in mininosDatabaseEntities.Donante on especie.donanteId equals donante.id
                            where especie.estado != 2 && especie.estado != 3
                            orderby especie.fecha descending
                            select new
                            {
                                EspeciesId = especie.id,
                                EspecieDonanteNombre = donante.nombre,
                                EspecieDonanteApellido = donante.apellido,
                                EspecieDonanteAlias = donante.alias,
                                EspecieCantidad = especie.cantidad,
                                EspecieTipo = especie.tipoEspecie,
                                EspecieMedida = especie.unidadMedida,
                                EspecieFecha = especie.fecha,
                                EspecieEstado = especie.estado,
                                EspecieDonanteId = especie.donanteId

                            };

                var result = query.ToList();

                gvEspeciesActiva.AutoGenerateColumns = false;
                gvEspeciesActiva.DataSource = result;
                gvEspeciesActiva.DataBind();
            }
        }

        protected void btnBuscarActiva_Click(object sender, EventArgs e)
        {
            string valorBusqueda = txtBusquedaActiva.Text.Trim();

            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from especie in mininosDatabaseEntities.DonacionEspecies
                            join donante in mininosDatabaseEntities.Donante on especie.donanteId equals donante.id
                            where especie.estado != 2 && especie.estado != 3 &&
                                  (donante.nombre.Contains(valorBusqueda) ||
                                   donante.apellido.Contains(valorBusqueda) ||
                                   donante.alias.Contains(valorBusqueda) ||
                                   especie.cantidad.ToString().Contains(valorBusqueda) ||
                                   especie.tipoEspecie.Contains(valorBusqueda) ||
                                   especie.unidadMedida.Contains(valorBusqueda) ||
                                   especie.fecha.ToString().Contains(valorBusqueda))
                            orderby especie.fecha descending
                            select new
                            {
                                EspeciesId = especie.id,
                                EspecieDonanteNombre = donante.nombre,
                                EspecieDonanteApellido = donante.apellido,
                                EspecieDonanteAlias = donante.alias,
                                EspecieCantidad = especie.cantidad,
                                EspecieTipo = especie.tipoEspecie,
                                EspecieMedida = especie.unidadMedida,
                                EspecieFecha = especie.fecha,
                                EspecieEstado = especie.estado,
                                EspecieDonanteId = especie.donanteId
                            };

                var result = query.ToList();

                gvEspeciesActiva.AutoGenerateColumns = false;
                gvEspeciesActiva.DataSource = result;
                gvEspeciesActiva.DataBind();
            }
        }

        private void listarDonacionesEspeciesHistorial()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from especie in mininosDatabaseEntities.DonacionEspecies
                            join donante in mininosDatabaseEntities.Donante on especie.donanteId equals donante.id
                            where especie.estado == 2
                            orderby especie.fecha descending
                            select new
                            {
                                EspeciesId = especie.id,
                                EspecieDonanteNombre = donante.nombre,
                                EspecieDonanteApellido = donante.apellido,
                                EspecieDonanteAlias = donante.alias,
                                EspecieCantidad = especie.cantidad,
                                EspecieTipo = especie.tipoEspecie,
                                EspecieMedida = especie.unidadMedida,
                                EspecieFecha = especie.fecha,
                                EspecieEstado = especie.estado,
                                EspecieDonanteId = especie.donanteId

                            };

                var result = query.ToList();

                gvEspeciesHistorial.AutoGenerateColumns = false;
                gvEspeciesHistorial.DataSource = result;
                gvEspeciesHistorial.DataBind();
            }
        }

        protected void btnBuscarHistorial_Click(object sender, EventArgs e)
        {
            string valorBusqueda = txtBuscarHistorial.Text.Trim();

            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from especie in mininosDatabaseEntities.DonacionEspecies
                            join donante in mininosDatabaseEntities.Donante on especie.donanteId equals donante.id
                            where especie.estado == 2 &&
                                  (donante.nombre.Contains(valorBusqueda) ||
                                   donante.apellido.Contains(valorBusqueda) ||
                                   donante.alias.Contains(valorBusqueda) ||
                                   especie.cantidad.ToString().Contains(valorBusqueda) ||
                                   especie.tipoEspecie.Contains(valorBusqueda) ||
                                   especie.unidadMedida.Contains(valorBusqueda) ||
                                   especie.fecha.ToString().Contains(valorBusqueda))
                            orderby especie.fecha descending
                            select new
                            {
                                EspeciesId = especie.id,
                                EspecieDonanteNombre = donante.nombre,
                                EspecieDonanteApellido = donante.apellido,
                                EspecieDonanteAlias = donante.alias,
                                EspecieCantidad = especie.cantidad,
                                EspecieTipo = especie.tipoEspecie,
                                EspecieMedida = especie.unidadMedida,
                                EspecieFecha = especie.fecha,
                                EspecieEstado = especie.estado,
                                EspecieDonanteId = especie.donanteId
                            };

                var result = query.ToList();

                gvEspeciesHistorial.AutoGenerateColumns = false;
                gvEspeciesHistorial.DataSource = result;
                gvEspeciesHistorial.DataBind();
            }
        }


        protected void gvEspecies_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (gvEspeciesActiva.HeaderRow != null)
            {
                gvEspeciesActiva.HeaderRow.Cells[1].Style.Add("display", "none");
                gvEspeciesActiva.HeaderRow.Cells[9].Style.Add("display", "none");
                gvEspeciesActiva.HeaderRow.Cells[10].Style.Add("display", "none");
            }

            foreach (GridViewRow row in gvEspeciesActiva.Rows)
            {
                row.Cells[1].Style.Add("display", "none");
                row.Cells[9].Style.Add("display", "none");
                row.Cells[10].Style.Add("display", "none");

            }


        }

        protected void gvEspeciesH_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (gvEspeciesHistorial.HeaderRow != null)
            {
                gvEspeciesHistorial.HeaderRow.Cells[0].Style.Add("display", "none");
                gvEspeciesHistorial.HeaderRow.Cells[8].Style.Add("display", "none");
                gvEspeciesHistorial.HeaderRow.Cells[9].Style.Add("display", "none");
            }

            foreach (GridViewRow row in gvEspeciesHistorial.Rows)
            {
                row.Cells[0].Style.Add("display", "none");
                row.Cells[8].Style.Add("display", "none");
                row.Cells[9].Style.Add("display", "none");

            }


        }

        protected void gv_Activa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEspeciesActiva.PageIndex = e.NewPageIndex;
            listarDonacionesEspecies();

        }

        protected void gv_Historial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEspeciesHistorial.PageIndex = e.NewPageIndex;
            listarDonacionesEspecies();

        }



        protected void btnEditarEspecies_Click(object sender, EventArgs e)
        {
            string cantidad, tipo, medida, nombre, apellido, alias;



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

            if (int.TryParse(selectedRow.Cells[10].Text, out int idDonante))
            {

                this.txtIdDonante.Text = idDonante.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[10].Text no es un número entero válido.");
            }

            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            alias = selectedRow.Cells[4].Text;
            cantidad = selectedRow.Cells[5].Text;
            tipo = selectedRow.Cells[6].Text;
            medida = selectedRow.Cells[7].Text;



            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtCantidad.Text = cantidad;
            this.txtTipoEspecie.Text = tipo;
            this.txtUnidadMedida.Text = medida;
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtAlias.Text = alias;
            this.txtIdDonante.Text = idDonante.ToString();





            DonacionEspecies especieEdit = new DonacionEspecies()
            {
                id = Int32.Parse(this.txtId.Text),
                cantidad = Double.Parse(this.txtCantidad.Text),
                tipoEspecie = this.txtTipoEspecie.Text,
                unidadMedida = this.txtUnidadMedida.Text,
                estado = 1


            };

            Donante donanteEdit = new Donante()
            {
                id = Int32.Parse(this.txtIdDonante.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                alias = this.txtAlias.Text,
                estado = 1


            };

            Session["DatosEspecie"] = especieEdit;
            Session["DatosDonanteEspecie"] = donanteEdit;
            Response.Redirect("~/wfEditarEspecia.aspx");
        }

        protected void btnEliminarEspecies_Click(object sender, EventArgs e)
        {
            string cantidad, tipo, medida, nombre, apellido, alias;



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

            if (int.TryParse(selectedRow.Cells[10].Text, out int idDonante))
            {

                this.txtIdDonante.Text = idDonante.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[10].Text no es un número entero válido.");
            }

            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            alias = selectedRow.Cells[4].Text;
            cantidad = selectedRow.Cells[5].Text;
            tipo = selectedRow.Cells[6].Text;
            medida = selectedRow.Cells[7].Text;



            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtCantidad.Text = cantidad;
            this.txtTipoEspecie.Text = tipo;
            this.txtUnidadMedida.Text = medida;
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtAlias.Text = alias;
            this.txtIdDonante.Text = idDonante.ToString();





            DonacionEspecies especieEliminar= new DonacionEspecies()
            {
                id = Int32.Parse(this.txtId.Text),
                cantidad = Double.Parse(this.txtCantidad.Text),
                tipoEspecie = this.txtTipoEspecie.Text,
                unidadMedida = this.txtUnidadMedida.Text,
                estado = 1


            };

            Donante donanteEliminar = new Donante()
            {
                id = Int32.Parse(this.txtIdDonante.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                alias = this.txtAlias.Text,
                estado = 1


            };

            Session["DatosEspecieEliminar"] = especieEliminar;
            Session["DatosDonanteEspecieEliminar"] = donanteEliminar;
            Response.Redirect("~/wfEliminarEspecia.aspx");


        }

        protected void btnResolverEspecies_Click(object sender, EventArgs e)
        {
            string cantidad, tipo, medida, nombre, apellido, alias;



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

            if (int.TryParse(selectedRow.Cells[10].Text, out int idDonante))
            {

                this.txtIdDonante.Text = idDonante.ToString();

            }
            else
            {

                Console.WriteLine("El valor en selectedRow.Cells[10].Text no es un número entero válido.");
            }

            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            alias = selectedRow.Cells[4].Text;
            cantidad = selectedRow.Cells[5].Text;
            tipo = selectedRow.Cells[6].Text;
            medida = selectedRow.Cells[7].Text;



            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtCantidad.Text = cantidad;
            this.txtTipoEspecie.Text = tipo;
            this.txtUnidadMedida.Text = medida;
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtAlias.Text = alias;
            this.txtIdDonante.Text = idDonante.ToString();





            DonacionEspecies especieResolver = new DonacionEspecies()
            {
                id = Int32.Parse(this.txtId.Text),
                cantidad = Double.Parse(this.txtCantidad.Text),
                tipoEspecie = this.txtTipoEspecie.Text,
                unidadMedida = this.txtUnidadMedida.Text,
                estado = 1


            };

            Donante donanteResolver = new Donante()
            {
                id = Int32.Parse(this.txtIdDonante.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                alias = this.txtAlias.Text,
                estado = 1


            };

            Session["DatosEspecieResolver"] = especieResolver;
            Session["DatosDonanteEspecieResolver"] = donanteResolver;
            Response.Redirect("~/wfResolverEspecia.aspx");



        }

    }
}