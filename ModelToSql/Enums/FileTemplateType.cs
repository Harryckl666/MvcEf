using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelToSql.Enums
{
    public enum FileTemplateType
    {
        [Description("招标公告")]
        招标公告 = 1000,
        [Description("中标公示")]
        中标公示 = 1001,
        [Description("其他文件")]
        其他文件 = 1002
    }
}
