using System;
using Entidades;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWebMininosUCA
{
    public class PublicacionDatos
    {
        public IQueryable<Publicacion> getPublicacion()
        {
            mininosDatabaseEntities dc = new mininosDatabaseEntities();
            IQueryable<Publicacion> query = dc.Publicacion;
            return query;
        }

        public void guardarPublicacion(Publicacion pbct)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Publicacion.Add(pbct);
                contexto.SaveChanges();
            }
        }

        public void EditarPublicacion(Publicacion modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Publicacion.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.fotoPublicacion = modelo.fotoPublicacion ?? consulta.fotoPublicacion;
                        consulta.titulo = modelo.titulo ?? consulta.titulo;
                        consulta.tipo = modelo.tipo ?? consulta.tipo;
                        consulta.contenido = modelo.contenido ?? consulta.contenido;
                        consulta.estado = modelo.estado;

                        contexto.Publicacion.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos publicacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EliminarPublicacion(Publicacion modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Publicacion.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.estado = modelo.estado;

                        contexto.Publicacion.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos residente: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public List<Publicacion> getPublicaciones()
        {
            try
            {
                mininosDatabaseEntities dc = new mininosDatabaseEntities();
                return dc.Publicacion.ToList();
            }
            catch (Exception err)
            {

                throw (err);
            }
        }
    }
}