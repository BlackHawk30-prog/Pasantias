using System;
using System.Text.RegularExpressions;

namespace Modelo
{
    public static class Utilidades
    {
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
