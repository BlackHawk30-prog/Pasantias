﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Postulaciones_Regional.aspx.cs" Inherits="Pasantias.Postulaciones_Regional" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="text-center">Postulantes a Pasantía</h1>
        
        <!-- Contenedor para el filtro y los botones -->
        <div class="form-group d-flex justify-content-between align-items-center mb-3">
            <!-- Contenedor para el botón de regresar y el filtro -->
            <div class="d-flex justify-content-between w-100">
                <!-- Botón Regresar alineado a la izquierda -->
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary" OnClick="btnRegresar_Click" />
                
                <!-- Filtro por Departamento alineado a la derecha -->
                <div class="d-flex">
                    <asp:DropDownList ID="ddlDepartamentos" runat="server" CssClass="form-control mx-2" />
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                </div>
            </div>
        </div>
        
        <!-- GridView con los postulantes -->
        <asp:GridView ID="grid_aplicantes" runat="server" AutoGenerateColumns="False" DataKeyNames="IDUsuario,CodigoMun,CodigoDep"
            CssClass="table table-condensed" OnRowCommand="grid_aplicantes_RowCommand">
            <Columns>
                <asp:BoundField DataField="Primer_Nombre" HeaderText="Nombre" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="Primer_Apellido" HeaderText="Apellido" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="DNI" HeaderText="DNI" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="Correo" HeaderText="Correo" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="Departamento" HeaderText="Departamento" ItemStyle-CssClass="text-center" />
                
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:LinkButton ID="Btn_Detalles" runat="server" Text="Detalles" CssClass="btn btn-info" 
                            CommandName="Detalles" CommandArgument='<%# Eval("IDUsuario") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:LinkButton ID="Btn_Aceptar" runat="server" Text="Aceptar" CssClass="btn btn-success" 
                            CommandName="Aceptar" CommandArgument='<%# Eval("IDUsuario") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:LinkButton ID="Btn_Rechazar" runat="server" Text="Rechazar" CssClass="btn btn-danger" 
                            CommandName="Rechazar" CommandArgument='<%# Eval("IDUsuario") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Estilos adicionales -->
    <style>
        .container { max-width: 800px; margin: 0 auto; padding: 20px; }
        .text-center { text-align: center; }
        .table-condensed { width: 100%; border-collapse: collapse; border: 1px solid #ddd; margin-top: 20px; }
        .table-condensed td, .table-condensed th { border: 1px solid #ddd; padding: 8px; text-align: left; }
        .table-condensed th { background-color: #4682B4; color: white; text-align: center; }
        .btn { display: inline-block; padding: 5px 10px; cursor: pointer; }
        .btn-info { background-color: #5bc0de; color: white; border: none; }
        .btn-success { background-color: #5cb85c; color: white; border: none; }
        .btn-danger { background-color: #d9534f; color: white; border: none; }
        .btn-info:hover { background-color: #46b8da; }
        .btn-success:hover { background-color: #4cae4c; }
        .btn-danger:hover { background-color: #d43f3a; }
        .d-flex { display: flex; }
        .justify-content-between { justify-content: space-between; }
        .align-items-center { align-items: center; }
        .mx-2 { margin-left: 0.5rem; margin-right: 0.5rem; }
    </style>
</asp:Content>
