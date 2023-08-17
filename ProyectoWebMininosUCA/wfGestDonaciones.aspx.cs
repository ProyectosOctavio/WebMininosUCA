using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProyectoWebMininosUCA
{
    public partial class wfGestDonaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDonacion("activo", 2);
                cargarDonacion("historial", 3);
            }
        }

        protected List<Donante> cargarDonacion(string tipo, int estado)
        {
            List<Donante> dn = new List<Donante>();

            using (var contextD = new mininosDatabaseEntities())
            {
                try
                {
                    contextD.Database.Connection.Open();
                    string query = @"SELECT dbo.DonacionMonetaria.fecha AS 'fechaMonetaria',
                    dbo.DonacionMonetaria.montoNi AS 'Monto',
                    dbo.DonacionEspecies.tipoEspecie,
                    dbo.DonacionEspecies.fecha AS 'fechaEspecie'
                    FROM dbo.DonacionMonetaria
                    INNER JOIN dbo.DonacionEspecies ON dbo.DonacionMonetaria.id = dbo.DonacionEspecies.id
                    WHERE dbo.DonacionMonetaria.estado = @estadoMonetario AND dbo.DonacionEspecies.estado = @estadoEspecie;";

                    using (var command = new SqlCommand(query, (SqlConnection)contextD.Database.Connection))
                    {
                        if (tipo == "activo" && estado != 3)
                        {
                            command.Parameters.AddWithValue("@estadoMonetario", estado);
                            command.Parameters.AddWithValue("@estadoEspecie", estado);
                        }
                        else if (tipo == "historial" && estado == 3)
                        {
                            command.Parameters.AddWithValue("@estadoMonetario", estado);
                            command.Parameters.AddWithValue("@estadoEspecie", estado);
                        }

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Donante dona = new Donante();

                                DonacionMonetaria donem = new DonacionMonetaria();
                                if (!reader.IsDBNull(reader.GetOrdinal("fechaMonetaria")))
                                {
                                    donem.fecha = reader.GetDateTime(reader.GetOrdinal("fechaMonetaria"));
                                   
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("Monto")))
                                {
                                    donem.montoNi = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Monto")));

                                }
                                DonacionEspecies donae = new DonacionEspecies();
                                if (!reader.IsDBNull(reader.GetOrdinal("tipoEspecie")))
                                {
                                    donae.tipoEspecie = reader.GetString(reader.GetOrdinal("tipoEspecie"));

                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("fechaEspecie")))
                                {

                                    donae.fecha = reader.GetDateTime(reader.GetOrdinal("fechaEspecie"));
                                

                                }

                                dona.DonacionMonetaria.Add(donem);
                                dona.DonacionEspecies.Add(donae);
                                dn.Add(dona);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Response.Write("Error de base de datos: " + ex.Message);
                    // Puedes tomar acciones adicionales aquí, como mostrar un mensaje de error al usuario
                }
                catch (Exception ex)
                {
                    Response.Write("Error inesperado: " + ex.Message);
                    // Puedes tomar acciones adicionales aquí, como mostrar un mensaje de error al usuario
                }
                finally
                {
                    contextD.Database.Connection.Close();
                }
            }

            return dn;
        }
    }
}
