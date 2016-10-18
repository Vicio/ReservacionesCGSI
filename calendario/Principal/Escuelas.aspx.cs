using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetProcsTableAdapters;
using DataSetFuncsTableAdapters;

public partial class Principal_Escuelas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Session["Permisos"].Equals("A"))
            Response.Redirect("../Principal/Inicio.aspx");
        LabelMensaje.Text = "";
        if ((bool)Session["ModificacionUsuario"])
        {
            divModificarEscuela.Visible = true;
            divEscuelas.Visible = false;
            divModificarEscuela.Visible = true;
            ButtonAgregar.Visible = true;
            ButtonModificar.Visible = false;
        }
    }
    protected void ButtonAgregarEscuela_Click(object sender, EventArgs e)
    {
        divEscuelas.Visible = false;
        divModificarEscuela.Visible = true;
        ButtonAgregar.Visible = true;
        ButtonModificar.Visible = false;
    }
    protected void ButtonConfirmar_Click(object sender, EventArgs e)
    {
        if ((bool)Session["ModificacionUsuario"])
            Response.Redirect("Cuentas.aspx");
        divEscuelas.Visible = true;
        divModificarEscuela.Visible = false;
        divMensajeConfirmacion.Visible = false;
        GridEscuelas.DataBind();
    }
    protected void ButtonAgregar_Click(object sender, EventArgs e)
    {
        SQLInjects injects = new SQLInjects();
        ProcsTableAdapter procesos = new ProcsTableAdapter();
        string escuela = injects.Remover(TextBoxEscuela.Text);
        string direccion = injects.Remover(TextBoxDireccion.Text);
        string telefono = injects.Remover(TextBoxTelefono.Text);
        try
        {
            procesos.agregar_escuela(escuela, direccion, telefono);
            LabelMensaje.Text = "Escuela Agregada Correctamente <br />";
            divMensajeConfirmacion.Visible = true;
            divModificarEscuela.Visible = false;
        }
        catch (Exception err)
        {
            LabelMensaje.Text = "Error al agregar la escuela <br />" + err.Message;
        }
    }
    protected void GridEscuelas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ModificarEscuela"))
        {
            divModificarEscuela.Visible = true;
            labelTextoID.Visible = true;
            labelID.Visible = true;
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = GridEscuelas.Rows[indice];
            labelID.Text = HttpUtility.HtmlDecode(GridEscuelas.DataKeys[indice].Values["ID"].ToString());
            TextBoxEscuela.Text = HttpUtility.HtmlDecode(fila.Cells[1].Text);
            TextBoxDireccion.Text = HttpUtility.HtmlDecode(fila.Cells[2].Text);
            TextBoxTelefono.Text = HttpUtility.HtmlDecode(fila.Cells[3].Text);
            divEscuelas.Visible = false;
            ButtonAgregar.Visible = false;
            ButtonModificar.Visible = true;
        }
    }
    protected void ButtonModificar_Click(object sender, EventArgs e)
    {
        SQLInjects injects = new SQLInjects();
        ProcsTableAdapter procesos = new ProcsTableAdapter();
        int id = Convert.ToInt32(labelID.Text);
        string escuela = injects.Remover(TextBoxEscuela.Text);
        string direccion = injects.Remover(TextBoxDireccion.Text);
        string telefono = injects.Remover(TextBoxTelefono.Text);
        try
        {
            procesos.modificar_escuela(id, escuela, direccion, telefono);
            LabelMensaje.Text = "Escuela Modificada Correctamente <br />";
            divMensajeConfirmacion.Visible = true;
            divModificarEscuela.Visible = false;
        }
        catch (Exception err)
        {
            LabelMensaje.Text = "Error al modificar los datos <br />" + err.Message;
        }

    }
    protected void ButtonCancelar_Click(object sender, EventArgs e)
    {
        if ((bool)Session["ModificacionUsuario"])
            Response.Redirect("Cuentas.aspx");
        divModificarEscuela.Visible = false;
        divEscuelas.Visible = true;
        TextBoxDireccion.Text = "";
        TextBoxEscuela.Text = "";
        TextBoxTelefono.Text = "";
        labelID.Visible = false;
        labelTextoID.Visible = false;
    }
}