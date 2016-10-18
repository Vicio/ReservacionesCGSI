using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetFuncsTableAdapters;
using DataSetProcsTableAdapters;

public partial class Login_Estudiantes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void buttonAceptar_Click(object sender, EventArgs e)
    {
        SQLInjects injects = new SQLInjects();
        FuncsTableAdapter funciones = new FuncsTableAdapter();
        obtener_datos_sesion_usuarioTableAdapter sesion = new obtener_datos_sesion_usuarioTableAdapter();
        ProcsTableAdapter procesos = new ProcsTableAdapter();

        string correo = injects.Remover(TextBoxCorreo.Text);
        string hashUsuario = "";
        try
        {
            hashUsuario = funciones.obtener_hash_usuario(correo);
            if (hashUsuario.Length < 5)
                MostrarError("Los datos que ingresó son incorrectos, por favor inténtelo de nuevo.");
            else
                if (PasswordHash.PasswordHash.ValidatePassword
                    (TextBoxPassword.Text, hashUsuario))
                {
                    if (sesion.GetData(hashUsuario, correo)[0].activacion_usuario)
                    {
                        if (sesion.GetData(hashUsuario, correo)[0].acceso)
                        {
                            MostrarError("Acceso secundario detectado, se desactivará la cuenta temporalmente");
                            procesos.desactivar_cuenta_usuario(sesion.GetData(hashUsuario, correo)[0].clave_usuario);
                            procesos.validar_salida_usuario(sesion.GetData(hashUsuario, correo)[0].clave_usuario);
                        }
                        else
                        {
                            Session["Logged"] = "Yes";
                            Session["Usuario"] = sesion.GetData(hashUsuario, correo)[0].nombre_usuario;
                            Session["Permisos"] = sesion.GetData(hashUsuario, correo)[0].nivel_usuario;
                            Session["Correo"] = correo;
                            Session["ID"] = sesion.GetData(hashUsuario, correo)[0].clave_usuario;
                            procesos.validar_entrada_usuario(int.Parse(Session["ID"].ToString()));
                            Response.Redirect("../Principal/Inicio.aspx");
                        }
                    }
                    else
                        MostrarError("Su cuenta aun no se encuentra activada.");
                }
                else
                    MostrarError("Los datos que ingresó son incorrectos, por favor inténtelo de nuevo.");

        }
        catch(Exception err)
        {
            MostrarError("Usted no se ha registrado.");
        }




    }

    private void MostrarError(string error)
    {
        textoError.Text = error + "<br />";
    }

    protected void LinkButtonOlvidePassword_Click(object sender, EventArgs e)
    {
        divAcceso.Visible = false;
        divPass.Visible = true;
    }
    protected void buttonCambiarPass_Click(object sender, EventArgs e)
    {

        try
        {
            SQLInjects injects = new SQLInjects();

            string correoUsuario = injects.Remover(TextBoxCorreoVerificacion.Text);
            FuncsTableAdapter funciones = new FuncsTableAdapter();
            string hash = funciones.obtener_hash_usuario(correoUsuario);
            obtener_datos_validacion_usuarioTableAdapter datos = new obtener_datos_validacion_usuarioTableAdapter();
            string nombreUsuario = datos.GetData(hash)[0].nombre_usuario + " " + datos.GetData(hash)[0].apellido_usuario;

            Correo correo = new Correo();
            correo.enviarMailCambioPass(correoUsuario, nombreUsuario, hash);
            LabelMensaje.Text = "Se le ha enviado un correo para cambiar su contraseña.";
        }
        catch(Exception err)
        {
            LabelMensaje.Text = "Ocurrió un problema al enviar su correo, intente de nuevo más tarde.";
        }
    }
}