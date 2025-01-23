<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Oficial.aspx.cs" Inherits="Pasantias.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="text-center">Oficial</h1> <!-- Título centrado -->

    <!-- Div contenedor centrado -->
    <div class="container d-flex justify-content-center mt-4">
        <div class="table-responsive" style="max-width: 800px;">
            <!-- Tabla con el GridView -->
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover mx-auto text-center"
                OnRowCommand="GridView1_RowCommand">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Seleccionar">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxSelect" runat="server" />
                            <asp:HiddenField ID="HiddenFieldIDHojaTiempo" runat="server" Value='<%# Eval("IDHojaTiempo") %>' />
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
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- Botones generales para aceptar/rechazar -->
    <div class="d-flex justify-content-center mt-3">
        <asp:Button ID="btnAceptarSeleccionados" runat="server" Text="Aceptar seleccionados" CssClass="btn btn-success mx-2" OnClick="btnAceptarSeleccionados_Click" />
        <asp:Button ID="btnRechazarSeleccionados" runat="server" Text="Rechazar seleccionados" CssClass="btn btn-danger mx-2" OnClick="btnRechazarSeleccionados_Click" />
    </div>
</asp:Content>
