<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Escuelas.aspx.cs" Inherits="Principal_Escuelas" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div runat="server" id="divMensajeConfirmacion" visible="false" class="divModificacion">
                <asp:Label runat="server" ID="LabelMensaje"></asp:Label>
                <asp:Button runat="server" ID="ButtonConfirmar" CssClass="botones" Text="Aceptar" OnClick="ButtonConfirmar_Click" />
            </div>
            <div runat="server" id="divEscuelas" class="divTablas">
                <asp:GridView ID="GridEscuelas" runat="server" CssClass="tablas" DataKeyNames="ID" HeaderStyle-CssClass="tablasEncabezados" AllowSorting="True" OnRowCommand="GridEscuelas_RowCommand" AutoGenerateColumns="False" DataSourceID="datosEscuelas">
                    <Columns>
                        <asp:BoundField DataField="ID" Visible="false" SortExpression="ID" />
                        <asp:BoundField DataField="Escuela" HeaderText="Escuela" SortExpression="Escuela" />
                        <asp:BoundField DataField="Dirección" HeaderText="Dirección" SortExpression="Dirección" />
                        <asp:BoundField DataField="Teléfono" HeaderText="Teléfono" SortExpression="Teléfono" />
                        <asp:ButtonField ControlStyle-CssClass="botonesTablas" ButtonType="Button" HeaderText="" CommandName="ModificarEscuela" Text="Modificar" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosEscuelas" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_escuelas]"></asp:SqlDataSource>
                <asp:Button runat="server" ID="ButtonAgregarEscuela" CssClass="botones" OnClick="ButtonAgregarEscuela_Click" Text="Agregar Escuela" />
            </div>
            <div runat="server" id="divModificarEscuela" visible="false" class="divModificacion">
                <asp:Label runat="server" ID="labelTextoID" Visible="false">ID:</asp:Label>
                <asp:Label runat="server" ID="labelID"></asp:Label>
                <br />
                <label>Escuela:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxEscuela" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label>Dirección:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxDireccion" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label>Teléfono:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxTelefono" CssClass="cajasTexto"></asp:TextBox>
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

