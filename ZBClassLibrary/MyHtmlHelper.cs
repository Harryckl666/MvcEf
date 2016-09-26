using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ZbClassLibrary
{
    public class MyHtmlHelper
    {
        /// <summary>
        /// 是否开启
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetYesNoList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "" });
            }
            list.Add(new SelectListItem { Text = "否", Value = "False" });
            list.Add(new SelectListItem { Text = "是", Value = "True" });
            return list;
        }
        /// <summary>
        /// 返回是否显示列表
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetDisplayList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "" });
            }
            list.Add(new SelectListItem { Text = "显示", Value = "True" });
            list.Add(new SelectListItem { Text = "不显示", Value = "False" });
            return list;
        }
        /// <summary>
        /// 是否开启
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetOpenList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "" });
            }
            list.Add(new SelectListItem { Text = "关闭", Value = "False" });
            list.Add(new SelectListItem { Text = "开启", Value = "True" });
            return list;
        }
        /// <summary>
        /// 返回状态是否正常
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetStatusList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "" });
            }
            list.Add(new SelectListItem { Text = "有效", Value = "True" });
            list.Add(new SelectListItem { Text = "无效", Value = "False" });
            return list;
        }
        /// <summary>
        /// 返回是否可授权
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetIsEmpowerList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "" });
            }
            list.Add(new SelectListItem { Text = "可授权", Value = "True" });
            list.Add(new SelectListItem { Text = "无授权", Value = "False" });
            return list;
        }
        /// <summary>
        /// 返回授权范围
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetEmpowerRangeList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "" });
            }
            list.Add(new SelectListItem { Text = "全局", Value = "True" });
            list.Add(new SelectListItem { Text = "角色", Value = "False" });
            return list;
        }
        /// <summary>
        /// 返回是否上架列表
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetShelvesList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "全部", Value = "" });
            }
            list.Add(new SelectListItem { Text = "已上架", Value = "True" });
            list.Add(new SelectListItem { Text = "已下架", Value = "False" });
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetWaterTypeList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "" });
            }
            list.Add(new SelectListItem { Text = "文字", Value = "0" });
            list.Add(new SelectListItem { Text = "图片", Value = "1" });
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetWaterPosList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "" });
            }
            list.Add(new SelectListItem { Text = "左上", Value = "1" });
            list.Add(new SelectListItem { Text = "上中", Value = "2" });
            list.Add(new SelectListItem { Text = "右上", Value = "3" });
            list.Add(new SelectListItem { Text = "左中", Value = "4" });
            list.Add(new SelectListItem { Text = "正中", Value = "5" });
            list.Add(new SelectListItem { Text = "右中", Value = "6" });
            list.Add(new SelectListItem { Text = "左下", Value = "7" });
            list.Add(new SelectListItem { Text = "下中", Value = "8" });
            list.Add(new SelectListItem { Text = "右下", Value = "9" });
            return list;
        }
        /// <summary>
        /// 页面类型
        /// </summary>
        /// <param name="isDefault">如果为true多一项(请选择)</param>
        /// <returns></returns>
        public static IList<SelectListItem> GetPageTypeList(bool isDefault = false)
        {
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isDefault)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "" });
            }
            list.Add(new SelectListItem { Text = "列表页", Value = "1" });
            list.Add(new SelectListItem { Text = "详细页", Value = "2" });
            return list;
        }
    }
}
