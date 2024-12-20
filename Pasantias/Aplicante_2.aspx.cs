﻿using Modelo;
using System;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Aplicante_2 : System.Web.UI.Page
    {
        Aplicante2 aplicante2;
        ConexionBD conectar;

        public static class EncriptacionAES
        {
            private static readonly string key = "clave_secreta_123";  // Clave secreta

            private static byte[] ObtenerClave(string clave)
            {
                byte[] claveBytes = Encoding.UTF8.GetBytes(clave);
                Array.Resize(ref claveBytes, 16);  // Ajustar a 16 bytes
                return claveBytes;
            }

            public static string Encriptar(string texto)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = ObtenerClave(key);
                    aes.IV = new byte[16];  // Vector de inicialización

                    ICryptoTransform encriptador = aes.CreateEncryptor(aes.Key, aes.IV);
                    byte[] textoBytes = Encoding.UTF8.GetBytes(texto);

                    byte[] encriptado = encriptador.TransformFinalBlock(textoBytes, 0, textoBytes.Length);
                    return Convert.ToBase64String(encriptado);
                }
            }

            public static string Desencriptar(string textoEncriptado)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = ObtenerClave(key);
                    aes.IV = new byte[16];  // Vector de inicialización

                    ICryptoTransform desencriptador = aes.CreateDecryptor(aes.Key, aes.IV);
                    byte[] encriptadoBytes = Convert.FromBase64String(textoEncriptado);

                    byte[] desencriptado = desencriptador.TransformFinalBlock(encriptadoBytes, 0, encriptadoBytes.Length);
                    return Encoding.UTF8.GetString(desencriptado);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dniEncriptado = Request.QueryString["DNI"];
                if (string.IsNullOrEmpty(dniEncriptado))
                {
                    lbl_Error.Text = "DNI no válido.";
                    lbl_Error.Visible = true;
                    return;
                }

                try
                {
                    string dniDesencriptado = EncriptacionAES.Desencriptar(dniEncriptado);
                    ViewState["DNI"] = dniDesencriptado;
                }
                catch (CryptographicException ex)
                {
                    lbl_Error.Text = $"Error al desencriptar el DNI: {ex.Message}";
                    lbl_Error.Visible = true;
                }
                catch (FormatException ex)
                {
                    lbl_Error.Text = $"Error de formato: {ex.Message}";
                    lbl_Error.Visible = true;
                }

                CargarDepartamentos();
            }
        }


        protected void Enviar_Click(object sender, EventArgs e)
        {
            RestablecerClasesError();
            bool esValido = true;
            lbl_Error.Text = string.Empty;

            // Validación de fecha de nacimiento
            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txt_Fecha.Text, out fechaNacimiento) || !Utilidades.ValidarEdad(fechaNacimiento))
            {
                esValido = false;
                txt_Fecha.CssClass += " error";
                lbl_Error.Text += "Ingrese una fecha de nacimiento válida (debe ser mayor de 16 años).<br/>";
            }
            else
            {
                txt_Fecha.CssClass = txt_Fecha.CssClass.Replace("error", "");
            }

            // Validación de teléfono
            if (!Utilidades.ValidarTelefono(txt_Telefono.Text))
            {
                esValido = false;
                txt_Telefono.CssClass += " error";
                lbl_Error.Text += "Ingrese un número de teléfono válido de 8 dígitos.<br/>";
            }
            else
            {
                txt_Telefono.CssClass = txt_Telefono.CssClass.Replace("error", "");
            }

            // Validación de universidad
            if (!Utilidades.ValidarCampoObligatorio(txt_Universidad.Text) || !Utilidades.ValidarTexto(txt_Universidad.Text))
            {
                esValido = false;
                txt_Universidad.CssClass += " error";
                lbl_Error.Text += "Ingrese un nombre de universidad válido.<br/>";
            }
            else
            {
                txt_Universidad.CssClass = txt_Universidad.CssClass.Replace("error", "");
            }

            // Validación de dirección
            if (!Utilidades.ValidarCampoObligatorio(txt_Direccion.Text) || !Utilidades.ValidarTexto(txt_Direccion.Text))
            {
                esValido = false;
                txt_Direccion.CssClass += " error";
                lbl_Error.Text += "Ingrese una dirección válida.<br/>";
            }
            else
            {
                txt_Direccion.CssClass = txt_Direccion.CssClass.Replace("error", "");
            }

            // Validación de sexo
            string sexo = txt_Hombre.Checked ? "Hombre" : txt_Mujer.Checked ? "Mujer" : "";
            if (string.IsNullOrEmpty(sexo))
            {
                esValido = false;
                lbl_Error.Text += "Seleccione su sexo.<br/>";
            }

            // Validación de foto
            byte[] fotoBytes = null;
            if (txt_Foto.HasFile && Utilidades.ValidarTipoArchivoFoto(txt_Foto.FileName))
            {
                fotoBytes = txt_Foto.FileBytes;
                txt_Foto.CssClass = txt_Foto.CssClass.Replace("error", "");
            }
            else
            {
                esValido = false;
                txt_Foto.CssClass += " error";
                lbl_Error.Text += "La foto debe estar en formato JPG o PNG.<br/>";
            }

            // Validación de currículum
            byte[] curriculumBytes = null;
            if (txt_Curriculum.HasFile && Utilidades.ValidarTipoArchivoCurriculum(txt_Curriculum.FileName))
            {
                curriculumBytes = txt_Curriculum.FileBytes;
                txt_Curriculum.CssClass = txt_Curriculum.CssClass.Replace("error", "");
            }
            else
            {
                esValido = false;
                txt_Curriculum.CssClass += " error";
                lbl_Error.Text += "El currículum debe estar en formato DOC, DOCX o PDF.<br/>";
            }

            // Validación de municipio
            string codigoMunSeleccionado = ddl_Municipio.SelectedValue; 
            if (string.IsNullOrEmpty(codigoMunSeleccionado) || codigoMunSeleccionado == "0")
            {
                esValido = false;
                ddl_Municipio.CssClass += " error";
                lbl_Error.Text += "Seleccione un municipio y Departamento válido.<br/>";
            }
            else
            {
                ddl_Municipio.CssClass = ddl_Municipio.CssClass.Replace("error", "").Trim();
            }
            if ( !Utilidades.ValidarCuentaTipo(txt_cuenta.Text) || !Utilidades.ValidarCuenta(txt_cuenta.Text))
            {
                lbl_Error.Text += "Debe ingresar un numero de cuenta BAC valido.<br/>";
                txt_cuenta.CssClass += " error";
                esValido = false;
            }

            // Mostrar mensaje de error si alguna validación falla
            if (!esValido)
            {
                lbl_Error.Visible = true;
                return;
            }

            // Crear el registro del aplicante
            aplicante2 = new Aplicante2();
            string dni = ViewState["DNI"].ToString();

            int resultado = aplicante2.Crear(
                fechaNacimiento,
                txt_Telefono.Text,
                txt_cuenta.Text,
                txt_Direccion.Text,
                txt_Universidad.Text,
                sexo,
                fotoBytes,
                curriculumBytes,
                dni
            );

            if (resultado > 0)
            {
                // Asociar residencia con el municipio seleccionado
                int resultadoResidencia = aplicante2.InsertarResidencia(dni, codigoMunSeleccionado.ToString());
                if (resultadoResidencia > 0)
                {
                    string correo = aplicante2.ObtenerCorreoPorDNI(dni);
                    if (!string.IsNullOrEmpty(correo))
                    {
                        EnviarCorreoAgradecimiento(correo);
                    }

                    // Mostrar el popup y redirigir después de aceptar
                    string script = "alert('Datos enviados correctamente.'); window.location='Redireccion.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "PopupRedireccion", script, true);
                }
                else
                {
                    lbl_Error.Text = "No se pudo registrar la residencia.";
                    lbl_Error.Visible = true;
                }
            }
            else
            {
                lbl_Error.Text = "Hubo un problema al enviar los datos.";
                lbl_Error.Visible = true;
            }


        }





        private void EnviarCorreoAgradecimiento(string destinatario)
        {
            try
            {
                using (MailMessage mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress("hreyesfotos@gmail.com"); // Dirección de correo de origen
                    mensaje.To.Add(destinatario);
                    mensaje.Subject = "Gracias por aplicar a la pasantía";
                    mensaje.Body = "Estimado(a) postulante,\n\nGracias por aplicar a nuestra pasantía. Su solicitud ha sido recibida exitosamente. Nos pondremos en contacto con usted en breve.\n\nSaludos cordiales,\nEl equipo de Pasantías";
                    mensaje.IsBodyHtml = false;

                    using (SmtpClient clienteSmtp = new SmtpClient())
                    {
                        clienteSmtp.Host = "smtp.gmail.com"; // Configura el servidor SMTP
                        clienteSmtp.Port = 587; // Puerto del servidor SMTP
                        clienteSmtp.Credentials = new System.Net.NetworkCredential("hreyesfotos@gmail.com", "ovqx ypvm vtbt fttp"); // Configura tus credenciales
                        clienteSmtp.EnableSsl = true;

                        clienteSmtp.Send(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_Error.Text = $"Error al enviar el correo de agradecimiento: {ex.Message}";
                lbl_Error.Visible = true;
            }
        }
        protected void ddl_Departamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado del Dropdown de Departamentos
            int codigoDep = int.Parse(ddl_Departamento.SelectedValue);

            // Cargar los municipios basados en el departamento seleccionado
            CargarMunicipios(codigoDep);
        }


        private void CargarDepartamentos()
        {
            try
            {
                conectar = new ConexionBD();
                conectar.AbrirConexion();

                string consulta = "SELECT CodigoDep, Departamento FROM Departamentos";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddl_Departamento.DataSource = reader;
                        ddl_Departamento.DataTextField = "Departamento"; // Columna que se mostrará en el DropDownList
                        ddl_Departamento.DataValueField = "CodigoDep";    // Valor asociado al elemento
                        ddl_Departamento.DataBind();
                    }
                }
                ddl_Departamento.Items.Insert(0, new ListItem("-- Seleccione un Departamento --", "0"));
            }
            catch (Exception ex)
            {
                lbl_Error.Text = $"Error al cargar los departamentos: {ex.Message}";
                lbl_Error.Visible = true;
            }
            finally
            {
                conectar.CerrarConexion();
            }
        }
        private void CargarMunicipios(int codigoDep)
        {
            try
            {
                conectar = new ConexionBD();
                conectar.AbrirConexion();

                string consulta = "SELECT CodigoMun, Municipio FROM Municipios WHERE CodigoDep = @CodigoDep";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@CodigoDep", codigoDep);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddl_Municipio.DataSource = reader;
                        ddl_Municipio.DataTextField = "Municipio"; // Columna que se mostrará en el DropDownList
                        ddl_Municipio.DataValueField = "CodigoMun"; // Valor asociado al elemento
                        ddl_Municipio.DataBind();
                    }
                }
                ddl_Municipio.Items.Insert(0, new ListItem("-- Seleccione un Municipio --", "0"));
            }
            catch (Exception ex)
            {
                lbl_Error.Text = $"Error al cargar los municipios: {ex.Message}";
                lbl_Error.Visible = true;
            }
            finally
            {
                conectar.CerrarConexion();
            }
        }
        private void RestablecerClasesError()
        {
            txt_Fecha.CssClass = txt_Fecha.CssClass.Replace(" error", "").Trim();
            txt_Telefono.CssClass = txt_Telefono.CssClass.Replace(" error", "").Trim();
            txt_Universidad.CssClass = txt_Universidad.CssClass.Replace(" error", "").Trim();
            txt_Direccion.CssClass = txt_Direccion.CssClass.Replace(" error", "").Trim();
            ddl_Municipio.CssClass = ddl_Municipio.CssClass.Replace(" error", "").Trim();
            txt_Hombre.CssClass = txt_Hombre.CssClass.Replace(" error", "").Trim();
            txt_Mujer.CssClass = txt_Mujer.CssClass.Replace(" error", "").Trim();
            txt_Foto.CssClass = txt_Foto.CssClass.Replace(" error", "").Trim();
            txt_Curriculum.CssClass = txt_Curriculum.CssClass.Replace(" error", "").Trim();
        }

    }
}
