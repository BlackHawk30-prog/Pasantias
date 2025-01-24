using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        RH rH;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (rH == null)
            {
                rH = new RH();
            }
            if (!IsPostBack)
            {
                string condicionrecursos = "RHConfirmado = 0";
                rH.grid_rh(GridView2, condicionrecursos);
            }

        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  if (e.CommandName == "IrHoja")
            {
                // Obtener el índice de la fila seleccionada
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow selectedRow = GridView1.Rows[index];

                // Aquí puedes obtener algún dato de la fila, si es necesario
                //string dni = selectedRow.Cells[1].Text; // Ejemplo: obtener el DNI

                // Redirigir a la página deseada, puedes pasar datos en la URL si lo necesitas
                Response.Redirect($"VistaHJ.aspx");
            }
        }

        protected void btnAceptarSeleccionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                // Busca el CheckBox y el HiddenField de la fila actual
                CheckBox chkSelect = (CheckBox)row.FindControl("CheckBoxSelect");
                HiddenField hiddenIdHojaTiempo = (HiddenField)row.FindControl("HiddenFieldIDHojaTiempo");

                // Verifica si el CheckBox está seleccionado
                if (chkSelect != null && chkSelect.Checked)
                {
                    // Verifica que el HiddenField tenga un valor válido
                    if (hiddenIdHojaTiempo != null && int.TryParse(hiddenIdHojaTiempo.Value, out int idHojaTiempo))
                    {
                        // Llama al método para aceptar la hoja de tiempo específica
                        rH.AceptarHojaDeTiempo(idHojaTiempo);
                    }
                }
            }

            // Refresca el GridView
            string condicionrecursos = "RHConfirmado = 0";
            rH.grid_rh(GridView2, condicionrecursos);
        }

        protected void btnRechazarSeleccionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                // Busca el CheckBox y el HiddenField de la fila actual
                CheckBox chkSelect = (CheckBox)row.FindControl("CheckBoxSelect");
                HiddenField hiddenIdHojaTiempo = (HiddenField)row.FindControl("HiddenFieldIDHojaTiempo");

                // Verifica si el CheckBox está seleccionado
                if (chkSelect != null && chkSelect.Checked)
                {
                    // Verifica que el HiddenField tenga un valor válido
                    if (hiddenIdHojaTiempo != null && int.TryParse(hiddenIdHojaTiempo.Value, out int idHojaTiempo))
                    {
                        // Llama al método para rechazar la hoja de tiempo específica
                        rH.RechazarHojaDeTiempo(idHojaTiempo);
                    }
                }
            }

            // Refresca el GridView
            string condicionrecursos = "RHConfirmado = 0";
            rH.grid_rh(GridView2, condicionrecursos);
        }
    }
}