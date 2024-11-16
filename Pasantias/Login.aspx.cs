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

            // Variables para almacenar rol e ID de usuario
            int rol;
            int idUsuario;

            // Verificar las credenciales y obtener el rol
            bool credencialesValidas = login.VerificarCredenciales(usuario, password, out rol, out idUsuario);

            // Redirigir o mostrar mensaje según el resultado de la verificación
            if (credencialesValidas)
            {
                if (rol == -1)
                {
                    // Mostrar mensaje si el usuario no tiene un rol asignado
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Usuario sin rol asignado.');", true);
                }
                else
                {
                    // Obtener datos adicionales del usuario
                    var datosUsuario = login.ObtenerDatosUsuario(idUsuario);

                    // Almacenar datos en la sesión
                    Session["UserRol"] = rol;
                    Session["UserID"] = idUsuario;
                    Session["PrimerNombre"] = datosUsuario.PrimerNombre;
                    Session["PrimerApellido"] = datosUsuario.PrimerApellido;
                    SessionStore.UserID = idUsuario; // Asignar el ID de usuario en SessionStore

                    // Redirigir a la página principal
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                // Mostrar un mensaje de error si las credenciales no son válidas
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Usuario o contraseña incorrectos');", true);
            }
        }
    }
}
