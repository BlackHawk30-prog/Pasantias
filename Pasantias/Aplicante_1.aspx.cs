using Modelo;
using System;
using System.Web.UI;

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

            // Reiniciar el mensaje de error
            lbl_Error.Text = "";
            lbl_Error.Visible = false;

            // Restablecer clases de error antes de la validación
            txt_Nombre1.CssClass = txt_Nombre1.CssClass.Replace(" error", "");
            txt_Nombre2.CssClass = txt_Nombre2.CssClass.Replace(" error", "");
            txt_Apellido1.CssClass = txt_Apellido1.CssClass.Replace(" error", "");
            txt_Apellido2.CssClass = txt_Apellido2.CssClass.Replace(" error", "");
            txt_DNI.CssClass = txt_DNI.CssClass.Replace(" error", "");
            txt_Correo.CssClass = txt_Correo.CssClass.Replace(" error", "");

            // Validar que ningún campo esté vacío
            if (string.IsNullOrWhiteSpace(txt_Nombre1.Text) ||
                string.IsNullOrWhiteSpace(txt_Apellido1.Text) ||
                string.IsNullOrWhiteSpace(txt_DNI.Text) ||
                string.IsNullOrWhiteSpace(txt_Correo.Text))
            {
                lbl_Error.Text = "Todos los campos son obligatorios.";
                lbl_Error.Visible = true;
                if (string.IsNullOrWhiteSpace(txt_Nombre1.Text)) txt_Nombre1.CssClass += " error";
                if (string.IsNullOrWhiteSpace(txt_Nombre2.Text)) txt_Nombre2.CssClass += " error";
                if (string.IsNullOrWhiteSpace(txt_Apellido1.Text)) txt_Apellido1.CssClass += " error";
                if (string.IsNullOrWhiteSpace(txt_Apellido2.Text)) txt_Apellido2.CssClass += " error";
                if (string.IsNullOrWhiteSpace(txt_DNI.Text)) txt_DNI.CssClass += " error";
                if (string.IsNullOrWhiteSpace(txt_Correo.Text)) txt_Correo.CssClass += " error";
                return;
            }

            // Validar que los campos de nombre y apellido no contengan caracteres especiales ni tres letras consecutivas
            if (!Utilidades.ValidarNombreApellido(txt_Nombre1.Text))
            {
                lbl_Error.Text = "El primer nombre no puede contener caracteres especiales ni tres letras consecutivas.";
                lbl_Error.Visible = true;
                txt_Nombre1.CssClass += " error";
                return;
            }

            if (!Utilidades.ValidarNombreApellido(txt_Apellido1.Text))
            {
                lbl_Error.Text = "El primer apellido no puede contener caracteres especiales ni tres letras consecutivas.";
                lbl_Error.Visible = true;
                txt_Apellido1.CssClass += " error";
                return;
            }

            // Validar que el correo tenga un formato válido
            if (!Utilidades.ValidarCorreo(txt_Correo.Text))
            {
                lbl_Error.Text = "El correo electrónico no es válido.";
                lbl_Error.Visible = true;
                txt_Correo.CssClass += " error";
                return;
            }

            // Validar que el DNI contenga exactamente 13 dígitos
            if (!Utilidades.ValidarDNI(txt_DNI.Text))
            {
                lbl_Error.Text = "El DNI debe contener exactamente 13 dígitos numéricos.";
                lbl_Error.Visible = true;
                txt_DNI.CssClass += " error";
                return;
            }

            // Intentar agregar el aplicante
            int resultado = aplicante1.agregar(0, txt_Nombre1.Text, txt_Nombre2.Text, txt_Apellido1.Text, txt_Apellido2.Text, txt_DNI.Text, txt_Correo.Text);

            if (resultado == -1)
            {
                lbl_Error.Text = "El DNI ya existe.";
                lbl_Error.Visible = true;
                txt_DNI.CssClass += " error";
                return;
            }

            if (resultado > 0)
            {
                int nuevoIDUsuario = aplicante1.ObtenerUltimoIDUsuario();
                Response.Redirect("Aplicante_2.aspx?idUsuario=" + nuevoIDUsuario);
            }
        }
    }
}
