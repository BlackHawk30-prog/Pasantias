<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Pasantias.Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Sign In - Creando Mi Futuro Aquí</title>
    <link href="https://fonts.googleapis.com/css2?family=Overpass:wght@400;600;700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="css/app-light.css" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="card shadow-sm p-4 mx-auto" style="max-width: 400px;">
            <div class="text-center">
                <img src="Logos/logosoficiales.jpg" alt="Logo Creando" class="img-fluid mb-3" width="900" height="500" />

                <h2 class="h5 mb-4">Sign in</h2>
            </div>

            <div class="form-group mb-3">
                <asp:Label ID="LabelEmail" runat="server" AssociatedControlID="indextxtEmail" Text="Email address HOLA" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="indextxtEmail" runat="server" AutoCompleteType="Email" CssClass="form-control" Placeholder="Email address" AutoComplete="off"></asp:TextBox>
            </div>

            <div class="form-group mb-3">
                <asp:Label ID="LabelPassword" runat="server" AssociatedControlID="indextxtPassword" Text="Password" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="indextxtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Password"></asp:TextBox>
            </div>

            <asp:Button ID="Button1" runat="server" Text="Sign In" CssClass="btn btn-primary w-100" />

            <div class="text-center mt-3">
                <a href='<%= ResolveUrl("~/ResetPassword.aspx") %>' class="forgot-password text-decoration-none">¿Olvidaste tu contraseña?</a>
            </div>
        </div>
    </form>
    <script src="js/bootstrap.bundle.min.js"></script>
</body>
</html>
