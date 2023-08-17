using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ResidentePatologiaDatos
    {
        public IQueryable<ResidentePatologia> getResidentePatologia()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<ResidentePatologia> query = db.ResidentePatologia;
            return query;
        }

        public void guardarResidentePatologia(ResidentePatologia modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.ResidentePatologia.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public void EditarResidentePatologia(ResidentePatologia modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.ResidentePatologia.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.patologiaId = modelo.patologiaId ?? consulta.patologiaId;
                        consulta.residenteId = modelo.residenteId ?? consulta.residenteId;
                        consulta.estado = modelo.estado;

                        contexto.ResidentePatologia.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos ResidentePatologia: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        public void EliminarResidentePatologia(ResidentePatologia modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.ResidentePatologia.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.patologiaId = modelo.patologiaId ?? consulta.patologiaId;
                        consulta.residenteId = modelo.residenteId ?? consulta.residenteId;
                        consulta.estado = modelo.estado;

                        contexto.ResidentePatologia.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos ResidentePatologia: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public List<ResidentePatologia> listarResidentePatologia()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {
                    List<ResidentePatologia> residentePatologia = contexto.ResidentePatologia.Where(rp => rp.estado != 3).ToList();
                    if (residentePatologia == null)
                    {
                        throw new Exception("La lista de ResidentePatologias es nula.");
                    }
                    return residentePatologia;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }

        public bool ExisteResidentePatologia(int ResidenteId, int PatologiaId)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                // Verificar si existe algún usuario con el nombre de usuario dado
                bool existe = model.ResidentePatologia.Any(rp => rp.residenteId.Equals(ResidenteId) && rp.patologiaId.Equals(PatologiaId));
                return existe;
            }
        }
    }
}
