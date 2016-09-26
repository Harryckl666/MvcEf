using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelToSql.Enums
{
    /// <summary>
    /// 资格审查方式
    /// </summary>
    public enum AuditModeType
    {
        [Description("资格预审")]
        Preliminary = 0,
        [Description("资格后审")]
        AfterTheTrial = 1
    }
}
