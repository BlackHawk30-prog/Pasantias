<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Convenios_Generales.aspx.cs" Inherits="Pasantias.Convenios_Generales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
    <h1 class="text-center">Convenios Generales</h1>
             
    <div class="form-group d-flex justify-content-between align-items-center">
        <!-- Botón Regresar alineado a la izquierda -->
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2"  />
    </div>

    <!-- Tabla centrada y con estilos -->
    <div class="form-container">
        <asp:GridView ID="grid_Convenios" runat="server" AutoGenerateColumns="False" DataKeyNames="IDConvenio,IDUsuario" 
            CssClass="table table-condensed">
            <Columns>
                <asp:BoundField DataField="Fecha_Inicio" HeaderText="Fecha de Inicio" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="fecha"></asp:BoundField>
                <asp:BoundField DataField="Fecha_Final" HeaderText="Fecha de Finalización" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="fecha"></asp:BoundField>
                <asp:BoundField DataField="IDUsuario" HeaderText="Usuario" ItemStyle-CssClass="actividades"></asp:BoundField>
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
    .table-condensed td.fecha, .table-condensed td.horas { text-align: center; }
    .btn { display: inline-block; padding: 5px 10px; cursor: pointer; }
    .btn-info { background-color: #5bc0de; color: white; border: none; }
    .btn-info:hover { background-color: #31b0d5; }
</style>

</asp:Content>
