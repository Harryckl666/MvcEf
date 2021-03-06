﻿using CommonClass;
using ModelToSql.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelToSql.Model
{
    public class ButtonDic : Entity
    {
        [StringLength(50)]
        [DisplayName("默认按钮名称")]
        public string BtnName { get; set; }

        [StringLength(50)]
        [DisplayName("按钮资源")]
        public string BtnUri { get; set; }

        [StringLength(50)]
        [DisplayName("默认按钮样式")]
        public string BtnClass { get; set; }
        [StringLength(50)]
        [Description("按钮图标别名")]
        public string BtnIco
        {
            get;
            set;

        }

        [DisplayName("按钮类型（grid和toolbar）")]
        public ButtonEnum BtnType { get; set; }

        public int Sort { get; set; }
    }
}
