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
    <asp:Button ID="Btn_Actualizar" runat="server" Text="Actualizar" CssClass="btn btn-success" Visible="False" OnClick="Btn_Actualizar_Click" />
    <br />
    <asp:Label ID="lbl_Mensaje" runat="server" Text="Activid:"></asp:Label>
    <br />
    <asp:GridView ID="grid_hojas" runat="server" AutoGenerateColumns="False" DataKeyNames="IDHoja,IDUsuario" CssClass="table-condensed" OnSelectedIndexChanged="grid_hojas_SelectedIndexChanged">
        <Columns>
           
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" 
                DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="fecha" />
            <asp:BoundField DataField="Actividades" HeaderText="Actividades" SortExpression="Actividades" ItemStyle-CssClass="actividades" />
            <asp:BoundField DataField="Horas" HeaderText="Horas Trabajadas" SortExpression="Horas" ItemStyle-CssClass="horas" />

            <asp:TemplateField ShowHeader="True" HeaderText="Accion">
                <ItemTemplate>
                    <asp:Button ID="Btn_Ver" runat="server" CausesValidation="False" CommandName="Select" Text="Ver" CssClass="btn btn-info" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="True" HeaderText="Accion">
                <ItemTemplate>
                    <asp:Button ID="Btn_Eliminar" runat="server" CausesValidation="False" CommandName="Delete" Text="Eliminar" CssClass="btn btn-danger" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!-- Estilo para la tabla -->
    <style>
        .table-condensed {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid #ddd;
        }

        .table-condensed td, .table-condensed th {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        .table-condensed tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .table-condensed th {
            background-color: #4CAF50;
            color: white;
            text-align: center;
        }

        .table-condensed td.fecha {
            text-align: center;
        }

        .table-condensed td.actividades {
            text-align: justify;
        }

        .table-condensed td.horas {
            text-align: center;
        }

        .btn {
            display: inline-block;
            padding: 5px 10px;
            cursor: pointer;
        }

        .btn-info {
            background-color: #17a2b8;
            color: white;
            border: none;
        }

        .btn-danger {
            background-color: #dc3545;
            color: white;
            border: none;
        }
    </style>
</asp:Content>
