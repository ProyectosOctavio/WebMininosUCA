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
    public class InfoBancariaNegocio
    {
        InfoBancariaDatos dti = new InfoBancariaDatos();
        public List<InfoBancaria> ListaInfoBancaria()
        {
            List<InfoBancaria> infoBancaria = dti.listarInfoBancaria();
            return infoBancaria;
        }

        public void GuardarInfoBancaria(InfoBancaria modelo)
        {
            try
            {
                dti.guardarInfoBancaria(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_InfoBancaria: " + e.StackTrace);
            }
        }

        public void NG_EditarInfoBancaria(InfoBancaria modelo)
        {
            try
            {
                dti.EditarInfoBancaria(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_InfoBancaria: " + e.StackTrace);
            }
        }

        public void NG_EliminarInfoBancaria(InfoBancaria modelo)
        {
            try
            {
                dti.EliminarInfoBancaria(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_InfoBancaria: " + e.StackTrace);
            }
        }

        public bool ValidarInfoBancariaExistente(string infoBancaria)
        {
            bool existe = false;
            try
            {
                existe = dti.ExisteInfoBancaria(infoBancaria);
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
