<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VistaHJ.aspx.cs" Inherits="Pasantias.VistaHJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Contenedor para centrar el contenido -->
    <div class="container">
        <h1 class="text-center">Hoja de Tiempo</h1>
        
        <!-- Campo para el total de horas -->
        <div class="form-group d-flex justify-content-end">
            <asp:Label ID="lbl_HorasTotales" runat="server" Text="Total de Horas: 0" CssClass="form-label"></asp:Label>
        </div>
        

        
        <!-- Mensaje de actividad -->
        <div class="form-group text-center">
            <asp:Label ID="lbl_Mensaje" runat="server" Text="" CssClass="form-label"></asp:Label>
        </div>
        
        <!-- Contenedor centrado para la tabla -->
        <div class="form-container">
            <div class="form-group text-center">
                <asp:GridView ID="grid_hoja" runat="server" AutoGenerateColumns="False" DataKeyNames="IDHoja" 
                 CssClass="table table-condensed" OnSelectedIndexChanged="grid_hojas_SelectedIndexChanged">
                 <Columns>
                     <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="fecha" />
                     <asp:BoundField DataField="Actividades" HeaderText="Actividades" SortExpression="Actividades" ItemStyle-CssClass="actividades" />
                     <asp:BoundField DataField="Horas" HeaderText="Horas Trabajadas" SortExpression="Horas" ItemStyle-CssClass="horas" ItemStyle-Width="100px" />

                     <asp:TemplateField ShowHeader="True" HeaderText="Accion">
                         <ItemTemplate>
                             <asp:Button ID="Btn_Ver" runat="server" CausesValidation="False" CommandName="Select" Text="Editar" CssClass="btn btn-info" />
                         </ItemTemplate>
                     </asp:TemplateField>

                     <asp:TemplateField ShowHeader="True" HeaderText="Accion">
                         <ItemTemplate>
                             <asp:Button ID="Btn_Eliminar" runat="server" CausesValidation="False" CommandName="Delete" Text="Eliminar" CssClass="btn btn-danger" />
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
        .btn-custom { background-color: #4682B4; color: white; border: none; }
        .btn-custom:hover { background-color: #36648B; }
    </style>
</asp:Content>
