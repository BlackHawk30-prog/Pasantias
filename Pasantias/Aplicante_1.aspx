<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aplicante_1.aspx.cs" Inherits="Pasantias.Aplicante_1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Aplica Hoy!</title>
    <!-- Incluye el estilo del archivo adjunto -->
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/feather.css" />
    <link rel="stylesheet" href="css/app-light.css" id="lightTheme" />
</head>
<body class="vertical light">
    <form runat="server">
        <!-- Contenedor principal reducido en tamaño -->
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-6 mx-auto"> <!-- Reduce el ancho a la mitad y lo centra -->
                    <!-- Logo en la parte superior -->
                    <div class="text-center mb-4">
                        <img alt="Logo USAID" src="Logos/logosoficiales.jpg" width="500" height="200" />
                    </div>

                    <!-- Formulario dentro de una tarjeta -->
                    <div class="card shadow mb-4">
                        <div class="card-header">
                            <h1 class="card-title text-center">Aplica Hoy!</h1>
                        </div>
                        <div class="card-body">
                            <div class="form-group">
                                <asp:Label ID="lbl_Nombre1" runat="server" Text="Primer Nombre:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_Nombre1" runat="server" placeholder="Escriba su Primer Nombre" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbl_Nombre2" runat="server" Text="Segundo Nombre:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_Nombre2" runat="server" placeholder="Escriba su Segundo Nombre" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbl_Apellido1" runat="server" Text="Primer Apellido:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_Apellido1" runat="server" placeholder="Escriba su Primer Apellido" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbl_Apellido2" runat="server" Text="Segundo Apellido:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_Apellido2" runat="server" placeholder="Escriba su Segundo Apellido" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbl_DNI" runat="server" Text="DNI:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_DNI" runat="server" placeholder="Escriba su Número de Identidad" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbl_Correo" runat="server" Text="Correo Electrónico:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_Correo" runat="server" placeholder="Escriba su Correo Electrónico" CssClass="form-control"></asp:TextBox>
                            </div>

                            <!-- Botón de envío -->
                            <div class="form-group text-center">
                                <asp:Button ID="Enviar" runat="server" Text="Enviar Datos" CssClass="btn btn-primary" />
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
</body>
</html>


