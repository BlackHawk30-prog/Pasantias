<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aplicante1.aspx.cs" Inherits="Pasantias.Aplicante1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <style>
     /* Centrar el formulario */
     .form-container {
         margin-bottom: 100px;
         display: flex;
         justify-content: center;
         align-items: center;
         height: 100vh; /* Altura total de la ventana */
     }

     .form-group {
         margin-bottom: 10px;
     }

     .form-wrapper {
         width: 100%;
         max-width: 400px;
         padding: 20px;
         background-color: #f9f9f9;
         border-radius: 10px;
         box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
     }

     label, input, select, .form-control {
         display: block;
         width: 100%;
         max-width: 400px;
     }

     input, .form-control {
         padding: 5px;
     }

     .radio-group {
         display: flex;
         align-items: center;
         gap: 10px; /* Espacio entre el radio button y la etiqueta */
     }
 </style>
            <div class="logo-container" >
         <img alt="Logo USAID" src="Logos/usaid.png"  width="500" height="200"/>
         <img alt="Logo Institución" src="Logos/logo.jpg"  width="500" height="200"/>
         <img alt="Logo Creando" src="Logos/creando.jpg"  width="100" height="100" />
    </div>
 <div class="form-container">
     <div class="form-wrapper">
         <h1>Aplica Hoy!</h1>
         <div class="form-group">
             <asp:Label ID="lbl_Nombre1" runat="server" Text="Primer Nombre:"></asp:Label>
             <asp:TextBox ID="txt_Nombre1" runat="server" placeholder="Escriba su Primer Nombre"></asp:TextBox>
         </div>
                  <div class="form-group">
             <asp:Label ID="lbl_Nombre2" runat="server" Text=" Segundo Nombre:"></asp:Label>
             <asp:TextBox ID="txt_Nombre2" runat="server" placeholder="Escriba su Segundo Nombre"></asp:TextBox>
         </div>

         <div class="form-group">
             <asp:Label ID="lbl_Apellido1" runat="server" Text="Primer Apellido:"></asp:Label>
             <asp:TextBox ID="txt_Apellido1" runat="server" placeholder="Escriba su Primer Apellido "></asp:TextBox>
         </div>
                  <div class="form-group">
             <asp:Label ID="lbl_Apellido2" runat="server" Text="Segundo Apellido:"></asp:Label>
             <asp:TextBox ID="txt_Apellido2" runat="server" placeholder="Escriba su Segundo Apellido "></asp:TextBox>
         </div>

         <div class="form-group">
             <asp:Label ID="lbl_DNI" runat="server" Text="DNI:"></asp:Label>
             <asp:TextBox ID="txt_DNI" runat="server" placeholder="Escriba su Numero de Identidad"></asp:TextBox>
         </div>

         <div class="form-group">
             <asp:Label ID="lbl_Correo" runat="server" Text="Correo Electrónico:"></asp:Label>
             <asp:TextBox ID="txt_Correo" runat="server" placeholder="Escriba su Correo Electrónico"></asp:TextBox>
         </div>

         <div class="form-group">
             <asp:Button ID="Enviar" runat="server" Text="Enviar Datos" />
         </div>
     </div>
 </div>
        </div>
    </form>
</body>
</html>

