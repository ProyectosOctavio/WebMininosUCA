using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Negocio
{
    public class UsuarioNegocio
    {
        UsuarioDatos dtu = new UsuarioDatos();

        AES aes = new AES();
        AesDatos aesDatos = new AesDatos();

        public List<Usuario> ListaUsuarios()
        {
            List<Usuario> usuarios = dtu.listarUsuario();
            return usuarios;
        }

        public Usuario GetUsuarioPorUsernameNegocio(String user)
        {
            try 
            { 
                return dtu.GetUsuarioPorUsernameDatos(user);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en GetUsuarioPorUsernameNegocio: " + e.StackTrace);
            }
        }

        public bool VerificarAdminUsuarioNegocio(String user)
        {
            try
            {
                Rol userRol = dtu.GetRolPorUsernameDatos(user);

                if (userRol.nombre.Equals("Administrador"))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en VerificarAdminUsuarioNegocio: " + e.StackTrace);
            }

            return false;
        }

        public void GuardarUsuario(Usuario modelo)
        {
            int idUsuarioGuardado = 0;
            bool guardado = false;
            string privateKey = "";
            string myIV = "";
            string pwEncrypted = "";

            try
            {
                using (Aes myAes = Aes.Create())
                {
                    privateKey = aesDatos.randomAlphaNumeric(32);
                    myIV = Convert.ToBase64String(myAes.IV);
                    pwEncrypted = aesDatos.Encrypt_Aes(modelo.pw, privateKey, myIV);
                }

                modelo.pw = pwEncrypted;
                idUsuarioGuardado = dtu.guardarUsuarioId(modelo);

                aes.idUsuario = idUsuarioGuardado;
                aes.token = privateKey;
                aes.iv = myIV;

                guardado = dtu.InsertarAES(aes);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en NG_Usuario: " + e.StackTrace);
            }
        }

        public void NG_EditarUsuario(Usuario modelo)
        {
            try
            {
                dtu.EditarUsuario(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Usuario - Editar : " + e.StackTrace);
            }
        }

        public void NG_EliminarUsuario(Usuario modelo)
        {
            try
            {
                dtu.EliminarUsuario(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Usuario - Eliminar : " + e.StackTrace);
            }
        }

        public bool ValidarCredencialesNegocio(string usuario, string pwd)
        {
            bool entrar = false;
            try
            {
                entrar = dtu.ValidarCredencialesDatos(usuario, pwd);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return entrar;
        }

        public bool ValidarUsuarioExistente(string usuario)
        {
            bool existe = false;
            try
            {
                existe = dtu.ExisteUsuario(usuario);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return existe;
        }

        public bool ValidarCorreoElectronicoExistente(string correo)
        {
            bool existe = false;
            try
            {
                existe = dtu.ExisteCorreoElectronico(correo);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error en la capa de negocio: " + e.Message.ToString());
                Debug.WriteLine(e.StackTrace.ToString());
            }
            return existe;
        }

    }
}