<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Cuentas.aspx.cs" Inherits="Principal_Cuentas" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div runat="server" id="divMensajeConfirmacion" visible="false" class="divModificacion">
                <asp:Label runat="server" ID="LabelMensaje"></asp:Label>
                <br />
                <asp:Button runat="server" ID="ButtonConfirmar" CssClass="botones" Text="Aceptar" OnClick="ButtonConfirmar_Click" />
            </div>
            <div runat="server" id="divUsuarios" class="divTablas">
                <asp:GridView ID="GridUsuarios" runat="server" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" OnRowCommand="GridUsuarios_RowCommand" DataKeyNames="ID" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="datosUsuarios">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="" Visible="false" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" SortExpression="Nombre" />
                        <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" ReadOnly="True" SortExpression="Apellidos" />
                        <asp:BoundField DataField="Escuela" HeaderText="Escuela" SortExpression="Escuela" />
                        <asp:BoundField DataField="Correo Electrónico" HeaderText="Correo Electrónico" SortExpression="Correo Electrónico" />
                        <asp:BoundField DataField="Nivel de Permisos" HeaderText="Nivel de Permisos" SortExpression="Nivel de Permisos" />
                        <asp:CheckBoxField DataField="Cuenta Activada" HeaderText="Cuenta Activada" SortExpression="Cuenta Activada" />
                        <asp:ButtonField ButtonType="Button" Text="Modificar" HeaderText="" CommandName="ModificarUsuario" ControlStyle-CssClass="botonesTablas" />
                        <asp:ButtonField ButtonType="Button" Text="Activar Cuenta" HeaderText="" CommandName="Activar" ControlStyle-CssClass="botonesTablas" />
                        <asp:ButtonField ButtonType="Button" Text="Desactivar Cuenta" HeaderText="" CommandName="Desactivar" ControlStyle-CssClass="botonesTablas" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_usuarios]"></asp:SqlDataSource>
                <asp:Button runat="server" ID="ButtonAgregarUsuario" CssClass="botones" OnClick="ButtonAgregarUsuario_Click" Text="Agregar Usuario" />
            </div>
            <div runat="server" id="divModificacionUsuarios" visible="false" class="divModificacion">
                <asp:Label runat="server" ID="labelTextoID" Visible="false">ID:</asp:Label>
                <asp:Label runat="server" ID="labelID"></asp:Label>
                <br />
                <label>Nombre:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxNombre" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label>Apellidos:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxApellidos" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label>Escuela:</label>
                <br />
                <asp:DropDownList ID="DropDownListEscuelas" CssClass="listas" runat="server" DataSourceID="datosEscuelas" DataTextField="Escuela" DataValueField="ID"></asp:DropDownList>
                <asp:SqlDataSource ID="datosEscuelas" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT [ID], [Escuela] FROM [vista_escuelas]"></asp:SqlDataSource>
                <br />
                <asp:Button runat="server" ID="ButtonAgregarEscuela" OnClick="ButtonAgregarEscuela_Click" CssClass="botones" Text="Agregar Escuela" />
                <br />
                <br />
                <label runat="server" id="labelCorreo">Correo Electrónico:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxCorreo" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label>Nivel de usuario:</label>
                <br />
                <asp:DropDownList ID="DropDownListNivelUsuario" CssClass="listas" runat="server">
                    <asp:ListItem Value="A" Text="Administrador" />
                    <asp:ListItem Value="U" Text="Usuario" />
                    <asp:ListItem Value="E" Text="Encargado" />
                </asp:DropDownList>
                <br />
                <asp:Button runat="server" ID="ButtonAgregar" Text="Aceptar" OnClick="ButtonAgregar_Click" CssClass="botones"/>
                <asp:Button runat="server" ID="ButtonModificar" Text="Aceptar" OnClick="ButtonModificar_Click" CssClass="botones"/>
                <asp:Button runat="server" ID="ButtonCancelar" Text="Cancelar" OnClick="ButtonCancelar_Click" CssClass="botones"/>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

