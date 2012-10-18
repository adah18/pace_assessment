using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;


namespace PAOnlineAssessment
{
    public partial class SampleExporting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExcelPackage pck = new ExcelPackage();
            var ws = pck.Workbook.Worksheets.Add("Sample1");

            ws.Cells["A1"].Value = "Sample 1";
            ws.Cells["A1"].Style.Font.Bold = true;

            //ws.Cells["A1"].Merge = true;
            //var shape = ws.Drawings.AddShape("Shape1", eShapeStyle.Rect);
            //shape.SetPosition(50, 200);
            //shape.SetSize(200, 100);
            //shape.Text = "Sample 1 saves to the Response.OutputStream";
            ws.Cells[2, 1].Value = "Sample DataTable Export"; // Heading Name
            ws.Cells[2,1,2, 10].Merge = true; //Merge columns start and end range
            

            Response.Clear();
            pck.SaveAs(Response.OutputStream);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=Sample1.xlsx");
            Response.End();
        }
    }
}
