<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aplicante_2.aspx.cs" Inherits="Pasantias.Aplicante_2" %>

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
            <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha de Nacimiento:"></asp:Label>
            <asp:TextBox ID="txt_Fecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>



        <div class="form-group">
            <asp:Label ID="lbl_Telefono" runat="server" Text="Número de Teléfono:"></asp:Label>
            <asp:TextBox ID="txt_Telefono" runat="server" placeholder="Escriba su Número de Teléfono"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lbl_Universidad" runat="server" Text="Ultimo Grado Academico Alcanzado:"></asp:Label>
            <asp:TextBox ID="txt_Universidad" runat="server" placeholder="Escriba su Universidad"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lbl_Direccion" runat="server" Text="Dirección (Especificar Departamento):"></asp:Label>
            <asp:TextBox ID="txt_Direccion" runat="server" placeholder="Escriba su Dirección Exacta"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lbl_Sexo" runat="server" Text="Sexo:"></asp:Label>
            <div class="radio-group">
                <asp:RadioButton ID="txt_Hombre" runat="server" GroupName="Sexo" Text="Hombre" />
                <asp:RadioButton ID="txt_Mujer" runat="server" GroupName="Sexo" Text="Mujer" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label ID="lbl_Fotografia" runat="server" Text="Fotografía Frontal:"></asp:Label>
            <asp:FileUpload ID="txt_Foto" runat="server" />
        </div>

        <div class="form-group">
            <asp:Label ID="lbl_Curriculum" runat="server" Text="Curriculum:"></asp:Label>
            <asp:FileUpload ID="txt_Curriculum" runat="server" />
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
