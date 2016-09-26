using CommonClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelToSql.Model
{
    [Description("组名")]
    public class Sys_Group : Entity
    {
        [StringLength(50)]
        [Required]
        [DisplayName("组名")]
        public string GroupName { get; set; }
        [DisplayName("部门编号")]
        public Guid SysDepartId { get; set; }
        [DisplayName("排序")]
        public int sort { get; set; }
    }
}
