using Modelo;
using System;
using System.Web.UI;
using static Pasantias.Aplicante_2;

namespace Pasantias
{
    public partial class Aplicante_1 : System.Web.UI.Page
    {
        Aplicante1 aplicante1;

        protected void Page_Load(object sender, EventArgs e)
        {
            // No se requiere lógica en Page_Load por ahora
        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            aplicante1 = new Aplicante1();

            lbl_Error.Text = "";
            lbl_Error.Visible = false;

            // Restablecer clases de error
            RestablecerClasesError();

            // Validaciones
            bool hayError = false;

            // Validar campo obligatorio
            if (!Utilidades.ValidarCampoObligatorio(txt_Nombre1.Text))
            {
                txt_Nombre1.CssClass += " error"; // Agregar clase de error
                hayError = true;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_Apellido1.Text))
            {
                txt_Apellido1.CssClass += " error";
                hayError = true;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_DNI.Text))
            {
                txt_DNI.CssClass += " error";
                hayError = true;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_Correo.Text))
            {
                txt_Correo.CssClass += " error";
                hayError = true;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_Apellido2.Text))
            {
                txt_Apellido2.CssClass += " error";
                hayError = true;
            }

            // Mensaje de error si hay algún campo obligatorio vacío
            if (hayError)
            {
                lbl_Error.Text = "Todos los campos son obligatorios.";
                lbl_Error.Visible = true;
                return;
            }

            // Validaciones adicionales
            if (!Utilidades.ValidarNombreApellido(txt_Nombre1.Text))
            {
                txt_Nombre1.CssClass += " error"; // Agregar clase de error
                lbl_Error.Text = "Los nombres no pueden contener caracteres especiales ni letras consecutivas.";
                lbl_Error.Visible = true;
                return;
            }

            if (!Utilidades.ValidarNombreApellido(txt_Apellido1.Text))
            {
                txt_Apellido1.CssClass += " error"; // Agregar clase de error
                lbl_Error.Text = "Los apellidos no pueden contener caracteres especiales ni letras consecutivas.";
                lbl_Error.Visible = true;
                return;
            }

          

            if (!Utilidades.ValidarNombreApellido(txt_Apellido2.Text))
            {
                txt_Apellido2.CssClass += " error"; // Agregar clase de error
                lbl_Error.Text = "El segundo apellido no puede contener caracteres especiales ni letras consecutivas.";
                lbl_Error.Visible = true;
                return;
            }

            if (!Utilidades.ValidarCorreo(txt_Correo.Text))
            {
                txt_Correo.CssClass += " error"; // Agregar clase de error
                lbl_Error.Text = "Correo no válido.";
                lbl_Error.Visible = true;
                return;
            }

            if (!Utilidades.ValidarDNI(txt_DNI.Text))
            {
                txt_DNI.CssClass += " error"; // Agregar clase de error
                lbl_Error.Text = "El DNI debe contener exactamente 13 dígitos.";
                lbl_Error.Visible = true;
                return;
            }

            // Intentar agregar el aplicante
            int resultado = aplicante1.agregar( txt_Nombre1.Text, txt_Nombre2.Text,
                                               txt_Apellido1.Text, txt_Apellido2.Text,
                                               txt_DNI.Text, txt_Correo.Text);

            if (resultado == -1)
            {
                lbl_Error.Text = "El DNI ya existe.";
                lbl_Error.Visible = true;
                return;
            }

            // Encriptar el DNI antes de redirigir
            string dni = txt_DNI.Text;
            string dniEncriptado = EncriptacionAES.Encriptar(dni);

            // Redirigir al segundo formulario usando el DNI encriptado
           
            Response.Redirect("Aplicante_2.aspx?DNI=" + Server.UrlEncode(dniEncriptado));
        }

        private void RestablecerClasesError()
        {
            // Eliminar la clase de error de los campos
            txt_Nombre1.CssClass = txt_Nombre1.CssClass.Replace(" error", "");
            txt_Nombre2.CssClass = txt_Nombre2.CssClass.Replace(" error", "");
            txt_Apellido1.CssClass = txt_Apellido1.CssClass.Replace(" error", "");
            txt_Apellido2.CssClass = txt_Apellido2.CssClass.Replace(" error", "");
            txt_DNI.CssClass = txt_DNI.CssClass.Replace(" error", "");
            txt_Correo.CssClass = txt_Correo.CssClass.Replace(" error", "");
        }
    }
}
