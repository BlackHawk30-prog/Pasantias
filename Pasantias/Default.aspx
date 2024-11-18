<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Pasantias._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container">
        <h1>Bienvenido al Sistema</h1>
        <h2><asp:Label ID="lblBienvenida" runat="server" Text=""></asp:Label></h2>
    </main>

    <style>
        .container {
            max-width: 600px;
            margin: 50px auto;
            padding: 20px;
            text-align: center;
        }
        h1 {
            font-size: 2.5rem;
            color: #4682B4;
        }
        h2 {
            font-size: 2rem;
            color: #5bc0de;
            margin-top: 20px;
        }
    </style>
</asp:Content>
