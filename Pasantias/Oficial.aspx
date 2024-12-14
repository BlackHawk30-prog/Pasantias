<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Oficial.aspx.cs" Inherits="Pasantias.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="text-center">Oficial</h1> <!-- Título centrado con botón -->

    <!-- Div contenedor centrado -->
    <div class="container d-flex justify-content-center mt-4"> <!-- Contenedor centralizado y margen superior -->
        <div class="table-responsive" style="max-width: 800px;"> <!-- Ancho máximo ajustado -->
            <!-- Centrar la tabla y agregar estilo con Bootstrap -->
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover mx-auto text-center"
                OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
                <Columns>

                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    

                    <asp:BoundField DataField="Primer_Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="DNI" HeaderText="DNI" />


                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="btn_hoja" runat="server" CausesValidation="False" 
                                        CommandName="IrHoja" Text="Hoja de Tiempo" CssClass="btn btn-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Accion">
                         <ItemTemplate>
                             <asp:LinkButton ID="Btn_Aceptar" runat="server" Text="Aceptar" CssClass="btn btn-success" 
                                 CommandName="Aceptar" CommandArgument='<%# Eval("IDUsuario") %>'></asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- Div contenedor para alinear los botones horizontalmente -->
    <div class="d-flex justify-content-center mt-3">
   <!--  <asp:Button ID="btn_acept" runat="server" Text="Aceptar" CommandName="Aceptar" CssClass="btn btn-success mx-2" Visible="True" OnClick="Button1_Click" /> -->
        <asp:Button ID="btn_rech" runat="server" Text="Rechazar" CssClass="btn btn-danger mx-2" Visible="True" OnClick="btn_rech_Click" />
    </div>
</asp:Content>
