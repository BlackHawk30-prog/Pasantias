using Microsoft.Ajax.Utilities;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Hoja_Tiempo : System.Web.UI.Page
    {
       HojaTiempo hojaTiempo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hojaTiempo = new HojaTiempo();
                hojaTiempo.grid_hojas(grid_hojas);
                
            }
        }

        protected void Btn_Agregar_Click(object sender, EventArgs e)
        {
            hojaTiempo = new HojaTiempo();

            // Ajusta la llamada según la firma de tu método
            if (hojaTiempo.crear(0, txt_Fecha.Text, txt_Actividades.Text, int.Parse(txt_Horas.Text), 1) > 0)
            {
                lbl_Mensaje.Text = "Registrado Exitosamente";
                hojaTiempo.grid_hojas(grid_hojas);
            }
        }

        protected void grid_hojas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(grid_hojas.SelectedRow.Cells[0].Text);
            txt_Fecha .Text = fecha.ToString("yyyy-MM-dd"); 
            txt_Actividades.Text = grid_hojas.SelectedRow.Cells[1].Text;
            txt_Horas.Text = grid_hojas.SelectedRow.Cells[2].Text;
            Btn_Actualizar.Visible = true;
        }

        protected void Btn_Actualizar_Click(object sender, EventArgs e)
        {
            hojaTiempo = new HojaTiempo();

        
            if (hojaTiempo.actualizar(Convert.ToInt32(grid_hojas.SelectedValue), txt_Fecha.Text, txt_Actividades.Text, int.Parse(txt_Horas.Text), 1) > 0)
            {
                lbl_Mensaje.Text = "Modificado Exitosamente";
                hojaTiempo.grid_hojas(grid_hojas);
            }
        }
    }
}