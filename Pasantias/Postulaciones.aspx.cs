using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using Modelo;

namespace Pasantias
{
    public partial class Postulaciones : System.Web.UI.Page
    {
        Postulacion Postulacion;
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
            // Inicialización del objeto Postulacion
            if (Postulacion == null)
            {
                Postulacion = new Postulacion();
            }

            if (!IsPostBack)
            {
                string condicionrecursos = "u.RHConfirmado = 0";
                Postulacion.grid_aplicantes(grid_aplicantes, condicionrecursos);
            }
        }

        protected void grid_aplicantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Aceptar")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                // Llamar al método para aceptar la postulación
                Postulacion.AceptarPostulacion(idUsuario);
                string condicionrecursos = "u.RHConfirmado = 0";
                Postulacion.grid_aplicantes(grid_aplicantes, condicionrecursos);

            }
            else if (e.CommandName == "Rechazar")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                // Llamar al método para rechazar la postulación
                Postulacion.RechazarPostulacion(idUsuario);

                // Obtener el correo del usuario por DNI y enviar el correo de rechazo
                string dni = Postulacion.ObtenerDNIporIDUsuario(idUsuario); // Método para obtener el DNI basado en el IDUsuario
                string correo = Postulacion.ObtenerCorreoPorDNI(dni);
                if (!string.IsNullOrEmpty(correo))
                {
                    EnviarCorreoRechazo(correo);
                }
                string condicionrecursos = "u.RHConfirmado = 0";
                Postulacion.grid_aplicantes(grid_aplicantes, condicionrecursos);

            }
            else if (e.CommandName == "Detalles")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                // Redirigir a la vista Detalle_Postulante con el IDUsuario como parámetro en la URL
                string idEncriptado = EncriptacionAES.Encriptar(idUsuario.ToString());
                Response.Redirect($"Detalle_Postulante.aspx?IDUsuario={idEncriptado}");
            }
        }

        private void EnviarCorreoRechazo(string destinatario)
        {
            try
            {
                using (MailMessage mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress("hreyesfotos@gmail.com");
                    mensaje.To.Add(destinatario);
                    mensaje.Subject = "Notificación de rechazo de pasantía";
                    mensaje.Body = "Estimado(a) postulante,\n\nLamentamos informarle que su solicitud para nuestra pasantía no ha sido aceptada. Le agradecemos por su interés y le deseamos éxito en sus futuros proyectos.\n\nSaludos cordiales,\nEl equipo de Pasantías";
                    mensaje.IsBodyHtml = false;

                    using (SmtpClient clienteSmtp = new SmtpClient())
                    {
                        clienteSmtp.Host = "smtp.gmail.com";
                        clienteSmtp.Port = 587;
                        clienteSmtp.Credentials = new System.Net.NetworkCredential("hreyesfotos@gmail.com", "ovqx ypvm vtbt fttp");
                        clienteSmtp.EnableSsl = true;

                        clienteSmtp.Send(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores en el envío del correo
                Console.WriteLine($"Error al enviar el correo de rechazo: {ex.Message}");
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {


            Response.Redirect("Default.aspx");

        }
    }
}
