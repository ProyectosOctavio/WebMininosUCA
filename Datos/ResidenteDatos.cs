using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Datos
{
    public class ResidenteDatos
    {
        public IQueryable<Residente> getResidente()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<Residente> query = db.Residente;
            return query;
        }

        public void guardarResidente(Residente modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Residente.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public void EditarResidente(Residente modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Residente.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.descripcion = modelo.descripcion ?? consulta.descripcion;
                        consulta.esterilizado = modelo.esterilizado;
                        consulta.fechaIngreso = modelo.fechaIngreso != null ? modelo.fechaIngreso : consulta.fechaIngreso;
                        consulta.fechaDefuncion = modelo.fechaDefuncion ?? consulta.fechaDefuncion;
                        consulta.fechaDesaparicion = modelo.fechaDesaparicion ?? consulta.fechaDesaparicion;
                        consulta.fechaNacimiento = modelo.fechaNacimiento ?? consulta.fechaNacimiento;
                        consulta.fotoId = modelo.fotoId ?? consulta.fotoId;
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        consulta.sexo = modelo.sexo ?? consulta.sexo;
                        consulta.zonaId = modelo.zonaId ?? consulta.zonaId;
                        consulta.estado = modelo.estado;


                        // Eliminar las patologías anteriores del residente
                        foreach (var residentePatologia in consulta.ResidentePatologia.ToList())
                        {
                            contexto.ResidentePatologia.Remove(residentePatologia);
                        }

                        // Agregar la nueva patología seleccionada al residente
                        foreach (var residentePatologia in modelo.ResidentePatologia)
                        {
                            var patologia = contexto.Patologia.FirstOrDefault(p => p.descripcion == residentePatologia.Patologia.descripcion);
                            if (patologia != null)
                            {
                                consulta.ResidentePatologia.Add(new ResidentePatologia
                                {
                                    Patologia = patologia,
                                    estado = residentePatologia.estado
                                });
                            }
                        }

                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error en capa de datos residente: " + e.Message);
                    }
                }
            }
        }


        public void EliminarResidente(Residente modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Residente.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.descripcion = modelo.descripcion ?? consulta.descripcion;
                        consulta.esterilizado = modelo.esterilizado;
                        consulta.fechaIngreso = modelo.fechaIngreso != null ? modelo.fechaIngreso : consulta.fechaIngreso;
                        consulta.fechaDefuncion = modelo.fechaDefuncion ?? consulta.fechaDefuncion;
                        consulta.fechaDesaparicion = modelo.fechaDesaparicion ?? consulta.fechaDesaparicion;
                        consulta.fechaNacimiento = modelo.fechaNacimiento ?? consulta.fechaNacimiento;
                        consulta.fotoId = modelo.fotoId ?? consulta.fotoId;
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        consulta.sexo = modelo.sexo ?? consulta.sexo;
                        consulta.zonaId = modelo.zonaId ?? consulta.zonaId;
                        consulta.estado = modelo.estado;

                        contexto.Residente.AddOrUpdate(consulta);
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

        public List<Residente> listarResidente()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {
                    List<Residente> residentes = contexto.Residente.Where(r => r.estado != 3).ToList();
                    if (residentes == null)
                    {
                        throw new Exception("La lista de residentes es nula.");
                    }
                    return residentes;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }

        public bool ExisteResidente(string nombreResidente)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                // Verificar si existe algún usuario con el nombre de usuario dado
                bool existe = model.Residente.Any(r => r.nombre.Equals(nombreResidente));
                return existe;
            }
        }


        public List<Patologia> ObtenerPatologiasDeResidente(int residenteId)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {
                    Residente residente = contexto.Residente
                        .Include("ResidentePatologia.Patologia")
                        .FirstOrDefault(r => r.id == residenteId);

                    if (residente != null)
                    {
                        List<Patologia> patologiasAsociadas = residente.ResidentePatologia
                            .Select(rp => rp.Patologia)
                            .ToList();

                        return patologiasAsociadas;
                    }
                    else
                    {
                        return new List<Patologia>();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }




    }
}
