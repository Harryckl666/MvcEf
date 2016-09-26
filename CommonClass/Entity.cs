using Easy.Framework.Core.Domain.Entities;
using Easy.Framework.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClass
{
    [Serializable]
    public class Entity :AggregateRoot, IAudited, ISoftDelete
    {
        /// <summary>
        /// 添加用户Id
        /// </summary>
        [Description("添加用户Id")]
        [StringLength(50)]
        public string AddUserId { get; set; }
        /// <summary>
        /// 修改用户Id
        /// </summary>
        [Description("修改用户Id")]
        [StringLength(50)]
        public string LastUserId { get; set; }

        /// <summary>
        ///获取或设置 添加时间
        /// </summary>
        [Description("获取或设置 添加时间")]
        [DataType(DataType.DateTime)]
        public DateTime AddDate { get; set; }

        /// <summary>
        ///获取或设置 修改时间
        /// </summary>
        [Description("获取或设置 修改时间")]
        [DataType(DataType.DateTime)]
        public DateTime? LastDate { get; set; }

        /// <summary>
        /// 获取或设置 获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [Description("逻辑删除")]
        public bool IsDeleted { get; set; }
    }
}
