<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Materiales.aspx.cs" Inherits="Principal_Materiales" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="contenidoCuerpo" Runat="server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div id="divLista" class="listaSeleccionTablas">
                <label>Seleccione alguna acción:</label>
                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="listaSeleccion_SelectedIndexChanged" ID="listaSeleccion">
                    <asp:ListItem>Ver Materiales Por Cantidad</asp:ListItem>
                    <asp:ListItem>Ver Materiales</asp:ListItem>
                    <asp:ListItem>Ver Marcas</asp:ListItem>
                    <asp:ListItem>Ver Modelos</asp:ListItem>
                    <asp:ListItem>Ver Tipos</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div runat="server" id="divMaterialCantidad" visible="true" class="divTablas">
                <asp:GridView ID="GridMaterialCantidad" runat="server" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="datosMaterialesCantidad">
                    <Columns>
                        <asp:BoundField DataField="Material" HeaderText="Material" SortExpression="Material" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ReadOnly="True" SortExpression="Cantidad" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosMaterialesCantidad" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_cantidad_materiales]"></asp:SqlDataSource>
            </div>
            <div runat="server" id="divMensajeConfirmacion" visible="false" class="divModificacion">
                <asp:Label runat="server" ID="LabelMensaje"></asp:Label>
                <asp:Button runat="server" ID="ButtonConfirmar" CssClass="botones" Text="Aceptar" OnClick="ButtonConfirmar_Click" />
            </div>
            <div runat="server" id="divMaterial" visible="false" class="divTablas">
                <asp:GridView ID="GridMaterial" runat="server" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="datosMaterial" OnRowCommand="GridMaterial_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="false" SortExpression="ID" />
                        <asp:BoundField DataField="No. Serie" HeaderText="No. Serie" SortExpression="No. Serie" />
                        <asp:BoundField DataField="Material" HeaderText="Material" SortExpression="Material" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                        <asp:BoundField DataField="Características" HeaderText="Características" SortExpression="Características" />
                        <asp:ButtonField ControlStyle-CssClass="botonesTablas" ButtonType="Button" HeaderText="" CommandName="ModificarMaterial" Text="Modificar" />
                    </Columns>
                    <HeaderStyle CssClass="tablasEncabezados" />
                </asp:GridView>
                <asp:SqlDataSource ID="datosMaterial" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_materiales]"></asp:SqlDataSource>
                <asp:Button runat="server" ID="ButtonAgregarMaterial" CssClass="botones" OnClick="ButtonAgregarMaterial_Click" Text="Agregar Material" />                
            </div>
            <div runat="server" id="divModificarMaterial" visible="false" class="divModificacion">
                <asp:Label runat="server" ID="labelTextoID" Visible="false">ID:</asp:Label>
                <asp:Label runat="server" ID="labelID"></asp:Label>
                <br />
                <label>Número de Serie:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxNumSerie" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label>Tipo de Material:</label>
                <br />
                <asp:DropDownList runat="server" ID="listaTipos" DataSourceID="DatosListaTipos" DataTextField="Tipo" DataValueField="ID"></asp:DropDownList>
                <asp:SqlDataSource ID="DatosListaTipos" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_tipos_materiales]"></asp:SqlDataSource>
                <br />
                <br />
                <label>Marca:</label>
                <br />
                <asp:DropDownList runat="server" ID="listaMarcas" DataSourceID="DatosListaMarcas" DataTextField="Marca" DataValueField="ID" ></asp:DropDownList>
                <asp:SqlDataSource ID="DatosListaMarcas" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_marcas_materiales]"></asp:SqlDataSource>
                <br />
                <br />
                <label>Modelo:</label>
                <br />
                <asp:DropDownList runat="server" ID="listaModelos" DataSourceID="DatosListaModelos" DataTextField="Modelo" DataValueField="ID"></asp:DropDownList>
                <asp:SqlDataSource ID="DatosListaModelos" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_modelos_materiales]"></asp:SqlDataSource>
                <br />
                <br />
                <label>Características:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxCaracteristicas" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="ButtonAgregar" Text="Aceptar" OnClick="ButtonAgregar_Click" CssClass="botones"/>
                <asp:Button runat="server" ID="ButtonModificar" Text="Aceptar" OnClick="ButtonModificar_Click" CssClass="botones"/>
                <asp:Button runat="server" ID="ButtonCancelar" Text="Cancelar" OnClick="ButtonCancelar_Click" CssClass="botones"/>
            </div>
            <div runat="server" id="divTipos" visible="false" class="divTablas">
                <asp:GridView ID="GridTipos" runat="server" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="datosTipos">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosTipos" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_tipos_materiales]"></asp:SqlDataSource>
                <br />
                <label>Agregar nuevo tipo:</label>
                <asp:TextBox runat="server" ID="TextBoxAgregarTipo" CssClass="cajasTexto" ></asp:TextBox>
                <asp:Button runat="server" ID="ButtonAgregarTipoMaterial" CssClass="botones" OnClick="ButtonAgregarTipoMaterial_Click" Text="Agregar Tipo" />
                <label runat="server" id="mensajeTipo"></label>
            </div>
            <div runat="server" id="divModelos" visible="false" class="divTablas">
                <asp:GridView ID="GridModelos" runat="server" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="datosModelos">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosModelos" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_modelos_materiales]"></asp:SqlDataSource>
                <br />
                <label>Agregar nuevo modelo:</label>
                <asp:TextBox runat="server" ID="TextBoxAgregarModelo" CssClass="cajasTexto" ></asp:TextBox>
                <asp:Button runat="server" ID="ButtonAgregarModelo" CssClass="botones" OnClick="ButtonAgregarModelo_Click" Text="Agregar Modelo" />
                <label runat="server" id="mensajeModelo"></label>
            </div>
            <div runat="server" id="divMarcas" visible="false" class="divTablas">
                <asp:GridView ID="GridMarcas" runat="server" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="datosMarcas">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosMarcas" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_marcas_materiales]"></asp:SqlDataSource>
                <br />
                <label>Agregar nueva marca:</label>
                <asp:TextBox runat="server" ID="TextBoxAgregarMarca" CssClass="cajasTexto" ></asp:TextBox>
                <asp:Button runat="server" ID="ButtonAgregarMarca" CssClass="botones" OnClick="ButtonAgregarMarca_Click" Text="Agregar Marca" />
                <label runat="server" id="mensajeMarca"></label>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

