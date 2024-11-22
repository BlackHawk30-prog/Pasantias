using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Postulaciones_Regional : System.Web.UI.Page
    {
        private Postulacion Postulacion; // Declaración del objeto Postulacion

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicialización del objeto Postulacion
            if (Postulacion == null)
            {
                Postulacion = new Postulacion();
            }

            if (!IsPostBack)
            {
                string condicionSeguridad = "u.RHConfirmado = 1 AND SConfirmado = 1 AND RConfirmado = 0";
                Postulacion.grid_aplicantes(grid_aplicantes, condicionSeguridad);
            }
        }

        protected void grid_aplicantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idUsuario = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Aceptar")
            {
                // Llamar al método para aceptar la postulación
                Postulacion.AceptarPostulacionRegional(idUsuario);

                // Obtener el correo del usuario por DNI y enviar el correo de agradecimiento
                string dni = Postulacion.ObtenerDNIporIDUsuario(idUsuario); // Suponiendo que este método existe
                string correo = Postulacion.ObtenerCorreoPorDNI(dni);
                if (!string.IsNullOrEmpty(correo))
                {
                    EnviarCorreoAgradecimiento(correo, idUsuario);

                }

                // Volver a cargar la tabla
                string condicionSeguridad = "u.RHConfirmado = 1 AND SConfirmado = 1 AND RConfirmado = 0";
                Postulacion.grid_aplicantes(grid_aplicantes, condicionSeguridad);
            }
            else if (e.CommandName == "Rechazar")
            {
                // Llamar al método para rechazar la postulación
                Postulacion.RechazarPostulacion(idUsuario);

                // Obtener el correo del usuario por DNI y enviar el correo de rechazo
                string dni = Postulacion.ObtenerDNIporIDUsuario(idUsuario); // Suponiendo que este método existe
                string correo = Postulacion.ObtenerCorreoPorDNI(dni);
                if (!string.IsNullOrEmpty(correo))
                {
                    EnviarCorreoRechazo(correo);
                }

                // Volver a cargar la tabla
                string condicionSeguridad = "u.RHConfirmado = 1 AND SConfirmado = 1 AND RConfirmado = 0";
                Postulacion.grid_aplicantes(grid_aplicantes, condicionSeguridad);
            }
            else if (e.CommandName == "Detalles")
            {
                // Redirigir a la vista Detalle_Postulante con el IDUsuario como parámetro en la URL
                Response.Redirect($"Detalle_Postulante.aspx?IDUsuario={idUsuario}");
            }
        }
        private void EnviarCorreoAgradecimiento(string destinatario, int idUsuario)
        {
            try
            {
                using (MailMessage mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress("hreyesfotos@gmail.com");
                    mensaje.To.Add(destinatario);
                    mensaje.Subject = "Notificación de Aceptación de Pasantía";

                    // Construir el enlace al formulario Convenio.aspx con el IDUsuario como parámetro
                    string urlConvenio = $"https://localhost:44316/Convenio.aspx?IDUsuario={idUsuario}";

                    // Crear el cuerpo del mensaje
                    mensaje.Body = $"Estimado(a) postulante,\n\n" +
                                   "Gracias por aplicar a nuestra pasantía. Su solicitud ha sido aceptada exitosamente. Nos pondremos en contacto con usted en breve.\n\n" +
                                   "Para continuar con el proceso, por favor leer el convenio en el siguiente enlace:\n" +
                                   $"{urlConvenio}\n\n" +
                                   "Saludos cordiales,\nEl equipo de Pasantías";

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
                // Manejar el error si ocurre
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
               // lbl_Error.Text = $"Error al enviar el correo de rechazo: {ex.Message}";
               // lbl_Error.Visible = true;
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {


            Response.Redirect("Default.aspx");

        }
    }
}