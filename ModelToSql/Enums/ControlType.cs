using System.ComponentModel;

namespace ModelToSql.Enums
{
    public enum ControlType
    {
        [Description("文本框")]
        Text = 0,
        [Description("文本域")]
        Textarea = 1,
        [Description("富文本")]
        RichText = 2,
        [Description("下拉")]
        Select = 3,
        [Description("多选下拉")]
        MultiSelect = 4,
        [Description("单选")]
        Radio = 5,
        [Description("多选")]
        Checkbox = 6,
        [Description("时间")]
        Time = 7,
        [Description("文件上传")]
        Upfile = 8,
        [Description("多文件上传")]
        MultiUpfile = 9,
    }
}
