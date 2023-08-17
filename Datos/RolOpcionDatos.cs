using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.Entity.Migrations;

namespace Datos
{
    public class RolOpcionDatos
    {
        public IQueryable<RolOpcion> GetRolOpcion()
        {
            using (var db = new mininosDatabaseEntities())
            {
                return db.RolOpcion.AsQueryable();
            }
        }

        public void GuardarRolOpcion(RolOpcion modelo)
        {
            using (var contexto = new mininosDatabaseEntities())
            {
                contexto.RolOpcion.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public void EditarRolOpcion(RolOpcion modelo)
        {
            using (var contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.RolOpcion.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.estado = modelo.estado;
                        consulta.rolId = modelo.rolId;
                        consulta.opcionId = modelo.opcionId;
                        contexto.RolOpcion.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error en la capa de datos RolOpcion: " + e.Message, e);
                    }
                }
            }
        }

        public void EliminarRolOpcion(RolOpcion modelo)
        {
            using (var contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.RolOpcion.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.estado = modelo.estado;
                        consulta.rolId = modelo.rolId;
                        consulta.opcionId = modelo.opcionId;
                        contexto.RolOpcion.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error en la capa de datos RolOpcion: " + e.Message, e);
                    }
                }
            }
        }

        public List<RolOpcion> ListarRolOpcion()
        {
            using (var contexto = new mininosDatabaseEntities())
            {
                try
                {
                    List<RolOpcion> rolOpciones = contexto.RolOpcion.Where(ro => ro.estado != 3).ToList();
                    if (rolOpciones == null)
                    {
                        throw new Exception("La lista de RolOpcion es nula.");
                    }
                    return rolOpciones;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }
    }
}
