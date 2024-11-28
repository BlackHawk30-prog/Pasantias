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

                    // Cargar los datos del usuario
                    CargarDatosUsuario(idUsuario);

                    // Cargar foto
                    string imagePath = ObtenerRutaFotoDesdeBD(idUsuario);
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        imgFoto.ImageUrl = ResolveUrl("~/" + imagePath);
                    }

                    // Cargar curriculum
                    string curriculumPath = ObtenerRutaCurriculumDesdeBD(idUsuario);
                    if (!string.IsNullOrEmpty(curriculumPath))
                    {
                        lnkCurriculum.NavigateUrl = ResolveUrl("~/" + curriculumPath);
                        lnkCurriculum.Visible = true;
                    }

                    // Cargar departamentos
                    CargarDepartamentos();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No se encontró el IDUsuario en la sesión.");
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void CargarDepartamentos()
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                string query = "SELECT CodigoDep, Departamento FROM departamentos";
                using (MySqlCommand cmd = new MySqlCommand(query, conectar.conectar))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        ddl_Departamento.DataSource = reader;
                        ddl_Departamento.DataTextField = "Departamento";
                        ddl_Departamento.DataValueField = "CodigoDep";
                        ddl_Departamento.DataBind();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar departamentos: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            ddl_Departamento.Items.Insert(0, new ListItem("--Seleccione un Departamento--", "0"));
        }

        private void CargarMunicipios(string codigoDepartamento)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                string query = "SELECT CodigoMun, Municipio FROM municipios WHERE CodigoDep = @CodigoDep";
                using (MySqlCommand cmd = new MySqlCommand(query, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@CodigoDep", codigoDepartamento);
                    using (var reader = cmd.ExecuteReader())
                    {
                        ddl_Municipio.DataSource = reader;
                        ddl_Municipio.DataTextField = "Municipio";
                        ddl_Municipio.DataValueField = "CodigoMun";
                        ddl_Municipio.DataBind();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar municipios: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            ddl_Municipio.Items.Insert(0, new ListItem("--Seleccione un Municipio--", "0"));
        }

        private void CargarDatosUsuario(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                string consulta = @"
                SELECT 
                    u.Primer_Nombre, 
                    u.Segundo_Nombre, 
                    u.Primer_Apellido, 
                    u.Segundo_Apellido, 
                    u.DNI, 
                    u.Correo, 
                    du.Fecha_Nacimiento, 
                    du.Telefono, 
                    du.Direccion, 
                    du.Grado_academico, 
                    du.Sexo,
                    m.CodigoMun, 
                    d.CodigoDep
                FROM usuarios u
                JOIN datos_usuario du ON u.IDUsuario = du.IDUsuario
                JOIN residencia r ON u.IDUsuario = r.IDUsuario
                JOIN municipios m ON r.CodigoMun = m.CodigoMun
                JOIN departamentos d ON m.CodigoDep = d.CodigoDep
                WHERE u.IDUsuario = @userId";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignación de datos al formulario
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

                            // Selección del departamento y municipio
                            ddl_Departamento.SelectedValue = reader["CodigoDep"].ToString(); // Código del departamento
                            CargarMunicipios(reader["CodigoDep"].ToString()); // Cargar municipios del departamento seleccionado
                            ddl_Municipio.SelectedValue = reader["CodigoMun"].ToString(); // Código del municipio

                            // Selección de género
                            string sexo = reader["Sexo"].ToString();
                            txt_Hombre.Checked = sexo == "Hombre";
                            txt_Mujer.Checked = sexo == "Mujer";
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


        protected void ddl_Departamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codigoDepartamento = ddl_Departamento.SelectedValue;
            if (!string.IsNullOrEmpty(codigoDepartamento) && codigoDepartamento != "0")
            {
                CargarMunicipios(codigoDepartamento);
            }
            else
            {
                ddl_Municipio.Items.Clear();
                ddl_Municipio.Items.Insert(0, new ListItem("--Seleccione un Municipio--", "0"));
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
            // Validar formulario antes de guardar
            if (!ValidarFormulario())
            {
                lbl_Error.Visible = true;
                return;
            }

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
                        imgFoto.ImageUrl = "~/Fotos/" + dni + "_foto" + Path.GetExtension(txt_Foto.FileName) + "?t=" + DateTime.Now.Ticks;
                    }

                    // Guardar el currículum si se ha seleccionado un archivo
                    if (txt_Curriculum.HasFile)
                    {
                        GuardarArchivo(txt_Curriculum, "Curriculum", dni + "_curriculum");
                        lnkCurriculum.NavigateUrl = "~/Curriculum/" + dni + "_curriculum" + Path.GetExtension(txt_Curriculum.FileName) + "?t=" + DateTime.Now.Ticks;
                        lnkCurriculum.Visible = true;
                    }

                    ActualizarDatosUsuario(idUsuario);

                    // Mostrar mensaje de éxito
                    ScriptManager.RegisterStartupScript(this, GetType(), "ChangesAlert",
                        "alert('Actualizado Exitosamente.'); window.location.href='Default.aspx';", true);
                }
                catch (Exception ex)
                {
                    // Mostrar mensaje de error
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert",
                        $"alert('Error al actualizar: {ex.Message}');", true);
                }
            }
            else
            {
                // Mensaje si no hay cambios
                ScriptManager.RegisterStartupScript(this, GetType(), "NoChangesAlert",
                    "alert('No se realizaron cambios.'); window.location.href='Default.aspx';", true);
            }
        }

        private bool ValidarFormulario()
        {
            lbl_Error.Text = string.Empty;
            bool esValido = true;

            // Resetear estilos previos
            txt_Nombre1.CssClass = txt_Nombre1.CssClass.Replace("error", "").Trim();
            txt_Nombre2.CssClass = txt_Nombre2.CssClass.Replace("error", "").Trim();
            txt_Apellido1.CssClass = txt_Apellido1.CssClass.Replace("error", "").Trim();
            txt_Apellido2.CssClass = txt_Apellido2.CssClass.Replace("error", "").Trim();
            txt_Correo.CssClass = txt_Correo.CssClass.Replace("error", "").Trim();
           
            txt_Fecha.CssClass = txt_Fecha.CssClass.Replace("error", "").Trim();

            // Validación de nombres y apellidos
            if (!Utilidades.ValidarCampoObligatorio(txt_Nombre1.Text) || !Utilidades.ValidarNombreApellido(txt_Nombre1.Text))
            {
                lbl_Error.Text += "Los nombres no pueden contener caracteres especiales ni letras consecutivas.<br/>";
                txt_Nombre1.CssClass += " error";
                esValido = false;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_Nombre2.Text) || !Utilidades.ValidarNombreApellido(txt_Nombre2.Text))
            {
                lbl_Error.Text += "Los nombres no pueden contener caracteres especiales ni letras consecutivas.<br/>";
                txt_Nombre2.CssClass += " error";
                esValido = false;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_Apellido1.Text) || !Utilidades.ValidarNombreApellido(txt_Apellido1.Text))
            {
                lbl_Error.Text += "Los apellidos no pueden contener caracteres especiales ni letras consecutivas.<br/>";
                txt_Apellido1.CssClass += " error";
                esValido = false;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_Apellido2.Text) || !Utilidades.ValidarNombreApellido(txt_Apellido2.Text))
            {
                lbl_Error.Text += "Los apellidos no pueden contener caracteres especiales ni letras consecutivas.<br/>";
                txt_Apellido2.CssClass += " error";
                esValido = false;
            }

            

            // Validación del correo
            if (!Utilidades.ValidarCorreo(txt_Correo.Text))
            {
                lbl_Error.Text += "El correo electrónico no es válido.<br/>";
                txt_Correo.CssClass += " error";
                esValido = false;
            }

            // Validación de fecha de nacimiento
            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txt_Fecha.Text, out fechaNacimiento) || !Utilidades.ValidarEdad(fechaNacimiento))
            {
                lbl_Error.Text += "Ingrese una fecha de nacimiento válida (debe ser mayor de 16 años).<br/>";
                txt_Fecha.CssClass += " error";
                esValido = false;
            }
            // Validación del municipio
            if (ddl_Municipio.SelectedValue == "0" || string.IsNullOrEmpty(ddl_Municipio.SelectedValue))  // Asumiendo que "0" es el valor por defecto o inválido
            {
                lbl_Error.Text += "Debe seleccionar un municipio.<br/>";
                ddl_Municipio.CssClass += " error";  // Agregar clase de error al ddl
                esValido = false;
            }
            return esValido;
        }


        private void GuardarArchivo(FileUpload fileUploadControl, string carpeta, string nombreArchivo)
        {
            string extension = Path.GetExtension(fileUploadControl.FileName).ToLower();
            if ((carpeta == "Fotos" && (extension == ".jpg" || extension == ".png")) ||
                (carpeta == "Curriculum" && (extension == ".doc" || extension == ".pdf" || extension == ".docx")))
            {
                string directorio = Path.Combine(Server.MapPath("~/" + carpeta));
                string rutaCompleta = Path.Combine(directorio, nombreArchivo + extension);

                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                fileUploadControl.SaveAs(rutaCompleta);
            }
            else
            {
                throw new InvalidOperationException("Formato de archivo no permitido.");
            }
        }
        private void ActualizarDatosUsuario(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                string updateUsuarios = @"
            UPDATE usuarios 
            SET 
                Primer_Nombre = @Nombre1, 
                Segundo_Nombre = @Nombre2, 
                Primer_Apellido = @Apellido1, 
                Segundo_Apellido = @Apellido2, 
                DNI = @DNI, 
                Correo = @Correo 
            WHERE IDUsuario = @userId";

                string updateDatosUsuario = @"
            UPDATE datos_usuario 
            SET 
                Fecha_Nacimiento = @FechaNacimiento, 
                Telefono = @Telefono, 
                Direccion = @Direccion, 
                Grado_academico = @Grado, 
                Sexo = @Sexo 
            WHERE IDUsuario = @userId";

                string updateResidencia = @"
            UPDATE residencia 
            SET CodigoMun = @CodigoMun 
            WHERE IDUsuario = @userId";

                // Actualizar datos en la tabla `usuarios`
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

                // Actualizar datos en la tabla `datos_usuario`
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

                // Actualizar datos en la tabla `residencia`
                using (MySqlCommand cmd3 = new MySqlCommand(updateResidencia, conectar.conectar))
                {
                    cmd3.Parameters.AddWithValue("@CodigoMun", ddl_Municipio.SelectedValue); // Código del municipio seleccionado
                    cmd3.Parameters.AddWithValue("@userId", userId);
                    cmd3.ExecuteNonQuery();
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
    du.Grado_academico, du.Sexo, r.CodigoMun 
FROM usuarios u 
JOIN datos_usuario du ON u.IDUsuario = du.IDUsuario 
JOIN residencia r ON u.IDUsuario = r.IDUsuario 
WHERE u.IDUsuario = @userId";

                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", (int)Session["UserID"]);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Verificar si hay cambios en los campos de texto
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
                                         ddl_Municipio.SelectedValue != reader["CodigoMun"].ToString() || // Verifica cambios en municipio
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



    }
}
