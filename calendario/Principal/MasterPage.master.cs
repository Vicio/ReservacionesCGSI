using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetProcsTableAdapters;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logged"].Equals("No"))
            Response.Redirect("../Login/Inicio.aspx");
        mensajeBienvenido.Text = "Bienvenido " + (string)Session["Usuario"];

        if (Session["Permisos"].Equals("A"))
        {
            linkReservaciones.Visible = true;
            linkReportes.Visible = true;
            linkCuentas.Visible = true;
            linkEventos.Visible = true;
            linkSalas.Visible = true;
            linkMateriales.Visible = true;
            linkEscuelas.Visible = true;
        }
        if (Session["Permisos"].Equals("E"))
        {
            linkReservaciones.Visible = true;
            linkReportes.Visible = true;
            linkEventos.Visible = true;
        }
        if (Session["Permisos"].Equals("U"))
        {
            linkReservaciones.Visible = true;
        }

    }
    protected void linkCierreSesion_Click(object sender, EventArgs e)
    {
        ProcsTableAdapter procesos = new ProcsTableAdapter();

        procesos.validar_salida_usuario((int) Session["ID"]);

        Session["Logged"] = "No";
        Session["Usuario"] = "";
        Session["Permisos"] = "";
        Session["Correo"] = "";
        Session["ID"] = 0;

        Response.Redirect("../Login/Inicio.aspx");
    }

}
