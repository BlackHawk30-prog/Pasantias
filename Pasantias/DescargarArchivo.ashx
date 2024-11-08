<%@ WebHandler Language="C#" Class="DescargarArchivo" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

public class DescargarArchivo : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        // Cambiar por el ID o parámetro que estás recibiendo, por ejemplo, ?idArchivo=123
        int idArchivo = int.Parse(context.Request.QueryString["idArchivo"]);

        // Configura la conexión a la base de datos
        string connectionString = ConfigurationManager.ConnectionStrings["TuCadenaDeConexion"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT NombreArchivo, TipoArchivo, DatosArchivo FROM tuTabla WHERE ID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", idArchivo);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string nombreArchivo = reader["NombreArchivo"].ToString();
                string tipoArchivo = reader["TipoArchivo"].ToString();
                byte[] datosArchivo = (byte[])reader["DatosArchivo"];

                // Configurar el tipo de contenido y nombre del archivo para la descarga
                context.Response.ContentType = tipoArchivo;
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
                context.Response.BinaryWrite(datosArchivo);
                context.Response.End();
            }
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
