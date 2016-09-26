using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Office.Interop.Word;
using System.IO;
using ZbClassLibrary;
using System.Text.RegularExpressions;
using System.Text;

/// <summary>
/// WordHelper 的摘要说明
/// </summary>
public class WordHelper
{
    Application appWord;
    Document doc;
    object oMissing = System.Reflection.Missing.Value;
    object objFalse = (object)false;
    object objTrue = (object)true;
    object objDocType = WdDocumentType.wdTypeDocument;
    public string Text = "";

    bool Exists = false;
    public WordHelper()
    {

    }
    public WordHelper(string filePath)
    {
        if (!File.Exists(filePath)) return;
        Exists = true;
        object objTemplate = filePath;
        appWord = new Application();
        //doc = new Document();
        doc = (Document)appWord.Documents.Add(ref objTemplate, ref objFalse, ref objDocType, ref objTrue);
        Text = doc.Content.Text.ToString();
    }
    public void Replace(string oldchr, string newchr)
    {
        object objReplace = WdReplace.wdReplaceAll;
        object findtext = oldchr;
        object newtext = newchr;
        appWord.Selection.Find.Execute(ref findtext, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref newtext, ref objReplace, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        doc.Content.Find.Execute(ref findtext, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref newtext, ref objReplace, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
    }
    public void SaveAs(string path)
    {
        object objTemplateb = path;
        doc.SaveAs(ref objTemplateb, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        doc.Close(ref oMissing, ref oMissing, ref oMissing);
        appWord.Quit(ref oMissing, ref oMissing, ref oMissing);
    }
    public void CreateWordTable(GSWordModel Model)
    {

        Microsoft.Office.Interop.Word.Table newTable = null;
        object what = WdGoToItem.wdGoToLine;

        object which = WdGoToDirection.wdGoToLast;

        object count = 99999999;

        doc.Application.Selection.GoTo(ref what, ref which, ref count, ref oMissing);
        doc.Paragraphs.Last.Range.Text = "\r\n";//
        int ThisR = 0;
        foreach (var item in Model.SModel)
        {

            //文档中创建表格
            newTable = doc.Tables.Add(appWord.Selection.Range, item.BidRankWord.Count + 2, 6, ref oMissing, ref oMissing);
            //填充表格内容
            newTable.Cell(1, 1).Range.Text = item.SectionName;
            newTable.Cell(1, 1).Range.Bold = 2;//设置单元格中字体为粗体
            newTable.Cell(1, 1).Range.Font.Size = 12;
            //合并单元格
            newTable.Cell(1, 1).Merge(newTable.Cell(1, 6));
            newTable.Rows[1].Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
            newTable.Rows[1].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中
            newTable.Cell(2, 1).Range.Text = "序号";
            newTable.Cell(2, 1).Range.Font.Size = 12;
            newTable.Cell(2, 2).Range.Text = "推荐顺序";
            newTable.Cell(2, 2).Range.Font.Size = 12;
            newTable.Cell(2, 3).Range.Text = "中标候选人名称";
            newTable.Cell(2, 3).Range.Font.Size = 12;
            newTable.Cell(2, 4).Range.Text = "投标报价";
            newTable.Cell(2, 4).Range.Font.Size = 12;
            newTable.Cell(2, 5).Range.Text = "工期承诺";
            newTable.Cell(2, 5).Range.Font.Size = 12;
            newTable.Cell(2, 6).Range.Text = "质量承诺";
            newTable.Cell(2, 6).Range.Font.Size = 12;
            newTable.Rows[2].Height = 25;
            newTable.Rows[2].Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
            newTable.Rows[2].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中
            int i = 1;
            foreach (var item1 in item.BidRankWord)
            {
                newTable.Cell((i + 2), 1).Range.Text = i.ToString();
                newTable.Cell((i + 2), 1).Range.Font.Size = 12;
                newTable.Cell((i + 2), 2).Range.Text = "推荐第" + i.ToString() + "中标候选人";
                newTable.Cell((i + 2), 2).Range.Font.Size = 12;
                newTable.Cell((i + 2), 3).Range.Text = item1.CompanyName;
                newTable.Cell((i + 2), 3).Range.Font.Size = 12;
                newTable.Cell((i + 2), 4).Range.Text = item1.DesignMoney;
                newTable.Cell((i + 2), 4).Range.Font.Size = 12;
                newTable.Cell((i + 2), 5).Range.Text = item1.CyclePromise;
                newTable.Cell((i + 2), 5).Range.Font.Size = 12;
                newTable.Cell((i + 2), 6).Range.Text = item1.QualityPromise;
                newTable.Cell((i + 2), 6).Range.Font.Size = 12;
                newTable.Rows[(i + 2)].Height = 25;
                newTable.Rows[(i + 2)].Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
                newTable.Rows[(i + 2)].Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

                i++;
            }

            //设置表格样式
            newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

            //在表格中增加行
            //  doc.Content.Tables[1].Rows.Add(ref oMissing);
            ThisR++;
            if (ThisR == Model.SModel.Count)
            {

                object oEndOfDoc = "\\endofdoc";
                Microsoft.Office.Interop.Word.Paragraph oPara2;
                object oRng = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oPara2 = doc.Content.Paragraphs.Add(ref oRng);
                oPara2.Range.Text = "      特此公示";
                oPara2.Range.Font.Size = 14;
                oPara2.Range.InsertParagraphAfter();
                oPara2.Range.InsertParagraphAfter();
                oRng = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oPara2 = doc.Content.Paragraphs.Add(ref oRng);
                oPara2.Range.Text = "招  标  人：" + Model.ZBR;
                oPara2.Range.Font.Size = 14;
                oPara2.Range.InsertParagraphAfter();
                oRng = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oPara2 = doc.Content.Paragraphs.Add(ref oRng);
                oPara2.Range.Text = "招标代理机构：" + Model.ZBDLJG;
                oPara2.Range.Font.Size = 14;
                oPara2.Range.InsertParagraphAfter();
                oRng = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oPara2 = doc.Content.Paragraphs.Add(ref oRng);
                oPara2.Range.Text = "联  系  电  话：" + Model.ZBDLJGDH;
                oPara2.Range.Font.Size = 14;
                oPara2.Range.InsertParagraphAfter();
                oRng = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oPara2 = doc.Content.Paragraphs.Add(ref oRng);
                oPara2.Range.Text = "                          日期：" + DateTime.Now.ToString("yyyy  年  MM  月  dd  日") + "";
                oPara2.Range.Font.Size = 14;
                oPara2.Range.InsertParagraphAfter();
            }


        }


    }
    #region 把Word文档装化为Html文件


    /// <summary>
    /// 转换word为html
    /// </summary>
    /// <param name="filename">doc文件路径</param>
    /// <param name="savefilename">pdf保存路径</param>
    public string ConvertWordHtml(object filename)
    {


        Object Nothing = System.Reflection.Missing.Value;
        //创建一个名为WordApp的组件对象
        Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
        //创建一个名为WordDoc的文档对象并打开
        /*2015-12-16修复，文档要以docx的方式打开*/
        // Microsoft.Office.Interop.Word.Document doc = (Document)wordApp.Documents.Add(ref filename, ref objFalse, ref objDocType, ref objTrue);
        Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
        //设置保存的格式
        object filefarmat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML;
        string GuId = System.Guid.NewGuid().ToString("N");
        //保存为html
        object savefilename = HttpContext.Current.Server.MapPath("/ALLFile/ProjectFile/WordHtml");
        if (!Directory.Exists(savefilename.ToString()))
        {
            Directory.CreateDirectory(savefilename.ToString());
        }
        savefilename = savefilename + "/" + GuId + ".html";
        doc.SaveAs(ref savefilename, ref filefarmat, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
        //关闭文档对象
        doc.Close(ref Nothing, ref Nothing, ref Nothing);
        //推出组建
        wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
        return ReadFile(savefilename.ToString());
    }
    /// <summary>  
    /// 读文件  
    /// </summary>  
    /// <param name="path">文件路径</param>  
    /// <returns></returns>  
    private string ReadFile(string Path)
    {
        try
        {
            StreamReader sr = new StreamReader(Path, Encoding.GetEncoding("GB2312"));

            string content = sr.ReadToEnd().ToString();
            Regex reg = new Regex("(?is)<body[^>]*>(?<body>.*?)</body>");
            string result = reg.Match(content).Groups["body"].Value;
            result = result.Replace("style='mso-spacerun:yes'", "");
            sr.Close();
            return result;
        }
        catch
        {
            return "";
        }
    }
}

    #endregion