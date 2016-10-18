<%@ Page Title="" Language="C#" MasterPageFile="~/Login/MasterPage.master" AutoEventWireup="true" CodeFile="Registro.aspx.cs" Inherits="Login_Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <div id="divRegistro">
        <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="panelActualizacion" runat="server">
            <ContentTemplate>
                <p>Se necesita una cuenta válida de la uaq para poder registrarse, en caso de no tenerla favor de solicitarla. Si usted no es estudiante o trabajador de la Universidad, favor de pasar directamente con el Coordinador de Videoconferencias.</p>
                <p>Los campos con * son obligatorios.</p>
                <asp:Label id="textoError" runat="server" style="color:red"></asp:Label>
                <div id="camposTexto" runat="server">
                    <div>
                        <label class="labelsRegistro">*Nombre:</label>
                        <br />
                        <asp:TextBox runat="server" CssClass="cajasTextoRegistro" ID="textBoxNombre"></asp:TextBox>
                        <br />
                    </div>
                    <div>
                        <label class="labelsRegistro">*Apellido(s):</label>
                        <br />
                        <asp:TextBox runat="server" CssClass="cajasTextoRegistro" ID="textBoxApellido"></asp:TextBox>
                        <br />
                    </div>
                    <div>
                        <label class="labelsRegistro">*Correo:</label>
                        <br />
                        <asp:TextBox runat="server" CssClass="cajasTextoRegistro" ID="textBoxCorreo"></asp:TextBox>
                        <label>@uaq.mx</label>
                    </div>
                    <div>
                        <label class="labelsRegistro">*Contraseña:</label>
                        <br />
                        <asp:TextBox TextMode="Password" runat="server" CssClass="cajasTextoRegistro" ID="textBoxPassword"></asp:TextBox>
                        <br />
                    </div>
                    <div>
                        <label class="labelsRegistro">*Repetir Contraseña:</label>
                        <br />
                        <asp:TextBox TextMode="Password" runat="server" CssClass="cajasTextoRegistro" ID="textBoxPasswordValidation"></asp:TextBox>
                        <br />
                    </div>
                    <div>
                        <asp:Button ID="buttonRegistro" CssClass="botones" Text="Registrar" runat="server" OnClick="buttonRegistro_Click" />
                        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="panelActualizacion" ID="UpdateProgressEspera">
                            <ProgressTemplate>
                                <img src="../Imagenes/animacionEspera.svg" />
                                <label id="labelEspera">Enviando solicitud de registro...</label>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="buttonRegistro" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

