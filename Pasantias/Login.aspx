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
                <asp:Label ID="lbl_Usuario" runat="server" AssociatedControlID="txt_Usuario" Text="Usuario" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Usuario" runat="server" CssClass="form-control" Placeholder="Usuario" AutoComplete="off"></asp:TextBox>
            </div>

            <div class="form-group mb-3 position-relative"> 
                <asp:Label ID="lbl_Password" runat="server" AssociatedControlID="txt_Password" Text="Password" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Password" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Password"></asp:TextBox>
                <span onclick="togglePasswordVisibility()" class="position-absolute" style="top: 50%; right: 10px; transform: translateY(-50%); cursor: pointer;">
                    <i id="toggleIcon" class="fas fa-eye"></i>
                </span>
            </div>

            <asp:Button ID="Button1" runat="server" Text="Sign In" CssClass="btn btn-primary w-100" OnClick="Button1_Click" />

            <div class="text-center mt-3">
                <a href='<%= ResolveUrl("~/ResetPassword.aspx") %>' class="forgot-password text-decoration-none">¿Olvidaste tu contraseña?</a>
            </div>
        </div>
    </form>

    <script src="https://kit.fontawesome.com/a076d05399.js"></script> <!-- para el icono de ojo -->
    <script src="js/bootstrap.bundle.min.js"></script>
    <script>
        function togglePasswordVisibility() {
            const passwordInput = document.getElementById('<%= txt_Password.ClientID %>');
            const toggleIcon = document.getElementById('toggleIcon');
            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                toggleIcon.classList.remove('fa-eye');
                toggleIcon.classList.add('fa-eye-slash');
            } else {
                passwordInput.type = 'password';
                toggleIcon.classList.remove('fa-eye-slash');
                toggleIcon.classList.add('fa-eye');
            }
        }
    </script>
</body>
</html>
