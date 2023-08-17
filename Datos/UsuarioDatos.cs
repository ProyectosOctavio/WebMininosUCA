using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using System.Data.Entity.Migrations;
using System.Diagnostics;

namespace Datos
{
    public class UsuarioDatos
    {
        Usuario user = new Usuario();
        AES aes = new AES();
        AesDatos aesDatos = new AesDatos();


        public IQueryable<Usuario> GetUsuario()
        {
            mininosDatabaseEntities db = new mininosDatabaseEntities();
            IQueryable<Usuario> query = db.Usuario;
            return query;
        }

        public Usuario GetUsuarioPorUsernameDatos(string user)
        {
            using (mininosDatabaseEntities db = new mininosDatabaseEntities())
            {
                Usuario usuario = db.Usuario.FirstOrDefault(u => u.username == user && u.estado != 3);
                if (usuario != null)
                {
                    return usuario;
                }
                return null;
            }
        }

        public Rol GetRolPorUsernameDatos(string user)
        {
            using (mininosDatabaseEntities db = new mininosDatabaseEntities())
            {
                Usuario usuario = db.Usuario.FirstOrDefault(u => u.username == user && u.estado != 3);
                if (usuario != null)
                {
                    Rol rol = db.Rol.FirstOrDefault(r => r.id == usuario.rolId && r.estado != 3);
                    if (rol != null)
                    {
                        return rol;
                    }
                }
                return null;
            }
        }

        public void guardarUsuario(Usuario modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Usuario.Add(modelo);
                contexto.SaveChanges();
            }
        }

        public int guardarUsuarioId(Usuario modelo)
        {
            int idUsuarioGuardado;
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                contexto.Usuario.Add(modelo);
                contexto.SaveChanges();
                idUsuarioGuardado = modelo.id; // Asignar el ID del usuario guardado
            }

            return idUsuarioGuardado;
        }

        public void EditarUsuario(Usuario modelo)
        {

            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Usuario.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.apellido = modelo.apellido ?? consulta.apellido;
                        consulta.estado = modelo.estado;
                        consulta.fechaCreacion = modelo.fechaCreacion != null ? modelo.fechaCreacion : consulta.fechaCreacion;
                        consulta.fotoId = modelo.fotoId ?? consulta.fotoId;
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        consulta.pw = modelo.pw ?? consulta.pw;
                        consulta.username = modelo.username ?? consulta.username;
                        consulta.email = modelo.email ?? consulta.email;
                        consulta.telefonoCel = modelo.telefonoCel ?? consulta.telefonoCel;
                        consulta.rolId = modelo.rolId ?? consulta.rolId;
                        contexto.Usuario.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(message: "Error en capa de datos usuario: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public void EliminarUsuario(Usuario modelo)
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                var consulta = contexto.Usuario.Find(modelo.id);
                if (consulta != null)
                {
                    try
                    {
                        consulta.apellido = modelo.apellido ?? consulta.apellido;
                        consulta.estado = modelo.estado;
                        consulta.fechaCreacion = modelo.fechaCreacion != null ? modelo.fechaCreacion : consulta.fechaCreacion;
                        consulta.fotoId = modelo.fotoId ?? consulta.fotoId;
                        consulta.nombre = modelo.nombre ?? consulta.nombre;
                        consulta.pw = modelo.pw ?? consulta.pw;
                        consulta.username = modelo.username ?? consulta.username;
                        consulta.email = modelo.email ?? consulta.email;
                        consulta.telefonoCel = modelo.telefonoCel ?? consulta.telefonoCel;
                        consulta.rolId = modelo.rolId ?? consulta.rolId;
                        contexto.Usuario.AddOrUpdate(consulta);
                        contexto.SaveChanges();
                    }
                    catch (Exception e)
                    {

                        throw new Exception(message: "Error en capa de datos usuario: " + e.Message);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public List<Usuario> listarUsuario()
        {
            using (mininosDatabaseEntities contexto = new mininosDatabaseEntities())
            {
                try
                {

                    List<Usuario> usuarios = contexto.Usuario.Where(u => u.estado != 3).ToList();
                    if (usuarios == null)
                    {
                        throw new Exception("La lista de usuarios es nula.");
                    }
                    return usuarios;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer la base de datos", ex);
                }




            }
        }

        public bool InsertarAES(AES aes)
        {
            bool guardado = false;
            using (mininosDatabaseEntities modelo = new mininosDatabaseEntities())
            {
                modelo.AES.Add(aes);
                modelo.SaveChanges();
                return guardado;
            }
        }

        public bool ValidarCredencialesDatos(string username, string pw)
        {
            using (mininosDatabaseEntities bd = new mininosDatabaseEntities())
            {
                user = bd.Usuario.Where(u => u.username.Equals(username)).FirstOrDefault();
                aes = bd.AES.Where(a => a.idUsuario == user.id).FirstOrDefault();

                if (user != null && aes != null)
                {
                    string decryptedPw = aesDatos.Decrypt_Aes(user.pw, aes.token, aes.iv);

                    if (username.Equals(user.username) && pw.Equals(decryptedPw))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            using (mininosDatabaseEntities bd = new mininosDatabaseEntities())
            {
                // Verificar si existe algún usuario con el nombre de usuario dado
                bool existe = bd.Usuario.Any(u => u.username.Equals(nombreUsuario));
                return existe;
            }
        }

        public bool ExisteCorreoElectronico(string correoElectronico)
        {
            using (mininosDatabaseEntities bd = new mininosDatabaseEntities())
            {
                // Verificar si existe algún usuario con el correo electrónico dado
                bool existe = bd.Usuario.Any(u => u.email.Equals(correoElectronico));
                return existe;
            }
        }
    }
}
