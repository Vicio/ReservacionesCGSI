<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Reservaciones.aspx.cs" Inherits="Calendario_Reservaciones" %>

<asp:Content ID="Contentido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true"  runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <label class="labels" runat="server" id="prueba"></label>
            <div runat="server" id="divAreaCalendario" class="areaCalendario">
                <div runat="server" id="divCalendario">
                    <label>Seleccione una fecha, los colores indican la disponibilidad de salas en el día:</label>
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td>
                                <span class="codigosColores" style="background-color:#97D86C"></span>
                                <label>Completamente Disponible</label>
                            </td>
                            <td>
                                <span class="codigosColores" style="background-color:#F2DB55"></span>
                                <label>Parcialmente Disponible</label>
                            </td>
                            <td>
                                <span class="codigosColores" style="background-color:#EE3423"></span>
                                <label>Ocupado</label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:Calendar ID="Calendario" runat="server" Visible="true"
                        BorderStyle="None" CellPadding="20" Font-Size="24px" BackColor="#FFFFFF" 
                        ShowGridLines="true" CssClass="calendario" SelectionMode="Day"
                        NextMonthText="Sig" PrevMonthText="Ant" OnDayRender="Calendario_DayRender" OnSelectionChanged="Calendario_SelectionChanged" >
                        <DayStyle BorderColor="#111547" ForeColor="#111547" BorderWidth="2px" />
                        <DayHeaderStyle BorderColor="#111547" ForeColor="#111547" BorderWidth="2px" />
                        <TitleStyle BackColor="#111547" ForeColor="#FFFFFF" BorderColor="#111547" BorderWidth="2px" />
                        <OtherMonthDayStyle Font-Size="0px" />
                        <NextPrevStyle ForeColor="#FFFFFF" />
                        <SelectedDayStyle />
                    </asp:Calendar>
                    <asp:Button runat="server" ID="ButtonAgregarDias" Text="Siguiente" CssClass="botones" OnClick="ButtonAgregarDias_Click" />
                    <label id="labelDiasSeleccionados" runat="server"></label>
                </div>
                <div runat="server" style="text-align:center;" id="divHora" visible="false">
                    <label>
                        Escoja el tiempo que el evento durará, puede seleccionar más de una hora,<br /> 
                        en caso de que el evento sea durante el fin de semana o posterior a las 4:00 PM<br />
                        se deberá pagar un monto extra por el servicio:
                    </label>
                    <br />
                    <br />
                    <label runat="server" id="LabelMensajeHoras" style="color:#EE3423"></label>
                    <br />
                    <br />
                    <asp:ListBox ID="ListBoxHoras" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListBoxHoras_SelectedIndexChanged"  CssClass="cajasDeLista" SelectionMode="Multiple">
                        <asp:ListItem Value="8" Text="8:00 AM - 9:00 AM" />
                        <asp:ListItem Value="9" Text="9:00 AM - 10:00 AM" />
                        <asp:ListItem Value="10" Text="10:00 AM - 11:00 AM" />
                        <asp:ListItem Value="11" Text="11:00 AM - 12:00 AM" />
                        <asp:ListItem Value="12" Text="12:00 PM - 1:00 PM" />
                        <asp:ListItem Value="13" Text="1:00 PM - 2:00 PM" />
                        <asp:ListItem Value="14" Text="2:00 PM - 3:00 PM" />
                        <asp:ListItem Value="15" Text="3:00 PM - 4:00 PM" />
                        <asp:ListItem Value="16" Text="4:00 PM - 5:00 PM" />
                        <asp:ListItem Value="17" Text="5:00 PM - 6:00 PM" />
                        <asp:ListItem Value="18" Text="6:00 PM - 7:00 PM" />
                        <asp:ListItem Value="19" Text="7:00 PM - 8:00 PM" />
                        <asp:ListItem Value="20" Text="8:00 PM - 9:00 PM" />
                    </asp:ListBox>
                    <br />
                    <asp:Button runat="server" CssClass="botones" ID="ButtonVerOtraSala" Text="Ver otra sala" OnClick="ButtonVolverListaSalas_Click" />
                    <asp:Button runat="server" ID="ButtonAgregarHoras" CssClass="botones" Text="Aceptar" OnClick="ButtonAgregarHoras_Click" />
                    <br />
                </div>
                <div runat="server" id="divSalas" visible="false">
                    <label>Seleccione una sala, los colores indican la disponibilidad de horas en la sala:</label>
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td>
                                <span class="codigosColores" style="background-color:#FFFFFF"></span>
                                <label>Completamente Disponible</label>
                            </td>
                            <td>
                                <span class="codigosColores" style="background-color:#FFF4B3"></span>
                                <label>Parcialmente Disponible</label>
                            </td>
                            <td>
                                <span class="codigosColores" style="background-color:#FFB7B3"></span>
                                <label>Ocupado</label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:GridView runat="server" ID="GridSalas" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" AutoGenerateColumns="False" DataSourceID="datosSalas" AllowSorting="True" OnRowCommand="gridSalas_RowCommand" DataKeyNames="ID">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                            <asp:BoundField DataField="Sala" HeaderText="Nombre" SortExpression="Sala" />
                            <asp:BoundField DataField="Ubicación" HeaderText="Ubicación" SortExpression="Ubicación" />
                            <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" SortExpression="Capacidad" />
                            <asp:ButtonField ControlStyle-CssClass="botonesTablas" ButtonType="Button" HeaderText="" CommandName="VerCaracteristicasSala" Text="Ver Características" />
                            <asp:ButtonField ControlStyle-CssClass="botonesTablas" ButtonType="Button" HeaderText="" CommandName="SeleccionarSala" Text="Seleccionar" />
                        </Columns>
                        <HeaderStyle CssClass="tablasEncabezados" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="datosSalas" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_salas]"></asp:SqlDataSource>
                    <asp:Button runat="server" ID="ButtonVolverCalendario" CssClass="botones" Text="Ir a calendario" OnClick="ButtonVolverCalendario_Click" />
                </div>
                <div runat="server" class="divModificacion" id="divCaracteristicasSala" visible="false">
                    <label>Nombre:</label>
                    <asp:Label runat="server" ID="labelNombreSala"></asp:Label>
                    <br />
                    <br />
                    <label>Ubicación:</label>
                    <asp:Label runat="server" ID="labelUbicacionSala"></asp:Label>
                    <br />
                    <br />
                    <label>Capacidad:</label>
                    <asp:Label runat="server" ID="labelCapacidadSala"></asp:Label>
                    <br />
                    <br />
                    <label>Características:</label>
                    <br />
                    <asp:Label runat="server" ID="labelCaracteristicasSala"></asp:Label>
                    <br />
                    <br />
                    <asp:Label runat="server" ID="labelDescripcionSala"></asp:Label>
                    <label>Imágenes:</label>
                    <br />
                    <br />
                    <br />
                    <asp:Button runat="server" CssClass="botones" ID="ButtonVolverListaSalas" Text="Ver otra sala" OnClick="ButtonVolverListaSalas_Click" />
                </div>
                <div runat="server" id="divEventos" class="divModificacion" visible="false">
                    <label runat="server" id="labelNombreEvento">Nombre del evento:</label>
                    <br />
                    <asp:TextBox runat="server" ID="TextBoxNombreEvento" CssClass="cajasTexto"></asp:TextBox>
                    <br />
                    <br />
                    <label>Otros servicios:</label>
                    <br />
                    <br />
                    <label>Grabación del evento</label>
                    <asp:CheckBox runat="server" ID="CheckBoxGrabar" />
                    <br />
                    <br />
                    <label>Transmisión</label>
                    <asp:CheckBox runat="server" ID="CheckBoxTransmitir" />
                    <br />
                    <br />
                    <label>Videoconferencia</label>
                    <asp:CheckBox runat="server" ID="CheckBoxVideoConferencia" />
                    <br />
                    <br />
                    <asp:Button runat="server" ID="ButtonAgregarEvento" CssClass="botones" Text="Aceptar" OnClick="ButtonAgregarEvento_Click" />
                    <asp:Button runat="server" ID="ButtonVolverSalas" CssClass="botones" Text="Regresar" OnClick="ButtonVolverHoras_Click" />
                    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="panelActualizacion" ID="UpdateProgressEspera">
                        <ProgressTemplate>
                            <br />
                            <br />
                            <img src="../Imagenes/animacionEspera.svg" />
                            <label id="labelEspera">Enviando solicitud</label>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <div runat="server" id="divMensajes" class="divModificacion" visible="false">
                    <asp:Label runat="server" ID="LabelMensaje"></asp:Label>
                    <br />
                    <br />
                    <asp:Button runat="server" ID="ButtonConfirmar" CssClass="botones" Text="Imprimir Solicitud" OnClick="ButtonConfirmar_Click" OnClientClick="window.open('Impresion.aspx')" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

