using System;
using System.Web.UI.WebControls;
using Modelo;

namespace Pasantias
{
    public partial class Postulaciones : System.Web.UI.Page
    {
        Postulacion Postulacion;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Asegurarte de inicializar el objeto Postulacion en cada carga de la página
            Postulacion = new Postulacion();

            if (!IsPostBack)
            {
                Postulacion.grid_aplicantes(grid_aplicantes);
            }
        }

        protected void grid_aplicantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Aceptar")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                // Llamar al método para aceptar la postulación
                Postulacion.AceptarPostulacion(idUsuario);
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
        }
    }
}
