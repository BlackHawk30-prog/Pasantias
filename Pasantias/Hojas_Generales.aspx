<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hojas_Generales.aspx.cs" Inherits="Pasantias.Hojas_Generales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
    <h1 class="text-center">Hojas Generales</h1>

    <!-- Tabla centrada y con estilos -->
    <div class="form-container">
        <asp:GridView ID="grid_Generales" runat="server" AutoGenerateColumns="False" DataKeyNames="IDHojaTiempo,IDUsuario" CssClass="table table-condensed">
            <Columns>
                <asp:BoundField DataField="IDHojaTiempo" HeaderText="Hoja de Tiempo"></asp:BoundField>
                <asp:BoundField DataField="IDUsuario" HeaderText="Usuario"></asp:BoundField>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha Creada" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
               
                <asp:TemplateField ShowHeader="True" HeaderText="Accion">
                    <ItemTemplate>
                        <asp:Button ID="Btn_Editar" runat="server" CausesValidation="False" CommandName="Select" Text="Editar" CssClass="btn btn-info" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>

<!-- Estilos adicionales -->
<style>
    .container { max-width: 800px; margin: 0 auto; padding: 20px; }
    .form-container { background-color: white; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); margin-bottom: 20px; }
    .form-group { margin-bottom: 1rem; }
    .form-label { font-weight: bold; }
    .table-condensed { width: 100%; border-collapse: collapse; border: 1px solid #ddd; margin-top: 20px; }
    .table-condensed td, .table-condensed th { border: 1px solid #ddd; padding: 8px; text-align: left; }
    .table-condensed th { background-color: #4682B4; color: white; text-align: center; }
    .btn { display: inline-block; padding: 5px 10px; cursor: pointer; }
    .btn-info { background-color: #5bc0de; color: white; border: none; }
    .btn-info:hover { background-color: #31b0d5; }
</style>

</asp:Content>
