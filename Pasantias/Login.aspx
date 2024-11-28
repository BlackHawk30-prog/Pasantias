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
                <img src="<%= ResolveUrl("~/Logos/logosoficiales.jpg") %>" alt="Logo Creando" class="img-fluid mb-3" width="900" height="500" />
                <h2 class="h5 mb-4">Sign in</h2>
            </div>
            
            <div class="form-group mb-3">
                <asp:Label ID="lbl_Usuario" runat="server" AssociatedControlID="txt_Usuario" Text="Usuario" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Usuario" runat="server" CssClass="form-control" Placeholder="Usuario" AutoComplete="off"></asp:TextBox>
            </div>

          <div class="form-group mb-3 d-flex align-items-center">
    <div class="flex-grow-1">
        <asp:Label ID="lbl_Password" runat="server" AssociatedControlID="txt_Password" Text="Password" CssClass="form-label"></asp:Label>
        <asp:TextBox ID="txt_Password" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Password"></asp:TextBox>
    </div>
    <button type="button" id="togglePasswordBtn" data-password-id="<%= txt_Password.ClientID %>" class="btn btn-primary h-100" style="margin-top: 30px; padding: 6px 10px;">Mostrar</button>
</div>



            <asp:Button ID="Button1" runat="server" Text="Sign In" CssClass="btn btn-primary w-100" OnClick="Button1_Click" />
        </div>
    </form>

    <script>
        document.getElementById("togglePasswordBtn").addEventListener("click", function () {
            const passwordInput = document.getElementById(this.getAttribute("data-password-id"));
            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                this.textContent = "Ocultar";
            } else {
                passwordInput.type = "password";
                this.textContent = "Mostrar";
            }
        });
    </script>
    <style>
       .form-group.mb-3.d-flex.align-items-center {
    display: flex;
    align-items: center; /* Asegura que los elementos se alineen verticalmente */
}

#togglePasswordBtn {
    padding: 0 10px;  /* Ajusta el tamaño del botón */
    margin-top: 0;  /* Asegura que el botón no se desplace hacia arriba o abajo */
    vertical-align: middle; /* Asegura la alineación vertical */
}


    </style>
</body>
</html>
