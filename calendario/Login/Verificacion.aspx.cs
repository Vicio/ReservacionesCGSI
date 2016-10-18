using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetProcsTableAdapters;
using DataSetFuncsTableAdapters;
using PasswordHash;

public partial class Login_Verificacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string validacion = Request.Url.AbsoluteUri.ToString();//se toma el string del url
        if (validacion.Contains("?VerificarCorreo="))// se verifica que contenga esta parte para saber si es una validacion de correo
        {
            try
            {
                validacion = validacion.Substring(validacion.IndexOf('=') + 1);
                obtener_datos_validacion_usuarioTableAdapter datos = new obtener_datos_validacion_usuarioTableAdapter();
                string nombre = datos.GetData(validacion)[0].nombre_usuario;
                string apellido = datos.GetData(validacion)[0].apellido_usuario;
                string email = datos.GetData(validacion)[0].correo_usuario;
                string institucion = datos.GetData(validacion)[0].nombre_escuela;

                Correo correo = new Correo();
                obtener_administradoresTableAdapter admins = new obtener_administradoresTableAdapter();

                for (int i = 0; i < admins.GetData().Rows.Count; i++ )
                {
                    string nombreAdmin = admins.GetData()[i].nombre_usuario + " " + admins.GetData()[i].apellido_usuario;
                    string correoAdmin = admins.GetData()[i].correo_usuario;
                    correo.enviarMailAdministrador(correoAdmin, nombreAdmin, nombre, apellido, email, institucion, validacion);

                }


                mensaje.InnerHtml = "Estimado usuario " + nombre + ", su cuenta se ha verificado, el administrador revisará y habilitará su cuenta, se le notificará por correo cuando esté activada";

            }
            catch(Exception err)
            {
                mensaje.InnerHtml = "Hubo un error al verificar la cuenta, favor de intentarlo más tarde, si el problema persiste, consulte con el administrador.";
            }
        }
        if (validacion.Contains("?HabilitarCorreo="))
        {
            try
            {
                validacion = validacion.Substring(validacion.IndexOf('=') + 1);
                obtener_datos_validacion_usuarioTableAdapter datos = new obtener_datos_validacion_usuarioTableAdapter();
                int claveUsuario = (int)datos.GetData(validacion)[0].clave_usuario;
                ProcsTableAdapter procesos = new ProcsTableAdapter();

                procesos.activar_cuenta_usuario(claveUsuario);


                Correo correo = new Correo();
                correo.enviarNotificacion((string)datos.GetData(validacion)[0].correo_usuario, (string)datos.GetData(validacion)[0].nombre_usuario);

                mensaje.InnerHtml = "Cuenta habilitada, se le notificará al usuario.";

            }
            catch(Exception err)
            {
                mensaje.InnerHtml = "Hubo un error al habilitar la cuenta.";
            }
        }
        if (validacion.Contains("?CambiarPassword="))
        {
            try
            {
                divCambioPass.Visible = true;
                validacion = validacion.Substring(validacion.IndexOf('=') + 1);
                obtener_datos_validacion_usuarioTableAdapter datos = new obtener_datos_validacion_usuarioTableAdapter();
                Session["tempClaveUsuario"] = (int)datos.GetData(validacion)[0].clave_usuario;
            }
            catch(Exception err)
            {
                mensaje.InnerHtml = "Hubo un error al cambiar su contraseña";
                divCambioPass.Visible = false;
            }
        }
        if (validacion.Contains("?ValidarEvento="))
        {
            string[] evento = validacion.Split('=');
            evento = evento[1].Split(':');
            ProcsTableAdapter procesos = new ProcsTableAdapter();
            procesos.validar_evento(int.Parse(evento[0]));
            mensaje.InnerHtml = "Evento habilitado.";
        }
    }
    protected void ButtonConfirmar_Click(object sender, EventArgs e)
    {
        if (TextBoxPassword.Text.Equals(TextBoxConfirmarPassword.Text))
        {
            try
            {
                ProcsTableAdapter procesos = new ProcsTableAdapter();
                string hash = PasswordHash.PasswordHash.CreateHash(TextBoxConfirmarPassword.Text);
                procesos.cambiar_password_usuario((int)Session["tempClaveUsuario"], hash);
                mensaje.InnerHtml = "Contraseña cambiada exitosamente.";
                divCambioPass.Visible = false;
            }
            catch (Exception err)
            {
                mensaje.InnerHtml = "Ha ocurrido un problema con la comunicación al servidor, intente de nuevo más tarde";
            }
        }
        else
            mensaje.InnerHtml = "Las contraseñas no coinciden";
    }
}