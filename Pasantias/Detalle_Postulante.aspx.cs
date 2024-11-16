using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Web.UI;

namespace Pasantias
{
    public partial class Detalle_Postulante : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica si hay un IDUsuario en la cadena de consulta
                if (Request.QueryString["IDUsuario"] != null)
                {
                    int idUsuario;
                    if (int.TryParse(Request.QueryString["IDUsuario"], out idUsuario))
                    {
                        System.Diagnostics.Debug.WriteLine($"IDUsuario recibido de QueryString: {idUsuario}");
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
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("El IDUsuario recibido no es válido.");
                        Response.Redirect("Postulaciones.aspx");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No se recibió un IDUsuario en la cadena de consulta.");
                    Response.Redirect("Postulaciones.aspx");
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
                System.Diagnostics.Debug.WriteLine("Error al cargar los datos del postulante: " + ex.Message);
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
                System.Diagnostics.Debug.WriteLine("Error al obtener la foto del postulante: " + ex.Message);
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
                System.Diagnostics.Debug.WriteLine("Error al obtener el curriculum del postulante: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return rutaCurriculum;
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {


            Response.Redirect("Postulaciones.aspx");

        }
    }
}
