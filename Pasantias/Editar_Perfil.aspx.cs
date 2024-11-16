using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Editar_Perfil : Page
    {
        private bool cambiosRealizados = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    int idUsuario = (int)Session["UserID"];
                    System.Diagnostics.Debug.WriteLine($"IDUsuario recibido de la sesión: {idUsuario}");
                    CargarDatosUsuario(idUsuario);

                    string imagePath = ObtenerRutaFotoDesdeBD(idUsuario);
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        imgFoto.ImageUrl = ResolveUrl("~/" + imagePath);
                    }

                    string curriculumPath = ObtenerRutaCurriculumDesdeBD(idUsuario);
                    if (!string.IsNullOrEmpty(curriculumPath))
                    {
                        lnkCurriculum.NavigateUrl = ResolveUrl("~/" + curriculumPath);
                        lnkCurriculum.Visible = true;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No se encontró el IDUsuario en la sesión.");
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void CargarDatosUsuario(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                string consulta = @"
                    SELECT 
                        u.Primer_Nombre, u.Segundo_Nombre, u.Primer_Apellido, u.Segundo_Apellido, 
                        u.DNI, u.Correo, du.Fecha_Nacimiento, du.Telefono, du.Direccion, 
                        du.Grado_academico, du.Sexo 
                    FROM usuarios u 
                    JOIN datos_usuario du ON u.IDUsuario = du.IDUsuario 
                    WHERE u.IDUsuario = @userId";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txt_Nombre1.Text = reader["Primer_Nombre"].ToString();
                            txt_Nombre2.Text = reader["Segundo_Nombre"].ToString();
                            txt_Apellido1.Text = reader["Primer_Apellido"].ToString();
                            txt_Apellido2.Text = reader["Segundo_Apellido"].ToString();
                            txt_DNI.Text = reader["DNI"].ToString();
                            txt_Correo.Text = reader["Correo"].ToString();
                            txt_Fecha.Text = Convert.ToDateTime(reader["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
                            txt_Telefono.Text = reader["Telefono"].ToString();
                            txt_Universidad.Text = reader["Grado_academico"].ToString();
                            txt_Direccion.Text = reader["Direccion"].ToString();
                            if (reader["Sexo"].ToString() == "Hombre")
                            {
                                txt_Hombre.Checked = true;
                            }
                            else if (reader["Sexo"].ToString() == "Mujer")
                            {
                                txt_Mujer.Checked = true;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar los datos del usuario: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }
        }

        private string ObtenerRutaFotoDesdeBD(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();
            string rutaFoto = null;

            try
            {
                string consulta = "SELECT Foto FROM datos_usuario WHERE IDUsuario = @userId";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != DBNull.Value)
                    {
                        rutaFoto = resultado.ToString();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al obtener la foto del usuario: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return rutaFoto;
        }

        private string ObtenerRutaCurriculumDesdeBD(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();
            string rutaCurriculum = null;

            try
            {
                string consulta = "SELECT Curriculum FROM datos_usuario WHERE IDUsuario = @userId";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != DBNull.Value)
                    {
                        rutaCurriculum = resultado.ToString();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al obtener el curriculum del usuario: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return rutaCurriculum;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (DatosModificados())
            {
                try
                {
                    int idUsuario = (int)Session["UserID"];
                    string dni = txt_DNI.Text.Trim();

                    // Guardar la foto si se ha seleccionado un archivo
                    if (txt_Foto.HasFile)
                    {
                        GuardarArchivo(txt_Foto, "Fotos", dni + "_foto");
                        // Agregar una marca de tiempo a la URL para forzar la actualización de la imagen
                        imgFoto.ImageUrl = "~/Fotos/" + dni + "_foto" + Path.GetExtension(txt_Foto.FileName) + "?t=" + DateTime.Now.Ticks;
                    }

                    // Guardar el currículum si se ha seleccionado un archivo
                    if (txt_Curriculum.HasFile)
                    {
                        GuardarArchivo(txt_Curriculum, "Curriculum", dni + "_curriculum");
                        // Agregar una marca de tiempo a la URL para forzar la actualización del currículum
                        lnkCurriculum.NavigateUrl = "~/Curriculum/" + dni + "_curriculum" + Path.GetExtension(txt_Curriculum.FileName) + "?t=" + DateTime.Now.Ticks;
                        lnkCurriculum.Visible = true;
                    }

                    // Llamar al método de actualización de datos del usuario
                    ActualizarDatosUsuario(idUsuario);

                    // Mostrar mensaje de éxito y redirigir
                    ScriptManager.RegisterStartupScript(this, GetType(), "ChangesAlert",
                        "alert('Actualizado Exitosamente.'); window.location.href='Default.aspx';", true);
                }
                catch (Exception ex)
                {
                    // Mostrar mensaje de error en caso de fallo
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert",
                        $"alert('Error al actualizar: {ex.Message}');", true);
                }
            }
            else
            {
                // Mostrar mensaje de que no hubo cambios
                ScriptManager.RegisterStartupScript(this, GetType(), "NoChangesAlert",
                    "alert('No se realizaron cambios.'); window.location.href='Default.aspx';", true);
            }
        }






        // Método para guardar el archivo en el sistema de archivos
        private void GuardarArchivo(FileUpload fileUploadControl, string carpeta, string nombreArchivo)
        {
            // Verificar el tipo de archivo permitido
            string extension = Path.GetExtension(fileUploadControl.FileName).ToLower();
            if ((carpeta == "Fotos" && (extension == ".jpg" || extension == ".png")) ||
                (carpeta == "Curriculum" && (extension == ".doc" || extension == ".docx" || extension == ".pdf" || extension == ".dox" || extension == ".360")))
            {
                // Definir el directorio y la ruta completa del archivo
                string directorio = Path.Combine(Server.MapPath("~/" + carpeta));
                string rutaCompleta = Path.Combine(directorio, nombreArchivo + extension);

                // Crear el directorio si no existe
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                // Guardar el archivo y reemplazar si ya existe
                fileUploadControl.SaveAs(rutaCompleta);
            }
            else
            {
                throw new InvalidOperationException("Formato de archivo no permitido.");
            }
        }
        private bool DatosModificados()
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();
            bool modificado = false;

            try
            {
                string consulta = @"
            SELECT u.Primer_Nombre, u.Segundo_Nombre, u.Primer_Apellido, u.Segundo_Apellido, 
                u.DNI, u.Correo, du.Fecha_Nacimiento, du.Telefono, du.Direccion, 
                du.Grado_academico, du.Sexo, du.Foto, du.Curriculum 
            FROM usuarios u 
            JOIN datos_usuario du ON u.IDUsuario = du.IDUsuario 
            WHERE u.IDUsuario = @userId";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", (int)Session["UserID"]);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Verificar si hay cambios en los campos de texto y en los archivos
                            modificado = txt_Nombre1.Text != reader["Primer_Nombre"].ToString() ||
                                         txt_Nombre2.Text != reader["Segundo_Nombre"].ToString() ||
                                         txt_Apellido1.Text != reader["Primer_Apellido"].ToString() ||
                                         txt_Apellido2.Text != reader["Segundo_Apellido"].ToString() ||
                                         txt_DNI.Text != reader["DNI"].ToString() ||
                                         txt_Correo.Text != reader["Correo"].ToString() ||
                                         txt_Fecha.Text != Convert.ToDateTime(reader["Fecha_Nacimiento"]).ToString("yyyy-MM-dd") ||
                                         txt_Telefono.Text != reader["Telefono"].ToString() ||
                                         txt_Universidad.Text != reader["Grado_academico"].ToString() ||
                                         txt_Direccion.Text != reader["Direccion"].ToString() ||
                                         (txt_Hombre.Checked && reader["Sexo"].ToString() != "Hombre") ||
                                         (txt_Mujer.Checked && reader["Sexo"].ToString() != "Mujer") ||
                                         txt_Foto.HasFile || // Verifica si se seleccionó una nueva foto
                                         txt_Curriculum.HasFile; // Verifica si se seleccionó un nuevo curriculum
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al verificar cambios: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return modificado;
        }


        private void ActualizarDatosUsuario(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                string updateUsuarios = @"
                    UPDATE usuarios SET 
                        Primer_Nombre = @Nombre1, Segundo_Nombre = @Nombre2, 
                        Primer_Apellido = @Apellido1, Segundo_Apellido = @Apellido2, 
                        DNI = @DNI, Correo = @Correo 
                    WHERE IDUsuario = @userId";

                string updateDatosUsuario = @"
                    UPDATE datos_usuario SET 
                        Fecha_Nacimiento = @FechaNacimiento, Telefono = @Telefono, 
                        Direccion = @Direccion, Grado_academico = @Grado, 
                        Sexo = @Sexo 
                    WHERE IDUsuario = @userId";

                using (MySqlCommand cmd1 = new MySqlCommand(updateUsuarios, conectar.conectar))
                {
                    cmd1.Parameters.AddWithValue("@Nombre1", txt_Nombre1.Text);
                    cmd1.Parameters.AddWithValue("@Nombre2", txt_Nombre2.Text);
                    cmd1.Parameters.AddWithValue("@Apellido1", txt_Apellido1.Text);
                    cmd1.Parameters.AddWithValue("@Apellido2", txt_Apellido2.Text);
                    cmd1.Parameters.AddWithValue("@DNI", txt_DNI.Text);
                    cmd1.Parameters.AddWithValue("@Correo", txt_Correo.Text);
                    cmd1.Parameters.AddWithValue("@userId", userId);
                    cmd1.ExecuteNonQuery();
                }

                using (MySqlCommand cmd2 = new MySqlCommand(updateDatosUsuario, conectar.conectar))
                {
                    cmd2.Parameters.AddWithValue("@FechaNacimiento", DateTime.Parse(txt_Fecha.Text));
                    cmd2.Parameters.AddWithValue("@Telefono", txt_Telefono.Text);
                    cmd2.Parameters.AddWithValue("@Direccion", txt_Direccion.Text);
                    cmd2.Parameters.AddWithValue("@Grado", txt_Universidad.Text);
                    cmd2.Parameters.AddWithValue("@Sexo", txt_Hombre.Checked ? "Hombre" : "Mujer");
                    cmd2.Parameters.AddWithValue("@userId", userId);
                    cmd2.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al actualizar datos del usuario: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (DatosModificados())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ConfirmarRegreso",
                    "if(confirm('Hay cambios sin guardar. ¿Desea regresar sin guardar los cambios?')) " +
                    "{ window.location.href='Default.aspx'; }", true);
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}



