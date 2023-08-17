using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.Entity.Migrations;

namespace Datos
{
   public class RolDatos
    {
        public IQueryable<Rol> getRol()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<Rol> query = db.Rol;
            return query;
        }

        public void guardarRol(Rol modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Rol.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public void EditarRol(Rol modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Rol.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.descripcion = modelo.descripcion ?? consulta.descripcion;
                        consulta.estado = modelo.estado;
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        contexto.Rol.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos Rol: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EliminarRol(Rol modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Rol.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.descripcion = modelo.descripcion ?? consulta.descripcion;
                        consulta.estado = modelo.estado;
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        contexto.Rol.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {

                        throw new Exception(message: "Error en capa de datos Rol: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public List<Rol> listarRol()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {

                    List<Rol> Rol = contexto.Rol.Where(u => u.estado != 3).ToList();
                    if (Rol == null)
                    {
                        throw new Exception("La lista de los Roles es nula.");
                    }
                    return Rol;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }




            }
        }

        public bool ExisteNombre(string Nombre)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                
                bool existe = model.Rol.Any(r => r.nombre.Equals(Nombre));
                return existe;
            }
        }



    }
}
