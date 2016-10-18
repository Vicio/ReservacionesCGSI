using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetProcsTableAdapters;

public partial class Principal_Materiales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Session["Permisos"].Equals("A"))
            Response.Redirect("../Principal/Inicio.aspx");
    }
    protected void listaSeleccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listaSeleccion.SelectedValue.Equals("Ver Materiales Por Cantidad"))
            MostrarTablaMaterialesPorCantidad();
        if (listaSeleccion.SelectedValue.Equals("Ver Materiales"))
            MostrarTablaMateriales();
        if (listaSeleccion.SelectedValue.Equals("Ver Marcas"))
            MostrarTablaMarcasMateriales();
        if (listaSeleccion.SelectedValue.Equals("Ver Modelos"))
            MostrarTablaModelosMateriales();
        if (listaSeleccion.SelectedValue.Equals("Ver Tipos"))
            MostrarTablaTiposMateriales();
    }

    private void MostrarTablaMaterialesPorCantidad()
    {
        divMaterialCantidad.Visible = true;
        divMaterial.Visible = false;
        divMarcas.Visible = false;
        divModelos.Visible = false;
        divTipos.Visible = false;
        divModificarMaterial.Visible = false;
        divMensajeConfirmacion.Visible = false;
    }

    private void MostrarTablaMateriales()
    {
        divMaterialCantidad.Visible = false;
        divMaterial.Visible = true;
        divMarcas.Visible = false;
        divModelos.Visible = false;
        divTipos.Visible = false;
        divModificarMaterial.Visible = false;
        divMensajeConfirmacion.Visible = false;
    }

    private void MostrarTablaTiposMateriales()
    {
        divMaterialCantidad.Visible = false;
        divMaterial.Visible = false;
        divMarcas.Visible = false;
        divModelos.Visible = false;
        divTipos.Visible = true;
        divMensajeConfirmacion.Visible = false;
    }

    private void MostrarTablaModelosMateriales()
    {
        divMaterialCantidad.Visible = false;
        divMaterial.Visible = false;
        divMarcas.Visible = false;
        divModelos.Visible = true;
        divTipos.Visible = false;
        divMensajeConfirmacion.Visible = false;
    }

    private void MostrarTablaMarcasMateriales()
    {
        divMaterialCantidad.Visible = false;
        divMaterial.Visible = false;
        divMarcas.Visible = true;
        divModelos.Visible = false;
        divTipos.Visible = false;
        divMensajeConfirmacion.Visible = false;
    }

    private void MostrarModificarMaterial()
    {
        divMaterialCantidad.Visible = false;
        divMaterial.Visible = false;
        divMarcas.Visible = false;
        divModelos.Visible = false;
        divTipos.Visible = false;
        divModificarMaterial.Visible = true;
        divMensajeConfirmacion.Visible = false;
    }

    private void MostrarDivMensaje()
    {
        divMaterialCantidad.Visible = false;
        divMaterial.Visible = false;
        divMarcas.Visible = false;
        divModelos.Visible = false;
        divTipos.Visible = false;
        divModificarMaterial.Visible = false;
        divMensajeConfirmacion.Visible = true;
    }

    protected void ButtonAgregarTipoMaterial_Click(object sender, EventArgs e)
    {
        if (!TextBoxAgregarTipo.Text.Trim().Equals(""))
            try
            {
                ProcsTableAdapter procesos = new ProcsTableAdapter();
                SQLInjects injects = new SQLInjects();
                string tipo = injects.Remover(TextBoxAgregarTipo.Text);
                procesos.agregar_tipo_material(tipo);
                mensajeTipo.InnerHtml = "Agregado correctamente";
                GridTipos.DataBind();
                if (listaTipos.Visible)
                    listaTipos.DataBind();
                TextBoxAgregarTipo.Text = "";
            }
            catch (Exception err)
            {
                mensajeTipo.InnerHtml = "Hubo un error al agregar el dato" + err.Message;
            }
        else
        {
            mensajeTipo.InnerHtml = "Escriba algun tipo de material";
            TextBoxAgregarTipo.Text = "";
        }
    }

    protected void ButtonAgregarModelo_Click(object sender, EventArgs e)
    {
        if (!TextBoxAgregarModelo.Text.Trim().Equals(""))
            try
            {
                ProcsTableAdapter procesos = new ProcsTableAdapter();
                SQLInjects injects = new SQLInjects();
                string modelo = injects.Remover(TextBoxAgregarModelo.Text);
                procesos.agregar_modelo_material(modelo);
                mensajeModelo.InnerHtml = "Agregado correctamente";
                GridModelos.DataBind();
                if (listaModelos.Visible)
                    listaModelos.DataBind();
                TextBoxAgregarModelo.Text = "";
            }
            catch (Exception err)
            {
                mensajeModelo.InnerHtml = "Hubo un error al agregar el dato" + err.Message;
            }
        else
        {
            mensajeModelo.InnerHtml = "Escriba algun modelo";
            TextBoxAgregarModelo.Text = "";
        }
    }
    protected void ButtonAgregarMarca_Click(object sender, EventArgs e)
    {
        if (!TextBoxAgregarMarca.Text.Trim().Equals(""))
            try
            {
                ProcsTableAdapter procesos = new ProcsTableAdapter();
                SQLInjects injects = new SQLInjects();
                string marca = injects.Remover(TextBoxAgregarMarca.Text);
                procesos.agregar_marca_material(marca);
                mensajeMarca.InnerHtml = "Agregado correctamente";
                GridMarcas.DataBind();
                if (listaMarcas.Visible)
                    listaMarcas.DataBind();
                TextBoxAgregarMarca.Text = "";
            }
            catch (Exception err)
            {
                mensajeMarca.InnerHtml = "Hubo un error al agregar el dato" + err.Message;
            }
        else
        {
            mensajeMarca.InnerHtml = "Escriba alguna marca";
            TextBoxAgregarMarca.Text = "";
        }

    }
    protected void GridMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ModificarMaterial"))
        {
            MostrarModificarMaterial();
            ButtonAgregar.Visible = false;
            ButtonModificar.Visible = true;
            labelTextoID.Visible = true;
            labelID.Visible = true;
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = GridMaterial.Rows[indice];
            labelID.Text = HttpUtility.HtmlDecode(GridMaterial.DataKeys[indice].Values["ID"].ToString());
            TextBoxNumSerie.Text = HttpUtility.HtmlDecode(fila.Cells[1].Text);
            TextBoxCaracteristicas.Text = HttpUtility.HtmlDecode(fila.Cells[5].Text);
            listaTipos.DataBind();
            listaModelos.DataBind();
            listaMarcas.DataBind();
            for (int i = 0; i < listaTipos.Items.Count; i++)
            {
                if (listaTipos.Items[i].Text == HttpUtility.HtmlDecode(fila.Cells[2].Text))
                {
                    listaTipos.Items[i].Selected = true;
                }
                else
                    listaTipos.Items[i].Selected = false;
            }
            for (int i = 0; i < listaMarcas.Items.Count; i++)
            {
                if (listaMarcas.Items[i].Text == HttpUtility.HtmlDecode(fila.Cells[3].Text))
                {
                    listaMarcas.Items[i].Selected = true;
                }
                else
                    listaMarcas.Items[i].Selected = false;
            }
            for (int i = 0; i < listaModelos.Items.Count; i++)
            {
                if (listaModelos.Items[i].Text == HttpUtility.HtmlDecode(fila.Cells[4].Text))
                {
                    listaModelos.Items[i].Selected = true;
                }
                else
                    listaModelos.Items[i].Selected = false;
            }

        }
    }
    protected void ButtonAgregar_Click(object sender, EventArgs e)
    {
        SQLInjects injects = new SQLInjects();
        ProcsTableAdapter procesos = new ProcsTableAdapter();

        try
        {
            string numSerie = injects.Remover(TextBoxNumSerie.Text);
            string caracteristicas = injects.Remover(TextBoxCaracteristicas.Text);
            int tipo = Convert.ToInt32(listaTipos.SelectedValue);
            int marca = Convert.ToInt32(listaMarcas.SelectedValue);
            int modelo = Convert.ToInt32(listaModelos.SelectedValue);
            procesos.agregar_material(numSerie, tipo, marca, modelo, caracteristicas);
            LabelMensaje.Text = "Material agregado correctamente";
            MostrarDivMensaje();
        }
        catch(Exception err)
        {
            LabelMensaje.Text = "Ocurrió un error al agregar el material: " + err.Message;
        }
    }
    protected void ButtonModificar_Click(object sender, EventArgs e)
    {
        SQLInjects injects = new SQLInjects();
        ProcsTableAdapter procesos = new ProcsTableAdapter();

        try
        {
            string numSerie = injects.Remover(TextBoxNumSerie.Text);
            string caracteristicas = injects.Remover(TextBoxCaracteristicas.Text);
            int tipo = Convert.ToInt32(listaTipos.SelectedValue);
            int marca = Convert.ToInt32(listaMarcas.SelectedValue);
            int modelo = Convert.ToInt32(listaModelos.SelectedValue);
            int idMaterial = Convert.ToInt32(labelID.Text);
            procesos.modificar_material(idMaterial, numSerie, tipo, marca, modelo, caracteristicas);
            LabelMensaje.Text = "Material modificado correctamente";
            MostrarDivMensaje();
        }
        catch (Exception err)
        {
            LabelMensaje.Text = "Ocurrió un error al modificar el material: " + err.Message;

        }
    }
    protected void ButtonCancelar_Click(object sender, EventArgs e)
    {
        TextBoxCaracteristicas.Text = "";
        TextBoxNumSerie.Text = "";
        MostrarTablaMateriales();
    }
    protected void ButtonAgregarMaterial_Click(object sender, EventArgs e)
    {
        MostrarModificarMaterial();
        ButtonAgregar.Visible = true;
        ButtonModificar.Visible = false;

    }
    protected void ButtonConfirmar_Click(object sender, EventArgs e)
    {
        TextBoxCaracteristicas.Text = "";
        TextBoxNumSerie.Text = "";
        MostrarTablaMateriales();
        GridMaterial.DataBind();
    }
}