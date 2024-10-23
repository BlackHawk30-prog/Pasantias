using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Aplicante_2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Enviar_Click(object sender, EventArgs e)
        {
            int idUsuario = 1; // Asumiendo que tienes el ID del usuario de alguna manera
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

            // Ajuste al método 'crear': eliminamos el parámetro '0' correspondiente a 'IDDatos_Usuario'
            Aplicante2 aplicante = new Aplicante2();
            aplicante.crear(telefono, direccion, gradoAcademico, sexo, fotoBytes, curriculumBytes, idUsuario);
        }


    }
}