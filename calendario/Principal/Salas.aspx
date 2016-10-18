<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Salas.aspx.cs" Inherits="Principal_Salas" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div id="divLista" class="listaSeleccionTablas">
                <label>Seleccione alguna acción:</label>
                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="listaSeleccion_SelectedIndexChanged" ID="listaSeleccion">
                    <asp:ListItem>Ver Salas</asp:ListItem>
                    <asp:ListItem>Ver Encargados</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div runat="server" id="divMensajeConfirmacion" visible="false" class="divModificacion">
                <asp:Label runat="server" ID="LabelMensaje"></asp:Label>
                <br />
                <asp:Button runat="server" ID="ButtonConfirmar" CssClass="botones" Text="Aceptar" OnClick="ButtonConfirmar_Click" />
            </div>
            <div runat="server" id="divSalas" class="divTablas">
                <asp:GridView ID="GridViewSalas" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="datosSalas" AllowSorting="True" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" OnRowCommand="GridViewSalas_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" Visible="false" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Sala" HeaderText="Sala" SortExpression="Sala" />
                        <asp:BoundField DataField="Ubicación" HeaderText="Ubicación" SortExpression="Ubicación" />
                        <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" SortExpression="Capacidad" />
                        <asp:ButtonField ButtonType="Button" Text="Modificar" HeaderText="" CommandName="ModificarSala" ControlStyle-CssClass="botonesTablas" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosSalas" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_salas]"></asp:SqlDataSource>
                <asp:Button runat="server" ID="ButtonAgregarSala" CssClass="botones" OnClick="ButtonAgregarSala_Click" Text="Agregar Sala" />
            </div>
            <div runat="server" id="divModificacionSalas" class="divModificacion" visible="false">
                <asp:Label runat="server" ID="labelTextoID" Visible="false">ID:</asp:Label>
                <asp:Label runat="server" ID="labelID"></asp:Label>
                <br />
                <label>Nombre de la sala:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxNombreSala" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label>Ubicación:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxUbicacionSala" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label runat="server" id="labelCapacidad">Capacidad:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxCapacidadSala" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label runat="server" id="labelCaracteristicas">Características:</label>
                <br />
                <asp:TextBox runat="server" TextMode="MultiLine" ID="TextBoxCaracteristicasSala" CssClass="cajasTextoMultilinea"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="ButtonAceptarAgregarSala" Text="Aceptar" OnClick="ButtonAceptarAgregarSala_Click" CssClass="botones"/>
                <asp:Button runat="server" ID="ButtonAceptarModificarSala" Text="Aceptar" OnClick="ButtonAceptarModificarSala_Click" CssClass="botones"/>
                <asp:Button runat="server" ID="ButtonCancelarAccionSala" Text="Cancelar" OnClick="ButtonCancelarAccionSala_Click" CssClass="botones"/>
                <br />
                <br />
                <br />
                <label runat="server" id="labelError" style="color:red;"></label>
            </div>
            <div runat="server" id="divEncargados" class="divTablas" visible="false">
                <asp:GridView ID="GridViewEncargados" runat="server" OnPreRender="GridViewEncargados_PreRender" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="datosEncargados" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados">
                    <Columns>
                        <asp:BoundField DataField="ID" Visible="false" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Sala" HeaderText="Sala" SortExpression="Sala" />
                        <asp:BoundField DataField="Encargado" HeaderText="Encargado" ReadOnly="True" SortExpression="Encargado" />
                        <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosEncargados" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_encargados_salas]"></asp:SqlDataSource>
                <asp:Button runat="server" ID="ButtonAsignarEncargado" CssClass="botones" OnClick="ButtonAsignarEncargado_Click" Text="Asignar Encargado" />                                
            </div>
            <div runat="server" id="divAsignarEncargados" class="divModificacion" visible="false">
                <label>Sala:</label>
                <br />
                <asp:DropDownList runat="server" ID="DropDownListSalas" CssClass="listas" DataSourceID="listaSalas" DataTextField="Sala" DataValueField="ID"></asp:DropDownList>
                <asp:SqlDataSource ID="listaSalas" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT [ID], [Sala] FROM [vista_salas]"></asp:SqlDataSource>
                <br />
                <br />
                <label>Encargado:</label>
                <br />
                <asp:DropDownList runat="server" ID="DropDownListEncargados" CssClass="listas" DataSourceID="listaEncargados" DataTextField="Encargado" DataValueField="ID"></asp:DropDownList>
                <asp:SqlDataSource ID="listaEncargados" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_encargados]"></asp:SqlDataSource>
                <br />
                <br />
                <asp:Button runat="server" ID="ButtonAceptarAsignarEncargado" Text="Aceptar" OnClick="ButtonAceptarAsignarEncargado_Click" CssClass="botones"/>
                <asp:Button runat="server" ID="ButtonCancelarAsignarEncargado" Text="Cancelar" OnClick="ButtonCancelarAsignarEncargado_Click" CssClass="botones"/>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

