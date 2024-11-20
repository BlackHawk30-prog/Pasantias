using System;
using System.Net.Mail;
using System.Web.UI.WebControls;
using Modelo;

namespace Pasantias
{
    public partial class Postulaciones : System.Web.UI.Page
    {
        Postulacion Postulacion;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Asegurarte de inicializar el objeto Postulacion en cada carga de la página
            Postulacion = new Postulacion();

            if (!IsPostBack)
            {
                Postulacion.grid_aplicantes(grid_aplicantes);
            }
        }

        protected void grid_aplicantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Aceptar")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                // Llamar al método para aceptar la postulación
                Postulacion.AceptarPostulacion(idUsuario);

                Postulacion.grid_aplicantes(grid_aplicantes);

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
                Postulacion.grid_aplicantes(grid_aplicantes);

            }
            else if (e.CommandName == "Detalles")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                // Redirigir a la vista Detalle_Postulante con el IDUsuario como parámetro en la URL
                Response.Redirect($"Detalle_Postulante.aspx?IDUsuario={idUsuario}");
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
    }
}
