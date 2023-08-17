using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ZonaDatos
    {
        public IQueryable<Zona> Zona()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<Zona> query = db.Zona;
            return query;
        }

        public void guardarZona(Zona modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Zona.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public void EditarZona(Zona modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Zona.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        consulta.estado = modelo.estado;

                        contexto.Zona.AddOrUpdate(consulta);
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

        public void EliminarZona(Zona modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Zona.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        consulta.estado = modelo.estado;

                        contexto.Zona.AddOrUpdate(consulta);
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

        public List<Zona> listarZona()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {
                    List<Zona> Zona = contexto.Zona.Where(p => p.estado != 3).ToList();
                    if (Zona == null)
                    {
                        throw new Exception("La lista de patologias es nula.");
                    }
                    return Zona;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }

        public bool ExisteZona(string nombZona)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                // Verificar si existe algún usuario con el nombre de usuario dado
                bool existe = model.Zona.Any(p => p.nombre.Equals(nombZona));
                return existe;
            }
        }

        public List<Zona> LlenarDropDownListZona()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {
                    List<Zona> Zona = contexto.Zona.ToList();
                    if (Zona == null)
                    {
                        throw new Exception("La lista de las zonas es nula.");
                    }
                    return Zona;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }
    }
}
