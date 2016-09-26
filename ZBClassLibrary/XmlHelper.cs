using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ZbClassLibrary
{
    public class XmlHelper
    {
        /// <summary>
        /// xml文档
        /// </summary>
        XmlDocument xd = new XmlDocument();

        /// <summary>
        /// 从文件中获得XmlDocument对象
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        public XmlHelper(string file,string type)
        {
            if (type == "path")
            {
                XmlTextReader xr = new XmlTextReader(file);
                xd.Load(xr);
                xr.Close();
            }

            if (type == "text")
            {
                xd.LoadXml(file);
            }
        }

        /// <summary>
        /// 获取第一个匹配的节点的属性值
        /// </summary>
        /// <param name="nodePath"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public string GetAttribute(string nodePath, string attrName)
        {
            XmlNode xn = xd.SelectSingleNode(nodePath);

            if (xn == null)//检测节点是否存在
            {
                return "";
            }
            if (xn.Attributes[attrName] == null)//检测属性是否存在
            {
                return "";
            }

            return xn.Attributes[attrName].Value;
        }


        /// <summary>
        /// 获取某个节点
        /// </summary>
        /// <param name="nodePath">节点PATH</param>
        public XmlNode GetNode(string nodePath)
        {
            XmlNode xn = xd.SelectSingleNode(nodePath);

            return xn;
        }

        /// <summary>
        /// 获取某个节点子集合指定属性值的节点
        /// </summary>
        /// <param name="nodePath">父节点PATH</param>
        public XmlNode GetNode(string nodePath,string attrName,string attrValue)
        {
            XmlNodeList list = GetChildNodes(nodePath);

            if (list != null)
            {
                foreach (XmlNode node in list)
                {
                    if (node.Attributes[attrName] != null)//检测属性是否存在
                    {
                        if (node.Attributes[attrName].Value.Trim() == attrValue)
                        {
                            return node;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取某个节点的所有子节点
        /// </summary>
        /// <param name="nodePath">节点PATH</param>
        public XmlNodeList GetChildNodes(string nodePath)
        {
            XmlNodeList nodes = null;

            XmlNode xn = xd.SelectSingleNode(nodePath);

            if (xn != null)//检测节点是否存在
            {
                nodes = xn.ChildNodes;
            }

            return nodes;
        }
    }
}
