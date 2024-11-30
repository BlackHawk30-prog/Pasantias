using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Pasantias
{
    public partial class Cambio_Password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si el usuario está autenticado
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btn_CambiarPassword_Click(object sender, EventArgs e)
        {
            lbl_Error.Visible = false;

            // Reiniciar clases CSS de los campos
            txt_NewPassword.CssClass = "form-control";
            txt_ConfirmPassword.CssClass = "form-control";

            string oldPassword = txt_OldPassword.Text.Trim();
            string newPassword = txt_NewPassword.Text.Trim();
            string confirmPassword = txt_ConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MostrarError("Todos los campos son obligatorios.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MostrarError("La nueva contraseña y su confirmación no coinciden.");
                MarcarCampoConError(txt_NewPassword);
                MarcarCampoConError(txt_ConfirmPassword);
                return;
            }

            if (!ValidarNuevaPassword(newPassword))
            {
                MostrarError("La nueva contraseña debe tener al menos 12 caracteres, incluir una letra mayúscula, una letra minúscula, un número y un carácter especial.");
                MarcarCampoConError(txt_NewPassword);
                return;
            }

            int userId = (int)Session["UserID"];
            if (!VerificarPasswordActual(userId, oldPassword))
            {
                MostrarError("La contraseña actual es incorrecta.");
                return;
            }

            if (ActualizarPassword(userId, newPassword))
            {
                // Mensaje de éxito y redirección
                ScriptManager.RegisterStartupScript(this, GetType(), "CambioExitoso",
                    "alert('Contraseña actualizada exitosamente.'); window.location.href='Default.aspx';", true);
            }
            else
            {
                MostrarError("Ocurrió un error al actualizar la contraseña. Intente nuevamente.");
            }
        }

        private bool VerificarPasswordActual(int userId, string oldPassword)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                string query = "SELECT Password FROM usuarios WHERE IDUsuario = @userId";
                using (MySqlCommand cmd = new MySqlCommand(query, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString() == oldPassword;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al verificar la contraseña: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return false;
        }

        private bool ActualizarPassword(int userId, string newPassword)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                string procedimiento = "CALL ActualizarPassword(@userId, @newPassword)";
                using (MySqlCommand cmd = new MySqlCommand(procedimiento, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al actualizar la contraseña: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return false;
        }


        private bool ValidarNuevaPassword(string password)
        {
            // Regex para validar la contraseña
            string pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{12,}$";
            return Regex.IsMatch(password, pattern);
        }

        private void MostrarError(string mensaje)
        {
            lbl_Error.Text = mensaje;
            lbl_Error.Visible = true;
        }

        private void MarcarCampoConError(System.Web.UI.WebControls.TextBox campo)
        {
            campo.CssClass += " is-invalid";
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
