<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Postulaciones.aspx.cs" Inherits="Pasantias.Postulaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <div style="display: flex; justify-content: center;">
<asp:GridView ID="grid_aplicantes" runat="server" AutoGenerateColumns="False" DataKeyNames="IDUsuario,IDDatos_Usuarios,IDRol">
    <Columns>
        <asp:BoundField DataField="Primer_Nombre" HeaderText="Nombre"></asp:BoundField>
        <asp:BoundField DataField="Primer_Apellido" HeaderText="Apellido"></asp:BoundField>
        <asp:BoundField DataField="DNI" HeaderText="DNI"></asp:BoundField>
        <asp:BoundField DataField="Correo" HeaderText="Correo"></asp:BoundField>
        <asp:BoundField DataField="Telefono" HeaderText="Telefono"></asp:BoundField>
        <asp:CommandField ShowSelectButton="True" ButtonType="Button" HeaderText="Detalles"></asp:CommandField>
        <asp:CommandField ShowSelectButton="True" ButtonType="Button" HeaderText="Aceptar"></asp:CommandField>
        <asp:CommandField ShowSelectButton="True" ButtonType="Button" HeaderText="Rechazar"></asp:CommandField>
    </Columns>
</asp:GridView>

</div>

</asp:Content>
