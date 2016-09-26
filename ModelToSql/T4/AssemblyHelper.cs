using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ModelToSql.Model.T4
{
    public static class AssemblyHelper
    {

        public static List<ModelT4> GetT4List(string assemblyName, string modelFolder)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            var types = assembly.GetTypes();
            var taList = new List<ModelT4>();
            foreach (var item in types)
            {

                if (item.Namespace != null && item.Namespace.Equals(assemblyName + modelFolder))
                {
                    var m = new ModelT4 { Name = item.Name };
                    var descAtt = (DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute));
                    if (descAtt != null)
                    {
                        m.Description = descAtt.Description;
                    }
                    taList.Add(m);
                }

            }
            return taList;
        }


        public static List<ModelT4> GetAssemblyProperty(string assemblyName, string modelFolder, string modelName)
        {
            Assembly tableObject = Assembly.Load(assemblyName);

            Type t = tableObject.GetType(assemblyName + modelFolder + "." + modelName);
            PropertyInfo[] ps = t.GetProperties();
            return ps.Select(item => new ModelT4 {Name = item.Name}).ToList();
        }
        public static ModelT4 GetAssemblyProperty(string assemblyName, string modelFolder, string modelName, string propertyName)
        {
            Assembly tableObject = Assembly.Load(assemblyName);

            Type t = tableObject.GetType(assemblyName + modelFolder + "." + modelName);
            PropertyInfo propertyInfo = t.GetProperty(propertyName); //获取指定名称的属性
            List<TypeInfo> infos = tableObject.DefinedTypes.ToList();
            string thistype = propertyInfo.PropertyType.Name;
            ModelT4 model = new ModelT4();
            if (infos.Count(w => w.Name == thistype) > 0)
            {
                model.DisPlayName = "model";
                model.Name = thistype;
            }
            if (thistype.IndexOf("ICollection", StringComparison.Ordinal) >= 0)
            {
                model.DisPlayName = "list";
            }
            if (((DisplayNameAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayNameAttribute))) != null)
            {
                model.Description = ((DisplayNameAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayNameAttribute))).DisplayName;// 显示值 
            }
            else if (((DescriptionAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DescriptionAttribute))) != null)
            {
                model.Description = ((DescriptionAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DescriptionAttribute))).Description;// 属性值 
            }
            return model;
        }
        //T4实体
        public class ModelT4
        {
            //类名
            public string Name { get; set; }

            public string DisPlayName { get; set; }
            //说明
            public string Description { get; set; }
        }
    }
}
