using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Postulaciones_Regional : System.Web.UI.Page
    {
        private Postulacion Postulacion; // Declaración del objeto Postulacion

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicialización del objeto Postulacion
            if (Postulacion == null)
            {
                Postulacion = new Postulacion();
            }

            if (!IsPostBack)
            {
                string condicionSeguridad = "u.RHConfirmado = 1 AND SConfirmado = 1 AND RConfirmado = 0";
                Postulacion.grid_aplicantes(grid_aplicantes, condicionSeguridad);
            }
        }

        protected void grid_aplicantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Aceptar")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                // Llamar al método para aceptar la postulación
                Postulacion.AceptarPostulacionRegional(idUsuario);
                // Volver a cargar la tabla
                Postulacion.grid_aplicantes(grid_aplicantes);
            }
            else if (e.CommandName == "Rechazar")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                // Llamar al método para rechazar la postulación
                Postulacion.RechazarPostulacion(idUsuario);
                // Volver a cargar la tabla
                Postulacion.grid_aplicantes(grid_aplicantes);
            }
            else if (e.CommandName == "Detalles")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                // Redirigir a la vista Detalle_Postulante con el IDUsuario como parámetro en la URL
                Response.Redirect($"Detalle_Postulante.aspx?IDUsuario={idUsuario}");
            }
        }
    }
}