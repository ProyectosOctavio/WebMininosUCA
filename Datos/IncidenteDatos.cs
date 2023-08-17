using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using System.Data.Entity.Migrations;
using System.Diagnostics;

namespace Datos
{
    public class IncidenteDatos
    {
        Incidente  Inc = new Incidente();
        NivelDeRiesgo NDR = new NivelDeRiesgo();
    

        public IQueryable<Incidente> getIncidentes()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<Incidente> query = db.Incidente;
            return query;
        }

        public void guardarIncidente(Incidente modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Incidente.Add(modelo);
                contexto.SaveChanges();
            }
        }



        public IQueryable<NivelDeRiesgo> getNiveldeRiesgo()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<NivelDeRiesgo> query = db.NivelDeRiesgo;
            return query;
        }

        public List<NivelDeRiesgo> listarNivelRiesgo()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {
                    List<NivelDeRiesgo> nivelDeRiesgos = contexto.NivelDeRiesgo.ToList();
                    if (nivelDeRiesgos == null)
                    {
                        throw new Exception("La lista de residentes es nula.");
                    }
                    return nivelDeRiesgos;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }

        public List<Incidente> GetIncidentesT()
        {
            try
            {
                mininosDatabaseEntities dc = new mininosDatabaseEntities();
                return dc.Incidente.ToList();
            }
            catch (Exception err)
            {

                throw (err);
            }
        }

        public void actualizarIncidente(Incidente dt)
        {
            try
            {
                mininosDatabaseEntities dc = new mininosDatabaseEntities();
                Incidente infoBD = dc.Incidente.Where(x => x.id == dt.id).FirstOrDefault();
                infoBD.estado = dt.estado;
                dc.SaveChanges();
            }
            catch (Exception err)
            {

                throw (err);
            }
        }
    }
}