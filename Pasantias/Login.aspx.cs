using System;
using Modelo;
using System.Web.UI;

namespace Pasantias
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Método para manejar el evento click del botón
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos de texto
            string usuario = txt_Usuario.Text;
            string password = txt_Password.Text;

            // Instanciar la clase Login (del namespace Modelo)
            Modelo.Login login = new Modelo.Login();

            // Variable para almacenar el rol
            int rol;

            // Verificar las credenciales y obtener el rol
            bool credencialesValidas = login.VerificarCredenciales(usuario, password, out rol);

            // Redirigir o mostrar mensaje según el resultado de la verificación
            if (credencialesValidas)
            {
                // Almacenar el rol en la sesión para uso global
                Session["UserRol"] = rol;

                // Redirigir a la página principal si las credenciales son correctas
                Response.Redirect("Hoja_Tiempo.aspx");
            }
            else
            {
                // Mostrar un mensaje de error si las credenciales no son válidas
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Usuario o contraseña incorrectos');", true);
            }
        }
    }
}
