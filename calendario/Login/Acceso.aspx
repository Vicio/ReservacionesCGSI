<%@ Page Title="" Language="C#" MasterPageFile="~/Login/MasterPage.master" AutoEventWireup="true" CodeFile="Acceso.aspx.cs" Inherits="Login_Estudiantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <div id="areaForma">
        <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="panelActualizacion" runat="server">
            <ContentTemplate>
                <div runat="server" id="divAcceso">
                    <asp:Label id="textoError" runat="server" style="color:red"></asp:Label>
                    <br />
                    <label>Correo:</label>
                    <asp:TextBox CssClass="cajasTexto" ID="TextBoxCorreo" runat="server"></asp:TextBox>
                    <label>Contraseña:</label>
                    <asp:TextBox CssClass="cajasTexto" TextMode="Password" ID="TextBoxPassword" runat="server"></asp:TextBox>
                    <asp:LinkButton runat="server" ID="LinkButtonOlvidePassword" Text="Olvidé la contraseña" OnClick="LinkButtonOlvidePassword_Click"></asp:LinkButton>
                    <br />
                    <br />
                    <asp:Button CssClass="botones" ID="buttonAceptar" Text="Aceptar" runat="server" OnClick="buttonAceptar_Click" />
                </div>
                <div runat="server" id="divPass" visible="false">
                    <label>Escriba su correo, le llegará una notificación:</label>
                    <br />
                    <asp:Label id="LabelMensaje" runat="server" style="color:red"></asp:Label>
                    <asp:TextBox CssClass="cajasTexto" ID="TextBoxCorreoVerificacion" runat="server"></asp:TextBox>
                    <asp:Button CssClass="botones" ID="buttonCambiarPass" Text="Aceptar" runat="server" OnClick="buttonCambiarPass_Click" />
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

