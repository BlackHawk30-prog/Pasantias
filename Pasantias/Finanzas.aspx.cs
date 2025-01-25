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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "IrHoja")
            {
                // Obtén el IDHojaTiempo desde el CommandArgument
                int idHojaTiempo = Convert.ToInt32(e.CommandArgument);

                // Redirige a la página de detalles, pasando el ID como parámetro en la Query String
                Response.Redirect($"VistaHJ.aspx?IDHojaTiempo={idHojaTiempo}");

            }


        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // if (e.CommandName == "IrHoja")
         //   {
                // Obtén el IDHojaTiempo desde el CommandArgument
              //  int idHojaTiempo = Convert.ToInt32(e.CommandArgument);

                // Redirige a la página de detalles, pasando el ID como parámetro en la Query String
              //  Response.Redirect($"VistaHJ.aspx?IDHojaTiempo={idHojaTiempo}");

           // }
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