﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editar_Perfil.aspx.cs" Inherits="Pasantias.Editar_Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Contenedor para centrar el contenido -->
    <div class="container">
        <h1 class="text-center">Editar Perfil</h1>

        <!-- Cuadro blanco para el formulario 1 -->
        <div class="form-container">
            <div class="form-group">
                <asp:Label ID="lbl_Nombre1" runat="server" Text="Primer Nombre:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Nombre1" runat="server" placeholder="Escriba su Primer Nombre" CssClass="form-control" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Nombre2" runat="server" Text="Segundo Nombre:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Nombre2" runat="server" placeholder="Escriba su Segundo Nombre" CssClass="form-control" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Apellido1" runat="server" Text="Primer Apellido:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Apellido1" runat="server" placeholder="Escriba su Primer Apellido" CssClass="form-control" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Apellido2" runat="server" Text="Segundo Apellido:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Apellido2" runat="server" placeholder="Escriba su Segundo Apellido" CssClass="form-control" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_DNI" runat="server" Text="DNI:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_DNI" runat="server" placeholder="Escriba su Número de Identidad" CssClass="form-control" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Correo" runat="server" Text="Correo Electrónico:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Correo" runat="server" placeholder="Escriba su Correo Electrónico" CssClass="form-control" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
        </div>

        <!-- Cuadro blanco para el formulario 2 -->
        <div class="form-container">
            <div class="form-group">
                <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha de Nacimiento:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Fecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Telefono" runat="server" Text="Número de Teléfono:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Telefono" runat="server" placeholder="Escriba su Número de Teléfono" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Universidad" runat="server" Text="Último Grado Académico Alcanzado:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Universidad" runat="server" placeholder="Escriba su Universidad" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Direccion" runat="server" Text="Dirección (Especificar Departamento):" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Direccion" runat="server" placeholder="Escriba su Dirección Exacta" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Sexo" runat="server" Text="Sexo:" CssClass="form-label"></asp:Label>
                <div class="form-check form-check-inline">
                    <asp:RadioButton ID="txt_Hombre" runat="server" GroupName="Sexo" CssClass="form-check-input" />
                    <asp:Label runat="server" AssociatedControlID="txt_Hombre" CssClass="form-check-label">Hombre</asp:Label>
                </div>
                <div class="form-check form-check-inline">
                    <asp:RadioButton ID="txt_Mujer" runat="server" GroupName="Sexo" CssClass="form-check-input" />
                    <asp:Label runat="server" AssociatedControlID="txt_Mujer" CssClass="form-check-label">Mujer</asp:Label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Fotografia" runat="server" Text="Fotografía Frontal (Solo se aceptan formatos JPG y PNG):" CssClass="form-label"></asp:Label>
                <asp:FileUpload ID="txt_Foto" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Curriculum" runat="server" Text="Curriculum (Solo se aceptan formatos .doc, .docx, .pdf, .dox, .360):" CssClass="form-label"></asp:Label>
                <asp:FileUpload ID="txt_Curriculum" runat="server" CssClass="form-control" />
            </div>
        </div>
        
    </div>

    <!-- Estilos adicionales -->
    <style>
        .container { max-width: 800px; margin: 0 auto; padding: 20px; }
        .form-container { background-color: white; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); margin-bottom: 20px; }
        .form-group { margin-bottom: 1rem; }
        .form-label { font-weight: bold; }
        .btn { display: inline-block; padding: 5px 10px; cursor: pointer; }
        .btn-primary { background-color: #007bff; color: white; border: none; }
        .btn-primary:hover { background-color: #0056b3; }
    </style>
</asp:Content>