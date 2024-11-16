using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Pasantias
{
    public partial class Postulaciones : System.Web.UI.Page
    {
        Postulacion Postulacion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Postulacion = new Postulacion();
                Postulacion.grid_aplicantes(grid_aplicantes);
            }
        }

        protected void grid_aplicantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtenemos el IDUsuario desde el CommandArgument
            int idUsuario = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Detalles")
            {
                // Redirige a la vista Detalle_Postulante con el IDUsuario en la query string
                Response.Redirect($"Detalle_Postulante.aspx?IDUsuario={idUsuario}");
            }
            else if (e.CommandName == "Aceptar")
            {
                // Cambia el valor de RHConfirmado a 1
                Postulacion.ActualizarEstadoUsuario(idUsuario, "RHConfirmado", 1);
                Postulacion.grid_aplicantes(grid_aplicantes); // Recargar datos
            }
            else if (e.CommandName == "Rechazar")
            {
                // Cambia el valor de Eliminado a 1
                Postulacion.ActualizarEstadoUsuario(idUsuario, "Eliminado", 1);
                Postulacion.grid_aplicantes(grid_aplicantes); // Recargar datos
            }
        }
    }
}
