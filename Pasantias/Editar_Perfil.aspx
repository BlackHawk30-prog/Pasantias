<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editar_Perfil.aspx.cs" Inherits="Pasantias.Editar_Perfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Contenedor para centrar el contenido -->
    <div class="container">
        <h1 class="text-center">Editar Perfil</h1>

        <!-- Cuadro blanco para el formulario 1 -->
        <div class="form-container">
            <div class="form-group text-center">
                <asp:Label ID="lbl_Error" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Nombre1" runat="server" Text="Primer Nombre:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Nombre1" runat="server"  CssClass="form-control error-check" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Nombre2" runat="server" Text="Segundo Nombre:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Nombre2" runat="server"  CssClass="form-control error-check" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Apellido1" runat="server" Text="Primer Apellido:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Apellido1" runat="server"  CssClass="form-control error-check" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Apellido2" runat="server" Text="Segundo Apellido:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Apellido2" runat="server"  CssClass="form-control error-check" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_DNI" runat="server" Text="DNI:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_DNI" runat="server"  CssClass="form-control error-check" ReadOnly></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Correo" runat="server" Text="Correo Electrónico:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Correo" runat="server" CssClass="form-control error-check" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            
        </div>

        <!-- Cuadro blanco para el formulario 2 -->
        <div class="form-container">
            <div class="form-group">
                <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha de Nacimiento:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Fecha" runat="server" CssClass="form-control error-check" TextMode="Date"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Telefono" runat="server" Text="Número de Teléfono:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Telefono" runat="server"  CssClass="form-control error-check" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Universidad" runat="server" Text="Último Grado Académico Alcanzado:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Universidad" runat="server"  CssClass="form-control error-check" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
                                <div class="form-group">
    <asp:Label ID="lbl_Departamento" runat="server" Text="Departamento:" CssClass="form-label"></asp:Label>
    <asp:DropDownList ID="ddl_Departamento" runat="server" AutoPostBack="true" CssClass="form-control" 
                      OnSelectedIndexChanged="ddl_Departamento_SelectedIndexChanged">
    </asp:DropDownList>
</div>

<div class="form-group">
    <asp:Label ID="lbl_Municipio" runat="server" Text="Municipio:" CssClass="form-label"></asp:Label>
    <asp:DropDownList ID="ddl_Municipio" runat="server" CssClass="form-control">
    </asp:DropDownList>
</div>
            <div class="form-group">
                <asp:Label ID="lbl_Direccion" runat="server" Text="Dirección Exacta:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txt_Direccion" runat="server"  CssClass="form-control error-check" OnKeyUp="clearError(this.id)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Sexo" runat="server" Text="Sexo:" CssClass="form-label"></asp:Label>
                <div class="form-check form-check-inline">
                    <asp:RadioButton ID="txt_Hombre" runat="server" GroupName="Sexo" CssClass="form-check-input" />
                    <asp:Label runat="server" AssociatedControlID="txt_Hombre" CssClass="form-check-label">Hombre</asp:Label>
                </div>
                <div class="form-check form-check-inline">
                    <asp:RadioButton ID="txt_Mujer" runat="server" GroupName="Sexo" CssClass="form-check-input" />
                    <asp:Label runat="server" AssociatedControlID="txt_Mujer" CssClass="form-check-label">Mujer</asp:Label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Fotografia" runat="server" Text="Fotografía Frontal (Solo se aceptan formatos JPG y PNG):" CssClass="form-label"></asp:Label>
                <asp:FileUpload ID="txt_Foto" runat="server" CssClass="form-control" />
                <asp:Image ID="imgFoto" runat="server" CssClass="img-thumbnail mt-2" />
            </div>
            <div class="form-group">
                <asp:Label ID="lbl_Curriculum" runat="server" Text="Curriculum (Solo se aceptan formatos .doc, .docx, .pdf, .dox, .360):" CssClass="form-label"></asp:Label>
                <asp:FileUpload ID="txt_Curriculum" runat="server" CssClass="form-control" />
                <asp:HyperLink ID="lnkCurriculum" runat="server" CssClass="btn-custom" Text="Descargar Curriculum" Visible="false" Target="_blank" />
            </div>

            <!-- Botón para Guardar Cambios -->
            <div class="text-center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary mx-2" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary mx-2" OnClick="btnRegresar_Click" />
            </div>
        </div>
    </div>

    <!-- Estilos adicionales -->
    <style>
        .container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
        }
        .form-container {
            background-color: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            margin-bottom: 20px;
        }
        .form-control.error {
            border-color: red !important;
        }
    </style>

    <!-- Scripts adicionales -->
    <script>
        function clearError(id) {
            document.getElementById(id).classList.remove("error");
        }
    </script>
</asp:Content>
