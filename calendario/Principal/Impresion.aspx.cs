using PdfSharp;
using PdfSharp.Charting;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Forms;
using PdfSharp.Pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Principal_Impresion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PdfDocument documento = new PdfDocument();
        documento.Info.Title = "Prueba";
        PdfPage pagina = new PdfPage();
        pagina.Orientation = PdfSharp.PageOrientation.Portrait;
        pagina.Size = PdfSharp.PageSize.Letter;
        pagina.Rotate = 0;
        documento.Pages.Add(pagina);

        XGraphics graficos = XGraphics.FromPdfPage(pagina);
        XFont fuente = new XFont("Arial", 10, XFontStyle.Regular);
        XTextFormatter formato = new XTextFormatter(graficos);

        const string pieDePagina = "Centro Universitario, Cerro de las Campanas, Santiago de Querétaro , Qro. México C.P. 76010\n"
            + "Tel. 01 (442) 192 12 00 Ext. 3270";
        XRect cuadro = new XRect(0, (pagina.Height / 20) * 19, pagina.Width, pagina.Height / 20);
        graficos.DrawRectangle(XBrushes.Transparent, cuadro);
        formato.Alignment = XParagraphAlignment.Center;
        formato.DrawString(pieDePagina, fuente, XBrushes.Gray, cuadro, XStringFormats.TopLeft);


        graficos.DrawLine(XPens.DarkGray, 50, ((pagina.Height / 20) * 19) - 5, pagina.Width - 50, ((pagina.Height / 20) * 19) - 5);

        string hashEvento = Session["HashEvento"].ToString();

        string fechaYReferencia = "Centro Universitario, " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day + " de " + DateTime.Now.Year + ".\n"
            + "Ref. " + Session["HashEvento"].ToString();

        fuente = new XFont("Arial", 8, XFontStyle.Regular);
        cuadro = new XRect((pagina.Width / 2) - 200, (pagina.Height / 20) * 3, (pagina.Width / 2) + 150, pagina.Height / 20);
        graficos.DrawRectangle(XBrushes.Transparent, cuadro);
        formato.Alignment = XParagraphAlignment.Right;
        formato.DrawString(fechaYReferencia, fuente, XBrushes.Black, cuadro, XStringFormats.TopLeft);


        const string presentacionTitulo = "L.I. ULISES BAJONERO CORONA\n"
                 + "Coordinador General de Servicios de Informatización\n"
            + "Presente.";
        fuente = new XFont("Arial", 12, XFontStyle.Bold);
        cuadro = new XRect(50, (pagina.Height / 20) * 5, pagina.Width - 100, pagina.Height / 20);
        graficos.DrawRectangle(XBrushes.Transparent, cuadro);
        formato.Alignment = XParagraphAlignment.Left;
        formato.DrawString(presentacionTitulo, fuente, XBrushes.Black, cuadro, XStringFormats.TopLeft);


        string solicitud = "Por este medio, su servidor, " + Session["Solicitante"] + " le solicita de la manera más atenta "
            + "hacer uso de la " + Session["NombreSalaSeleccionada"] + ", ubicada en " + Session["UbicacionSala"] + " para el siguiente evento:\n\n"
            + "\"" + Session["NombreEvento"] + "\"\nFecha:\n";

        ArrayList horas = (ArrayList)Session["HorasSeleccionadas"];
        List<DateTime> dias = (List<DateTime>)Session["FechasSeleccionadas"];

        for (int i = 0; i < dias.Count; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (dias[i].Year == dias[j].Year)
                {
                    if (dias[i].Month == dias[j].Month)
                    {
                        if (dias[i].Day < dias[j].Day)
                        {
                            DateTime temp = dias[i];
                            dias[i] = dias[j];
                            dias[j] = temp;
                        }
                    }
                    if (dias[i].Month < dias[j].Month)
                    {
                        DateTime temp = dias[i];
                        dias[i] = dias[j];
                        dias[j] = temp;
                    }
                }
                if (dias[i].Year < dias[j].Year)
                {
                    DateTime temp = dias[i];
                    dias[i] = dias[j];
                    dias[j] = temp;
                }
            }
        }

        for(int i = 0; i < dias.Count - 1; i++)
        {
            solicitud += dias[i].Day.ToString();
            if (dias[i].Month != dias[i + 1].Month)
            {
                solicitud += " de " + dias[i].ToString("MMMM");
                if (dias[i].Year != dias[i + 1].Year)
                {
                    solicitud += " de " + dias[i].Year + "\n";
                }
                else
                    solicitud += "; ";

            }
            else
                solicitud += ", ";

        }

        solicitud += dias[dias.Count - 1].Day + " de " + dias[dias.Count - 1].ToString("MMMM") + " de " + dias[dias.Count - 1].Year + "\n";


        solicitud += "Horario: ";

        for (int i = 1; i < horas.Count - 1; i++)
        {
            if(int.Parse(horas[i - 1].ToString()) + 1 == int.Parse(horas[i].ToString()))
            {
                if (int.Parse(horas[i].ToString()) != int.Parse(horas[i + 1].ToString()) - 1)
                {
                    if(int.Parse(horas[i].ToString()) + 1 > 12)
                    {
                        solicitud += "a " + (int.Parse(horas[i].ToString()) + 1 - 12) + ":00 pm, ";
                    }
                    else
                    {
                        if(int.Parse(horas[i].ToString()) + 1 == 12)
                            solicitud += "a " + (int.Parse(horas[i].ToString()) + 1) + ":00 pm, ";
                        else
                            solicitud += "a " + (int.Parse(horas[i].ToString()) + 1) + ":00 am, ";
                    }
                }
            }
            else
            {
                if (int.Parse(horas[i].ToString()) > 12)
                {
                    solicitud += (int.Parse(horas[i].ToString()) - 12) + ":00 pm ";
                }
                else
                {
                    if (int.Parse(horas[i].ToString()) == 12)
                        solicitud += (int.Parse(horas[i].ToString())) + ":00 pm ";
                    else
                        solicitud += (int.Parse(horas[i].ToString())) + ":00 am ";
                }

                if (int.Parse(horas[i].ToString()) + 1 != int.Parse(horas[i + 1].ToString()) && int.Parse(horas[i].ToString()) - 1 != int.Parse(horas[i - 1].ToString()))
                {
                    if (int.Parse(horas[i].ToString()) + 1 > 12)
                    {
                        solicitud += "a "+ (int.Parse(horas[i].ToString()) + 1 - 12) + ":00 pm, ";
                    }
                    else
                    {
                        if (int.Parse(horas[i].ToString()) == 12)
                            solicitud += "a " + (int.Parse(horas[i].ToString()) + 1) + ":00 pm, ";
                        else
                            solicitud += "a " + (int.Parse(horas[i].ToString()) + 1) + ":00 am, ";
                    }
                }

            }
        }

        solicitud += "\nCapacidad: " + Session["CapacidadSala"] + " personas\n";
        solicitud += "Grabación: " + Session["Grabacion"] + "\n";
        solicitud += "Videoconferencia: " + Session["VideoConferencia"] + "\n";
        solicitud += "Streaming: " + Session["Streaming"] + "\n";

        fuente = new XFont("Arial", 12, XFontStyle.Regular);
        cuadro = new XRect(50, (pagina.Height / 20) * 7, pagina.Width - 100, (pagina.Height / 20) * 2);
        graficos.DrawRectangle(XBrushes.Transparent, cuadro);
        formato.Alignment = XParagraphAlignment.Left;
        formato.DrawString(solicitud, fuente, XBrushes.Black, cuadro, XStringFormats.TopLeft);



        const string mensajeUAQ = "Atentamente\n \"EDUCO EN LA VERDAD Y EN EL HONOR\"";
        fuente = new XFont("Arial", 12, XFontStyle.Bold);
        cuadro = new XRect(0, (pagina.Height / 20) * 14, pagina.Width, pagina.Height / 20);
        graficos.DrawRectangle(XBrushes.Transparent, cuadro);
        formato.Alignment = XParagraphAlignment.Center;
        formato.DrawString(mensajeUAQ, fuente, XBrushes.Black, cuadro, XStringFormats.TopLeft);


        graficos.DrawLine(XPens.Black, 150, ((pagina.Height / 20) * 17) - 5, pagina.Width - 150, ((pagina.Height / 20) * 17) - 5);


        const string nombreYFirma = "Nombre y firma del director del área";
        fuente = new XFont("Arial", 12, XFontStyle.Bold);
        cuadro = new XRect(0, (pagina.Height / 20) * 17, pagina.Width, pagina.Height / 20);
        graficos.DrawRectangle(XBrushes.Transparent, cuadro);
        formato.Alignment = XParagraphAlignment.Center;
        formato.DrawString(nombreYFirma, fuente, XBrushes.Black, cuadro, XStringFormats.TopLeft);
        string direccionImagen = ConfigurationManager.AppSettings["directorioServidor"] + "Imagenes/bannerUAQ.jpg";

        HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(direccionImagen);
        webRequest.AllowWriteStreamBuffering = true;
        WebResponse webResponse = webRequest.GetResponse();

        System.Drawing.Image imagenOriginal = System.Drawing.Image.FromStream(webResponse.GetResponseStream());
        webResponse.Close();
        XImage imagen = XImage.FromGdiPlusImage(imagenOriginal);
        graficos.DrawImage(imagen, 40, 30, 300, 60);


        MemoryStream stream = new MemoryStream();
        documento.Save(stream, false);

        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        stream.Close();
        Response.End();

    }
}