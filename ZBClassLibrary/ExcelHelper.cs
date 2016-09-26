using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Office.Interop.Excel;
using System.IO;

/// <summary>
/// ExcelHelper 的摘要说明
/// </summary>
public class ExcelHelper
{
    Microsoft.Office.Interop.Excel.Application excel;
    Workbook wb;
    Worksheet xSheet;
    object oMissing = System.Reflection.Missing.Value;
    bool Exists = false;
    public ExcelHelper(string filePath)
	{
        if (!File.Exists(filePath)) return;
        Exists = true;
        object objTemplate = filePath;
        excel = new Microsoft.Office.Interop.Excel.Application();
        excel.Visible = false;
        excel.DisplayAlerts = false; 
        excel.AlertBeforeOverwriting = false;
        wb = excel.Workbooks._Open(filePath, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);

	}
    public void Replace(string oldchr, string newchr)
    {
        if (!Exists) return;
        object findtext = oldchr;
        object newtext = newchr;
        for (int i = 1; i <= wb.Sheets.Count; i++)
        {
            xSheet = (Worksheet)wb.Sheets[i];
            try
            {
                xSheet.Cells.Replace(findtext, newtext, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
            }
            catch
            {
            }
        }
    }
    public void SaveAs(string path)
    {
        if (!Exists) return;
        object objTemplateb = path;
        wb.SaveAs(objTemplateb, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, oMissing, oMissing, oMissing, oMissing,
                       Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive
                       , oMissing, oMissing, oMissing, oMissing, oMissing);
        NAR(xSheet);
        wb.Close(false, oMissing, oMissing);
        NAR(wb);
        excel.Quit();
        NAR(excel);
        System.GC.Collect();
    }
    private void NAR(object o)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(o);//强制释放一个对象
        }
        catch
        {
        }
        finally
        {
            o = null;
        }
    }
    public static void NARA(object o)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(o);//强制释放一个对象
        }
        catch
        {
        }
        finally
        {
            o = null;
        }
    }
    public static void ConvertHTMLtoXLS(string sourcePath)
    {
        if (!File.Exists(sourcePath)) return;
        //int i = sourcePath.LastIndexOf(".");
        //string ext = sourcePath.Substring(i);
        object targetPath = sourcePath + ".temp";// Server.MapPath("./提成报表1.xls");


        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
        object missing = Type.Missing;
        object target = targetPath;
        //object type = excelType;
        Workbook workBook = excel.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
                missing, missing, missing, missing, missing, missing, missing, missing, missing);
        workBook.SaveAs(targetPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing,
            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
        missing, missing, missing, missing, missing);
        workBook.Close(true, missing, missing);
        NARA(workBook);
        excel.Quit();
        NARA(excel);
        if (File.Exists(targetPath.ToString()))
        {
            File.Delete(sourcePath);
            File.Move(targetPath.ToString(), sourcePath);
        }
    }
}
