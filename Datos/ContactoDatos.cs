using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Data.Entity;

namespace Datos
{
    public class ContactoDatos
    {

        public Contacto getContacto()
        {
            using (mininosDatabaseEntities db = new mininosDatabaseEntities())
            {
                IQueryable<Contacto> query = db.Contacto;
                return query.First();
            }
        }

        public void EditarContacto(Contacto modelo)
        {
            using (mininosDatabaseEntities db = new mininosDatabaseEntities())
            {
                var query = db.Contacto.Find(modelo.id);
                if (query != null)
                {
                    try
                    {
                        // Marcar la entidad como modificada antes de guardar los cambios.
                        db.Entry(query).State = EntityState.Modified;

                        // Actualizar solo las propiedades modificadas en el objeto consulta.
                        query.telefono = modelo.telefono ?? query.telefono;
                        query.correo = modelo.correo ?? query.correo;
                        query.correo2 = modelo.correo2 ?? query.correo2;
                        query.twitter = modelo.twitter ?? query.twitter;
                        query.insta = modelo.insta ?? query.insta;
                        query.facebook = modelo.facebook ?? query.facebook;
                        db.Contacto.AddOrUpdate(query);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos contacto: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public List<Contacto> listarContacto()
        {
            using (mininosDatabaseEntities db = new mininosDatabaseEntities())
            {
               List<Contacto> contactos = db.Contacto.ToList();
               return contactos;
            }
        }
    }
}