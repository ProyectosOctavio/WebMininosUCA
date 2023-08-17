using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using Datos;

namespace Negocio
{
    public class RolOpcionNegocio
    {
        private RolOpcionDatos datosRolOpcion = new RolOpcionDatos();

        public List<RolOpcion> ListarRolOpcion()
        {
            try
            {
                List<RolOpcion> rolOpciones = datosRolOpcion.ListarRolOpcion();
                return rolOpciones;
            }
            catch (Exception e)
            {
                throw new Exception("Error en NG_RolOpcion - Listar: " + e.Message, e);
            }
        }

        public void GuardarRolOpcion(RolOpcion modelo)
        {
            try
            {
                datosRolOpcion.GuardarRolOpcion(modelo);
            }
            catch (Exception e)
            {
                throw new Exception("Error en NG_RolOpcion - Guardar: " + e.Message, e);
            }
        }

        public void EditarRolOpcion(RolOpcion modelo)
        {
            try
            {
                datosRolOpcion.EditarRolOpcion(modelo);
            }
            catch (Exception e)
            {
                throw new Exception("Error en NG_RolOpcion - Editar: " + e.Message, e);
            }
        }

        public void EliminarRolOpcion(RolOpcion modelo)
        {
            try
            {
                datosRolOpcion.EliminarRolOpcion(modelo);
            }
            catch (Exception e)
            {
                throw new Exception("Error en NG_RolOpcion - Eliminar: " + e.Message, e);
            }
        }
    }
}

