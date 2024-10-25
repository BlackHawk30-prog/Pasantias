using Modelo;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Aplicante_2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el IDUsuario de la query string
                string idUsuarioString = Request.QueryString["IDUsuario"];
                if (!string.IsNullOrEmpty(idUsuarioString))
                {
                    ViewState["IDUsuario"] = idUsuarioString; // Almacena el IDUsuario en ViewState para usarlo más tarde
                }
                else
                {
                    lbl_Error.Text = "No se proporcionó un ID de usuario válido.";
                }
            }
        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            if (ViewState["IDUsuario"] == null)
            {
                lbl_Error.Text = "Error al obtener el ID de usuario.";
                return;
            }

            int idUsuario = Convert.ToInt32(ViewState["IDUsuario"]); // Recupera el IDUsuario desde ViewState

            // Validar campos obligatorios
            if (!Utilidades.ValidarCampoObligatorio(txt_Telefono.Text) ||
                !Utilidades.ValidarCampoObligatorio(txt_Direccion.Text) ||
                !Utilidades.ValidarCampoObligatorio(txt_Universidad.Text) ||
                !Utilidades.ValidarCampoObligatorio(txt_Fecha.Text))
            {
                lbl_Error.Text = "Todos los campos son obligatorios.";
                return;
            }

            // Validar teléfono
            if (!Utilidades.ValidarTelefono(txt_Telefono.Text))
            {
                lbl_Error.Text = "El número de teléfono debe contener exactamente 8 dígitos y solo números.";
                return;
            }

            // Validar edad
            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txt_Fecha.Text, out fechaNacimiento) || !Utilidades.ValidarEdad(fechaNacimiento))
            {
                lbl_Error.Text = "Debes tener al menos 17 años.";
                return;
            }

            // Validar texto (Grado Académico y Dirección)
            if (!Utilidades.ValidarTexto(txt_Universidad.Text) || !Utilidades.ValidarTexto(txt_Direccion.Text))
            {
                lbl_Error.Text = "El grado académico y la dirección no pueden contener caracteres especiales.";
                return;
            }

            // Validar tipo de archivo de la foto
            if (txt_Foto.HasFile && !Utilidades.ValidarTipoArchivoFoto(txt_Foto.FileName))
            {
                lbl_Error.Text = "Solo se permiten archivos de imagen JPG o PNG para la foto.";
                return;
            }

            // Validar tipo de archivo del currículum
            if (txt_Curriculum.HasFile && !Utilidades.ValidarTipoArchivoCurriculum(txt_Curriculum.FileName))
            {
                lbl_Error.Text = "Solo se permiten archivos .doc, .docx o PDF para el currículum.";
                return;
            }

            // Si todas las validaciones son correctas, convertir datos y proceder
            int telefono = int.Parse(txt_Telefono.Text);
            string direccion = txt_Direccion.Text;
            string gradoAcademico = txt_Universidad.Text;
            string sexo = txt_Hombre.Checked ? "Hombre" : "Mujer";

            byte[] fotoBytes = null;
            byte[] curriculumBytes = null;

            if (txt_Foto.HasFile)
            {
                fotoBytes = txt_Foto.FileBytes; // Obtén los bytes del archivo de foto
            }

            if (txt_Curriculum.HasFile)
            {
                curriculumBytes = txt_Curriculum.FileBytes; // Obtén los bytes del archivo de currículum
            }

            // Crear el objeto Aplicante2 y guardar los datos en la base de datos
            Aplicante2 aplicante = new Aplicante2();
            aplicante.crear(telefono, direccion, gradoAcademico, sexo, fotoBytes, curriculumBytes, idUsuario);

            // Limpiar campos o redirigir al usuario después del registro exitoso
            lbl_Error.Text = "Datos enviados exitosamente.";
        }
    }
}
