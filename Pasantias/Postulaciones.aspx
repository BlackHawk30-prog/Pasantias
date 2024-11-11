<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Postulaciones.aspx.cs" Inherits="Pasantias.Postulaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="text-center">Postulantes a Pasantia</h1>
        
        <!-- Tabla de postulantes -->
        <asp:GridView ID="grid_aplicantes" runat="server" AutoGenerateColumns="False" DataKeyNames="IDUsuario,IDDatos_Usuarios,IDRol" 
                      CssClass="table table-condensed">
            <Columns>
                <asp:BoundField DataField="Primer_Nombre" HeaderText="Nombre" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="Primer_Apellido" HeaderText="Apellido" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="DNI" HeaderText="DNI" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="Correo" HeaderText="Correo" ItemStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" ItemStyle-CssClass="text-center"></asp:BoundField>
                
              
                <asp:TemplateField HeaderText="Detalles">
                    <ItemTemplate>
                        <asp:Button ID="Btn_Detalles" runat="server" Text="Detalles" CssClass="btn btn-info" />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Aceptar">
                    <ItemTemplate>
                        <asp:Button ID="Btn_Aceptar" runat="server" Text="Aceptar" CssClass="btn btn-success" />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Rechazar">
                    <ItemTemplate>
                        <asp:Button ID="Btn_Rechazar" runat="server" Text="Rechazar" CssClass="btn btn-danger" />
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
    </style>
</asp:Content>
