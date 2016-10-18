using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Principal_Eventos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Permisos"].Equals("U"))
            Response.Redirect("../Principal/Inicio.aspx");

    }
    private void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {

                try
                {
                    if (((CheckBox)row.Cells[i].Controls[0]).Checked == ((CheckBox)previousRow.Cells[i].Controls[0]).Checked)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
                catch(ArgumentOutOfRangeException err)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }

            }
        }
    }
    protected void vistaEventos_PreRender(object sender, EventArgs e)
    {
        MergeRows(vistaEventos);

    }
}