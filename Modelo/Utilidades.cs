using System;
using System.Text.RegularExpressions;

namespace Modelo
{
    public static class Utilidades
    {
        // Método para validar que el campo no esté en blanco
        public static bool ValidarCampoObligatorio(string valor)
        {
            return !string.IsNullOrWhiteSpace(valor);
        }

        // Validar teléfono: Solo números y exactamente 8 dígitos
        public static bool ValidarTelefono(string telefono)
        {
            return telefono.Length == 8 && Regex.IsMatch(telefono, @"^\d+$");
        }

        // Validar edad mínima de 17 años a partir de la fecha de nacimiento
        public static bool ValidarEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (fechaNacimiento > DateTime.Now.AddYears(-edad)) edad--;
            return edad >= 17;
        }

        // Validar Grado Académico y Dirección: no deben contener caracteres especiales, ni más de 2 comas o puntos consecutivos, ni 3 letras iguales consecutivas
        public static bool ValidarTexto(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s,.]+$") &&  // Solo letras, espacios, comas y puntos
                   !Regex.IsMatch(valor, @"([,.])\1{1,}") &&    // No más de 2 comas o puntos seguidos
                   !Regex.IsMatch(valor, @"([a-zA-Z])\1{2,}");  // No más de 2 letras iguales seguidas
        }

        // Validar tipo de archivo de la foto (solo jpg, png)
        public static bool ValidarTipoArchivoFoto(string nombreArchivo)
        {
            string extension = System.IO.Path.GetExtension(nombreArchivo).ToLower();
            return extension == ".jpg" || extension == ".png";
        }

        // Validar tipo de archivo del currículum (solo .doc, .docx, .pdf)
        public static bool ValidarTipoArchivoCurriculum(string nombreArchivo)
        {
            string extension = System.IO.Path.GetExtension(nombreArchivo).ToLower();
            return extension == ".doc" || extension == ".docx" || extension == ".pdf";
        }
        // Método para validar nombres y apellidos
        public static bool ValidarNombreApellido(string valor)
        {
            // Verificar que no contenga caracteres especiales y números
            if (Regex.IsMatch(valor, @"^[a-zA-Z]+$"))
            {
                // Verificar que no tenga 3 letras seguidas
                return !Regex.IsMatch(valor, @"([a-zA-Z])\1{2,}");
            }
            return false;
        }

        // Método para validar el formato del correo electrónico
        public static bool ValidarCorreo(string correo)
        {
            // Usar una expresión regular para validar el formato del correo
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Método para validar el DNI
        public static bool ValidarDNI(string dni)
        {
            // Verificar que solo contenga números y que tenga exactamente 13 caracteres
            return dni.Length == 13 && Regex.IsMatch(dni, @"^\d+$");
        }
    }
}
