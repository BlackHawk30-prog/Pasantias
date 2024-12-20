﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Finanzas.aspx.cs" Inherits="Pasantias.Finanzas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="text-center"> Finanzass </h1> <!-- Título centrado -->

    <!-- Div contenedor centrado -->
    <div class="container d-flex justify-content-center mt-4"> <!-- Contenedor centralizado y margen superior -->
        <div class="table-responsive" style="max-width: 800px;"> <!-- Ancho máximo ajustado -->
            <!-- Centrar la tabla y agregar estilo con Bootstrap -->
            <asp:GridView ID="grid_fina" runat="server" AutoGenerateColumns="False" 
                CssClass="table table-striped table-bordered table-hover mx-auto text-center" 
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Primer_Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                    <asp:BoundField DataField="Segundo_Nombre" HeaderText="Supervisor" />
                    <asp:BoundField DataField="id" HeaderText="Recursos Humanos" />

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="btn_hoja" runat="server" CausesValidation="False" 
                                CommandName="Select" Text="Hoja de Tiempo" CssClass="btn btn-primary" />
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

