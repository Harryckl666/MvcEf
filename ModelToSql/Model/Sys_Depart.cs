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
    [Description("部门")]
    public class Sys_Depart : Entity
    {
        [StringLength(50)]
        [Required]
        [DisplayName("部门名称")]
        public string DepartName { get; set; }
        [DisplayName("排序")]
        public int sort { get; set; }
        [DisplayName("公司编号")]
        public Guid? Sys_CompanyId { get; set; }

    }
}
