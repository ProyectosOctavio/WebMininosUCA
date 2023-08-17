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
    public class DonanteNegocio
    {
        DonanteDatos dtu = new DonanteDatos();


        public List<Donante> ListaDonante()
        {
            List<Donante> donantes = dtu.listarDonante();
            return donantes;
        }

        public void GuardarDonante(Donante modelo)
        {
            try
            {
                dtu.guardarDonante(modelo);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en NG_GuardarDonante: " + e.StackTrace);
            }
        }

        public void NG_EditarDonante(Donante modelo)
        {
            try
            {
                dtu.EditarDonante(modelo);
            }
            catch (Exception e)
            {
                throw new Exception(message: "Error en NG_Donante - Editar : " + e.StackTrace);
            }
        }

        public void NG_EliminarDonante(Donante modelo)
        {
            try
            {
                dtu.EliminarDonante(modelo);
            }
            catch (Exception e)
            {

                throw new Exception(message: "Error en NG_Donante - Eliminar : " + e.StackTrace);
            }
        }
    }
}