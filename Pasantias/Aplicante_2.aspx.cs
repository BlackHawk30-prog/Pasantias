using Modelo;
using System;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;

namespace Pasantias
{
    public partial class Aplicante_2 : System.Web.UI.Page
    {
        Aplicante2 aplicante2;

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
                string dniEncriptado = Request.QueryString["DNI"];
                if (string.IsNullOrEmpty(dniEncriptado))
                {
                    lbl_Error.Text = "DNI no válido.";
                    lbl_Error.Visible = true;
                    return;
                }

              //  if (dniEncriptado.Length % 4 != 0 || !IsBase64String(dniEncriptado))
              //  {
           //         lbl_Error.Text = "El DNI encriptado no tiene un formato válido.";
            //        lbl_Error.Visible = true;
            //        return;
            //    }

                try
                {
                    string dniDesencriptado = EncriptacionAES.Desencriptar(dniEncriptado);
                    ViewState["DNI"] = dniDesencriptado;
                }
                catch (CryptographicException ex)
                {
                    lbl_Error.Text = $"Error al desencriptar el DNI: {ex.Message}";
                    lbl_Error.Visible = true;
                }
                catch (FormatException ex)
                {
                    lbl_Error.Text = $"Error de formato: {ex.Message}";
                    lbl_Error.Visible = true;
                }
            }
        }

        private bool IsBase64String(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length % 4 != 0)
                return false;

            foreach (char c in s)
            {
                if (!char.IsLetterOrDigit(c) && c != '+' && c != '/' && c != '=')
                    return false;
            }
            return true;
        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            bool esValido = true;
            lbl_Error.Text = string.Empty;

            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txt_Fecha.Text, out fechaNacimiento) || !Utilidades.ValidarEdad(fechaNacimiento))
            {
                esValido = false;
                txt_Fecha.CssClass += " error";
                lbl_Error.Text += "Ingrese una fecha de nacimiento válida (debe ser mayor de 16 años).<br/>";
            }
            else
            {
                txt_Fecha.CssClass = txt_Fecha.CssClass.Replace("error", "");
            }

            if (!Utilidades.ValidarTelefono(txt_Telefono.Text))
            {
                esValido = false;
                txt_Telefono.CssClass += " error";
                lbl_Error.Text += "Ingrese un número de teléfono válido de 8 dígitos.<br/>";
            }
            else
            {
                txt_Telefono.CssClass = txt_Telefono.CssClass.Replace("error", "");
            }

            if (!Utilidades.ValidarCampoObligatorio(txt_Universidad.Text) || !Utilidades.ValidarTexto(txt_Universidad.Text))
            {
                esValido = false;
                txt_Universidad.CssClass += " error";
                lbl_Error.Text += "Ingrese un nombre de universidad válido.<br/>";
            }
            else
            {
                txt_Universidad.CssClass = txt_Universidad.CssClass.Replace("error", "");
            }

            if (!Utilidades.ValidarCampoObligatorio(txt_Direccion.Text) || !Utilidades.ValidarTexto(txt_Direccion.Text))
            {
                esValido = false;
                txt_Direccion.CssClass += " error";
                lbl_Error.Text += "Ingrese una dirección válida.<br/>";
            }
            else
            {
                txt_Direccion.CssClass = txt_Direccion.CssClass.Replace("error", "");
            }

            string sexo = txt_Hombre.Checked ? "Hombre" : txt_Mujer.Checked ? "Mujer" : "";
            if (string.IsNullOrEmpty(sexo))
            {
                esValido = false;
                lbl_Error.Text += "Seleccione su sexo.<br/>";
            }

            if (!txt_Foto.HasFile || !Utilidades.ValidarTipoArchivoFoto(txt_Foto.FileName))
            {
                esValido = false;
                txt_Foto.CssClass += " error";
                lbl_Error.Text += "La foto debe estar en formato JPG o PNG.<br/>";
            }
            else
            {
                txt_Foto.CssClass = txt_Foto.CssClass.Replace("error", "");
            }

            if (!txt_Curriculum.HasFile || !Utilidades.ValidarTipoArchivoCurriculum(txt_Curriculum.FileName))
            {
                esValido = false;
                txt_Curriculum.CssClass += " error";
                lbl_Error.Text += "El currículum debe estar en formato DOC, DOCX o PDF.<br/>";
            }
            else
            {
                txt_Curriculum.CssClass = txt_Curriculum.CssClass.Replace("error", "");
            }

            if (!esValido)
            {
                lbl_Error.Visible = true;
                return;
            }

            aplicante2 = new Aplicante2();
            string dni = ViewState["DNI"].ToString();

            int resultado = aplicante2.Crear(
                txt_Telefono.Text,
                txt_Direccion.Text,
                txt_Universidad.Text,
                sexo,
                txt_Foto.FileBytes,
                txt_Curriculum.FileBytes,
                dni
            );

            if (resultado > 0)
            {
                string correo = aplicante2.ObtenerCorreoPorDNI(dni);

                if (correo != null)
                {
                    EnviarCorreoAgradecimiento(correo);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Datos enviados exitosamente, pero no se encontró el correo.');", true);
                }
            }
            else
            {
                lbl_Error.Text = "Hubo un problema al enviar los datos.";
                lbl_Error.Visible = true;
            }
        }

        private void EnviarCorreoAgradecimiento(string destinatario)
        {
            try
            {
                using (MailMessage mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress("hreyesfotos@gmail.com"); // Dirección de correo de origen
                    mensaje.To.Add(destinatario);
                    mensaje.Subject = "Gracias por aplicar a la pasantía";
                    mensaje.Body = "Estimado(a) postulante,\n\nGracias por aplicar a nuestra pasantía. Su solicitud ha sido recibida exitosamente. Nos pondremos en contacto con usted en breve.\n\nSaludos cordiales,\nEl equipo de Pasantías";
                    mensaje.IsBodyHtml = false;

                    using (SmtpClient clienteSmtp = new SmtpClient())
                    {
                        clienteSmtp.Host = "smtp.gmail.com"; // Configura el servidor SMTP
                        clienteSmtp.Port = 587; // Puerto del servidor SMTP
                        clienteSmtp.Credentials = new System.Net.NetworkCredential("hreyesfotos@gmail.com", "ovqx ypvm vtbt fttp"); // Configura tus credenciales
                        clienteSmtp.EnableSsl = true;

                        clienteSmtp.Send(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_Error.Text = $"Error al enviar el correo de agradecimiento: {ex.Message}";
                lbl_Error.Visible = true;
            }
        }
    }
}

