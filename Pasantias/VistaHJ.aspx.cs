using Modelo;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class VistaHJ : System.Web.UI.Page
    {
        Vista_HJ vistahj = new Vista_HJ();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (int.TryParse(Request.QueryString["IDHojaTiempo"], out int idHojaTiempo))
                {
                    SessionStore.HojaID = idHojaTiempo; 
                    CargarHojaDeTiempo(idHojaTiempo);
                }
                else
                {
                    lbl_Mensaje.Text = "No se pudo cargar la hoja de tiempo.";
                }
            }
        }

        private void CargarHojaDeTiempo(int idHojaTiempo)
        {
            try
            {
                // Llama al método para cargar los datos al GridView
                vistahj.grid_hojas(grid_hojas);

                // Calcula el total de horas trabajadas
                double totalHoras = 0;
                foreach (GridViewRow row in grid_hojas.Rows)
                {
                    if (double.TryParse(row.Cells[2].Text, out double horas))
                    {
                        totalHoras += horas;
                    }
                }
                lbl_HorasTotales.Text = $"Total de Horas: {totalHoras}";
            }
            catch (Exception ex)
            {
                lbl_Mensaje.Text = $"Ocurrió un error: {ex.Message}";
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            // Redirigir a la página anterior
            Response.Redirect("PaginaAnterior.aspx");
        }

        protected void grid_hojas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int idDetalle = Convert.ToInt32(e.CommandArgument);
                // Aquí puedes agregar lógica para eliminar el detalle de la hoja
                lbl_Mensaje.Text = $"Eliminado el detalle con ID: {idDetalle}";
            }
        }

        protected void grid_hojas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
