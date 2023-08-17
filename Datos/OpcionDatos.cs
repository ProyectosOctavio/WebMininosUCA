using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.Entity.Migrations;
namespace Datos
{
    public class OpcionDatos
    {
        public IQueryable<Opcion> getOpcion()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<Opcion> query = db.Opcion;
            return query;
        }

        public void guardarOpcion(Opcion modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Opcion.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public void EditarOpcion(Opcion modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Opcion.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.accion= modelo.accion?? consulta.accion;
                        consulta.descripcion = modelo.descripcion ?? consulta.descripcion;
                        consulta.estado = modelo.estado;
                        consulta.url = modelo.url ?? consulta.url;
                        
                        contexto.Opcion.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos Opcion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EliminarOpcion(Opcion modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Opcion.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.accion = modelo.accion ?? consulta.accion;
                        consulta.descripcion = modelo.descripcion ?? consulta.descripcion;
                        consulta.estado = modelo.estado;
                        consulta.url = modelo.url ?? consulta.url;
                        contexto.Opcion.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {

                        throw new Exception(message: "Error en capa de datos Opcion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public List<Opcion> listarOpcion()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {

                    List<Opcion> Opcion = contexto.Opcion.Where(u => u.estado != 3).ToList();
                    if (Opcion == null)
                    {
                        throw new Exception("La lista de las Opciones es nula.");
                    }
                    return Opcion;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }




            }
        }
        public bool ExisteAccion(string Accion)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                // Verificar si existe algún usuario con el nombre de usuario dado
                bool existe = model.Opcion.Any(o => o.accion.Equals(Accion));
                return existe;
            }
        }

        public bool ExisteURL(string URL)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                // Verificar si existe algún usuario con el correo electrónico dado
                bool existe = model.Opcion.Any(o => o.url.Equals(URL));
                return existe;
            }
        }



    }
}

