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
    [Description("公司名称")]
    public class Sys_Company : Entity
    {
        [StringLength(50)]
        [Required]
        [DisplayName("公司名称")]
        public string companyName { get; set; }
        [DisplayName("排序")]
        public int sort { get; set; }
        [DisplayName("父编号")]
        public Guid? ParentId { get; set; }
    }
}
