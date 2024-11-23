<%@ Page Title="Cambio de Contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cambio_Password.aspx.cs" Inherits="Pasantias.Cambio_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <!-- Título -->
                <div class="text-center mb-4">
                    <h1 class="card-title text-center">Cambio de Contraseña</h1>
                </div>

                <!-- Card principal con el fondo -->
                <div class="card shadow mb-4">


                    <!-- Cuerpo del formulario -->
                    <div class="card-body">
                        <!-- Mensaje de error -->
                        <div class="form-group text-center">
                            <asp:Label ID="lbl_Error" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                        </div>

                        <!-- Contraseña actual -->
                        <div class="form-group">
                            <label for="txt_OldPassword">Contraseña Actual:</label>
                            <asp:TextBox ID="txt_OldPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su contraseña actual"></asp:TextBox>
                        </div>

                        <!-- Nueva contraseña -->
                        <div class="form-group">
                            <label for="txt_NewPassword">Nueva Contraseña:</label>
                            <asp:TextBox ID="txt_NewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su nueva contraseña"></asp:TextBox>
                        </div>

                        <!-- Confirmar nueva contraseña -->
                        <div class="form-group">
                            <label for="txt_ConfirmPassword">Confirmar Nueva Contraseña:</label>
                            <asp:TextBox ID="txt_ConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirme su nueva contraseña"></asp:TextBox>
                        </div>

                        <!-- Botones -->
                        <div class="form-group text-center">
                            <asp:Button ID="btn_CambiarPassword" runat="server" Text="Cambiar Contraseña" CssClass="btn btn-primary" OnClick="btn_CambiarPassword_Click" />
                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" OnClick="btnRegresar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
