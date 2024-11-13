using Microsoft.Ajax.Utilities;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                // Obtener el parámetro IDHojaTiempo de la URL
                int idHojaTiempo;
                if (int.TryParse(Request.QueryString["IDHojaTiempo"], out idHojaTiempo))
                {
                    hojaTiempo = new HojaTiempo();
                    SessionStore.HojaID = idHojaTiempo; // Guardar el ID en la sesión
                    grid_hojas.DataSource = hojaTiempo.grid_hojas(); // Llama al método para cargar datos
                    grid_hojas.DataBind(); // Enlaza los datos al GridView
                    CalcularTotalHoras(); // Calcula las horas totales
                }
                else
                {
                    lbl_Mensaje.Text = "ID de Hoja de Tiempo no válido.";
                }
            }
        }

        protected void Btn_Agregar_Click(object sender, EventArgs e)
        {
            hojaTiempo = new HojaTiempo();

            if (string.IsNullOrWhiteSpace(txt_Fecha.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa una fecha.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Horas.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa un número de horas.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Actividades.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa las actividades.";
                return;
            }

            if (!ValidarTexto(txt_Actividades.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa actividades válidas sin caracteres especiales o tres letras consecutivas.";
                return;
            }

            if (int.TryParse(txt_Horas.Text, out int horas))
            {
                if (horas < 1 || horas > 8)
                {
                    lbl_Mensaje.Text = "Por favor, ingresa un número de horas entre 1 y 8.";
                    return;
                }

                int idUsuario = (int)Session["UserID"];
                string fechaString = txt_Fecha.Text;

                if (hojaTiempo.crear(0, fechaString, txt_Actividades.Text, horas, idUsuario) > 0)
                {
                    lbl_Mensaje.Text = "Registrado Exitosamente";
                    hojaTiempo.grid_hojas(grid_hojas);

                    txt_Fecha.Text = "";
                    txt_Horas.Text = "";
                    txt_Actividades.Text = "";
                }
                else
                {
                    lbl_Mensaje.Text = "Error al registrar la hoja de tiempo.";
                }
            }
            else
            {
                lbl_Mensaje.Text = "Por favor, ingresa un número válido de horas.";
            }
            CalcularTotalHoras();
        }

        protected void grid_hojas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(grid_hojas.SelectedRow.Cells[0].Text);
            txt_Fecha.Text = fecha.ToString("yyyy-MM-dd");
            txt_Actividades.Text = grid_hojas.SelectedRow.Cells[1].Text;
            txt_Horas.Text = grid_hojas.SelectedRow.Cells[2].Text;
            Btn_Actualizar.Visible = true;
            Btn_Agregar.Visible = false;
            Button1.Visible = true;
            CalcularTotalHoras();
        }

        protected void Btn_Actualizar_Click(object sender, EventArgs e)
        {
            hojaTiempo = new HojaTiempo();

            if (string.IsNullOrWhiteSpace(txt_Fecha.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa una fecha.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Horas.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa un número de horas.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Actividades.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa las actividades.";
                return;
            }

            if (!ValidarTexto(txt_Actividades.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa actividades válidas sin caracteres especiales o tres letras consecutivas.";
                return;
            }

            if (int.TryParse(txt_Horas.Text, out int horas))
            {
                if (horas < 1 || horas > 8)
                {
                    lbl_Mensaje.Text = "Por favor, ingresa un número de horas entre 1 y 8.";
                    return;
                }
                int idUsuario = (int)Session["UserID"];
                if (hojaTiempo.actualizar(Convert.ToInt32(grid_hojas.SelectedValue), txt_Fecha.Text, txt_Actividades.Text, horas, 1) > 0)
                {
                    lbl_Mensaje.Text = "Modificado Exitosamente";
                    hojaTiempo.grid_hojas(grid_hojas);

                    txt_Fecha.Text = "";
                    txt_Horas.Text = "";
                    txt_Actividades.Text = "";
                    Btn_Actualizar.Visible = false;
                    Btn_Agregar.Visible = true;
                    Button1.Visible = false;
                }
                else
                {
                    lbl_Mensaje.Text = "Error al modificar la hoja de tiempo.";
                }
            }
            else
            {
                lbl_Mensaje.Text = "Por favor, ingresa un número válido de horas.";
            }
            CalcularTotalHoras();
        }

        protected void grid_hojas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            hojaTiempo = new HojaTiempo();
            int IDHoja = Convert.ToInt32(grid_hojas.DataKeys[e.RowIndex].Value);

            if (hojaTiempo.eliminar(IDHoja) > 0)
            {
                lbl_Mensaje.Text = "Eliminado Exitosamente";
                hojaTiempo.grid_hojas(grid_hojas);
                txt_Fecha.Text = "";
                txt_Horas.Text = "";
                txt_Actividades.Text = "";
                Button1.Visible = false;
                Btn_Actualizar.Visible = false;
                Btn_Agregar.Visible = true;
            }
            else
            {
                lbl_Mensaje.Text = "Hubo un error al marcar el registro como eliminado.";
            }
            e.Cancel = true;
            CalcularTotalHoras();
        }

        private bool ValidarTexto(string texto)
        {
            string regexCaracteresEspeciales = @"^[^<>!@#$%^&*()_+=-]+$";
            string regexTresLetrasSeguidas = @"^(?!.*([a-zA-Z])\1{2})";
            string regexPuntosComasSeguidos = @"(?<![.,])[.,]{2,}(?![.,])";

            if (!Regex.IsMatch(texto, regexCaracteresEspeciales) ||
                !Regex.IsMatch(texto, regexTresLetrasSeguidas) ||
                Regex.IsMatch(texto, regexPuntosComasSeguidos))
            {
                return false;
            }

            return true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            txt_Fecha.Text = "";
            txt_Horas.Text = "";
            txt_Actividades.Text = "";
            Btn_Actualizar.Visible = false;
            Btn_Agregar.Visible = true;
            Button1.Visible = false;
        }

        private void CalcularTotalHoras()
        {
            int totalHoras = 0;

            foreach (GridViewRow row in grid_hojas.Rows)
            {
                if (int.TryParse(row.Cells[2].Text, out int horas))
                {
                    totalHoras += horas;
                }
            }

            lbl_HorasTotales.Text = $"Total de Horas: {totalHoras}";
        }
    }
}
