using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Pasantias
{
    public partial class Convenio : System.Web.UI.Page
    {
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
                // Validar si el IDUsuario viene en la URL
                if (!string.IsNullOrEmpty(Request.QueryString["IDUsuario"]))
                {
                    try
                    {
                        // Desencriptar el IDUsuario
                        string idUsuarioEncriptado = Request.QueryString["IDUsuario"];
                        string idUsuarioDesencriptado = EncriptacionAES.Desencriptar(idUsuarioEncriptado);

                        // Almacenar el IDUsuario desencriptado en ViewState
                        ViewState["IDUsuario"] = Convert.ToInt32(idUsuarioDesencriptado);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error al desencriptar el IDUsuario: " + ex.Message + "');</script>");
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    Response.Write("<script>alert('IDUsuario no proporcionado. Redirigiendo...');</script>");
                    Response.Redirect("Default.aspx");
                }

                // Calcular las fechas
                DateTime fechaInicio = DateTime.Now;
                DateTime fechaFinal = fechaInicio.AddMonths(11);

                // Asignar las fechas a los campos
                txt_FechaInicio.Text = fechaInicio.ToString("yyyy-MM-dd");
                txt_FechaFinal.Text = fechaFinal.ToString("yyyy-MM-dd");

                // Hacer los campos de solo lectura
                txt_FechaInicio.ReadOnly = true;
                txt_FechaFinal.ReadOnly = true;
            }
        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            InsertarConvenio(1, 0); // Aceptado = 1, Rechazado = 0
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            InsertarConvenio(0, 1); // Aceptado = 0, Rechazado = 1
        }

        private void InsertarConvenio(int aceptado, int rechazado)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                // Verificar si el IDUsuario está en el ViewState
                if (ViewState["IDUsuario"] == null)
                {
                    Response.Write("<script>alert('IDUsuario no encontrado.');</script>");
                    return;
                }

                int idUsuario = Convert.ToInt32(ViewState["IDUsuario"]);

                // Verificar si ya existe un convenio para el usuario
                string consultaExistencia = "SELECT COUNT(*) FROM convenio WHERE IDUsuario = @IDUsuario";
                using (MySqlCommand cmdExistencia = new MySqlCommand(consultaExistencia, conectar.conectar))
                {
                    cmdExistencia.Parameters.AddWithValue("@IDUsuario", idUsuario);
                    int existeConvenio = Convert.ToInt32(cmdExistencia.ExecuteScalar());

                    if (existeConvenio > 0)
                    {
                        // Si ya existe un convenio, mostrar mensaje y salir
                        Response.Write("<script>alert('Ya existe un convenio para este usuario.');</script>");
                        return;
                    }
                }

                // Calcular fechas
                DateTime fechaInicio = DateTime.Now;
                DateTime fechaFinal = fechaInicio.AddMonths(11);

                // Insertar nuevo convenio
                string query = @"
INSERT INTO convenio (Fecha_Inicio, Fecha_Final, Aceptado, Rechazado, IDUsuario) 
VALUES (@FechaInicio, @FechaFinal, @Aceptado, @Rechazado, @IDUsuario)";

                using (MySqlCommand cmd = new MySqlCommand(query, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Aceptado", aceptado);
                    cmd.Parameters.AddWithValue("@Rechazado", rechazado);
                    cmd.Parameters.AddWithValue("@IDUsuario", idUsuario);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        if (aceptado == 1)
                        {
                            // Actualizar el rol y credenciales
                            string correo = ObtenerCorreoPorIDUsuario(idUsuario, conectar);
                            ActualizarRolYCredenciales(conectar, idUsuario, correo);
                        }

                        string mensaje = aceptado == 1 ? "Convenio aceptado exitosamente." : "Convenio rechazado exitosamente.";
                        // Mostrar el mensaje y cerrar la ventana
                        Response.Write($"<script>alert('{mensaje}'); window.close();</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('No se insertaron registros.');</script>");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Response.Write($"<script>alert('Error de base de datos: {ex.Message}');</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error inesperado: {ex.Message}');</script>");
            }
            finally
            {
                conectar.CerrarConexion();
            }
        }

        private void ActualizarRolYCredenciales(ConexionBD conectar, int idUsuario, string correo)
        {
            try
            {
                // Obtener el primer nombre, primer apellido y DNI del usuario
                string consultaUsuario = "SELECT Primer_Nombre, Primer_Apellido, DNI FROM usuarios WHERE IDUsuario = @IDUsuario";
                using (MySqlCommand cmdUsuario = new MySqlCommand(consultaUsuario, conectar.conectar))
                {
                    cmdUsuario.Parameters.AddWithValue("@IDUsuario", idUsuario);

                    using (MySqlDataReader reader = cmdUsuario.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string primerNombre = reader["Primer_Nombre"].ToString();
                            string primerApellido = reader["Primer_Apellido"].ToString();
                            string dni = reader["DNI"].ToString();

                            // Generar credenciales
                            string usuario = primerNombre.Substring(0, 1) + primerApellido;
                            string password = dni;

                            reader.Close(); // Importante cerrar el lector antes de ejecutar otro comando

                            // Actualizar el rol del usuario a IDRol = 2
                            string consultaRol = "SELECT COUNT(*) FROM roles_usuarios WHERE IDUsuario = @IDUsuario";
                            using (MySqlCommand cmdExistenciaRol = new MySqlCommand(consultaRol, conectar.conectar))
                            {
                                cmdExistenciaRol.Parameters.AddWithValue("@IDUsuario", idUsuario);
                                int existeRol = Convert.ToInt32(cmdExistenciaRol.ExecuteScalar());

                                if (existeRol > 0)
                                {
                                    // Actualizar el rol existente
                                    string actualizarRol = @"
                            UPDATE roles_usuarios
                            SET IDRol = 2
                            WHERE IDUsuario = @IDUsuario";
                                    using (MySqlCommand cmdActualizarRol = new MySqlCommand(actualizarRol, conectar.conectar))
                                    {
                                        cmdActualizarRol.Parameters.AddWithValue("@IDUsuario", idUsuario);
                                        cmdActualizarRol.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('El usuario no tiene un rol registrado previamente.');</script>");
                                    return;
                                }
                            }

                            // Actualizar credenciales en la tabla usuarios
                            string updateUsuario = @"
                    UPDATE usuarios 
                    SET Usuario = @Usuario, Password = @Password 
                    WHERE IDUsuario = @IDUsuario";
                            using (MySqlCommand cmdUpdateUsuario = new MySqlCommand(updateUsuario, conectar.conectar))
                            {
                                cmdUpdateUsuario.Parameters.AddWithValue("@Usuario", usuario);
                                cmdUpdateUsuario.Parameters.AddWithValue("@Password", password);
                                cmdUpdateUsuario.Parameters.AddWithValue("@IDUsuario", idUsuario);
                                cmdUpdateUsuario.ExecuteNonQuery();
                            }

                            // Enviar correo con las credenciales
                            EnviarCorreoCredenciales(correo, usuario, password);
                        }
                        else
                        {
                            Response.Write("<script>alert('Usuario no encontrado en la base de datos.');</script>");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Response.Write($"<script>alert('Error al actualizar rol o credenciales: {ex.Message}');</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error inesperado: {ex.Message}');</script>");
            }
        }

        private string ObtenerCorreoPorIDUsuario(int idUsuario, ConexionBD conectar)
        {
            string correo = null;

            try
            {
                // Consulta SQL para obtener el correo basado en el IDUsuario
                string consulta = "SELECT Correo FROM usuarios WHERE IDUsuario = @IDUsuario LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@IDUsuario", idUsuario);
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        correo = resultado.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al obtener el correo: {ex.Message}');</script>");
            }

            return correo;
        }

        private void EnviarCorreoCredenciales(string destinatario, string usuario, string password)
        {
            try
            {
                using (MailMessage mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress("hreyesfotos@gmail.com");
                    mensaje.To.Add(destinatario);
                    mensaje.Subject = "Credenciales de acceso";
                    mensaje.Body = $"Su usuario es: {usuario} \n Su contraseña es: {password}";

                    mensaje.IsBodyHtml = true;

                    using (SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        clienteSmtp.Credentials = new System.Net.NetworkCredential("hreyesfotos@gmail.com", "ovqx ypvm vtbt fttp");
                        clienteSmtp.EnableSsl = true;
                        clienteSmtp.Send(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al enviar el correo: {ex.Message}');</script>");
            }
        }
    }
}
