using Modelo;
using System;
using System.Web.UI;

namespace Pasantias
{
    public partial class Aplicante_2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idUsuarioString = Request.QueryString["IDUsuario"];
                if (!string.IsNullOrEmpty(idUsuarioString))
                {
                    ViewState["IDUsuario"] = idUsuarioString;
                }
                else
                {
                    lbl_Error.Text = "No se proporcionó un ID de usuario válido.";
                }
            }
        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            bool hasError = false;
            lbl_Error.Text = "";  // Reset error message

            if (ViewState["IDUsuario"] == null)
            {
                lbl_Error.Text = "Error al obtener el ID de usuario.";
                return;
            }

            int idUsuario = Convert.ToInt32(ViewState["IDUsuario"]);

            // Validar campos obligatorios
            if (!Utilidades.ValidarCampoObligatorio(txt_Telefono.Text))
            {
                txt_Telefono.CssClass += " error";
                hasError = true;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_Direccion.Text))
            {
                txt_Direccion.CssClass += " error";
                hasError = true;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_Universidad.Text))
            {
                txt_Universidad.CssClass += " error";
                hasError = true;
            }
            if (!Utilidades.ValidarCampoObligatorio(txt_Fecha.Text))
            {
                txt_Fecha.CssClass += " error";
                hasError = true;
            }

            // Validar teléfono
            if (!Utilidades.ValidarTelefono(txt_Telefono.Text))
            {
                lbl_Error.Text += "El número de teléfono debe contener exactamente 8 dígitos y solo números.<br/>";
                txt_Telefono.CssClass += " error";
                hasError = true;
            }

            // Validar edad
            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txt_Fecha.Text, out fechaNacimiento) || !Utilidades.ValidarEdad(fechaNacimiento))
            {
                lbl_Error.Text += "Debes tener al menos 1 años.<br/>";
                txt_Fecha.CssClass += " error";
                hasError = true;
            }

            // Validar texto (Grado Académico y Dirección)
            if (!Utilidades.ValidarTexto(txt_Universidad.Text))
            {
                lbl_Error.Text += "El grado académico no puede contener caracteres especiales.<br/>";
                txt_Universidad.CssClass += " error";
                hasError = true;
            }
            if (!Utilidades.ValidarTexto(txt_Direccion.Text))
            {
                lbl_Error.Text += "La dirección no puede contener caracteres especiales.<br/>";
                txt_Direccion.CssClass += " error";
                hasError = true;
            }

            // Validar tipo de archivo de la foto
            if (txt_Foto.HasFile && !Utilidades.ValidarTipoArchivoFoto(txt_Foto.FileName))
            {
                lbl_Error.Text += "Solo se permiten archivos de imagen JPG o PNG para la foto.<br/>";
                txt_Foto.CssClass += " error";
                hasError = true;
            }

            // Validar tipo de archivo del currículum
            if (txt_Curriculum.HasFile && !Utilidades.ValidarTipoArchivoCurriculum(txt_Curriculum.FileName))
            {
                lbl_Error.Text += "Solo se permiten archivos .doc, .docx, .pdf, .dox, .360 para el currículum.<br/>";
                txt_Curriculum.CssClass += " error";
                hasError = true;
            }

            if (hasError)
            {
                lbl_Error.Text = "Por favor, corrija los errores antes de enviar el formulario.";
                return;
            }

            // Proceder con el envío de los datos si no hay errores
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Datos enviados exitosamente.');", true);
        }
    }
}
