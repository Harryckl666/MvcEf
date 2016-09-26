using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelToSql.Enums
{
    [Description("资源标识")]
    //资源标识，从1000开始
    public enum ResourceIdentity
    {
        [Description("项目八大块标识")]
        ProjectInfo = 1000
    }
}
