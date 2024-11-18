<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hojas_Generales.aspx.cs" Inherits="Pasantias.Hojas_Generales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
    <h1 class="text-center">Hojas Generales</h1>
             
<div class="form-group d-flex justify-content-between align-items-center">
    <!-- Botón Regresar alineado a la izquierda -->
    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" OnClick="btnRegresar_Click" />
    
    <!-- Boton para nueva hoja -->
     <asp:Button ID="BtnNuevaHoja" runat="server" Text="Nueva Hoja de Tiempo" CssClass="btn btn-info" OnClick="BtnNuevaHoja_Click" />
</div>

    <!-- Tabla centrada y con estilos -->
    <div class="form-container">
        <asp:GridView ID="grid_Generales" runat="server" AutoGenerateColumns="False" DataKeyNames="IDHojaTiempo,IDUsuario" 
            CssClass="table table-condensed" OnRowCommand="grid_Generales_RowCommand">
            <Columns>
                <asp:BoundField DataField="IDHojaTiempo" HeaderText="Hoja de Tiempo" ItemStyle-CssClass="fecha"></asp:BoundField>
                <asp:BoundField DataField="IDUsuario" HeaderText="Usuario" ItemStyle-CssClass="actividades"></asp:BoundField>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha Creada" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="fecha"></asp:BoundField>
                <asp:TemplateField ShowHeader="True" HeaderText="Accion">
                    <ItemTemplate>
                        <asp:Button ID="Btn_Editar" runat="server" CausesValidation="False" CommandName="Editar" 
                            Text="Editar" CssClass="btn btn-info" CommandArgument='<%# Eval("IDHojaTiempo") %>' />
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
    .table-condensed td.fecha, .table-condensed td.horas { text-align: center; }
    .btn { display: inline-block; padding: 5px 10px; cursor: pointer; }
    .btn-info { background-color: #5bc0de; color: white; border: none; }
    .btn-info:hover { background-color: #31b0d5; }
</style>

</asp:Content>
