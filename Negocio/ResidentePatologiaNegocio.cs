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
    public class ResidentePatologiaNegocio
    {
        ResidentePatologiaDatos dtrp = new ResidentePatologiaDatos();
        public List<ResidentePatologia> ListaResidentePatologia()
        {
            List<ResidentePatologia> residentePatologia = dtrp.listarResidentePatologia();
            return residentePatologia;
        }

        public void GuardarResidentePatologia(ResidentePatologia modelo)
        {
            try
            {
                dtrp.guardarResidentePatologia(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_ResidentePatologia: " + e.StackTrace);
            }
        }

        public void NG_EditarResidentePatologia(ResidentePatologia modelo)
        {
            try
            {
                dtrp.EditarResidentePatologia(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_ResidentePatologia - Editar : " + e.StackTrace);
            }
        }
        public void NG_EliminarResidentePatologia(ResidentePatologia modelo)
        {
            try
            {
                dtrp.EliminarResidentePatologia(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_ResidentePatologia - Eliminar : " + e.StackTrace);
            }
        }

        public bool ValidarResidentePatologiaExistente(int residenteId, int patologiaId)
        {
            bool existe = false;
            try
            {
                existe = dtrp.ExisteResidentePatologia(residenteId, patologiaId);
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
