using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Web;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Reflection;
using ZBClassLibrary;

namespace ZbClassLibrary
{
    public class ExcelExport
    {
        enum ColumnName { A1 = 1, B1 = 2, C1 = 3, D1 = 4, E1 = 5, F1 = 6, G1 = 7, H1 = 8, I1 = 9, J1 = 10, K1 = 11, L1 = 12, M1 = 13, N1 = 14, O1 = 15, P1 = 16, Q1 = 17, R1 = 18, S1 = 19, T1 = 20, U1 = 21, V1 = 22, W1 = 23, X1 = 24, Y1 = 25, Z1 = 26 }
        /// <summary>   
        /// 直接导出Excel   
        /// </summary>   
        /// <param name="ds">数据源DataSet</param>   
        /// <param name="fileName">保存文件名(例如：E:\a.xls)</param>   
        /// <returns></returns>   
        public bool DoExport(DataSet ds, string fileName)
        {
            if (ds.Tables.Count == 0 || fileName == string.Empty)
            {
                return false;
            }
            Application excel = new Application();
            int rowindex = 1;
            int colindex = 0;
            Workbook work = excel.Workbooks.Add(true);
            //Worksheet sheet1 = (Worksheet)work.Worksheets[0];   
            System.Data.DataTable table = ds.Tables[0];
            foreach (DataColumn col in table.Columns)
            {
                colindex++;
                excel.Cells[1, colindex] = col.ColumnName;
            }
            foreach (DataRow row in table.Rows)
            {
                rowindex++;
                colindex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    colindex++;
                    excel.Cells[rowindex, colindex] = row[col.ColumnName].ToString();
                }
            }
            excel.Visible = false;
            //((Worksheet)work.Sheets[0]).Name = "sss";   
            excel.ActiveWorkbook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            excel.Quit();
            excel = null;
            GC.Collect();
            return true;
        }
        /// <summary>   
        /// 直接导出Excel   
        /// </summary>   
        /// <param name="ds">数据源DataSet</param>   
        /// <param name="columns">列名数组,允许为空(columns=null),为空则表使用默认数据库列名 </param>   
        /// <param name="fileName">保存文件名(例如：E:\a.xls)</param>   
        /// <returns></returns>   
        public bool DoExport(DataSet ds, string[] columns, string title, string fileName)
        {
            if (ds.Tables.Count == 0 || fileName == string.Empty)
            {
                return false;
            }
            Application excel = new Application();
            int rowindex = 2;
            int colindex = 0;
            Workbook work = excel.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Worksheet sheetDest = work.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value) as Microsoft.Office.Interop.Excel.Worksheet;//给工作薄添加一个Sheet  
            // sheetDest.Name = "sheet1";

            System.Data.DataTable table = ds.Tables[0];
            if (columns != null)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    colindex++;
                    if (columns[i] != null && columns[i] != "")
                    {
                        excel.Cells[2, colindex] = columns[i];
                    }
                    else
                    {
                        excel.Cells[2, colindex] = table.Columns[i].ColumnName;

                    }
                    excel.Cells[2, colindex].Font.Size = 15;
                    excel.Cells[2, colindex].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    excel.Cells[2, colindex].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.ActiveSheet.Rows[2].Font.Bold = true;   //设置第一行加粗
                    excel.ActiveSheet.Rows[2].Font.Bold = true;   //设置第一行加粗
                }
            }
            else
            {
                foreach (DataColumn col in table.Columns)
                {
                    colindex++;
                    excel.Cells[1, colindex] = col.ColumnName;
                }
            }

            excel.Cells[1, 1] = title;   //第一行标题
            Range rngRow = (Microsoft.Office.Interop.Excel.Range)sheetDest.Columns[1, Type.Missing];
            rngRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter; //第一行标题居中
            int columnIndex = table.Columns.Count;        //得到数据总列数
            string cName = ((ColumnName)columnIndex).ToString();
            sheetDest.get_Range("A1", cName).Merge(sheetDest.get_Range("A1", cName).MergeCells);//合并单元格
            excel.ActiveSheet.Rows[1].Font.Name = "华文行楷";//设置第一行字体
            excel.ActiveSheet.Rows[1].Font.Color = Color.Red; //设置第一行为红色
            excel.ActiveSheet.Rows[1].Font.Size = 30;
            excel.ActiveSheet.Rows[1].Font.Bold = true;   //设置第一行加粗
            //excel.ActiveSheet.Rows[1].Font.UnderLine = true;//设置第一行有下划线

            //  rngRow.NumberFormatLocal = "@";     //设置单元格格式为文本
            // rngRow.ColumnWidth = 15;     //设置单元格的宽度
            //rngRow.Cells.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 153).ToArgb();     //设置单元格的背景色
            // rngRow.Borders.LineStyle = 1;     //设置单元格边框的粗细
            // rngRow.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());     //给单元格加边框
            //rngRow.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone; //设置单元格上边框为无边框
            // rngRow.EntireColumn.AutoFit();     //自动调整列宽


            foreach (DataRow row in table.Rows)
            {
                rowindex++;
                colindex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    colindex++;
                    excel.Cells[rowindex, colindex] = row[col.ColumnName].ToString();
                    excel.Cells[rowindex, colindex].Font.Size = 12;
                    Range range = (Microsoft.Office.Interop.Excel.Range)sheetDest.Columns[colindex, Type.Missing];
                    range.Borders.LineStyle = 1;     //设置单元格边框的粗细
                    range.Borders.get_Item(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //sheetDest.Columns[colindex, Type.Missing].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    excel.Cells[rowindex, colindex].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //((Microsoft.Office.Interop.Excel.Range)sheetDest.Cells[2, 2]).ColumnWidth = 30;
                    range.EntireColumn.AutoFit();     //自动调整列宽
                }
            }
            sheetDest.get_Range("A1", cName).Columns.AutoFit();//合并单元格
            excel.Visible = false;
            // ((Worksheet)work.Sheets[0]).Name = "sss";   
            excel.ActiveWorkbook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            work.Close();
            excel.Quit();
            excel = null;
            GC.Collect();
            return true;
        }

        /// <summary>   
        /// 直接导出Excel   
        /// </summary>   
        /// <param name="sql">SQL查询语句</param>   
        /// <param name="columns">列名数组</param>   
        /// <param name="fileName">保存文件名(例如：E:\a.xls)</param>   
        /// <returns></returns>   
        public bool DoExport(string sql, string[] columns, string title, string fileName)
        {
            DataSet ds = DBSQLHelper.ExecuteDataSet(sql);
            if (ds.Tables.Count == 0 || fileName == string.Empty)
            {
                return false;
            }
            Application excel = new Application();
            int rowindex = 2;
            int colindex = 0;
            Workbook work = excel.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Worksheet sheetDest = work.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value) as Microsoft.Office.Interop.Excel.Worksheet;//给工作薄添加一个Sheet  
            sheetDest.Name = title;

            System.Data.DataTable table = ds.Tables[0];
            if (columns != null)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    colindex++;
                    if (columns[i] != null && columns[i] != "")
                    {
                        excel.Cells[2, colindex] = columns[i];
                    }
                    else
                    {
                        excel.Cells[2, colindex] = table.Columns[i].ColumnName;

                    }
                    excel.Cells[2, colindex].Font.Size = 15;
                    excel.Cells[2, colindex].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    excel.Cells[2, colindex].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.ActiveSheet.Rows[2].Font.Bold = true;   //设置第一行加粗
                    excel.ActiveSheet.Rows[2].Font.Bold = true;   //设置第一行加粗
                }
            }
            else
            {
                foreach (DataColumn col in table.Columns)
                {
                    colindex++;
                    excel.Cells[1, colindex] = col.ColumnName;
                }
            }

            excel.Cells[1, 1] = title;   //第一行标题
            Range rngRow = (Microsoft.Office.Interop.Excel.Range)sheetDest.Columns[1, Type.Missing];
            rngRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter; //第一行标题居中
            int columnIndex = table.Columns.Count;        //得到数据总列数
            string cName = ((ColumnName)columnIndex).ToString();
            sheetDest.get_Range("A1", cName).Merge(sheetDest.get_Range("A1", cName).MergeCells);//合并单元格
            excel.ActiveSheet.Rows[1].Font.Name = "华文行楷";//设置第一行字体
            excel.ActiveSheet.Rows[1].Font.Color = Color.Red; //设置第一行为红色
            excel.ActiveSheet.Rows[1].Font.Size = 30;
            excel.ActiveSheet.Rows[1].Font.Bold = true;   //设置第一行加粗
            //excel.ActiveSheet.Rows[1].Font.UnderLine = true;//设置第一行有下划线

            //  rngRow.NumberFormatLocal = "@";     //设置单元格格式为文本
            // rngRow.ColumnWidth = 15;     //设置单元格的宽度
            //rngRow.Cells.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 153).ToArgb();     //设置单元格的背景色
            // rngRow.Borders.LineStyle = 1;     //设置单元格边框的粗细
            // rngRow.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, System.Drawing.Color.Black.ToArgb());     //给单元格加边框
            //rngRow.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone; //设置单元格上边框为无边框
            // rngRow.EntireColumn.AutoFit();     //自动调整列宽


            foreach (DataRow row in table.Rows)
            {
                rowindex++;
                colindex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    colindex++;
                    excel.Cells[rowindex, colindex] = row[col.ColumnName].ToString();
                    excel.Cells[rowindex, colindex].Font.Size = 12;
                    Range range = (Microsoft.Office.Interop.Excel.Range)sheetDest.Columns[colindex, Type.Missing];
                    range.Borders.LineStyle = 1;     //设置单元格边框的粗细
                    range.Borders.get_Item(XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //sheetDest.Columns[colindex, Type.Missing].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    excel.Cells[rowindex, colindex].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    //((Microsoft.Office.Interop.Excel.Range)sheetDest.Cells[2, 2]).ColumnWidth = 30;
                    range.EntireColumn.AutoFit();     //自动调整列宽
                }
            }
            sheetDest.get_Range("A1", cName).Columns.AutoFit();//合并单元格
            excel.Visible = false;
            // ((Worksheet)work.Sheets[0]).Name = "sss";   
            excel.ActiveWorkbook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, null, null, false, false, XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
            work.Close();
            excel.Quit();
            excel = null;
            GC.Collect();
            return true;
        }
        /// <summary>   
        /// 通过流导出Excel   
        /// </summary>   
        /// <param name="ds">数据源DataSet</param>   
        /// <param name="fileName">保存文件名(例如：a.xls)</param>   
        /// <returns></returns>   
        public bool StreamExport(DataSet ds, string fileName)
        {
            if (ds.Tables.Count == 0 || fileName == string.Empty)
            {
                return false;
            }
            System.Data.DataTable dt = ds.Tables[0];
            StringBuilder content = new StringBuilder();
            StringBuilder strtitle = new StringBuilder();
            content.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'>");
            content.Append("<head><title></title><meta http-equiv='Content-Type' content=\"text/html; charset=gb2312\"></head><body><table x:str cellspacing='0' rules='all' border='1' id='title1' style=\"border-collapse:collapse;\" mce_style=\"border-collapse:collapse;\">");
            content.Append("<tr>");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                content.Append("<td>" + dt.Columns[j].ColumnName + "</td>");
            }
            content.Append("</tr>\n");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                content.Append("<tr>");
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    content.Append("<td>" + dt.Rows[j][k].ToString() + "</td>");
                }
                content.Append("</tr>\n");
            }
            content.Append("</table></body></html>");
            content.Replace(" ", "");
            //fileContent = (byte[])System.Text.Encoding.Default.GetBytes(content.ToString());       
            //System.Web.UI.WebControls.   
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            System.Web.HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);//HttpUtility.UrlEncode(fileName));   
            //            HttpUtility.UrlEncode(fileName,Encoding.UTF8);   
            //System.Web.HttpContext.Current.Response.ContentEncoding=System.Text.Encoding.Default;   
            System.Web.HttpContext.Current.Response.Write(content.ToString());
            System.Web.HttpContext.Current.Response.End();
            return true;
        }
        /// <summary>   
        /// 通过流导出Excel   
        /// </summary>   
        /// <param name="ds">数据源DataSet</param>   
        /// <param name="columns">列名数组,允许为空(columns=null),为空则表使用默认数据库列名</param>   
        /// <param name="fileName"></param>   
        /// <returns></returns>   
        public bool StreamExport(DataSet ds, string[] columns, string fileName)
        {
            if (ds.Tables.Count == 0 || fileName == string.Empty)
            {
                return false;
            }
            System.Data.DataTable dt = ds.Tables[0];
            StringBuilder content = new StringBuilder();
            StringBuilder strtitle = new StringBuilder();
            content.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'>");
            content.Append("<head><title></title><meta http-equiv='Content-Type' content=\"text/html; charset=gb2312\"></head><body><table x:str cellspacing='0' rules='all' border='1' id='title1' style=\"border-collapse:collapse;\" mce_style=\"border-collapse:collapse;\">");
            content.Append("<tr>");
            if (columns != null)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    if (columns[i] != null && columns[i] != "")
                    {
                        content.Append("<td>" + columns[i] + "</td>");
                    }
                    else
                    {
                        content.Append("<td>" + dt.Columns[i].ColumnName + "</td>");
                    }
                }
            }
            else
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    content.Append("<td>" + dt.Columns[j].ColumnName + "</td>");
                }
            }
            content.Append("</tr>\n");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                content.Append("<tr>");
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    content.Append("<td>" + dt.Rows[j][k].ToString() + "</td>");
                }
                content.Append("</tr>\n");
            }
            content.Append("</table></body></html>");
            content.Replace(" ", "");
            //fileContent = (byte[])System.Text.Encoding.Default.GetBytes(content.ToString());       
            //System.Web.UI.WebControls.   
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            System.Web.HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);//HttpUtility.UrlEncode(fileName));   
            //            HttpUtility.UrlEncode(fileName,Encoding.UTF8);   
            //System.Web.HttpContext.Current.Response.ContentEncoding=System.Text.Encoding.Default;   
            System.Web.HttpContext.Current.Response.Write(content.ToString());
            System.Web.HttpContext.Current.Response.End();
            return true;
        }
        /// <summary>   
        /// 通过流导出Excel   
        /// </summary>   
        /// <param name="ds">数据源DataSet</param>   
        /// <param name="columns">列名数组,允许为空(columns=null),为空则表使用默认数据库列名</param>   
        /// <param name="fileName"></param>   
        /// <returns></returns>   
        public bool StreamExport(string sql, string[] columns, string fileName)
        {

            DataSet ds = DBSQLHelper.ExecuteDataSet(sql);
            if (ds.Tables.Count == 0 || fileName == string.Empty)
            {
                return false;
            }
            System.Data.DataTable dt = ds.Tables[0];
            StringBuilder content = new StringBuilder();
            StringBuilder strtitle = new StringBuilder();
            content.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'>");
            content.Append("<head><title></title><meta http-equiv='Content-Type' content=\"text/html; charset=gb2312\"></head><body><table x:str cellspacing='0' rules='all' border='1' id='title1' style=\"border-collapse:collapse;\" mce_style=\"border-collapse:collapse;\">");
            content.Append("<tr>");
            if (columns != null)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    if (columns[i] != null && columns[i] != "")
                    {
                        content.Append("<td>" + columns[i] + "</td>");
                    }
                    else
                    {
                        content.Append("<td>" + dt.Columns[i].ColumnName + "</td>");
                    }
                }
            }
            else
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    content.Append("<td>" + dt.Columns[j].ColumnName + "</td>");
                }
            }
            content.Append("</tr>\n");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                content.Append("<tr>");
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    content.Append("<td>" + dt.Rows[j][k].ToString() + "</td>");
                }
                content.Append("</tr>\n");
            }
            content.Append("</table></body></html>");
            content.Replace(" ", "");
            //fileContent = (byte[])System.Text.Encoding.Default.GetBytes(content.ToString());       
            //System.Web.UI.WebControls.   
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            System.Web.HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);//HttpUtility.UrlEncode(fileName));   
            //            HttpUtility.UrlEncode(fileName,Encoding.UTF8);   
            //System.Web.HttpContext.Current.Response.ContentEncoding=System.Text.Encoding.Default;   
            System.Web.HttpContext.Current.Response.Write(content.ToString());
            System.Web.HttpContext.Current.Response.End();
            return true;
        }

    }
}
