using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class PatologiaDatos
    {
        public IQueryable<Patologia> getPatologia()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<Patologia> query = db.Patologia;
            return query;
        }

        public void guardarPatologia(Patologia modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Patologia.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public void EditarPatologia(Patologia modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Patologia.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.descripcion = modelo.descripcion ?? consulta.descripcion;
                        consulta.estado = modelo.estado;

                        contexto.Patologia.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos patologia: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EliminarPatologia(Patologia modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Patologia.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.descripcion = modelo.descripcion ?? consulta.descripcion;
                        consulta.estado = modelo.estado;

                        contexto.Patologia.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos patologia: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        public List<Patologia> listarPatologia()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {
                    List<Patologia> patologia = contexto.Patologia.Where(p => p.estado != 3).ToList();
                    if (patologia == null)
                    {
                        throw new Exception("La lista de patologias es nula.");
                    }
                    return patologia;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }

        public bool ExistePatologia(string descPatologia)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                // Verificar si existe algún usuario con el nombre de usuario dado
                bool existe = model.Patologia.Any(p => p.descripcion.Equals(descPatologia));
                return existe;
            }
        }


        public Patologia ObtenerPatologiaPorDescripcion(string descripcion)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                Patologia patologia = contexto.Patologia.FirstOrDefault(p => p.descripcion.Equals(descripcion));
                return patologia;
            }
        }

    }





}
