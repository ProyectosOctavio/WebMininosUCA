using System;
using Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoWebMininosUCA
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        protected List<Usuario> CargarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                try
                {
                    mininosDatabaseEntities.Database.Connection.Open();

                    string query = "SELECT dbo.Usuario.fotoId, dbo.Usuario.nombre, dbo.Usuario.apellido FROM dbo.Usuario WHERE estado != 3 AND dbo.Usuario.username != 'admin';";

                    SqlCommand command = new SqlCommand(query, (SqlConnection)mininosDatabaseEntities.Database.Connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Usuario item = new Usuario();

                        item.fotoId = (byte[])reader["fotoId"];

                        item.nombre = (string)reader["nombre"];

                        item.apellido = (string)reader["apellido"];
                        
                        usuarios.Add(item);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    mininosDatabaseEntities.Database.Connection.Close();
                }
            }

            return usuarios;
        }

        public class DonanteMonetario
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Alias { get; set; }
            public double Monto { get; set; }
        }

        protected List<DonanteMonetario> CargarDonantesMonetarios()
        {
            List<DonanteMonetario> donantes = new List<DonanteMonetario>();

            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                try
                {
                    mininosDatabaseEntities.Database.Connection.Open();

                    var query = from donacion in mininosDatabaseEntities.DonacionMonetaria
                                join donante in mininosDatabaseEntities.Donante on donacion.donanteId equals donante.id
                                where donacion.estado == 2
                                orderby donacion.montoNi descending
                                select new DonanteMonetario
                                {
                                    Nombre = donante.nombre,
                                    Apellido = donante.apellido,
                                    Alias = donante.alias,
                                    Monto = donacion.montoNi
                                };

                    donantes = query.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    mininosDatabaseEntities.Database.Connection.Close();
                }
            }

            return donantes;
        }

        public class DonanteEspecia
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Alias { get; set; }
            public string Tipo { get; set; }
            public double Cantidad { get; set; }
            public string Medida { get; set; }
        }

        protected List<DonanteEspecia> CargarDonantesEspecias()
        {
            List<DonanteEspecia> donantes2 = new List<DonanteEspecia>();

            using (var mininosDatabaseEntities = new mininosDatabaseEntities())
            {
                try
                {
                    mininosDatabaseEntities.Database.Connection.Open();

                    var query = from especie in mininosDatabaseEntities.DonacionEspecies
                                join donante in mininosDatabaseEntities.Donante on especie.donanteId equals donante.id
                                where especie.estado == 2
                                orderby especie.cantidad descending
                                select new DonanteEspecia
                                {
                                    Nombre = donante.nombre,
                                    Apellido = donante.apellido,
                                    Alias = donante.alias,
                                    Tipo = especie.tipoEspecie,
                                    Cantidad = especie.cantidad,
                                    Medida = especie.unidadMedida
                                };

                    donantes2 = query.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    mininosDatabaseEntities.Database.Connection.Close();
                }
            }

            return donantes2;
        }






    }
}