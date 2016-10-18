<%@ Page Title="" Language="C#" MasterPageFile="~/Login/MasterPage.master" AutoEventWireup="true" CodeFile="Verificacion.aspx.cs" Inherits="Login_Verificacion" %>

<asp:Content ID="Contentido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <p runat="server" id="mensaje"></p>
    <div runat="server" id="divCambioPass" style="width:40%; margin-left:30%;" visible="false">
        <label>Contraseña:</label>
        <asp:TextBox runat="server" ID="TextBoxPassword" CssClass="cajasTexto" TextMode="Password" />
        <label>Repetir Contraseña:</label>
        <asp:TextBox runat="server" ID="TextBoxConfirmarPassword" CssClass="cajasTexto" TextMode="Password" />
        <asp:Button runat="server" ID="ButtonConfirmar" Text="Confirmar" OnClick="ButtonConfirmar_Click" CssClass="botones" />
    </div>
</asp:Content>

