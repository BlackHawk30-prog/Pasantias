<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hoja_Tiempo.aspx.cs" Inherits="Pasantias.Hoja_Tiempo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Hoja de Tiempo</h1>

    <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha:"></asp:Label>
    <asp:TextBox ID="txt_Fecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
    <br />

    <asp:Label ID="lbl_Actividades" runat="server" Text="Actividades Realizadas:"></asp:Label>
    <asp:TextBox ID="txt_Actividades" runat="server" CssClass="form-control"></asp:TextBox>
    <br />

    <asp:Label ID="lbl_Horas" runat="server" Text="Horas Trabajadas:"></asp:Label>
    <asp:TextBox ID="txt_Horas" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
    <br />

    <asp:Button ID="Btn_Agregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="Btn_Agregar_Click" />
    <asp:Button ID="Btn_Actualizar" runat="server" Text="Actualizar" CssClass="btn btn-success" />
    <br />

    <asp:GridView ID="grid_hojas" runat="server" AutoGenerateColumns="False" DataKeyNames="IDHoja,IDUsuario" CssClass="table-condensed">
        <Columns>
            <asp:BoundField DataField="Fecha" HeaderText="Fecha"></asp:BoundField>
            <asp:BoundField DataField="Actividades" HeaderText="Actividades"></asp:BoundField>
            <asp:BoundField DataField="Horas" HeaderText="Horas Trabajadas"></asp:BoundField>
 <asp:TemplateField ShowHeader="False">
     <ItemTemplate>
         <asp:Button ID="Btn_Ver" runat="server" CausesValidation="False" CommandName="Select" Text="Ver" />
     </ItemTemplate>
     <ControlStyle CssClass="btn btn-info"  />
     
 </asp:TemplateField>
 <asp:TemplateField ShowHeader="False">
     <ItemTemplate>
         <asp:Button ID="Btn_Eliminar" runat="server" CausesValidation="False" CommandName="Delete" Text="Eliminar"  />
     </ItemTemplate>
     <ControlStyle CssClass="btn btn-danger" />
 </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
