using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Hojas_Generales : System.Web.UI.Page
    {
        HojasGenerales Hojas;

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
                Hojas = new HojasGenerales();
                Hojas.grid_Generales(grid_Generales);
            }
        }

        protected void BtnNuevaHoja_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            Hojas = new HojasGenerales();

            // Llama al método crear para insertar una nueva hoja de tiempo
            int resultado = Hojas.crear(userID);

            if (resultado > 0)
            {
                // Después de crear la hoja de tiempo, consulta el IDHojaTiempo más reciente del usuario
                int idHojaTiempo;
                using (var conectar = new ConexionBD())
                {
                    conectar.AbrirConexion();
                    string consulta = "SELECT IDHojaTiempo FROM hoja_tiempo WHERE IDUsuario = @IDUsuario ORDER BY IDHojaTiempo DESC LIMIT 1;\r\n";
                    MySqlCommand obtenerId = new MySqlCommand(consulta, conectar.conectar);
                    obtenerId.Parameters.AddWithValue("@IDUsuario", userID);
                    idHojaTiempo = Convert.ToInt32(obtenerId.ExecuteScalar());
                    SessionStore.HojaID = idHojaTiempo;
                    conectar.CerrarConexion();
                }

                string idEncriptado = EncriptacionAES.Encriptar(idHojaTiempo.ToString());
                Response.Redirect($"Hoja_Tiempo.aspx?IDHojaTiempo={idEncriptado}");
              //  Response.Redirect($"Hoja_Tiempo.aspx?IDHojaTiempo={idHojaTiempo}", false);

            }
            else
            {
                Response.Write("<script>alert('Error al crear la hoja de tiempo.');</script>");
            }
        }



        protected void grid_Generales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                // Obtener el IDHojaTiempo del CommandArgument
                string idHojaTiempo = e.CommandArgument.ToString();

                // Redirigir a la página Hoja_Tiempo con el IDHojaTiempo como parámetro en la URL
                string idEncriptado = EncriptacionAES.Encriptar(idHojaTiempo.ToString());
                Response.Redirect($"Hoja_Tiempo.aspx?IDHojaTiempo={idEncriptado}");
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {


            Response.Redirect("Default.aspx");

        }
    }
}
