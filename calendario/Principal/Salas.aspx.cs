using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetProcsTableAdapters;
using DataSetFuncsTableAdapters;

public partial class Principal_Salas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Session["Permisos"].Equals("A"))
            Response.Redirect("../Principal/Inicio.aspx");
    }
    protected void GridViewSalas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ModificarSala"))
        {
            FuncsTableAdapter funciones = new FuncsTableAdapter();
            mostrarModificacionSalas();
            ButtonAceptarAgregarSala.Visible = false;
            ButtonAceptarModificarSala.Visible = true;
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = GridViewSalas.Rows[indice];
            labelID.Text = HttpUtility.HtmlDecode(GridViewSalas.DataKeys[indice].Values["ID"].ToString());
            TextBoxNombreSala.Text = HttpUtility.HtmlDecode(fila.Cells[1].Text);
            TextBoxUbicacionSala.Text = HttpUtility.HtmlDecode(fila.Cells[2].Text);
            TextBoxCapacidadSala.Text = HttpUtility.HtmlDecode(fila.Cells[3].Text);
            TextBoxCaracteristicasSala.Text = funciones.ver_caracteristicas_sala(Convert.ToInt32(labelID.Text));
            labelTextoID.Visible = true;
        }
    }
    protected void ButtonAgregarSala_Click(object sender, EventArgs e)
    {
        mostrarModificacionSalas();
        ButtonAceptarAgregarSala.Visible = true;
        ButtonAceptarModificarSala.Visible = false;
    }
    protected void listaSeleccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listaSeleccion.SelectedValue.Equals("Ver Salas"))
            mostrarSalas();
        if (listaSeleccion.SelectedValue.Equals("Ver Encargados"))
            mostrarEncargados();
    }
    protected void ButtonCancelarAccionSala_Click(object sender, EventArgs e)
    {
        mostrarSalas();
        limpiarCamposAgregarSalas();
    }
    protected void ButtonAceptarModificarSala_Click(object sender, EventArgs e)
    {
        try
        {
            ProcsTableAdapter procesos = new ProcsTableAdapter();
            procesos.modificar_sala
                (
                    Convert.ToInt32(labelID.Text),
                    TextBoxNombreSala.Text,
                    TextBoxUbicacionSala.Text,
                    Convert.ToInt32(TextBoxCapacidadSala.Text),
                    TextBoxCaracteristicasSala.Text
                );
            mostrarConfirmacion();
            LabelMensaje.Text = "Sala modificada correctamente.";
        }
        catch (FormatException err)
        {
            labelError.InnerHtml = "Escribir la capacidad en formato numérico";
        }
        catch (System.Data.SqlClient.SqlException err)
        {
            labelError.InnerHtml = "Error de conexión, pruebe de nuevo más tarde";
        }


    }
    protected void ButtonAceptarAgregarSala_Click(object sender, EventArgs e)
    {
        try
        {
            ProcsTableAdapter procesos = new ProcsTableAdapter();
            procesos.agregar_sala(TextBoxNombreSala.Text, TextBoxUbicacionSala.Text, Convert.ToInt32(TextBoxCapacidadSala.Text), TextBoxCaracteristicasSala.Text);
            mostrarConfirmacion();
            LabelMensaje.Text = "Sala agregada correctamente.";

        }
        catch(FormatException err)
        {
            labelError.InnerHtml = "Escribir la capacidad en formato numérico";
        }
        catch(System.Data.SqlClient.SqlException err)
        {
            labelError.InnerHtml = "Error de conexión, pruebe de nuevo más tarde";
        }

    }
    private void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Text == previousRow.Cells[i].Text)
                {
                    row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                    previousRow.Cells[i].Visible = false;
                }
            }
        }
    }
    protected void ButtonConfirmar_Click(object sender, EventArgs e)
    {
        if(LabelMensaje.Text.StartsWith("Encargado"))
        {
            mostrarEncargados();
            GridViewEncargados.DataBind();

        }
        else
        {
            mostrarSalas();
            GridViewSalas.DataBind();
        }
    }
    protected void GridViewEncargados_PreRender(object sender, EventArgs e)
    {
        MergeRows(GridViewEncargados);
    }
    protected void ButtonAsignarEncargado_Click(object sender, EventArgs e)
    {
        mostrarAsignacionEncargados();
    }

    private void mostrarAsignacionEncargados()
    {
        divSalas.Visible = false;
        divModificacionSalas.Visible = false;
        divMensajeConfirmacion.Visible = false;
        divEncargados.Visible = false;
        divAsignarEncargados.Visible = true;
    }

    private void mostrarConfirmacion()
    {
        divMensajeConfirmacion.Visible = true;
        divModificacionSalas.Visible = false;
        divSalas.Visible = false;
        divEncargados.Visible = false;
        divAsignarEncargados.Visible = false;
    }

    private void mostrarSalas()
    {
        divSalas.Visible = true;
        divModificacionSalas.Visible = false;
        divMensajeConfirmacion.Visible = false;
        divAsignarEncargados.Visible = false;
        divEncargados.Visible = false;
    }

    private void mostrarEncargados()
    {
        divSalas.Visible = false;
        divModificacionSalas.Visible = false;
        divMensajeConfirmacion.Visible = false;
        divEncargados.Visible = true;
        divAsignarEncargados.Visible = false;
    }

    private void mostrarModificacionSalas()
    {
        divSalas.Visible = false;
        divModificacionSalas.Visible = true;
        divMensajeConfirmacion.Visible = false;
        divEncargados.Visible = false;
        divAsignarEncargados.Visible = false;
    }

    private void limpiarCamposAgregarSalas()
    {
        TextBoxNombreSala.Text = "";
        TextBoxUbicacionSala.Text = "";
        TextBoxCapacidadSala.Text = "";
        TextBoxCaracteristicasSala.Text = "";
        labelID.Text = "";
        labelTextoID.Visible = false;
    }
    protected void ButtonCancelarAsignarEncargado_Click(object sender, EventArgs e)
    {
        mostrarEncargados();

    }
    protected void ButtonAceptarAsignarEncargado_Click(object sender, EventArgs e)
    {
        ProcsTableAdapter procesos = new ProcsTableAdapter();
        procesos.asignar_encargado_sala
            (
                Convert.ToInt32(DropDownListEncargados.SelectedValue),
                Convert.ToInt32(DropDownListSalas.SelectedValue)
            );
        mostrarConfirmacion();
        LabelMensaje.Text = "Encargado asignado correctamente";
    }
}