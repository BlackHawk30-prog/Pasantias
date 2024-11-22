using Modelo;
using MySql.Data.MySqlClient;
using System;

namespace Pasantias
{
    public partial class Convenio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validar si el IDUsuario viene en la URL
                if (!string.IsNullOrEmpty(Request.QueryString["IDUsuario"]))
                {
                    // Almacenar el IDUsuario en ViewState para usarlo más adelante
                    ViewState["IDUsuario"] = Convert.ToInt32(Request.QueryString["IDUsuario"]);
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




    }
}
