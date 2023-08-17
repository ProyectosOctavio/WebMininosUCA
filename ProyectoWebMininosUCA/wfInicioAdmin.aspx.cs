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
    public partial class wfInicioAdmin : System.Web.UI.Page
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
    }
}