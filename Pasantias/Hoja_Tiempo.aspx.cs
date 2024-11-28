using Modelo;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;


namespace Pasantias
{
    public partial class Hoja_Tiempo : System.Web.UI.Page
    {
        HojaTiempo hojaTiempo;
        public static class EncriptacionAES
        {
            private static readonly string key = "clave_secreta_123";  // Clave secreta

            private static byte[] ObtenerClave(string clave)
            {
                byte[] claveBytes = Encoding.UTF8.GetBytes(clave);
                Array.Resize(ref claveBytes, 16);  // Ajustar a 16 bytes
                return claveBytes;
            }

            public static string Encriptar(string texto)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = ObtenerClave(key);
                    aes.IV = new byte[16];  // Vector de inicialización

                    ICryptoTransform encriptador = aes.CreateEncryptor(aes.Key, aes.IV);
                    byte[] textoBytes = Encoding.UTF8.GetBytes(texto);

                    byte[] encriptado = encriptador.TransformFinalBlock(textoBytes, 0, textoBytes.Length);
                    return Convert.ToBase64String(encriptado);
                }
            }

            public static string Desencriptar(string textoEncriptado)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = ObtenerClave(key);
                    aes.IV = new byte[16];  // Vector de inicialización

                    ICryptoTransform desencriptador = aes.CreateDecryptor(aes.Key, aes.IV);
                    byte[] encriptadoBytes = Convert.FromBase64String(textoEncriptado);

                    byte[] desencriptado = desencriptador.TransformFinalBlock(encriptadoBytes, 0, encriptadoBytes.Length);
                    return Encoding.UTF8.GetString(desencriptado);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idHojaTiempoEncriptado = Request.QueryString["IDHojaTiempo"];
                if (!string.IsNullOrWhiteSpace(idHojaTiempoEncriptado))
                {
                    try
                    {
                        // Guardar el ID encriptado en la sesión
                        Session["IDHojaTiempoEncriptado"] = idHojaTiempoEncriptado;

                        // Desencriptar el ID
                        string idHojaTiempoDesencriptado = EncriptacionAES.Desencriptar(idHojaTiempoEncriptado);
                        if (int.TryParse(idHojaTiempoDesencriptado, out int idHojaTiempo))
                        {
                            // Guardar el ID desencriptado en la sesión
                            SessionStore.HojaID = idHojaTiempo;

                            // Verificar el estado de PConfirmado desde la base de datos
                            hojaTiempo = new HojaTiempo();
                            int pConfirmado = hojaTiempo.ObtenerPConfirmado(idHojaTiempo); // Método para obtener PConfirmado

                            // Guardar PConfirmado en la sesión
                            Session["PConfirmado"] = pConfirmado;

                            // Validar si la hoja ya está confirmada
                            if (pConfirmado == 1)
                            {
                                // Ocultar los botones excepto "Regresar"
                                Btn_Agregar.Visible = false;
                                Btn_Actualizar.Visible = false;
                                Button1.Visible = false;
                                Btn_Enviar.Visible = false;

                                // Ocultar la tabla
                                grid_hojas.Visible = false;

                                // Mostrar mensaje de confirmación
                                lbl_Mensaje.Text = "La hoja de tiempo ya está confirmada y no puede ser modificada.";
                                lbl_Mensaje.ForeColor = System.Drawing.Color.Green;
                                return; // Salir del método para evitar cargar datos innecesarios
                            }

                            // Mostrar la tabla y cargar datos si no está confirmada
                            grid_hojas.Visible = true;
                            grid_hojas.DataSource = hojaTiempo.grid_hojas();
                            grid_hojas.DataBind();

                            // Calcular total de horas
                            CalcularTotalHoras();
                        }
                        else
                        {
                            lbl_Mensaje.Text = "ID de Hoja de Tiempo no válido.";
                            lbl_Mensaje.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch (Exception ex)
                    {
                        lbl_Mensaje.Text = "Error al procesar el ID de Hoja de Tiempo.";
                        lbl_Mensaje.ForeColor = System.Drawing.Color.Red;
                        // Opcional: Registrar el error en el log
                        // LogError(ex);
                    }
                }
                else
                {
                    lbl_Mensaje.Text = "ID de Hoja de Tiempo no proporcionado.";
                    lbl_Mensaje.ForeColor = System.Drawing.Color.Red;
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

                int idHojaTiempo = (int)SessionStore.HojaID;
                string fechaString = txt_Fecha.Text;

                if (hojaTiempo.crear(fechaString, txt_Actividades.Text, horas, idHojaTiempo) > 0)
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

                int idHojaTiempo = (int)SessionStore.HojaID;
                if (hojaTiempo.actualizar(Convert.ToInt32(grid_hojas.SelectedValue), txt_Fecha.Text, txt_Actividades.Text, horas, idHojaTiempo) > 0)
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
        protected void Button2_Click(object sender, EventArgs e)
        {
            // Validar si PConfirmado es 1 y ocultar botones
            if (Session["PConfirmado"] != null && Convert.ToInt32(Session["PConfirmado"]) == 1)
            {
                Btn_Agregar.Visible = false;
                Btn_Actualizar.Visible = false;
                Button1.Visible = false;
                Btn_Enviar.Visible = false;
                lbl_Mensaje.Text = "La hoja de tiempo ya está confirmada y no puede ser modificada.";
                lbl_Mensaje.ForeColor = System.Drawing.Color.Green;
                return;
            }

            // Limpiar los campos de texto
            txt_Fecha.Text = "";
            txt_Horas.Text = "";
            txt_Actividades.Text = "";

            // Ajustar la visibilidad de los botones
            Btn_Actualizar.Visible = false;
            Btn_Agregar.Visible = true;
            Button1.Visible = false;

            try
            {
                // Obtener el ID encriptado desde la sesión
                string idHojaTiempoEncriptado = Session["IDHojaTiempoEncriptado"] as string;

                if (string.IsNullOrEmpty(idHojaTiempoEncriptado))
                {
                    lbl_Mensaje.Text = "ID de Hoja de Tiempo no encontrado en la sesión.";
                    lbl_Mensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Desencriptar el ID
                string idHojaTiempoDesencriptado = EncriptacionAES.Desencriptar(idHojaTiempoEncriptado);

                // Convertir a entero
                if (int.TryParse(idHojaTiempoDesencriptado, out int idHojaTiempo))
                {
                    // Crear una instancia de la clase HojaTiempo
                    HojaTiempo hoja = new HojaTiempo();

                    // Llamar al método para confirmar la hoja de tiempo
                    int filasModificadas = hoja.ConfirmarHojaTiempo(idHojaTiempo);

                    // Mostrar un mensaje de confirmación al usuario
                    if (filasModificadas > 0)
                    {
                        lbl_Mensaje.Text = "La hoja de tiempo ha sido confirmada exitosamente.";
                        lbl_Mensaje.ForeColor = System.Drawing.Color.Green;

                        // Actualizar la variable de sesión para reflejar la confirmación
                        Session["PConfirmado"] = 1;

                        // Ocultar los botones excepto "Regresar"
                        Btn_Agregar.Visible = false;
                        Btn_Actualizar.Visible = false;
                        Button1.Visible = false;
                        Btn_Enviar.Visible = false;
                    }
                    else
                    {
                        lbl_Mensaje.Text = "No se pudo confirmar la hoja de tiempo. Verifique el ID.";
                        lbl_Mensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lbl_Mensaje.Text = "El ID de Hoja de Tiempo desencriptado no es válido.";
                    lbl_Mensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lbl_Mensaje.Text = "Ocurrió un error al confirmar la hoja de tiempo.";
                lbl_Mensaje.ForeColor = System.Drawing.Color.Red;

                // Opcional: Registrar el error en el log
                // LogError(ex);
            }
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
        protected void btnRegresar_Click(object sender, EventArgs e)
        {


            Response.Redirect("Hojas_Generales.aspx");

        }
    }
}