<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Pasantias.ResetPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Reset your password" />
    <link rel="icon" href="favicon.ico" />
    <!-- Simple bar CSS -->
    <link rel="stylesheet" href="css/simplebar.css" />
    <!-- Fonts CSS -->
    <link href="https://fonts.googleapis.com/css2?family=Overpass:wght@100;200;300;400;600;700;800;900&display=swap" rel="stylesheet" />
    <!-- Icons CSS -->
    <link rel="stylesheet" href="css/feather.css" />
    <!-- Date Range Picker CSS -->
    <link rel="stylesheet" href="css/daterangepicker.css" />
    <!-- App CSS -->
    <link rel="stylesheet" href="css/app-light.css" id="lightTheme" />
    <link rel="stylesheet" href="css/app-dark.css" id="darkTheme" state="disabled" />
    <title>Reset Password</title>
    <style>
        /* Estilos para centrar el formulario */
        .centered-form-container {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
        }
        .centered-form {
            max-width: 400px;
            width: 100%;
        }
    </style>
</head>

<body class="light">
    <form id="form1" runat="server">
        <div class="centered-form-container">
            <div class="centered-form">
                <div class="text-center mb-4">
                    <a href="./index.html">
                        <img src="Logos/logosoficiales.jpg" alt="Logo Creando" class="mb-3" width="400" height="200"/>
                    </a>
                    <h4>Ingrese su correo electronico</h4>
                </div>

                <div class="form-group">
                    <asp:TextBox ID="txt_Correo" runat="server" AutoCompleteType="Email" CssClass="form-control form-control-lg" Placeholder="Correo Electronico" required="true" AutoComplete="off"></asp:TextBox>
                </div>

                <asp:Button ID="ResetPass" runat="server" Text="Reestablecer Contraseña" CssClass="btn btn-lg btn-primary btn-block"  />

                <asp:Label runat="server" ID="lblError" CssClass="text-danger"></asp:Label>
                <asp:Label runat="server" ID="Label1"></asp:Label>
            </div>
        </div>
    </form>

    <script src="js/jquery.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/simplebar.min.js"></script>
    <script src="js/daterangepicker.js"></script>
    <script src="js/jquery.stickOnScroll.js"></script>
    <script src="js/tinycolor-min.js"></script>
    <script src="js/config.js"></script>
    <script src="js/apps.js"></script>
    <!-- Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-56159088-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());
        gtag('config', 'UA-56159088-1');
    </script>
</body>
</html>
