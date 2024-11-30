<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aplicante_2.aspx.cs" Inherits="Pasantias.Aplicante_2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Aplica Hoy!</title>
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/feather.css" />
    <link rel="stylesheet" href="css/app-light.css" id="lightTheme" />

    <style>
        .required { color: red; }
        .error { border-color: red !important; }
    </style>
    <script type="text/javascript">
        function clearError(id) {
            document.getElementById(id).classList.remove('error');
        }
    </script>
</head>
<body class="vertical light">
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <div class="text-center mb-4">
                        <h1 class="card-title text-center">Aplica Hoy!</h1>
                    </div>

                    <div class="card shadow mb-4">
                        <div class="card-header text-center">
                            <img alt="Logos Oficiales" src="Logos/logosoficiales.jpg" width="500" height="200" />
                        </div>

                        <div class="card-body">
                            <div class="form-group text-center">
                                <asp:Label ID="lbl_Error" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha de Nacimiento:" CssClass="form-label"></asp:Label>
                                  <span class="required">*</span>
                                <asp:TextBox ID="txt_Fecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_Telefono" runat="server" Text="Número de Teléfono:" CssClass="form-label"></asp:Label>
                                  <span class="required">*</span>
                                <asp:TextBox ID="txt_Telefono" runat="server" placeholder="Escriba su Número de Teléfono" CssClass="form-control"></asp:TextBox>
                            </div>
                                <div class="form-group">
                                <asp:Label ID="lbl_cuenta" runat="server" Text="Numero de cuenta BAC:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_cuenta" runat="server" placeholder="Escriba su Número de cuenta BAC" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_Universidad" runat="server" Text="Último Grado Académico Alcanzado:" CssClass="form-label"></asp:Label>
                                  <span class="required">*</span>
                                <asp:TextBox ID="txt_Universidad" runat="server" placeholder="Escriba su Universidad" CssClass="form-control"></asp:TextBox>
                            </div>
                                                <div class="form-group">
                                <asp:Label ID="lbl_Departamento" runat="server" Text="Departamento:" CssClass="form-label"></asp:Label>
                                                      <span class="required">*</span>
                                <asp:DropDownList ID="ddl_Departamento" runat="server" AutoPostBack="true" CssClass="form-control" 
                                                  OnSelectedIndexChanged="ddl_Departamento_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_Municipio" runat="server" Text="Municipio:" CssClass="form-label"></asp:Label>
                                  <span class="required">*</span>
                                <asp:DropDownList ID="ddl_Municipio" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>


                                    <div class="form-group">
                                        <asp:Label ID="lbl_Direccion" runat="server" Text="Dirección Exacta:" CssClass="form-label"></asp:Label>
                                          <span class="required">*</span>
                                        <asp:TextBox ID="txt_Direccion" runat="server" placeholder="Escriba su Dirección Exacta" CssClass="form-control"></asp:TextBox>
                                    </div>


                            <div class="form-group">
                                <asp:Label ID="lbl_Sexo" runat="server" Text="Sexo:" CssClass="form-label"></asp:Label>
                                  <span class="required">*</span>
                                <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="txt_Hombre" runat="server" GroupName="Sexo" Text="" CssClass="form-check-input" />
                                    <asp:Label runat="server" AssociatedControlID="txt_Hombre" CssClass="form-check-label">Hombre</asp:Label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="txt_Mujer" runat="server" GroupName="Sexo" Text="" CssClass="form-check-input" />
                                    <asp:Label runat="server" AssociatedControlID="txt_Mujer" CssClass="form-check-label">Mujer</asp:Label>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_Fotografia" runat="server" Text="Fotografía Frontal (Solo se aceptan formatos JPG y PNG):" CssClass="form-label"></asp:Label>
                                  <span class="required">*</span>
                                <asp:FileUpload ID="txt_Foto" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbl_Curriculum" runat="server" Text="Curriculum (Solo se aceptan formatos .doc, .docx, .pdf, .dox):" CssClass="form-label"></asp:Label>
                                  <span class="required">*</span>
                                <asp:FileUpload ID="txt_Curriculum" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-group text-center">
                                <asp:Button ID="Enviar" runat="server" Text="Enviar Datos" CssClass="btn btn-primary" OnClick="Enviar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Scripts -->
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.bundle.min.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            // Seleccionar todos los inputs y escuchar el evento "input"
            const inputs = document.querySelectorAll(".error");
            inputs.forEach(input => {
                input.addEventListener("input", function () {
                    // Eliminar la clase "error" al escribir en el campo
                    this.classList.remove("error");
                });
            });
        });
    </script>

    <style>
        .error {
    border: 2px solid red;
   
}

    </style>
</body>
</html>
