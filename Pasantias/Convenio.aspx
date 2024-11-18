<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Convenio.aspx.cs" Inherits="Pasantias.Convenio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Aplica Hoy!</title>
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/feather.css" />
    <link rel="stylesheet" href="css/app-light.css" id="lightTheme" />
    <style>
        .required { color: red; }
        .error { border-color: red !important; }
    </style>
</head>
<body class="vertical light">
    <form runat="server">
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-6 mx-auto">
                    <div class="text-center mb-4">
                        <h1 class="card-title text-center">Convenio Creando mi Futuro Aquí</h1>
                    </div>

                    <div class="card shadow mb-4">
                        <div class="card-header text-center">
                            <img alt="Logo USAID" src="Logos/logosoficiales.jpg" class="img-fluid" />
                        </div>
                        <div class="card-body">
                            <!-- Párrafo informativo -->
                            <div class="form-group">
                                <asp:Label ID="lbl_Parrafo" runat="server" Text="Favor leer detenidamente:" CssClass="form-label"></asp:Label>
                                <asp:TextBox 
                                    ID="txt_Parrafo" 
                                    runat="server" 
                                    TextMode="MultiLine" 
                                    Rows="5" 
                                    CssClass="form-control" 
                                    ReadOnly="true">
                Esta es la prueba para la firma del convenio.
                                </asp:TextBox>
                            </div>

                            <!-- Fecha de inicio -->
                            <div class="form-group">
                                <asp:Label ID="lbl_FechaInicio" runat="server" Text="Fecha de Inicio de la pasantía:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_FechaInicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>


                            <!-- Fecha de finalización -->
                            <div class="form-group">
                                <asp:Label ID="lbl_FechaFinal" runat="server" Text="Fecha de Finalización de la pasantía:" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_FechaFinal" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>

    <!-- Scripts -->
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.bundle.min.js"></script>
</body>
</html>
