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
    [Description("角色")]
    public class Sys_User : Entity
    {
        [StringLength(50)]
        [Required]
        [DisplayName("用户名称")]
        public string SysRealName { get; set; }
        [Description("密码")]
        public string Password { get; set; }
        public virtual ICollection<Sys_lcy> syslcy { get; set; }

        [DefaultValue(false)]
        public bool IsSuperManager { get; set; }
    }
}
