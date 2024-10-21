using System;
using System.Collections.Generic;
using System.Linq;
using Modelo;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Aplicante_1 : System.Web.UI.Page
    {
        Aplicante1 aplicante1;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            aplicante1 = new Aplicante1();

            // Validar que ningún campo esté vacío
            if (string.IsNullOrWhiteSpace(txt_Nombre1.Text) || string.IsNullOrWhiteSpace(txt_Apellido1.Text) ||
                string.IsNullOrWhiteSpace(txt_DNI.Text) || string.IsNullOrWhiteSpace(txt_Correo.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Todos los campos son obligatorios.');", true);
                return; // Salir del método si hay campos vacíos
            }

            // Validar que los campos de nombre y apellido no contengan caracteres especiales ni números
            if (!Utilidades.ValidarNombreApellido(txt_Nombre1.Text) ||
                !Utilidades.ValidarNombreApellido(txt_Apellido1.Text) ||
                !Utilidades.ValidarNombreApellido(txt_Apellido2.Text) ||
                !Utilidades.ValidarNombreApellido(txt_Nombre2.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Los nombres y apellidos no pueden contener caracteres especiales ni números.');", true);
                return;
            }

            // Validar que el correo tenga un formato válido
            if (!Utilidades.ValidarCorreo(txt_Correo.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El correo electrónico no es válido.');", true);
                return;
            }

            // Validar que el DNI contenga exactamente 13 dígitos
            if (!Utilidades.ValidarDNI(txt_DNI.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El DNI debe contener exactamente 13 dígitos.');", true);
                return;
            }


            // Intentar agregar el aplicante
            int resultado = aplicante1.agregar(0, txt_Nombre1.Text, txt_Nombre2.Text, txt_Apellido1.Text, txt_Apellido2.Text, txt_DNI.Text, txt_Correo.Text);

            // Verificar si el DNI ya existe
            if (resultado == -1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El DNI ya existe.');", true);
                return; // Salir si el DNI ya está registrado
            }

            // Si la inserción fue exitosa, redirigir a otra página
            if (resultado > 0)
            {
                Response.Redirect("Aplicante_2.aspx"); // Cambia "Aplicante2.aspx" al nombre real de la página a la que quieres redirigir
            }
        }

        // Método para validar nombres y apellidos
        private bool ValidarNombreApellido(string valor)
        {
            // Verificar que no contenga caracteres especiales y números
            if (System.Text.RegularExpressions.Regex.IsMatch(valor, @"^[a-zA-Z]+$"))
            {
                // Verificar que no tenga 3 letras seguidas
                return !System.Text.RegularExpressions.Regex.IsMatch(valor, @"([a-zA-Z])\1{2,}");
            }
            return false;
        }

        // Método para validar el formato del correo electrónico
        private bool ValidarCorreo(string correo)
        {
            // Usar una expresión regular para validar el formato del correo
            return System.Text.RegularExpressions.Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Método para validar el DNI
        private bool ValidarDNI(string dni)
        {
            // Verificar que solo contenga números y que tenga exactamente 13 caracteres
            return dni.Length == 13 && System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d+$");
        }
    }
}
