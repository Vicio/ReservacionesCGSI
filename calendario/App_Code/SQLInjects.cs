using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SQLInjects
/// </summary>
public class SQLInjects
{
	public SQLInjects()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string Remover(string Texto)
    {
        string temp = "";
        for (int i = 0; i < Texto.Length; i++)
        {
            if (Texto[i] == '\'' || Texto[i] == ';' ||
                Texto[i] == '%' || Texto[i] == '\"' ||
                Texto[i] == '\\' || Texto[i] == '/' ||
                Texto[i] == '|')
                i++;
            if(i < Texto.Length)
                temp += Texto[i];
        }

        return temp;
    }
}