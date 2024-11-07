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
            if (!IsPostBack)
            {
                oficial = new Oficial();
                oficial.grid_oficial(GridView1);
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
                // Obtener el índice de la fila seleccionada
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow selectedRow = GridView1.Rows[index];

                // Aquí puedes obtener algún dato de la fila, si es necesario
                //string dni = selectedRow.Cells[1].Text; // Ejemplo: obtener el DNI

                // Redirigir a la página deseada, puedes pasar datos en la URL si lo necesitas
                Response.Redirect($"VistaHJ.aspx");
            }
        }
    }
}