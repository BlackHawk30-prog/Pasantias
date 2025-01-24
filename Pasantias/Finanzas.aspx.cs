using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Finanzas : System.Web.UI.Page
    {
        Finanza finanzas;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (finanzas == null)
            {
                finanzas = new Finanza();
            }
            if (!IsPostBack)
            {
                string condicionrecursos = "FConfirmado = 0";
                finanzas.grid_fina(Gridfina, condicionrecursos);
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
            foreach (GridViewRow row in Gridfina.Rows)
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
                        finanzas.AceptarHojaDeTiempo(idHojaTiempo);
                    }
                }
            }

            // Refresca el GridView
            string condicionrecursos = "FConfirmado = 0";
            finanzas.grid_fina(Gridfina, condicionrecursos);
        }

        protected void btnRechazarSeleccionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Gridfina.Rows)
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
                        finanzas.RechazarHojaDeTiempo(idHojaTiempo);
                    }
                }
            }

            // Refresca el GridView
            string condicionrecursos = "FConfirmado = 0";
            finanzas.grid_fina(Gridfina, condicionrecursos);
        }
    }
}