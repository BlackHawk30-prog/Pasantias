﻿using Modelo;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Detalle_Postulante : Page
    {
        Postulacion Postulacion;
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
                CargarDepartamentos();
                // Verifica si hay un IDUsuario en la cadena de consulta
                if (Request.QueryString["IDUsuario"] != null)
                {
                    try
                    {
                        // Desencripta el IDUsuario recibido
                        string idUsuarioEncriptado = Request.QueryString["IDUsuario"];
                        string idUsuarioDesencriptado = EncriptacionAES.Desencriptar(idUsuarioEncriptado);
                        int idUsuario = int.Parse(idUsuarioDesencriptado);

                        System.Diagnostics.Debug.WriteLine($"IDUsuario desencriptado: {idUsuario}");

                        // Cargar datos del usuario
                        CargarDatosUsuario(idUsuario);

                        // Cargar foto del usuario
                        string imagePath = ObtenerRutaFotoDesdeBD(idUsuario);
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            imgFoto.ImageUrl = ResolveUrl("~/" + imagePath);
                            lnkFoto.Visible = true;
                            lnkFoto.CommandArgument = imagePath; // Ruta de la foto como argumento
                        }

                        // Cargar curriculum del usuario
                        string curriculumPath = ObtenerRutaCurriculumDesdeBD(idUsuario);
                        if (!string.IsNullOrEmpty(curriculumPath))
                        {
                            lnkCurriculum.NavigateUrl = ResolveUrl("~/" + curriculumPath);
                            lnkCurriculum.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error al desencriptar IDUsuario o procesar datos: {ex.Message}");
                        Response.Redirect("Postulaciones.aspx");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No se recibió un IDUsuario en la cadena de consulta.");
                    Response.Redirect("Postulaciones.aspx");
                }
            }
        }
        private void CargarDepartamentos()
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                // Llamar al procedimiento almacenado
                string query = "CALL CargarDepartamentos()";
                using (MySqlCommand cmd = new MySqlCommand(query, conectar.conectar))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ddl_Departamento.DataSource = reader;
                            ddl_Departamento.DataTextField = "Departamento";
                            ddl_Departamento.DataValueField = "CodigoDep";
                            ddl_Departamento.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar departamentos: " + ex.Message);
                // Opcional: Mostrar mensaje al usuario en la interfaz
            }
            finally
            {
                conectar.CerrarConexion();
            }

            // Agregar un elemento vacío como opción inicial
            ddl_Departamento.Items.Insert(0, new ListItem("Seleccione un departamento", ""));
        }


        private void CargarMunicipios(string codigoDepartamento)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                // Llamar al procedimiento almacenado
                string query = "CALL CargarMunicipios(@CodigoDep)";
                using (MySqlCommand cmd = new MySqlCommand(query, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@CodigoDep", codigoDepartamento);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            ddl_Municipio.DataSource = reader;
                            ddl_Municipio.DataTextField = "Municipio";
                            ddl_Municipio.DataValueField = "CodigoMun";
                            ddl_Municipio.DataBind();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar municipios: " + ex.Message);
                // Opcional: Mostrar mensaje al usuario en la interfaz
            }
            finally
            {
                conectar.CerrarConexion();
            }

            // Agregar un elemento inicial como opción predeterminada
            ddl_Municipio.Items.Insert(0, new ListItem("Seleccione un municipio", ""));
        }

        protected void ddl_Departamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codigoDepartamento = ddl_Departamento.SelectedValue;
            if (!string.IsNullOrEmpty(codigoDepartamento) && codigoDepartamento != "0")
            {
                CargarMunicipios(codigoDepartamento);
            }
            else
            {
                ddl_Municipio.Items.Clear();
                ddl_Municipio.Items.Insert(0, new ListItem( "0"));
            }
        }

        private void CargarDatosUsuario(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();

            try
            {
                string procedimiento = "ObtenerDatosUsuario";

                using (MySqlCommand cmd = new MySqlCommand(procedimiento, conectar.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_IDUsuario", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignación de datos al formulario
                            txt_Nombre1.Text = reader["Primer_Nombre"].ToString();
                            txt_Nombre2.Text = reader["Segundo_Nombre"].ToString();
                            txt_Apellido1.Text = reader["Primer_Apellido"].ToString();
                            txt_Apellido2.Text = reader["Segundo_Apellido"].ToString();
                            txt_DNI.Text = reader["DNI"].ToString();
                            txt_Correo.Text = reader["Correo"].ToString();
                            txt_Fecha.Text = Convert.ToDateTime(reader["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
                            txt_Telefono.Text = reader["Telefono"].ToString();
                            txt_cuenta.Text = reader["Cuenta"].ToString();
                            txt_Universidad.Text = reader["Grado_academico"].ToString();
                            txt_Direccion.Text = reader["Direccion"].ToString();

                            // Selección del departamento y municipio
                            ddl_Departamento.SelectedValue = reader["CodigoDep"].ToString(); // Código del departamento
                            CargarMunicipios(reader["CodigoDep"].ToString()); // Cargar municipios del departamento seleccionado
                            ddl_Municipio.SelectedValue = reader["CodigoMun"].ToString(); // Código del municipio

                            // Selección de género
                            string sexo = reader["Sexo"].ToString();
                            txt_Hombre.Checked = sexo == "Hombre";
                            txt_Mujer.Checked = sexo == "Mujer";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar los datos del usuario: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }
        }


        private string ObtenerRutaFotoDesdeBD(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();
            string rutaFoto = null;

            try
            {
                string consulta = "SELECT Foto FROM datos_usuario WHERE IDUsuario = @userId";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != DBNull.Value)
                    {
                        rutaFoto = resultado.ToString();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al obtener la foto del postulante: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return rutaFoto;
        }

        private string ObtenerRutaCurriculumDesdeBD(int userId)
        {
            ConexionBD conectar = new ConexionBD();
            conectar.AbrirConexion();
            string rutaCurriculum = null;

            try
            {
                string consulta = "SELECT Curriculum FROM datos_usuario WHERE IDUsuario = @userId";
                using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != DBNull.Value)
                    {
                        rutaCurriculum = resultado.ToString();
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al obtener el curriculum del postulante: " + ex.Message);
            }
            finally
            {
                conectar.CerrarConexion();
            }

            return rutaCurriculum;
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["IDUsuario"] != null)
            {
                try
                {
                    // Desencripta el IDUsuario recibido
                    string idUsuarioEncriptado = Request.QueryString["IDUsuario"];
                    string idUsuarioDesencriptado = EncriptacionAES.Desencriptar(idUsuarioEncriptado);
                    int idUsuario = int.Parse(idUsuarioDesencriptado);

                    ConexionBD conectar = new ConexionBD();
                    conectar.AbrirConexion();

                    try
                    {
                        // Consulta para obtener los valores de los campos relevantes
                        string consulta = @"
                    SELECT RHConfirmado, SConfirmado, RConfirmado 
                    FROM usuarios 
                    WHERE IDUsuario = @idUsuario";

                        using (MySqlCommand cmd = new MySqlCommand(consulta, conectar.conectar))
                        {
                            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int rhConfirmado = reader.GetInt32("RHConfirmado");
                                    int sConfirmado = reader.GetInt32("SConfirmado");
                                    int rConfirmado = reader.GetInt32("RConfirmado");

                                    // Lógica para redirigir dependiendo de los valores (1 o 0)
                                    if (rhConfirmado == 0)
                                    {
                                        Response.Redirect("Postulaciones.aspx");
                                    }
                                    else if (rhConfirmado == 1 && sConfirmado == 0)
                                    {
                                        Response.Redirect("Postulaciones_Seguridad.aspx");
                                    }
                                    else if (rhConfirmado == 1 && sConfirmado == 1 && rConfirmado == 0)
                                    {
                                        Response.Redirect("Postulaciones_Regional.aspx");
                                    }
                                    else
                                    {
                                        // Si todos los valores son 0, redirigir a una página por defecto
                                        Response.Redirect("Postulaciones.aspx");
                                    }
                                }
                                else
                                {
                                    // Si no se encuentra información para el usuario
                                    MostrarMensaje("No se encontró información del usuario.");
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error al obtener datos: {ex.Message}");
                        MostrarMensaje("Ocurrió un error al procesar la solicitud.");
                    }
                    finally
                    {
                        conectar.CerrarConexion();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al desencriptar IDUsuario o procesar datos: {ex.Message}");
                    MostrarMensaje("El ID del usuario no es válido.");
                }
            }
            else
            {
                MostrarMensaje("No se recibió el ID del usuario.");
            }
        }




        protected void lnkFoto_Click(object sender, EventArgs e)
        {
            string rutaFoto = ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument;
            string rutaAbsoluta = Server.MapPath("~/" + rutaFoto);

            if (File.Exists(rutaAbsoluta))
            {
                Response.Clear();
                Response.ContentType = "image/jpeg"; // Cambia según el tipo de imagen, si no siempre es JPG
                Response.AppendHeader("Content-Disposition", $"attachment; filename={Path.GetFileName(rutaFoto)}");
                Response.WriteFile(rutaAbsoluta);
                Response.End();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Archivo no encontrado: {rutaAbsoluta}");
            }
        }

      //  protected void btnAceptar_Click(object sender, EventArgs e)
    //    {
      //      if (Request.QueryString["IDUsuario"] != null)
        //    {
          //    int idUsuario;
           //     if (int.TryParse(Request.QueryString["IDUsuario"], out idUsuario))
            //    {
              //      try
                //    {
                  //      // Instanciar la clase Postulacion
                    //    Postulacion postulacion = new Postulacion();
                    //
                        // Llamar al método para aceptar la postulación
                      //  postulacion.AceptarPostulacion(idUsuario);
                        //MostrarMensaje("La postulación fue aceptada exitosamente.");

                        // Opcional: redirigir o recargar la página
              //          Response.Redirect("Postulaciones.aspx");
                //    }
                  //  catch (Exception ex)
                    //{
                      //  System.Diagnostics.Debug.WriteLine($"Error al aceptar postulación: {ex.Message}");
                        //MostrarMensaje("Ocurrió un error al aceptar la postulación.");
                  //  }
             //   }
         //   }
      //  }

      //  protected void btnRechazar_Click(object sender, EventArgs e)
      //  {
       ///     if (Request.QueryString["IDUsuario"] != null)
        //    {
       //         int idUsuario;
        //        if (int.TryParse(Request.QueryString["IDUsuario"], out idUsuario))
         //       {
          //          try
           //         {
                        // Instanciar la clase Postulacion
             //           Postulacion postulacion = new Postulacion();
        //
                        // Llamar al método para rechazar la postulación
               //         postulacion.RechazarPostulacion(idUsuario);
                //        MostrarMensaje("La postulación fue rechazada exitosamente.");

                        // Opcional: redirigir o recargar la página
                 //       Response.Redirect("Postulaciones.aspx");
                  //  }
                  //  catch (Exception ex)
                  //  {
                  //      System.Diagnostics.Debug.WriteLine($"Error al rechazar postulación: {ex.Message}");
                 //       MostrarMensaje("Ocurrió un error al rechazar la postulación.");
            //        }
          //      }
         //   }
      //  }


        private void MostrarMensaje(string mensaje)
        {
            // Utiliza un script de JavaScript para mostrar un mensaje al usuario
            string script = $"alert('{mensaje}');";
            ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", script, true);
        }


    }
}
