using System;
using Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace Datos
{
    public class DonacionDatos
    {
        public IQueryable<DonacionMonetaria> getDonacionMonetaria()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<DonacionMonetaria> query = db.DonacionMonetaria;
            return query;
        }

        public IQueryable<DonacionEspecies> getDonacionEspecies()
        {
            mininosDatabaseEntities db2 = new mininosDatabaseEntities();
            IQueryable<DonacionEspecies> query = db2.DonacionEspecies;
            return query;
        }

        public List<DonacionEspecies> getDonacionesEspeciesConFechaDatos(DateTime fechaInicio, DateTime fechaFin)
        {
            using (mininosDatabaseEntities db = new mininosDatabaseEntities())
            {
                List<DonacionEspecies> donaciones = db.DonacionEspecies.Where(
                        d => 
                        d.fecha >= fechaInicio 
                        && d.fecha <= fechaFin 
                        && d.estado == 2
                    ).ToList();
                return donaciones;
            }
        }

        public IQueryable<Donante> getDonante()
        {
            mininosDatabaseEntities db3 = new mininosDatabaseEntities();
            IQueryable<Donante> query = db3.Donante;
            return query;
        }

        public IQueryable<ResidenteDonante> getResidenteDonante()
        {
            mininosDatabaseEntities db4 = new mininosDatabaseEntities();
            IQueryable<ResidenteDonante> query = db4.ResidenteDonante;
            return query;
        }


        public void guardarDonacionMonetaria(DonacionMonetaria modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.DonacionMonetaria.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public void guardarDonacionEspecies(DonacionEspecies modelo2)
        {
            using (mininosDatabaseEntities contexto2 = new mininosDatabaseEntities())
            {
                contexto2.DonacionEspecies.Add(modelo2);
                contexto2.SaveChanges();
            }
        }

        public void guardarDonante(Donante modelo3)
        {
            using (mininosDatabaseEntities contexto3 = new mininosDatabaseEntities())
            {
                contexto3.Donante.Add(modelo3);
                contexto3.SaveChanges();
            }
        }

        public void guardarResidenteDonante(ResidenteDonante modelo4)
        {
            using (mininosDatabaseEntities contexto4 = new mininosDatabaseEntities())
            {
                contexto4.ResidenteDonante.Add(modelo4);
                contexto4.SaveChanges();
            }
        }

        public void EditarDonacionMonetaria(DonacionMonetaria modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.DonacionMonetaria.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {

                        consulta.fecha = modelo.fecha != default(DateTime) ? modelo.fecha : consulta.fecha;
                        consulta.montoNi = modelo.montoNi != default(double) ? modelo.montoNi : consulta.montoNi;
                        consulta.donanteId = modelo.donanteId != default(int) ? modelo.donanteId : consulta.donanteId;
                        consulta.voucher = modelo.voucher ?? consulta.voucher;
                        consulta.estado = modelo.estado != default(int) ? modelo.estado : consulta.estado;
                        contexto.DonacionMonetaria.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos donacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }


        public void EditarDonacionEspecies(DonacionEspecies modelo2)
        {

            using (mininosDatabaseEntities contexto2 = new mininosDatabaseEntities())
            {
                var consulta2 = contexto2.DonacionEspecies.Find(modelo2.id);
                if (consulta2 != null)
                {
                    try
                    {

                        consulta2.fecha = modelo2.fecha != default(DateTime) ? modelo2.fecha : consulta2.fecha;
                        consulta2.cantidad = modelo2.cantidad != default(double) ? modelo2.cantidad : consulta2.cantidad;
                        consulta2.tipoEspecie = modelo2.tipoEspecie != default(string) ? modelo2.tipoEspecie : consulta2.tipoEspecie;
                        consulta2.donanteId = modelo2.donanteId != default(int) ? modelo2.donanteId : consulta2.donanteId;
                        consulta2.unidadMedida = modelo2.unidadMedida != default(string) ? modelo2.unidadMedida : consulta2.unidadMedida;
                        consulta2.estado = modelo2.estado != default(int) ? modelo2.estado : consulta2.estado;
                        contexto2.DonacionEspecies.AddOrUpdate(consulta2);
                        contexto2.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos donacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EditarApadrinamiento(ResidenteDonante modelo3)
        {

            using (mininosDatabaseEntities contexto3 = new mininosDatabaseEntities())
            {
                var consulta3 = contexto3.ResidenteDonante.Find(modelo3.id);
                if (consulta3 != null)
                {
                    try
                    {


                        consulta3.donanteId = modelo3.donanteId != default(int) ? modelo3.donanteId : consulta3.donanteId;
                        consulta3.residenteId = modelo3.residenteId != default(int) ? modelo3.residenteId : consulta3.residenteId;
                        consulta3.estado = modelo3.estado != default(int) ? modelo3.estado : consulta3.estado;
                        contexto3.ResidenteDonante.AddOrUpdate(consulta3);
                        contexto3.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos donacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }


        public void EliminarDonacionMonetaria(DonacionMonetaria modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.DonacionMonetaria.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {

                        consulta.fecha = modelo.fecha != default(DateTime) ? modelo.fecha : consulta.fecha;
                        consulta.montoNi = modelo.montoNi != default(double) ? modelo.montoNi : consulta.montoNi;
                        consulta.donanteId = modelo.donanteId != default(int) ? modelo.donanteId : consulta.donanteId;
                        consulta.voucher = modelo.voucher ?? consulta.voucher;
                        consulta.estado = modelo.estado != default(int) ? modelo.estado : consulta.estado;
                        contexto.DonacionMonetaria.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos donacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EliminarDonacionEspecies(DonacionEspecies modelo2)
        {

            using (mininosDatabaseEntities contexto2 = new mininosDatabaseEntities())
            {
                var consulta2 = contexto2.DonacionEspecies.Find(modelo2.id);
                if (consulta2 != null)
                {
                    try
                    {

                        consulta2.fecha = modelo2.fecha != default(DateTime) ? modelo2.fecha : consulta2.fecha;
                        consulta2.cantidad = modelo2.cantidad != default(double) ? modelo2.cantidad : consulta2.cantidad;
                        consulta2.tipoEspecie = modelo2.tipoEspecie != default(string) ? modelo2.tipoEspecie : consulta2.tipoEspecie;
                        consulta2.donanteId = modelo2.donanteId != default(int) ? modelo2.donanteId : consulta2.donanteId;
                        consulta2.unidadMedida = modelo2.unidadMedida != default(string) ? modelo2.unidadMedida : consulta2.unidadMedida;
                        consulta2.estado = modelo2.estado != default(int) ? modelo2.estado : consulta2.estado;
                        contexto2.DonacionEspecies.AddOrUpdate(consulta2);
                        contexto2.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos donacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }


        public void EliminarApadrinamiento(ResidenteDonante modelo3)
        {

            using (mininosDatabaseEntities contexto3= new mininosDatabaseEntities())
            {
                var consulta3 = contexto3.ResidenteDonante.Find(modelo3.id);
                if (consulta3 != null)
                {
                    try
                    {

                       
                        consulta3.donanteId = modelo3.donanteId != default(int) ? modelo3.donanteId : consulta3.donanteId;
                        consulta3.residenteId = modelo3.residenteId != default(int) ? modelo3.residenteId : consulta3.residenteId;
                        consulta3.estado = modelo3.estado != default(int) ? modelo3.estado : consulta3.estado;
                        contexto3.ResidenteDonante.AddOrUpdate(consulta3);
                        contexto3.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos donacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }


        public void ResolverDonacionMonetaria(DonacionMonetaria modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.DonacionMonetaria.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {

                        consulta.fecha = modelo.fecha != default(DateTime) ? modelo.fecha : consulta.fecha;
                        consulta.montoNi = modelo.montoNi != default(double) ? modelo.montoNi : consulta.montoNi;
                        consulta.voucher = modelo.voucher ?? consulta.voucher;
                        consulta.estado = modelo.estado != default(int) ? modelo.estado : consulta.estado;
                        contexto.DonacionMonetaria.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos donacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void ResolverDonacionEspecies(DonacionEspecies modelo2)
        {

            using (mininosDatabaseEntities contexto2 = new mininosDatabaseEntities())
            {
                var consulta2 = contexto2.DonacionEspecies.Find(modelo2.id);
                if (consulta2 != null)
                {
                    try
                    {

                        consulta2.fecha = modelo2.fecha != default(DateTime) ? modelo2.fecha : consulta2.fecha;
                        consulta2.cantidad = modelo2.cantidad != default(double) ? modelo2.cantidad : consulta2.cantidad;
                        consulta2.tipoEspecie = modelo2.tipoEspecie != default(string) ? modelo2.tipoEspecie : consulta2.tipoEspecie;
                        consulta2.donanteId = modelo2.donanteId != default(int) ? modelo2.donanteId : consulta2.donanteId;
                        consulta2.unidadMedida = modelo2.unidadMedida != default(string) ? modelo2.unidadMedida : consulta2.unidadMedida;
                        consulta2.estado = modelo2.estado != default(int) ? modelo2.estado : consulta2.estado;
                        contexto2.DonacionEspecies.AddOrUpdate(consulta2);
                        contexto2.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos donacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void ResolverApadrinamiento(ResidenteDonante modelo3)
        {

            using (mininosDatabaseEntities contexto3 = new mininosDatabaseEntities())
            {
                var consulta3 = contexto3.ResidenteDonante.Find(modelo3.id);
                if (consulta3 != null)
                {
                    try
                    {


                        consulta3.donanteId = modelo3.donanteId != default(int) ? modelo3.donanteId : consulta3.donanteId;
                        consulta3.residenteId = modelo3.residenteId != default(int) ? modelo3.residenteId : consulta3.residenteId;
                        consulta3.estado = modelo3.estado != default(int) ? modelo3.estado : consulta3.estado;
                        contexto3.ResidenteDonante.AddOrUpdate(consulta3);
                        contexto3.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos donacion: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }




        public List<DonacionMonetaria> listarDonacionMonetaria()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {
                    List<DonacionMonetaria> DonacionMonetaria = contexto.DonacionMonetaria.Where(u => u.estado != 3).ToList();
                    if (DonacionMonetaria == null)
                    {
                        throw new Exception("La lista de donacion monetaria es nula.");
                    }
                    return DonacionMonetaria;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }

        public List<DonacionEspecies> listarDonacionEspecies()
        {
            using (mininosDatabaseEntities contexto2 = new mininosDatabaseEntities())
            {
                try
                {
                    List<DonacionEspecies> DonacionEspecies = contexto2.DonacionEspecies.Where(j => j.estado != 3).ToList();
                    if (DonacionEspecies == null)
                    {
                        throw new Exception("La lista de donacion especies es nula.");
                    }
                    return DonacionEspecies;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }

        public List<Donante> listarDonante()
        {
            using (mininosDatabaseEntities contexto3 = new mininosDatabaseEntities())
            {
                try
                {
                    List<Donante> Donante = contexto3.Donante.Where(f => f.estado != 3).ToList();
                    if (Donante== null)
                    {
                        throw new Exception("La lista de donantes es nula.");
                    }
                    return Donante;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }

        public List<ResidenteDonante> listarResidenteDonante()
        {
            using (mininosDatabaseEntities contexto4 = new mininosDatabaseEntities())
            {
                try
                {
                    List<ResidenteDonante> ResidenteDonante = contexto4.ResidenteDonante.ToList();
                    if (ResidenteDonante == null)
                    {
                        throw new Exception("La lista de donantes es nula.");
                    }
                    return ResidenteDonante;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }
            }
        }




        public int ObtenerUltimoIdDonante()
        {
            using (mininosDatabaseEntities contexto3 = new mininosDatabaseEntities())
            {
                var ultimoDonante = contexto3.Donante.OrderByDescending(d => d.id).FirstOrDefault(); //trae el id del ultimo donante
                return ultimoDonante != null ? ultimoDonante.id : 0;
            }
        }

        public int ObtenerIdDonanteExistentePorCorreo(string correoElectronico)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var donanteExistente = contexto.Donante.FirstOrDefault(d => d.correo == correoElectronico);
                return donanteExistente != null ? donanteExistente.id : 0;
            }
        }

        public int ObtenerIdDonanteExistentePorAliasYTelefono(string alias, string telefono)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var donanteExistente = contexto.Donante.FirstOrDefault(d => d.alias == alias && d.numTelefono == telefono);
                return donanteExistente != null ? donanteExistente.id : 0;
            }
        }

        public bool ExisteCorreo(string correoDonante)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                // Verificar si existe 
                bool existe = model.Donante.Any(d => d.correo.Equals(correoDonante));
                return existe;
            }
        }

        public bool ExisteAlias(string aliasDonante)
        {
            using (mininosDatabaseEntities model = new mininosDatabaseEntities())
            {
                // Verificar si existe 
                bool existe = model.Donante.Any(d => d.alias.Equals(aliasDonante));
                return existe;
            }

        }
            public bool ExisteTelefono(string telefonoDonante)
            {
                using (mininosDatabaseEntities model = new mininosDatabaseEntities())
                {
                    // Verificar si existe 
                    bool existe = model.Donante.Any(d => d.numTelefono.Equals(telefonoDonante));
                    return existe;
                }
            }

        }

        
}
