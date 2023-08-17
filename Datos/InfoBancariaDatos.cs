using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class InfoBancariaDatos
    {
        public IQueryable<InfoBancaria> getInfoBancaria()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<InfoBancaria> query = db.InfoBancaria;
            return query;
        }

        public void guardarInfoBancaria(InfoBancaria modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.InfoBancaria.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public void EditarInfoBancaria(InfoBancaria modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.InfoBancaria.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.banco = modelo.banco ?? consulta.banco;
                        consulta.numeroCuenta = modelo.numeroCuenta ?? consulta.numeroCuenta;
                        consulta.moneda = modelo.moneda ?? consulta.moneda;
                        consulta.estado = modelo.estado;

                        contexto.InfoBancaria.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos infoBancaria: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EliminarInfoBancaria(InfoBancaria modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.InfoBancaria.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.banco = modelo.banco ?? consulta.banco;
                        consulta.numeroCuenta = modelo.numeroCuenta ?? consulta.numeroCuenta;
                        consulta.moneda = modelo.moneda ?? consulta.moneda;
                        consulta.estado = modelo.estado;

                        contexto.InfoBancaria.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos infoBancaria: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public List<InfoBancaria> listarInfoBancaria()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {
                    List<InfoBancaria> infoBancaria = contexto.InfoBancaria.Where(i => i.estado != 3).ToList();
                    if (infoBancaria == null)
                    {
                        throw new Exception("La lista de informacion bancaria es nula.");
                    }
                    return infoBancaria;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }

        public bool ExisteInfoBancaria(string numCuenta)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                // Verificar si existe algún usuario con el nombre de usuario dado
                bool existe = model.InfoBancaria.Any(i => i.numeroCuenta.Equals(numCuenta));
                return existe;
            }
        }
    }
}
