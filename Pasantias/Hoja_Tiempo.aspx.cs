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
                hojaTiempo = new HojaTiempo();
                grid_hojas.DataSource = hojaTiempo.grid_hojas(); // Asignar el DataTable al GridView
                grid_hojas.DataBind(); // Asegúrate de llamar a DataBind para enlazar los datos
                CalcularTotalHoras();
            }
        }

        protected void Btn_Agregar_Click(object sender, EventArgs e)
        {
            hojaTiempo = new HojaTiempo();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txt_Fecha.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa una fecha.";
                return; // Salir del método si el campo de fecha está vacío
            }

            if (string.IsNullOrWhiteSpace(txt_Horas.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa un número de horas.";
                return; // Salir del método si el campo de horas está vacío
            }

            if (string.IsNullOrWhiteSpace(txt_Actividades.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa las actividades.";
                return; // Salir del método si el campo de actividades está vacío
            }

            // Validar caracteres especiales y otras condiciones en las actividades
            if (!ValidarTexto(txt_Actividades.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa actividades válidas sin caracteres especiales o tres letras consecutivas.";
                return; // Salir del método si hay un error de validación
            }

            // No se valida la conversión de la fecha
            // int horas;
            // Validar la conversión de horas
            if (int.TryParse(txt_Horas.Text, out int horas))
            {
                // Validar que las horas estén entre 1 y 8
                if (horas < 1 || horas > 8)
                {
                    lbl_Mensaje.Text = "Por favor, ingresa un número de horas entre 1 y 8.";
                    return; // Salir del método si las horas no están en el rango permitido
                }

                int idUsuario = 1; // Cambiar esto según el contexto de tu aplicación

                // Convertir la fecha a string en el formato que tu base de datos espera
                string fechaString = txt_Fecha.Text;  // Cambia el formato si es necesario

                // Llamar al método 'crear' pasando 'fechaString' en lugar de 'fecha'
                if (hojaTiempo.crear(0, fechaString, txt_Actividades.Text, horas, idUsuario) > 0)
                {
                    lbl_Mensaje.Text = "Registrado Exitosamente";
                    hojaTiempo.grid_hojas(grid_hojas);

                    // Limpiar los campos después de un registro exitoso
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
            // Boton de agregar otro
            Button1.Visible = true;
            CalcularTotalHoras();
        }

        protected void Btn_Actualizar_Click(object sender, EventArgs e)
        {
            hojaTiempo = new HojaTiempo();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txt_Fecha.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa una fecha.";
                return; // Salir del método si el campo de fecha está vacío
            }

            if (string.IsNullOrWhiteSpace(txt_Horas.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa un número de horas.";
                return; // Salir del método si el campo de horas está vacío
            }

            if (string.IsNullOrWhiteSpace(txt_Actividades.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa las actividades.";
                return; // Salir del método si el campo de actividades está vacío
            }

            // Validar caracteres especiales y otras condiciones en las actividades
            if (!ValidarTexto(txt_Actividades.Text))
            {
                lbl_Mensaje.Text = "Por favor, ingresa actividades válidas sin caracteres especiales o tres letras consecutivas.";
                return; // Salir del método si hay un error de validación
            }

            // No se valida la conversión de la fecha
            // Validar la conversión de horas
            if (int.TryParse(txt_Horas.Text, out int horas))
            {
                // Validar que las horas estén entre 1 y 8
                if (horas < 1 || horas > 8)
                {
                    lbl_Mensaje.Text = "Por favor, ingresa un número de horas entre 1 y 8.";
                    return; // Salir del método si las horas no están en el rango permitido
                }

                // Llamar al método 'actualizar' con los parámetros validados
                if (hojaTiempo.actualizar(Convert.ToInt32(grid_hojas.SelectedValue), txt_Fecha.Text, txt_Actividades.Text, horas, 1) > 0)
                {
                    lbl_Mensaje.Text = "Modificado Exitosamente";
                    hojaTiempo.grid_hojas(grid_hojas);

                    // Limpiar los campos después de un registro exitoso
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

            // Obtén el ID de la hoja de tiempo desde el GridView
            int IDHoja = Convert.ToInt32(grid_hojas.DataKeys[e.RowIndex].Value);

            // Llama al método que marca el registro como eliminado
            if (hojaTiempo.eliminar(IDHoja) > 0)
            {
                lbl_Mensaje.Text = "Eliminado Exitosamente";
                hojaTiempo.grid_hojas(grid_hojas);  // Refresca el GridView
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

            // Cancelar el evento de eliminación para evitar la eliminación física
            e.Cancel = true;
            CalcularTotalHoras();
        }

        private bool ValidarTexto(string texto)
        {
            // Expresión regular para validar que no haya caracteres especiales (excepto punto y coma)
            string regexCaracteresEspeciales = @"^[^<>!@#$%^&*()_+=-]+$";
            // Expresión regular para validar que no haya más de dos letras seguidas
            string regexTresLetrasSeguidas = @"^(?!.*([a-zA-Z])\1{2})"; // No permite tres letras seguidas
            // Expresión regular para validar que no haya más de un punto o coma seguido
            string regexPuntosComasSeguidos = @"(?<![.,])[.,]{2,}(?![.,])";

            // Validar cada condición
            if (!Regex.IsMatch(texto, regexCaracteresEspeciales) ||
                !Regex.IsMatch(texto, regexTresLetrasSeguidas) ||
                Regex.IsMatch(texto, regexPuntosComasSeguidos))
            {
                return false; // Retorna falso si alguna validación falla
            }

            return true; // Retorna verdadero si todas las validaciones son exitosas
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

            // Iterar sobre cada fila del GridView para sumar las horas
            foreach (GridViewRow row in grid_hojas.Rows)
            {
                // Obtener las horas desde la columna correspondiente (asumiendo que las horas están en la tercera columna)
                if (int.TryParse(row.Cells[2].Text, out int horas))
                {
                    totalHoras += horas; // Sumar horas al total
                }
            }

            // Mostrar el total de horas en un Label (deberías tener un Label para mostrar el total)
            lbl_HorasTotales.Text = $"Total de Horas: {totalHoras}"; // Asegúrate de tener un Label con este ID
        }

    }
}

