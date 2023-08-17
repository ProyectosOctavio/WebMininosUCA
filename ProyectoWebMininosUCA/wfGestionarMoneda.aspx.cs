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
    public partial class wfGestionMonetaria : System.Web.UI.Page
    {
   

        public object SqlHelper { get; private set; }


        private bool GridViewColumnsAdded = false;

        private void AgregarColumnas()
        {
            if (!GridViewColumnsAdded)
            {
                var columnas = new[]
                {
            new { NombreCampo = "MonetarioId", HeaderText = "Id" },
            new { NombreCampo = "MonetarioDonanteNombre", HeaderText = "Nombre" },
            new { NombreCampo = "MonetarioDonanteApellido", HeaderText = "Apellido" },
            new { NombreCampo = "MonetarioDonanteAlias", HeaderText = "Alias" },
            new { NombreCampo = "MonetarioMonto", HeaderText = "Monto" },
            new { NombreCampo = "MonetarioVoucher", HeaderText = "Voucher" },
            new { NombreCampo = "MonetarioFecha", HeaderText = "Fecha de la donacion"},
             new { NombreCampo = "MonetarioEstado", HeaderText = "Estado de la donacion" },
             new { NombreCampo = "MonetarioDonanteId", HeaderText = "id del donante"},
            
        };

                foreach (var columna in columnas)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = columna.NombreCampo;
                    boundField.HeaderText = columna.HeaderText;
                    gvMonedaActiva.Columns.Add(boundField);
                    gvMonedaHistorial.Columns.Add(boundField);
                }

                GridViewColumnsAdded = true;
            }
        }

        private void listarDonacionesMonetarias() 
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from monetaria in mininosDatabaseEntities.DonacionMonetaria
                            join donante in mininosDatabaseEntities.Donante on monetaria.donanteId equals donante.id
                            where monetaria.estado != 2 && monetaria.estado != 3
                            orderby monetaria.fecha descending
                            select new
                            {
                                MonetarioId = monetaria.id,
                                MonetarioDonanteNombre = donante.nombre,
                                MonetarioDonanteApellido = donante.apellido,
                                MonetarioDonanteAlias = donante.alias,
                                MonetarioMonto = monetaria.montoNi,
                                MonetarioVoucher = monetaria.voucher,
                                MonetarioFecha = monetaria.fecha,
                                MonetarioEstado = monetaria.estado,
                                MonetarioDonanteId = monetaria.donanteId

                            };

                var result = query.ToList();

                gvMonedaActiva.AutoGenerateColumns = false;
                gvMonedaActiva.DataSource = result;
                gvMonedaActiva.DataBind();
            }
        }

        protected void btnBuscarActiva_Click(object sender, EventArgs e)
        {
            string valorBusqueda = txtBusquedaActiva.Text.Trim();

            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from monetaria in mininosDatabaseEntities.DonacionMonetaria
                            join donante in mininosDatabaseEntities.Donante on monetaria.donanteId equals donante.id
                            where monetaria.estado != 2 && monetaria.estado != 3 &&
                                  (donante.nombre.Contains(valorBusqueda) ||
                                   donante.apellido.Contains(valorBusqueda) ||
                                   donante.alias.Contains(valorBusqueda) ||
                                   monetaria.fecha.ToString().Contains(valorBusqueda) ||
                                   monetaria.montoNi.ToString().Contains(valorBusqueda))
                            orderby monetaria.fecha descending
                            select new
                            {
                                MonetarioId = monetaria.id,
                                MonetarioDonanteNombre = donante.nombre,
                                MonetarioDonanteApellido = donante.apellido,
                                MonetarioDonanteAlias = donante.alias,
                                MonetarioMonto = monetaria.montoNi,
                                MonetarioVoucher = monetaria.voucher,
                                MonetarioFecha = monetaria.fecha,
                                MonetarioEstado = monetaria.estado,
                                MonetarioDonanteId = monetaria.donanteId
                            };

                var result = query.ToList();

                gvMonedaActiva.AutoGenerateColumns = false;
                gvMonedaActiva.DataSource = result;
                gvMonedaActiva.DataBind();
            }
        }

        protected void btnBuscarHistorial_Click(object sender, EventArgs e)
        {
            string valorBusqueda = txtBuscarHistorial.Text.Trim();

            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from monetaria in mininosDatabaseEntities.DonacionMonetaria
                            join donante in mininosDatabaseEntities.Donante on monetaria.donanteId equals donante.id
                            where monetaria.estado == 2 &&
                                  (donante.nombre.Contains(valorBusqueda) ||
                                   donante.apellido.Contains(valorBusqueda) ||
                                   donante.alias.Contains(valorBusqueda) ||
                                   monetaria.fecha.ToString().Contains(valorBusqueda) ||
                                   monetaria.montoNi.ToString().Contains(valorBusqueda))
                            orderby monetaria.fecha descending
                            select new
                            {
                                MonetarioId = monetaria.id,
                                MonetarioDonanteNombre = donante.nombre,
                                MonetarioDonanteApellido = donante.apellido,
                                MonetarioDonanteAlias = donante.alias,
                                MonetarioMonto = monetaria.montoNi,
                                MonetarioVoucher = monetaria.voucher,
                                MonetarioFecha = monetaria.fecha,
                                MonetarioEstado = monetaria.estado,
                                MonetarioDonanteId = monetaria.donanteId
                            };

                var result = query.ToList();

                gvMonedaHistorial.AutoGenerateColumns = false;
                gvMonedaHistorial.DataSource = result;
                gvMonedaHistorial.DataBind();
            }
        }



        private void listarDonacionesMonetariasHistorial()
        {
            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                var query = from monetaria in mininosDatabaseEntities.DonacionMonetaria
                            join donante in mininosDatabaseEntities.Donante on monetaria.donanteId equals donante.id
                            where monetaria.estado == 2
                            orderby monetaria.fecha descending
                            select new
                            {
                                MonetarioId = monetaria.id,
                                MonetarioDonanteNombre = donante.nombre,
                                MonetarioDonanteApellido = donante.apellido,
                                MonetarioDonanteAlias = donante.alias,
                                MonetarioMonto = monetaria.montoNi,
                                MonetarioVoucher = monetaria.voucher,
                                MonetarioFecha = monetaria.fecha,
                                MonetarioEstado = monetaria.estado,
                                MonetarioDonanteId = monetaria.donanteId

                            };

                var result = query.ToList();

                gvMonedaHistorial.AutoGenerateColumns = false;
                gvMonedaHistorial.DataSource = result;
                gvMonedaHistorial.DataBind();
            }
        }

        protected void gvMoneda_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (gvMonedaActiva.HeaderRow != null)
            {
                gvMonedaActiva.HeaderRow.Cells[1].Style.Add("display", "none");
                gvMonedaActiva.HeaderRow.Cells[6].Style.Add("display", "none");
                gvMonedaActiva.HeaderRow.Cells[8].Style.Add("display", "none");
                gvMonedaActiva.HeaderRow.Cells[9].Style.Add("display", "none");
            }

            foreach (GridViewRow row in gvMonedaActiva.Rows)
            {
                row.Cells[1].Style.Add("display", "none");
                row.Cells[6].Style.Add("display", "none");
                row.Cells[8].Style.Add("display", "none");
                row.Cells[9].Style.Add("display", "none");

            }


        }

        protected void gvMonedaH_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (gvMonedaHistorial.HeaderRow != null)
            {
                gvMonedaHistorial.HeaderRow.Cells[0].Style.Add("display", "none");
                gvMonedaHistorial.HeaderRow.Cells[5].Style.Add("display", "none");
                gvMonedaHistorial.HeaderRow.Cells[7].Style.Add("display", "none");
                gvMonedaHistorial.HeaderRow.Cells[8].Style.Add("display", "none");
            }

            foreach (GridViewRow row in gvMonedaHistorial.Rows)
            {
                row.Cells[0].Style.Add("display", "none");
                row.Cells[5].Style.Add("display", "none");
                row.Cells[7].Style.Add("display", "none");
                row.Cells[8].Style.Add("display", "none");

            }




        }

        protected void gv_Activa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMonedaActiva.PageIndex = e.NewPageIndex;
            listarDonacionesMonetarias();
          
        }

        protected void gv_Historial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMonedaHistorial.PageIndex = e.NewPageIndex;
            listarDonacionesMonetarias();

        }




        protected void btnEditarMoneda_Click(object sender, EventArgs e)
        {

            string montoni , nombre, apellido, alias;
           
            

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

            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            alias = selectedRow.Cells[4].Text;
            montoni = selectedRow.Cells[5].Text;
           
           
           

            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtMonto.Text = montoni;
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtAlias.Text = alias;





            DonacionMonetaria monetariaEdit = new DonacionMonetaria()
            {
                id = Int32.Parse(this.txtId.Text),
                montoNi = Double.Parse(this.txtMonto.Text),
                estado = 1


            };

            Donante donanteEdit = new Donante()
            {
                id = Int32.Parse(this.txtId.Text),
                nombre = this.txtNombre.Text,
                apellido = this.txtApellido.Text,
                alias = this.txtAlias.Text,
                estado = 1


            };

            Session["DatosMonetario"] = monetariaEdit;
            Session["DatosDonante"] = donanteEdit;
            Response.Redirect("~/wfEditarMoneda.aspx");
        }

        protected void btnEliminarMoneda_Click(object sender, EventArgs e)
        {

            string montoni, nombre, apellido, alias;



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

            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            alias = selectedRow.Cells[4].Text;
            montoni = selectedRow.Cells[5].Text;
            



            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtMonto.Text = montoni;
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtAlias.Text = alias;





            DonacionMonetaria monetariaEliminar = new DonacionMonetaria()
            {
                id = Int32.Parse(this.txtId.Text),
                montoNi = Double.Parse(this.txtMonto.Text),
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

            Session["DatosMonetarioEliminar"] = monetariaEliminar;
            Session["DatosDonanteEliminar"] = donanteEliminar;
            Response.Redirect("~/wfEliminarMoneda.aspx");
        }

        protected void btnResolverMoneda_Click(object sender, EventArgs e)
        {

            string montoni, nombre, apellido, alias;



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

            nombre = selectedRow.Cells[2].Text;
            apellido = selectedRow.Cells[3].Text;
            alias = selectedRow.Cells[4].Text;
            montoni = selectedRow.Cells[5].Text;
            



            //Mandando datos a los campos
            this.txtId.Text = id.ToString();
            this.txtMonto.Text = montoni;
            this.txtNombre.Text = nombre;
            this.txtApellido.Text = apellido;
            this.txtAlias.Text = alias;





            DonacionMonetaria monetariaResolver = new DonacionMonetaria()
            {
                id = Int32.Parse(this.txtId.Text),
                montoNi = Double.Parse(this.txtMonto.Text),
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

            Session["DatosMonetarioResolver"] = monetariaResolver;
            Session["DatosDonanteResolver"] = donanteResolver;
            Response.Redirect("~/wfResolverMoneda.aspx");

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                AgregarColumnas();
               


            }

            listarDonacionesMonetarias();
            listarDonacionesMonetariasHistorial();


        }
    }
}