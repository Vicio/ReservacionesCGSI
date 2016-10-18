<%@ Application Language="C#" %>

<script runat="server">
    DataSetProcsTableAdapters.ProcsTableAdapter procesos = new DataSetProcsTableAdapters.ProcsTableAdapter();

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        Session["Logged"] = "No";
        Session["Usuario"] = "";
        Session["Permisos"] = "";
        Session["Correo"] = "";
        Session["ID"] = 0;
        Session["ModID"] = -1;
        Session["ModNombre"] = "";
        Session["ModApellido"] = "";
        Session["ModCorreo"] = "";
        Session["ModificacionUsuario"] = false;
        Session["tempClaveUsuario"] = -1;
        Session["FechasSeleccionadas"] = new List<DateTime>();
        Session["HorasSeleccionadas"] = new ArrayList();
        Session["SalaSeleccionada"] = 0;
        Session["UbicacionSala"] = "";
        Session["CapacidadSala"] = "";
        Session["HashEvento"] = "";
        Session["NombreSalaSeleccionada"] = "";
        Session["NombreEvento"] = "";
        Session["Grabacion"] = "No";
        Session["VideoConferencia"] = "No";
        Session["Streaming"] = "No";
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        procesos.validar_salida_usuario((int)Session["ID"]);
        Session["Usuario"] = "";
        Session["Permisos"] = "";
        Session["Correo"] = "";
        Session["ID"] = 0;
        Session["ModID"] = -1;
        Session["ModNombre"] = "";
        Session["ModApellido"] = "";
        Session["ModCorreo"] = "";
        Session["ModificacionUsuario"] = false;
        Session["tempClaveUsuario"] = -1;
        Session["FechasSeleccionadas"] = new List<DateTime>();
        Session["HorasSeleccionadas"] = new ArrayList();
        Session["SalaSeleccionada"] = 0;
        Session["UbicacionSala"] = "";
        Session["CapacidadSala"] = "";
        Session["HashEvento"] = "";
        Session["NombreSalaSeleccionada"] = "";
        Session["NombreEvento"] = "";
        Session["Grabacion"] = "No";
        Session["VideoConferencia"] = "No";
        Session["Streaming"] = "No";
    }


</script>
