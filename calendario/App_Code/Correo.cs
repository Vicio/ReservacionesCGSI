using System;
using System.Collections.Generic;
using System.Collections;
using System.Net.Mail;
using System.Configuration;

/// <summary>
/// Summary description for Correo
/// </summary>
public class Correo
{
	public Correo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string enviarMailUsuario(string email, string nombre, string hash)
    {
        /* 
         * Este es el método que controla el envío hacia los correos de los clientes, siempre y cuando el correo sea válido
         * 
         * */
        try
        {
            
            MailAddress correo = new MailAddress(email, nombre); //correo del cliente
            MailAddress correoServidor = new MailAddress("dsiv.uaq@hotmail.com", "Universidad Autónoma de Querétaro"); // correo del servidor

            MailMessage MM = new MailMessage(correoServidor, correo); //cuerpo del correo
            MM.Subject = "Verificación de cuenta (no responder)";
            string directorioServidor = ConfigurationManager.AppSettings["directorioServidor"];
            MM.Body = "<a href=\"" + directorioServidor + "Login/Verificacion.aspx?VerificarCorreo=" + hash + "\" >Click aquí para verificar su cuenta.</a>";
            MM.IsBodyHtml = true;
            MM.Priority = MailPriority.High; //prioridad del correo

            SmtpClient smtp = new SmtpClient(); // Librería para el envío por smtp
            // el tipo de conexion y el manejo del servidor están configurados en el archivo web.config
            smtp.EnableSsl = true; // permitir conexion segura
            smtp.Send(MM); // enviar correo
            //Se cambia la interfaz para informarle al cliente que no hubo fallos al enviarle el correo de validacion
            return "Se le ha enviado una notificación a su correo para verificar la autenticidad de su nueva cuenta.";
        }
        catch (Exception err)
        {
            return "El correo no existe."; // falla el envío del correo
        }
    }


    public int enviarMailCambioPass(string email, string nombre, string hash)
    {
        /* 
         * Este es el método que controla el envío hacia los correos de los clientes, siempre y cuando el correo sea válido
         * 
         * */
        try
        {
            MailAddress correo = new MailAddress(email, nombre); //correo del cliente
            MailAddress correoServidor = new MailAddress("dsiv.uaq@hotmail.com", "Universidad Autónoma de Querétaro"); // correo del servidor
            string directorioServidor = ConfigurationManager.AppSettings["directorioServidor"];

            MailMessage MM = new MailMessage(correoServidor, correo); //cuerpo del correo
            MM.Subject = "Cambio de contraseña (no responder)";
            MM.Body = "<a href=\"" + directorioServidor + "Login/Verificacion.aspx?CambiarPassword=" + hash + "\" >Click aquí para cambiar su contraseña.</a>";
            MM.IsBodyHtml = true;
            MM.Priority = MailPriority.High; //prioridad del correo

            SmtpClient smtp = new SmtpClient(); // Librería para el envío por smtp
            // el tipo de conexion y el manejo del servidor están configurados en el archivo web.config
            smtp.EnableSsl = true; // permitir conexion segura
            smtp.Send(MM); // enviar correo
            //Se cambia la interfaz para informarle al cliente que no hubo fallos al enviarle el correo de validacion
            return 0;
        }
        catch (Exception err)
        {
            return -1; // falla el envío del correo
        }
    }



    public void enviarNotificacion(string email, string nombre)
    {
        /* 
         * Este es el método que controla el envío hacia los correos de los clientes, siempre y cuando el correo sea válido
         * 
         * */
        MailAddress correo = new MailAddress(email, nombre); //correo del cliente
        MailAddress correoServidor = new MailAddress("dsiv.uaq@hotmail.com", "Universidad Autónoma de Querétaro"); // correo del servidor

        MailMessage MM = new MailMessage(correoServidor, correo); //cuerpo del correo
        MM.Subject = "Su cuenta ha sido habilitada (no responder)";
        MM.Body = "El administrador ha aceptado su petición, ahora usted puede acceder al sistema de reservación.";
        MM.IsBodyHtml = true;
        MM.Priority = MailPriority.High; //prioridad del correo

        SmtpClient smtp = new SmtpClient(); // Librería para el envío por smtp
        // el tipo de conexion y el manejo del servidor están configurados en el archivo web.config
        smtp.EnableSsl = true; // permitir conexion segura
        try
        {
            smtp.Send(MM); // enviar correo
            //Se cambia la interfaz para informarle al cliente que no hubo fallos al enviarle el correo de validacion
        }
        catch (Exception err)
        {
        }
    }

    public int enviarMailAdministrador(string email, string nombre, string nombreUsuario, string apellidoUsuario, string correoE, string institucion, string claveUsuario)
    {
        MailAddress correo = new MailAddress(email, nombre); //correo del cliente
        MailAddress correoServidor = new MailAddress("dsiv.uaq@hotmail.com", "Universidad Autónoma de Querétaro"); // correo del servidor

        string directorioServidor = ConfigurationManager.AppSettings["directorioServidor"];
        MailMessage MM = new MailMessage(correoServidor, correo); //cuerpo del correo
        MM.Subject = "Activación de la cuenta de " + nombreUsuario + " " + apellidoUsuario + " (no responder)";
        MM.Body = "El siguiente usuario ha hecho una petición de registro:<br />"
            + nombreUsuario + " " + apellidoUsuario + "<br />"
            + "Correo: " + correoE + "<br />"
            + "Institución: " + institucion + "<br />"
            + "<a href=\"" + directorioServidor + "Login/Verificacion.aspx?HabilitarCorreo=" + claveUsuario + "\" >Click aquí para habilitar la cuenta del usuario.</a>";
        MM.IsBodyHtml = true;
        MM.Priority = MailPriority.High; //prioridad del correo

        SmtpClient smtp = new SmtpClient(); // Librería para el envío por smtp
        // el tipo de conexion y el manejo del servidor están configurados en el archivo web.config
        smtp.EnableSsl = true; // permitir conexion segura
        try
        {
            smtp.Send(MM); // enviar correo
            //Se cambia la interfaz para informarle al cliente que no hubo fallos al enviarle el correo de validacion
            return 0;
        }
        catch (Exception err)
        {
            return -1;
        }
    }


    public int enviarEventoAdministrador(string email, string nombre, string nombreUsuario, string apellidoUsuario, string nombreEvento, string nombreSala, List<DateTime> dias, ArrayList horas, string idEvento)
    {
        MailAddress correo = new MailAddress(email, nombre); //correo del cliente
        MailAddress correoServidor = new MailAddress("dsiv.uaq@hotmail.com", "Universidad Autónoma de Querétaro"); // correo del servidor

        string directorioServidor = ConfigurationManager.AppSettings["directorioServidor"];
        MailMessage MM = new MailMessage(correoServidor, correo); //cuerpo del correo
        MM.Subject = "Solicitud de reservación para " + nombreUsuario + " " + apellidoUsuario + " (no responder)";
        MM.Body = "El siguiente usuario ha solicitado la reservación de una sala:<br />"
            + nombreUsuario + " " + apellidoUsuario + "<br />"
            + "Evento: " + nombreEvento + "<br />"
            + "Sala: " + nombreSala + "<br />";

        MM.Body += "Días: ";
        
        for(int i = 0; i < dias.Count; i++)
        {
            MM.Body += "" + dias[i].Day.ToString() + " de " + dias[i].ToString("MMMM");
            if (i < dias.Count - 1)
                MM.Body += ", ";
        }
        MM.Body += "<br />Horas: ";
        for (int i = 0; i < horas.Count; i++)
        {
            MM.Body += "" + horas[i].ToString() + ":00";
            if (i < horas.Count - 1)
                MM.Body += ", ";
        }
        MM.Body += "<br /><br />";

        idEvento = idEvento + ":" + PasswordHash.PasswordHash.CreateHash(idEvento);

        MM.Body += "<a href=\"" + directorioServidor + "Login/Verificacion.aspx?ValidarEvento=" + idEvento + "\" >Click aquí para habilitar el evento del usuario.</a>";
        MM.IsBodyHtml = true;
        MM.Priority = MailPriority.High; //prioridad del correo

        SmtpClient smtp = new SmtpClient(); // Librería para el envío por smtp
        // el tipo de conexion y el manejo del servidor están configurados en el archivo web.config
        smtp.EnableSsl = true; // permitir conexion segura
        try
        {
            smtp.Send(MM); // enviar correo
            //Se cambia la interfaz para informarle al cliente que no hubo fallos al enviarle el correo de validacion
            return 0;
        }
        catch (Exception err)
        {
            return -1;
        }
    }


}