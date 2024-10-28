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

        // Método para validar nombres y apellidos sin caracteres especiales ni 3 letras iguales consecutivas
        public static bool ValidarNombreApellido(string valor)
        {
            // Verificar que no contenga caracteres especiales ni números
            if (Regex.IsMatch(valor, @"^[a-zA-Z]+$"))
            {
                // Verificar que no tenga 3 letras iguales seguidas
                return !Regex.IsMatch(valor, @"([a-zA-Z])\1{2,}");
            }
            return false;
        }

        // Método para validar el formato del correo electrónico
        public static bool ValidarCorreo(string correo)
        {
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Método para validar el DNI: solo números y exactamente 13 caracteres
        public static bool ValidarDNI(string dni)
        {
            return dni.Length == 13 && Regex.IsMatch(dni, @"^\d+$");
        }

        // Método para validar que el teléfono solo contenga números y tenga exactamente 8 dígitos
        public static bool ValidarTelefono(string telefono)
        {
            return telefono.Length == 8 && Regex.IsMatch(telefono, @"^\d+$");
        }

        // Método para validar edad mínima de 17 años a partir de la fecha de nacimiento
        public static bool ValidarEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (fechaNacimiento > DateTime.Now.AddYears(-edad)) edad--;
            return edad >= 17;
        }

        // Método para validar texto en campos como dirección o grado académico (sin caracteres especiales)
        public static bool ValidarTexto(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s,.]+$") &&      // Solo letras, espacios, comas y puntos
                   !Regex.IsMatch(valor, @"([,.])\1{1,}") &&        // No más de 2 comas o puntos seguidos
                   !Regex.IsMatch(valor, @"([a-zA-Z])\1{2,}");      // No más de 2 letras iguales seguidas
        }

        // Método para validar tipo de archivo de la foto (solo jpg, png)
        public static bool ValidarTipoArchivoFoto(string nombreArchivo)
        {
            string extension = System.IO.Path.GetExtension(nombreArchivo).ToLower();
            return extension == ".jpg" || extension == ".png";
        }

        // Método para validar tipo de archivo del currículum (solo .doc, .docx, .pdf)
        public static bool ValidarTipoArchivoCurriculum(string nombreArchivo)
        {
            string extension = System.IO.Path.GetExtension(nombreArchivo).ToLower();
            return extension == ".doc" || extension == ".docx" || extension == ".pdf" || extension == ".dox" || extension == ".360";
        }

    }
}
