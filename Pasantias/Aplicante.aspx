<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aplicante.aspx.cs" Inherits="Pasantias.Aplicante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Centrar el formulario */
        .form-container {
            margin-top: 100px;
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

    <div class="form-container">
        <div class="form-wrapper">
            <h1>Aplica Hoy!</h1>
            <div class="form-group">
                <asp:Label ID="lbl_Nombre" runat="server" Text="Nombre:"></asp:Label>
                <asp:TextBox ID="txt_Nombre" runat="server" placeholder="Escriba su Nombre"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lbl_Apellidos" runat="server" Text="Apellidos:"></asp:Label>
                <asp:TextBox ID="txt_Apellido" runat="server" placeholder="Escriba sus Apellidos Completos"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lbl_DNI" runat="server" Text="DNI:"></asp:Label>
                <asp:TextBox ID="txt_DNI" runat="server" placeholder="Escriba su Numero de Identidad"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha de Nacimiento:"></asp:Label>
                <asp:TextBox ID="txt_Fecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lbl_Correo" runat="server" Text="Correo Electrónico:"></asp:Label>
                <asp:TextBox ID="txt_Correo" runat="server" placeholder="Escriba su Correo Electrónico"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lbl_Telefono" runat="server" Text="Número de Teléfono:"></asp:Label>
                <asp:TextBox ID="txt_Telefono" runat="server" placeholder="Escriba su Número de Teléfono"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lbl_Universidad" runat="server" Text="Universidad:"></asp:Label>
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
                <asp:Label ID="lbl_Fotografia" runat="server" Text="Fotografía:"></asp:Label>
                <asp:FileUpload ID="txt_Foto" runat="server" />
            </div>

            <div class="form-group">
                <asp:Label ID="lbl_Curriculum" runat="server" Text="Curriculum:"></asp:Label>
                <asp:FileUpload ID="txt_Curriculum" runat="server" />
            </div>

            <div class="form-group">
                <asp:Button ID="Enviar" runat="server" Text="Enviar Datos" OnClick="Enviar_Click" />
            </div>
        </div>
    </div>
</asp:Content>
