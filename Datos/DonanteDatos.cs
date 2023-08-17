using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using System.Data.Entity.Migrations;
using System.Diagnostics;

namespace Datos
{
    public class DonanteDatos
    {
        public IQueryable<Donante> getDonante()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<Donante> query = db.Donante;
            return query;
        }

        public void guardarDonante(Donante modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Donante.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public int guardarDonanteId(Donante modelo)
        {
            int idDonanteGuardado;
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Donante.Add(modelo);
                contexto.SaveChanges();
                idDonanteGuardado = modelo.id; // Asignar el ID del donante guardado
            }

            return idDonanteGuardado;
        }

        public void EditarDonante(Donante modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Donante.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.alias = modelo.alias ?? consulta.alias;
                        consulta.ciudad = modelo.ciudad ?? consulta.ciudad;
                        consulta.correo = modelo.correo ?? consulta.correo;
                        consulta.apellido = modelo.apellido ?? consulta.apellido;
                        consulta.estado = modelo.estado;
                        consulta.numTelefono = modelo.numTelefono ?? consulta.numTelefono;
                        consulta.pais = modelo.pais ?? consulta.pais;
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        contexto.Donante.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos Donante: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EliminarDonante(Donante modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Donante.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.alias = modelo.alias ?? consulta.alias;
                        consulta.ciudad = modelo.ciudad ?? consulta.ciudad;
                        consulta.correo = modelo.correo ?? consulta.correo;
                        consulta.apellido = modelo.apellido ?? consulta.apellido;
                        consulta.estado = modelo.estado;
                        consulta.numTelefono = modelo.numTelefono ?? consulta.numTelefono;
                        consulta.pais = modelo.pais ?? consulta.pais;
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        contexto.Donante.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {

                        throw new Exception(message: "Error en capa de datos Donante: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public List<Donante> listarDonante()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {

                    List<Donante> donantes = contexto.Donante.Where(u => u.estado != 3).ToList();
                    if (donantes == null)
                    {
                        throw new Exception("La lista de donantes es nula.");
                    }
                    return donantes;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }




            }
        }

    }
}