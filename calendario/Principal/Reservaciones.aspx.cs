using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetFuncsTableAdapters;
using DataSetProcsTableAdapters;
using System.Drawing;

public partial class Calendario_Reservaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Calendario.EnableViewState = false;
    }
    protected void Calendario_DayRender(object sender, DayRenderEventArgs e)
    {
        GridSalas.DataBind();
        if (e.Day.IsToday)
        {
            e.Cell.BackColor = System.Drawing.Color.FromArgb(83, 179, 206);
        }

        Calendario.SelectedDates.Clear();

        if (Session["FechasSeleccionadas"] != null)
        {
            List<DateTime> listaFechas = (List<DateTime>)Session["FechasSeleccionadas"];
            foreach (DateTime fecha in listaFechas)
            {
                Calendario.SelectedDates.Add(fecha);
            }
        }

        colorearFechas(sender, e);

    }

    protected void colorearFechas(object sender, DayRenderEventArgs e)
    {
        if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            e.Cell.BackColor = System.Drawing.Color.FromArgb(144, 146, 149);
            e.Day.IsSelectable = false;
        }
        if (e.Day.Date < DateTime.Today)
        {
            e.Cell.BackColor = System.Drawing.Color.FromArgb(144, 146, 149);
            e.Day.IsSelectable = false;
        }
        if (e.Day.Date > DateTime.Today)
        {
            string fecha = e.Day.Date.Month + "/" + e.Day.Date.Day + "/" + e.Day.Date.Year;
            regresar_horas_ocupadasTableAdapter horas = new regresar_horas_ocupadasTableAdapter();
            int[] salas = new int[GridSalas.Rows.Count];

            for (int i = 0; i < salas.Length; i++)
            {
                salas[i] = Convert.ToInt32(GridSalas.DataKeys[i].Values["ID"]);
            }

            int horasLibres = salas.Length * ListBoxHoras.Items.Count;

            int horasOcupadas = 0;

            for (int i = 0; i < salas.Length; i++)
            {
                horasOcupadas += horas.GetData(fecha, salas[i]).Rows.Count;
            }

            horasLibres -= horasOcupadas;

            e.Cell.BackColor = System.Drawing.Color.FromArgb(151, 216, 108);
            if (horasLibres < salas.Length * ListBoxHoras.Items.Count)
            {
                e.Cell.BackColor = System.Drawing.Color.FromArgb(242, 219, 85);
            }
            if (horasLibres <= 0)
                e.Cell.BackColor = System.Drawing.Color.FromArgb(238, 52, 35);
            if (e.Day.IsSelected)
            {
                e.Cell.BackColor = Color.FromArgb(0, 101, 164);
                e.Cell.ForeColor = Color.White;
            }

        }
    }
    protected void Calendario_SelectionChanged(object sender, EventArgs e)
    {
        if (Calendario.SelectedDate >= DateTime.Today)
        {
            List<DateTime> listaFechas = (List<DateTime>)Session["FechasSeleccionadas"];
            if (listaFechas.Contains(Calendario.SelectedDate))
                listaFechas.Remove(Calendario.SelectedDate);
            else
                listaFechas.Add(Calendario.SelectedDate);

            Session["FechasSeleccionadas"] = listaFechas;
        }

    }
    protected void ButtonAgregarHoras_Click(object sender, EventArgs e)
    {

        ArrayList horasSeleccionadas = new ArrayList();
        for (int i = 0; i < ListBoxHoras.Items.Count; i++ )
        {
            if(ListBoxHoras.Items[i].Selected)
                horasSeleccionadas.Add(ListBoxHoras.Items[i].Value);
        }

        Session["HorasSeleccionadas"] = horasSeleccionadas;
        if (horasSeleccionadas.Count > 0)
            mostrarEventos();
        else
            LabelMensajeHoras.InnerHtml = "Debe seleccionar una hora";

    }
    protected void gridSalas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("VerCaracteristicasSala"))
        {
            mostrarCaracteristicasSala();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = GridSalas.Rows[indice];
            int idSala = Convert.ToInt32(HttpUtility.HtmlDecode(GridSalas.DataKeys[indice].Values["ID"].ToString()));
            labelNombreSala.Text = HttpUtility.HtmlDecode(fila.Cells[1].Text);
            labelUbicacionSala.Text = HttpUtility.HtmlDecode(fila.Cells[2].Text);
            labelCapacidadSala.Text = HttpUtility.HtmlDecode(fila.Cells[3].Text);

            try
            {
                FuncsTableAdapter funciones = new FuncsTableAdapter();

                labelCaracteristicasSala.Text = funciones.ver_caracteristicas_sala(idSala);
            }
            catch(Exception err)
            {

            }

        }
        if(e.CommandName.Equals("SeleccionarSala"))
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            Session["SalaSeleccionada"] = Convert.ToInt32(HttpUtility.HtmlDecode(GridSalas.DataKeys[indice].Values["ID"].ToString()));
            Session["NombreSalaSeleccionada"] = HttpUtility.HtmlDecode(GridSalas.Rows[indice].Cells[1].Text);
            Session["UbicacionSala"] = HttpUtility.HtmlDecode(GridSalas.Rows[indice].Cells[2].Text);
            Session["CapacidadSala"] = HttpUtility.HtmlDecode(GridSalas.Rows[indice].Cells[3].Text);
            List<DateTime> listaFechas = (List<DateTime>)Session["FechasSeleccionadas"];
            regresar_horas_ocupadasTableAdapter horasOcupadas = new regresar_horas_ocupadasTableAdapter();
            DataSetFuncs.regresar_horas_ocupadasDataTable horas;
            mostrarSeleccionHoras();
            for (int k = 0; k < listaFechas.Count; k++)
            {
                horas = horasOcupadas.GetData(listaFechas[k].Month + "/" + listaFechas[k].Day + "/" + listaFechas[k].Year, Convert.ToInt32(Session["SalaSeleccionada"]));
                for (int i = 0; i < horas.Rows.Count; i++)
                {
                    for (int j = 0; j < ListBoxHoras.Items.Count; j++)
                    {
                        if (Convert.ToInt32(ListBoxHoras.Items[j].Value) == horas[i].horas.Hours)
                        {
                            ListBoxHoras.Items[j].Enabled = false;
                        }
                    }

                }
            }
            if (Calendario.SelectedDate.Day == DateTime.Now.Day)
                for(int i = 0; i < ListBoxHoras.Items.Count; i++)
                    if (Convert.ToInt32(ListBoxHoras.Items[i].Value) <= DateTime.Now.Hour)
                        ListBoxHoras.Items[i].Enabled = false;

        }
    }
    protected void ButtonVolverListaSalas_Click(object sender, EventArgs e)
    {
        mostrarSeleccionSalas();
        for (int i = 0; i < ListBoxHoras.Items.Count; i++)
            ListBoxHoras.Items[i].Enabled = true;
    }
    protected void ButtonAgregarEvento_Click(object sender, EventArgs e)
    {
        ProcsTableAdapter procesos = new ProcsTableAdapter();
        FuncsTableAdapter funciones = new FuncsTableAdapter();
        List<DateTime> fechaSeleccionada = (List<DateTime>)Session["FechasSeleccionadas"];
        ArrayList horasSeleccionadas = (ArrayList)Session["HorasSeleccionadas"];
        bool success = true;

        procesos.agregar_evento
            (
                (int)Session["ID"],
                (int)Session["SalaSeleccionada"],
                TextBoxNombreEvento.Text,
                CheckBoxGrabar.Checked,
                CheckBoxTransmitir.Checked,
                CheckBoxVideoConferencia.Checked,
                false
            );

        int idEvento = (int)funciones.obtener_id_evento_agregado
            (
                (int)Session["SalaSeleccionada"],
                (int)Session["ID"],
                TextBoxNombreEvento.Text
            );

        obtener_datos_validacion_usuarioTableAdapter datosUsuario = new obtener_datos_validacion_usuarioTableAdapter();

        var datosUsu = datosUsuario.GetData(funciones.obtener_hash_usuario(Session["Correo"].ToString()));


        for (int j = 0; j < fechaSeleccionada.Count; j++)
        {
            for (int i = 0; i < horasSeleccionadas.Count; i++)
            {
                TimeSpan horaSeleccionada = TimeSpan.FromHours(Convert.ToDouble(horasSeleccionadas[i]));
                try
                {
                    if (TextBoxNombreEvento.Text.Trim().Equals(""))
                    {
                        labelNombreEvento.InnerHtml = "Nombre del evento (escriba un nombre para el evento):";
                    }
                    else
                    {
                        procesos.agregar_detalle_evento
                            (
                                idEvento,
                                fechaSeleccionada[j],
                                horaSeleccionada
                            );
                    }
                }
                catch (Exception err)
                {
                    mostrarMensaje();
                    LabelMensaje.Text = "Ocurrió un error al agregar el evento";
                    success = false;
                }
            }

        }

        if(success)
        {
            Correo correo = new Correo();
            obtener_administradoresTableAdapter admins = new obtener_administradoresTableAdapter();
            for (int i = 0; i < admins.GetData().Rows.Count; i++)
            {
                string nombreAdmin = admins.GetData()[i].nombre_usuario + " " + admins.GetData()[i].apellido_usuario;
                string correoAdmin = admins.GetData()[i].correo_usuario;
                correo.enviarEventoAdministrador
                    (
                        correoAdmin,
                        nombreAdmin,
                        datosUsu[0].nombre_usuario,
                        datosUsu[0].apellido_usuario,
                        TextBoxNombreEvento.Text,
                        Session["NombreSalaSeleccionada"].ToString(),
                        fechaSeleccionada,
                        horasSeleccionadas,
                        idEvento.ToString()
                    );

            }
            Session["NombreEvento"] = TextBoxNombreEvento.Text;
            Session["Solicitante"] = datosUsu[0].nombre_usuario + " " + datosUsu[0].apellido_usuario;
            Session["HashEvento"] = idEvento.ToString() + ":" + PasswordHash.PasswordHash.CreateHash(idEvento.ToString());
            horasSeleccionadas.Insert(0, -1);
            horasSeleccionadas.Add(-1);
            Session["HorasSeleccionadas"] = horasSeleccionadas;

            if (CheckBoxGrabar.Checked)
                Session["Grabacion"] = "Si";
            if (CheckBoxTransmitir.Checked)
                Session["Streaming"] = "Si";
            if (CheckBoxVideoConferencia.Checked)
                Session["VideoConferencia"] = "Si";
            mostrarMensaje();
            LabelMensaje.Text = "El evento ha sido registrado correctamente, imprima 3 copias de su solicitud para aprobar su registro. <br />"
                + "El periodo de entrega de la solicitud es de 3 días, posterior a eso el registro se perderá y necesitará solicitarlo de nuevo.";
        }


    }
    protected void ButtonCancelar_Click(object sender, EventArgs e)
    {
        Session["FechaSeleccionada"] = "";
        Session["HorasSeleccionadas"] = "";
        Session["SalaSeleccionada"] = 0;
        Session["NombreEvento"] = "";
        Session["GrabacionEvento"] = false;
        Session["TransmisionEvento"] = false;
        Session["VideoconferenciaEvento"] = false;
        mostrarCalendario();

    }

    protected void ListBoxHoras_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void mostrarCalendario()
    {
        divCalendario.Visible = true;
        divHora.Visible = false;
        divSalas.Visible = false;
        divEventos.Visible = false;
        divMensajes.Visible = false;
        Calendario.SelectedDates.Remove(Calendario.SelectedDate);
    }

    private void mostrarSeleccionHoras()
    {
        divCalendario.Visible = false;
        divHora.Visible = true;
        divSalas.Visible = false;
        divEventos.Visible = false;
        divMensajes.Visible = false;
    }

    private void mostrarSeleccionSalas()
    {
        divCalendario.Visible = false;
        divHora.Visible = false;
        divSalas.Visible = true;
        divEventos.Visible = false;
        divCaracteristicasSala.Visible = false;
        divMensajes.Visible = false;
    }

    private void limpiarColoresSalas()
    {
        for(int i = 0; i < GridSalas.Rows.Count; i++)
        {
            GridSalas.Rows[i].BackColor = System.Drawing.Color.White;
        }
    }
    private void mostrarCaracteristicasSala()
    {
        divCalendario.Visible = false;
        divHora.Visible = false;
        divSalas.Visible = false;
        divEventos.Visible = false;
        divCaracteristicasSala.Visible = true;
        divMensajes.Visible = false;
    }

    private void mostrarEventos()
    {
        divCalendario.Visible = false;
        divHora.Visible = false;
        divSalas.Visible = false;
        divEventos.Visible = true;
        divMensajes.Visible = false;

    }

    private void mostrarMensaje()
    {
        divCalendario.Visible = false;
        divHora.Visible = false;
        divSalas.Visible = false;
        divEventos.Visible = false;
        divMensajes.Visible = true;

    }

    protected void ButtonConfirmar_Click(object sender, EventArgs e)
    {
        mostrarCalendario();
    }
    protected void ButtonVolverCalendario_Click(object sender, EventArgs e)
    {
        mostrarCalendario();
    }
    protected void ButtonVolverHoras_Click(object sender, EventArgs e)
    {
        mostrarSeleccionHoras();
    }

    protected void ButtonAgregarDias_Click(object sender, EventArgs e)
    {
        if(((List<DateTime>)Session["FechasSeleccionadas"]).Count > 0)
        {
            mostrarSeleccionSalas();
            labelDiasSeleccionados.InnerHtml = "";
        }
        else
        {
            labelDiasSeleccionados.InnerHtml = "Seleccione al menos un día.";
        }
    }
}