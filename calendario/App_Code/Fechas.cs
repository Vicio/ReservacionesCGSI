using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Fechas
/// </summary>
public class Fechas
{
	public Fechas()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public ArrayList CalcularFechasContinuas(DateTime[] fechas)
    {
        ArrayList formatoFechas = new ArrayList();
        formatoFechas.Add(fechas[0].ToLongDateString());
        for (int i = 1; i < fechas.Length - 1; i++)
        {
            if(fechas[i] != fechas[i + 1].AddDays(-1))
            {
                formatoFechas.Add(fechas[i].ToLongDateString());
                formatoFechas.Add(",");
                formatoFechas.Add(fechas[i + 1].ToLongDateString());
            }
        }
        
        return formatoFechas;
    }

    public ArrayList CalcularHorasContinuas(DateTime[] horas)
    {
        ArrayList formatoHoras = new ArrayList();
        formatoHoras.Add(horas[0].ToShortTimeString());
        for (int i = 1; i < horas.Length - 1; i++)
        {
            if (horas[i] != horas[i + 1].AddHours(-1))
            {
                formatoHoras.Add(horas[i].ToShortTimeString());
                formatoHoras.Add(",");
                formatoHoras.Add(horas[i + 1].ToShortTimeString());
            }
        }

        return formatoHoras;
    }
}