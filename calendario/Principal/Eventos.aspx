<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Eventos.aspx.cs" Inherits="Principal_Eventos" %>

<asp:Content ID="Contentido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div runat="server" id="divEventos" class="divTablas">
                <asp:GridView ID="vistaEventos" runat="server" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" AutoGenerateColumns="False" PageSize="20" DataSourceID="SqlDataSourceEventos" AllowPaging="True" AllowSorting="True" OnPreRender="vistaEventos_PreRender">
                    <Columns>
                        <asp:BoundField DataField="Evento" HeaderText="Evento" SortExpression="Evento" />
                        <asp:BoundField DataField="Responsable" HeaderText="Responsable" ReadOnly="True" SortExpression="Responsable" />
                        <asp:BoundField DataField="Sala" HeaderText="Sala" SortExpression="Sala" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Hora del Evento" HeaderText="Hora del Evento" SortExpression="Hora del Evento" />
                        <asp:CheckBoxField DataField="Grabación" HeaderText="Grabación" SortExpression="Grabación" />
                        <asp:CheckBoxField DataField="Transmisión" HeaderText="Transmisión" SortExpression="Transmisión" />
                        <asp:CheckBoxField DataField="Videoconferencia" HeaderText="Videoconferencia" SortExpression="Videoconferencia" />
                        <asp:CheckBoxField DataField="Permitido" HeaderText="Permitido" SortExpression="Permitido" />
                    </Columns>
                    <HeaderStyle CssClass="tablasEncabezados" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceEventos" runat="server" ConnectionString="<%$ ConnectionStrings:conexionCalendario %>" SelectCommand="SELECT * FROM [vista_eventos]"></asp:SqlDataSource>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

