<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RH.aspx.cs" Inherits="Pasantias.WebForm2" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="text-center">Recursos Humanos</h1> <!-- Título centrado -->

    <!-- Div contenedor centrado -->
    <div class="container d-flex justify-content-center mt-4"> <!-- Contenedor centralizado y margen superior -->
        <div class="table-responsive" style="max-width: 800px;"> <!-- Ancho máximo ajustado -->
            <!-- Centrar la tabla y agregar estilo con Bootstrap -->
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover mx-auto text-center" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>

               
                   
                   <asp:TemplateField HeaderText="Seleccionar">
                       <ItemTemplate>
                           <asp:CheckBox ID="CheckBoxSelect" runat="server" />
                           <asp:HiddenField ID="HiddenFieldIDHojaTiempo" runat="server" Value='<%# Eval("IDHojaTiempo") %>' />
                       </ItemTemplate>
                   </asp:TemplateField>



                   
                   <asp:BoundField DataField="Primer_Nombre" HeaderText="Nombre" />
                   <asp:BoundField DataField="DNI" HeaderText="DNI" />
                   <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />

                   
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="btnHojaTiempo" runat="server" CausesValidation="False"
                            CommandName="IrHoja" CommandArgument='<%# Eval("IDHojaTiempo") %>' 
                            Text="Hoja de Tiempo" CssClass="btn btn-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>

     <div class="d-flex justify-content-center mt-3">
        <asp:Button ID="btnAceptarSeleccionados" runat="server" Text="Aceptar seleccionados" CssClass="btn btn-success mx-2" OnClick="btnAceptarSeleccionados_Click" />
        <asp:Button ID="btnRechazarSeleccionados" runat="server" Text="Rechazar seleccionados" CssClass="btn btn-danger mx-2" OnClick="btnRechazarSeleccionados_Click" />
     </div>
</asp:Content>

