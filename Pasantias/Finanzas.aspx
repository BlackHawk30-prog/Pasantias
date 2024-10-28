<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Finanzas.aspx.cs" Inherits="Pasantias.Finanzas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="text-center"> Finanzas </h1> 

    <!-- Div contenedor centrado -->
    <div class="container text-center">
    <div class="table-responsive">
            <!-- Centrar la tabla y agregar estilo con Bootstrap -->
            <asp:GridView ID="grid_fina" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover mx-auto" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
    <Columns>

        <asp:BoundField DataField="Primer_Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="DNI" HeaderText="DNI" />
        <asp:BoundField DataField="Segundo_Nombre" HeaderText="Supervisor" />
        <asp:BoundField DataField="IDUsuario" HeaderText="Recuros Humanos" />
       
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:Button ID="btn_hoja" runat="server" CausesValidation="False" CommandName="Select" Text="Hoja de Tiempo" CssClass="btn btn-primary" />
            </ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:Button ID="btn_borrar" runat="server" CausesValidation="False" CommandName="Delete" Text="Borrar" CssClass="btn btn-danger" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

        </div>
    </div>
</asp:Content>
