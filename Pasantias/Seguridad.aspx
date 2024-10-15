<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Seguridad.aspx.cs" Inherits="Pasantias.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <h1>Seguridad </h1>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="btn_selec" runat="server" CausesValidation="False" CommandName="Select" Text="Ver" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="btn_borrar" runat="server" CausesValidation="False" CommandName="Delete" Text="Borrar" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Primer_Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="DNI" HeaderText="DNI" />
            <asp:BoundField DataField="Horas" HeaderText="Total Horas" />
            <asp:BoundField DataField="Hoja de tiempo" HeaderText="Hoja de tiempo" />
            <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
            <asp:BoundField DataField="RH" HeaderText="Recursos Humanos" />
        </Columns>
    </asp:GridView>
</asp:Content>
