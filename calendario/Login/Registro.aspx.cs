using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetProcsTableAdapters;
using DataSetFuncsTableAdapters;
using PasswordHash;

public partial class Login_Registro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void buttonRegistro_Click(object sender, EventArgs e)
    {
        ProcsTableAdapter procesos = new ProcsTableAdapter();
        FuncsTableAdapter funciones = new FuncsTableAdapter();
        string hash = "";
        SQLInjects injects = new SQLInjects();

        Correo correo = new Correo();
        //textBoxCorreo.Text += "@uaq.mx";
        if (textBoxNombre.Text.Equals(""))
            MostrarError("Debe escribir su nombre");
        else
            if (textBoxApellido.Text.Equals(""))
                MostrarError("Debe escribir su apellido");
            else
                if (textBoxCorreo.Text.Equals("") || textBoxCorreo.Text.Contains("@"))
                    MostrarError("Debe escribir su correo adecuadamente");
                else
                    if (textBoxPassword.Text.Equals(""))
                        MostrarError("Debe escribir una contraseña");
                    else
                        if (textBoxPassword.Text.Equals(textBoxPasswordValidation.Text))
                        {
                            if ((bool)funciones.verificar_correo_registrado(injects.Remover(textBoxCorreo.Text + "@uaq.mx")))
                                MostrarError("El correo ya se encuentra registrado.");
                            else
                            {
                                hash = PasswordHash.PasswordHash.CreateHash(textBoxPassword.Text);
                                string err = correo.enviarMailUsuario(textBoxCorreo.Text + "@uaq.mx", textBoxNombre.Text, hash);
                                if (err[0] == 'E')
                                    MostrarError(err);
                                else
                                {
                                    procesos.agregar_usuario(injects.Remover(textBoxNombre.Text), injects.Remover(textBoxApellido.Text), 1,
                                        injects.Remover(textBoxCorreo.Text + "@uaq.mx"), hash, "U", false, false);
                                    MostrarError("Su cuenta se ha registrado correctamente, le llegará una notificación a su correo");
                                    camposTexto.Visible = false;
                                }
                            }
                        }
                        else
                            MostrarError("Las contraseñas no coinciden");
    }

    private void MostrarError(string error)
    {
        textoError.Text = error + "<br />";
    }


}