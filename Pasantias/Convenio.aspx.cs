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
                // Calcular fechas
                DateTime fechaInicio = DateTime.Now; // Fecha actual
                DateTime fechaFinal = fechaInicio.AddMonths(11); // Fecha 11 meses después

                string query = @"
            INSERT INTO convenio (Fecha_Inicio, Fecha_Final, Aceptado, Rechazado, IDUsuario) 
            VALUES (@FechaInicio, @FechaFinal, @Aceptado, @Rechazado, @IDUsuario)";

                using (MySqlCommand cmd = new MySqlCommand(query, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Aceptado", aceptado);
                    cmd.Parameters.AddWithValue("@Rechazado", rechazado);
                    cmd.Parameters.AddWithValue("@IDUsuario", 94); // Sustituir por Session["UserID"] en producción

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        string mensaje = aceptado == 1 ? "Convenio aceptado exitosamente." : "Convenio rechazado exitosamente.";
                        Response.Write($"<script>alert('{mensaje}');</script>");
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
