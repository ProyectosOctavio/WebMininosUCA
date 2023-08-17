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
    public class PatologiaNegocio
    {
        PatologiaDatos dtp = new PatologiaDatos();
        public List<Patologia> ListaPatologia()
        {
            List<Patologia> patologia = dtp.listarPatologia();
            return patologia;
        }

        public void GuardarPatologia(Patologia modelo)
        {
            try
            {
                dtp.guardarPatologia(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Patologia: " + e.StackTrace);
            }
        }

        public void NG_EditarPatologia(Patologia modelo)
        {
            try
            {
                dtp.EditarPatologia(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Patologia - Editar : " + e.StackTrace);
            }
        }

        public void NG_EliminarPatologia(Patologia modelo)
        {
            try
            {
                dtp.EliminarPatologia(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Patologia - Eliminar : " + e.StackTrace);
            }
        }

        public bool ValidarPatologiaExistente(string patologia)
        {
            bool existe = false;
            try
            {
                existe = dtp.ExistePatologia(patologia);
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
