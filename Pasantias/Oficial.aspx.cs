using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Oficial oficial;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (oficial == null)
            {
                oficial = new Oficial();
            }
            if (!IsPostBack)
            {
                string condicionrecursos = "OConfirmado = 0";
                oficial.grid_oficial(GridView1, condicionrecursos);
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
 


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
 //           foreach (GridViewRow row in GridView1.Rows)
            {
                // Busca el CheckBox en la fila actual
 //               CheckBox chkSelect = (CheckBox)row.FindControl("CheckBoxSelect");

                // Verifica si el CheckBox está seleccionado
 //               if (chkSelect != null && chkSelect.Checked)
                {
                    // Realiza alguna acción con las filas seleccionadas
 //                   string nombre = row.Cells[1].Text; // Nombre
 //                   string dni = row.Cells[2].Text; // DNI

                    // Aquí puedes agregar la lógica que desees para los elementos seleccionados
                }
            }

 
        }

        protected void btn_rech_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

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

        protected void btnAceptarSeleccionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
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
                        oficial.AceptarHojaDeTiempo(idHojaTiempo);
                    }
                }
            }

            // Refresca el GridView
            string condicionrecursos = "OConfirmado = 0";
            oficial.grid_oficial(GridView1, condicionrecursos);
        }

        protected void btnRechazarSeleccionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
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
                        oficial.RechazarHojaDeTiempo(idHojaTiempo);
                    }
                }
            }

            // Refresca el GridView
            string condicionrecursos = "OConfirmado = 0";
            oficial.grid_oficial(GridView1, condicionrecursos);
        }

        protected void GridView1_SelectedIndexChanged2(object sender, EventArgs e)
        {

        }
    }
}