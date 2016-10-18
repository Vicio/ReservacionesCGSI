using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetProcsTableAdapters;
using DataSetFuncsTableAdapters;

public partial class Principal_Cuentas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((bool)Session["ModificacionUsuario"])
        {
            if ((int)Session["ModID"] == -1)
                mostrarAgregarUsuario();
            else
                mostrarModificarUsuario();
            TextBoxNombre.Text = Session["ModNombre"].ToString();
            TextBoxApellidos.Text = Session["ModApellido"].ToString();
            TextBoxCorreo.Text = Session["ModCorreo"].ToString();
        }
        if (!Session["Permisos"].Equals("A"))
            Response.Redirect("../Principal/Inicio.aspx");
    }
    protected void GridUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Activar"))
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            ProcsTableAdapter procesos = new ProcsTableAdapter();
            procesos.activar_cuenta_usuario(Convert.ToInt32(GridUsuarios.DataKeys[indice].Values["ID"].ToString()));
            GridUsuarios.DataBind();
        }
        if (e.CommandName.Equals("Desactivar"))
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            ProcsTableAdapter procesos = new ProcsTableAdapter();
            if (!GridUsuarios.DataKeys[indice].Values["ID"].ToString().Equals(Session["ID"].ToString()))
                procesos.desactivar_cuenta_usuario(Convert.ToInt32(GridUsuarios.DataKeys[indice].Values["ID"].ToString()));
            GridUsuarios.DataBind();
        }
        if(e.CommandName.Equals("ModificarUsuario"))
        {
            SQLInjects injects = new SQLInjects();

            int indice = Convert.ToInt32(e.CommandArgument);
            mostrarModificarUsuario();
            GridViewRow fila = GridUsuarios.Rows[indice];

            labelID.Text = HttpUtility.HtmlDecode(GridUsuarios.DataKeys[indice].Values["ID"].ToString());
            TextBoxNombre.Text = HttpUtility.HtmlDecode(fila.Cells[1].Text);
            TextBoxApellidos.Text = HttpUtility.HtmlDecode(fila.Cells[2].Text);
            TextBoxCorreo.Text = HttpUtility.HtmlDecode(fila.Cells[4].Text);
            DropDownListEscuelas.DataBind();
            for (int i = 0; i < DropDownListEscuelas.Items.Count; i++)
            {
                if (DropDownListEscuelas.Items[i].Text == HttpUtility.HtmlDecode(fila.Cells[3].Text))
                {
                    DropDownListEscuelas.Items[i].Selected = true;
                }
                else
                    DropDownListEscuelas.Items[i].Selected = false;
            }

            DropDownListNivelUsuario.SelectedValue = fila.Cells[5].Text;

        }
    }
    protected void ButtonAgregarUsuario_Click(object sender, EventArgs e)
    {
        mostrarAgregarUsuario();
    }
    protected void ButtonAgregar_Click(object sender, EventArgs e)
    {
        SQLInjects injects = new SQLInjects();

        string nombre = injects.Remover(TextBoxNombre.Text);
        string apellido = injects.Remover(TextBoxApellidos.Text);
        string correo = injects.Remover(TextBoxCorreo.Text);
        string hash = PasswordHash.PasswordHash.CreateHash(nombre + apellido);
        ProcsTableAdapter procesos = new ProcsTableAdapter();
        FuncsTableAdapter funciones = new FuncsTableAdapter();
        try
        {
            Correo correoUsuario = new Correo();
            if((bool)funciones.verificar_correo_registrado(correo))
            {
                labelCorreo.InnerHtml = "Correo Electrónico: (Este Correo ya se encuentra registrado)";
            }
            else
            {
                procesos.agregar_usuario(nombre, apellido, Convert.ToInt32(DropDownListEscuelas.SelectedValue), correo, hash, DropDownListNivelUsuario.SelectedValue, false, false);
                correoUsuario.enviarMailCambioPass(correo, nombre, hash);
                LabelMensaje.Text = "Usuario Agregado Correctamente";
                mostrarDivMensaje();
            }
        }
        catch(Exception err)
        {
            LabelMensaje.Text = "Error al agregar el usuario";
            mostrarDivMensaje();
        }

    }
    protected void ButtonModificar_Click(object sender, EventArgs e)
    {
        SQLInjects injects = new SQLInjects();

        string nombre = injects.Remover(TextBoxNombre.Text);
        string apellido = injects.Remover(TextBoxApellidos.Text);
        string correo = injects.Remover(TextBoxCorreo.Text);
        string hash = PasswordHash.PasswordHash.CreateHash(nombre + apellido);
        ProcsTableAdapter procesos = new ProcsTableAdapter();

        try
        {
            procesos.modificar_usuario(Convert.ToInt32(labelID.Text), nombre, apellido, Convert.ToInt32(DropDownListEscuelas.SelectedValue), correo, DropDownListNivelUsuario.SelectedValue);
            LabelMensaje.Text = "Usuario Modificado Correctamente";
            mostrarDivMensaje();
        }
        catch(Exception err)
        {
            LabelMensaje.Text = "Error al modificar los datos";
            mostrarDivMensaje();
        }

    }
    protected void ButtonCancelar_Click(object sender, EventArgs e)
    {
        mostrarTablaUsuarios();
        LimpiarDatosModificacionSesion();
    }

    protected void mostrarTablaUsuarios()
    {
        divUsuarios.Visible = true;
        divModificacionUsuarios.Visible = false;
        TextBoxApellidos.Text = "";
        TextBoxNombre.Text = "";
        TextBoxCorreo.Text = "";
    }

    protected void mostrarAgregarUsuario()
    {
        labelID.Visible = false;
        labelTextoID.Visible = false;
        divUsuarios.Visible = false;
        divModificacionUsuarios.Visible = true;
        ButtonModificar.Visible = false;
        ButtonAgregar.Visible = true;
    }
    protected void mostrarModificarUsuario()
    {
        labelID.Visible = true;
        labelTextoID.Visible = true;
        divUsuarios.Visible = false;
        divModificacionUsuarios.Visible = true;
        ButtonModificar.Visible = true;
        ButtonAgregar.Visible = false;
    }

    protected void mostrarDivMensaje()
    {
        divModificacionUsuarios.Visible = false;
        divMensajeConfirmacion.Visible = true;
        TextBoxApellidos.Text = "";
        TextBoxNombre.Text = "";
        TextBoxCorreo.Text = "";
        
    }
    protected void ButtonConfirmar_Click(object sender, EventArgs e)
    {
        divUsuarios.Visible = true;
        divModificacionUsuarios.Visible = false;
        divMensajeConfirmacion.Visible = false;
        GridUsuarios.DataBind();
    }
    protected void ButtonAgregarEscuela_Click(object sender, EventArgs e)
    {
        Session["ModificacionUsuario"] = true;
        if(labelID.Visible == true)
            Session["ModID"] = Convert.ToInt32(labelID.Text);
        Session["ModNombre"] = TextBoxNombre.Text;
        Session["ModApellido"] = TextBoxApellidos.Text;
        Session["ModCorreo"] = TextBoxCorreo.Text;
        Response.Redirect("Escuelas.aspx");
    }

    protected void LimpiarDatosModificacionSesion()
    {
        Session["ModID"] = -1;
        Session["ModNombre"] = "";
        Session["ModApellido"] = "";
        Session["ModCorreo"] = "";
        Session["ModificacionUsuario"] = false;
    }
}