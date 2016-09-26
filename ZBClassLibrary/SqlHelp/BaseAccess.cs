using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPocoModel;
using PetaPoco;
using System.Reflection;
namespace ZBClassLibrary
{
    /// <summary>
    /// 数据库操作辅助类
    /// </summary>
    public class BaseAccess<T> where T : new()
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        protected DBHelper db = DBHelper.GetInstance();

        /// <summary>
        /// 表主键
        /// </summary>
        protected string primarykey = "";
        /// <summary>
        /// 实体类名称
        /// </summary>
        protected string modelname = "";
        /// <summary>
        /// 默认查询条件
        /// </summary>
        protected string WHERE = " 1=1 ";
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseAccess(DBHelper sdb = null)
        {
            Type t = typeof(T);
            object[] keyattrs = t.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
            object[] nameattrs = t.GetCustomAttributes(typeof(TableNameAttribute), true);
            PrimaryKeyAttribute keyattr = null;
            TableNameAttribute nameattr = null;
            if (keyattrs.Length == 1)
            {
                keyattr = (PrimaryKeyAttribute)keyattrs[0];
                this.primarykey = keyattr.Value;
            }
            if (nameattrs.Length == 1)
            {
                nameattr = (TableNameAttribute)nameattrs[0];
                this.modelname = nameattr.Value;
            }
            if (sdb != null)
            {
                db = sdb;
            }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="objClass"></param>
        /// <param name="propertyName"></param>
        public virtual object getProperty(T objClass, string propertyName)
        {
            if (objClass != null)
            {
                PropertyInfo[] infos = objClass.GetType().GetProperties();
                foreach (PropertyInfo info in infos)
                {
                    if (info.Name == propertyName && info.CanRead)
                    {
                        return info.GetValue(objClass, null);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="objClass"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public virtual void setProperty(ref T objClass, string propertyName, object value)
        {
            if (objClass != null)
            {
                PropertyInfo[] infos = objClass.GetType().GetProperties();
                foreach (PropertyInfo info in infos)
                {
                    if (info.Name == propertyName && info.CanWrite)
                    {
                        info.SetValue(objClass, value, null);
                    }
                }
            }
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>    
        public virtual object ObjectInsert(T entity)
        {

            object result = db.Insert(entity);
            return result;
        }
        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int ObjectUpdate(T entity)
        {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo p in ps)
            {
                object value = p.GetValue(entity, null);
                if (value != null && p.Name != primarykey)
                {
                    sb.Append(p.Name);
                    sb.Append(",");
                }
            }
            string[] colunms = sb.ToString().TrimEnd(',').Split(',');
            int result = db.Update(entity, colunms);
            return result;
        }
        /// <summary>
        /// 条件更新某字段的值
        /// </summary>
        public virtual int WhereUpdate(string where, string feild, object value)
        {
            int result = db.Update<T>("SET " + feild + "=@0 WHERE " + WHERE + where, value);
            return result;
        }
        /// <summary>
        /// 更新某字段的值
        /// </summary>
        /// <param name="id">主键值</param>
        /// <param name="feild">字段名称</param>
        /// <param name="value">字段值</param>
        public virtual int ObjectUpdate(object id, string feild, object value)
        {
            return db.Update<T>("SET " + feild + "=@0 WHERE " + primarykey + "=@1", value, id);
        }
        /// <summary>
        /// 删除id对应的实体
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int ObjectDelete(object id)
        {
            int result = db.Delete<T>(id);
            return result;
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int ObjectDelete(T entity)
        {
            int result = db.Delete<T>(entity); ;
            return result;
        }
        /// <summary>
        /// 根据Id 更新feild字段的值
        /// </summary>
        public virtual int UpdateField(object id, string feild, object value)
        {
            return db.Update<T>("SET " + feild + "=@0 WHERE " + primarykey + "=@1", value, id);
        }
        /// <summary>
        /// 根据Id 假删,更新假删 bool型标示字段feild
        /// </summary>
        public virtual int UpdateDelete(int id, string feild = "IsDelete")
        {
            return db.Update<T>("SET " + feild + "=@0 WHERE " + primarykey + "=@1", "True", id);
        }
        /// <summary>
        /// 根据Id 假删,更新假删 bool型标示字段feild
        /// </summary>
        public virtual int UpdateDelete(decimal id, string feild = "IsDelete")
        {
            return db.Update<T>("SET " + feild + "=@0 WHERE " + primarykey + "=@1", "True", id);
        }
        /// <summary>
        /// 根据条件 假删,更新假删 bool型标示字段feild
        /// </summary>
        public virtual int UpdateDelete(string where, string feild = "IsDelete")
        {
            var sql = string.Format("SET " + feild + "={0} WHERE  {1}", "1", WHERE + where);
            return db.Update<T>(sql);
        }
        /// <summary>
        /// 根据条件，删除实体
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int ObjectDelete(string where)
        {
            var sql = string.Format("DELETE FROM {0} WHERE {1}", modelname, WHERE + where);
            return db.Execute(sql, null);
        }
        /// <summary>
        /// 统计实体数量
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual long ObjectCount(string where)
        {
            return db.ExecuteScalar<long>("SELECT Count(*) FROM " + modelname + " WHERE " + WHERE + where);
        }
        /// <summary>
        /// Id获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetObject(object id)
        {
            return db.SingleOrDefault<T>("SELECT TOP 1 * FROM " + modelname + " WHERE " + primarykey + "=@0", id);
        }
        /// <summary>
        /// 检查主键值是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool IsExists(object id)
        {
            return db.Exists<T>(id);
        }
        /// <summary>
        /// Id获取一个实体的feild字段值
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="feild"></param>
        /// <returns></returns>
        public virtual object GetModelFeild(object id, string field)
        {
            object result = db.ExecuteScalar<object>("SELECT " + field + " FROM " + modelname + " WHERE " + primarykey + "=@0", id);
            if (result == null)
                return "";
            return result;
            //var md = db.SingleOrDefault<T>("SELECT " + feild + " FROM " + modelname + " WHERE " + primarykey + "=@0", id);
            //var feildvalue = getProperty(md, feild);
            //if (feildvalue == null)
            //{
            //    return "";
            //}
            //else
            //{
            //    return feildvalue;
            //}
        }
        /// <summary>
        /// 条件获取一个实体的feild字段值
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="feild"></param>
        /// <returns></returns>
        public virtual object GetModelFeildWhere(string where, string order, string feild)
        {
            var sql = string.Format("SELECT " + feild + " FROM " + modelname + " WHERE  {0} {1}", WHERE + where, order);
            var md = db.SingleOrDefault<T>(sql);
            var feildvalue = getProperty(md, feild);
            if (feildvalue == null)
            {
                return "";
            }
            else
            {
                return feildvalue;
            }
        }
        /// <summary>
        /// 条件获取一个实体
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual T GetObject(string where, string order)
        {
            var sql = string.Format("SELECT TOP 1 * FROM " + modelname + " WHERE {0} {1} ", WHERE + where, order);
            T t = db.SingleOrDefault<T>(sql);
            if (t != null)
            {
                return t;
            }
            else
            {
                T poco = new T();
                return poco;
            }
        }
        /// <summary>
        /// 按条件获取多个实体
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetModelList(string where, string order)
        {
            var sql = string.Format("SELECT * FROM " + modelname + " WHERE {0} {1} ", WHERE + where, order);
            return db.Fetch<T>(sql);
        }
        /// <summary>
        /// 按条件获取前 count 个实体
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetModelList(int count, string where, string order)
        {
            var sql = string.Format("SELECT TOP " + count + " * FROM " + modelname + " WHERE {0} {1} ", WHERE + where, order);
            return db.Fetch<T>(sql);
        }
        /// <summary>
        /// 分页查询多个实体
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="where"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual List<T> GetPager(out long count, out long allpage, int pagesize = 20, int pageindex = 1, string where = "", string order = "")
        {
            var sql = string.Format("SELECT * FROM " + modelname + " WHERE {0} {1} ", WHERE + where, order);
            Page<T> page = db.Page<T>(pageindex, pagesize, sql);
            count = page.TotalItems;
            allpage = page.TotalPages;
            return page.Items;
        }
        /// <summary>
        /// 自定义分页查询多个实体
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="where"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual List<T> GetPager(out long count, out long allpage, int pagesize = 20, int pageindex = 1, string sqlstr = "")
        {
            var sql = sqlstr;
            Page<T> page = db.Page<T>(pageindex, pagesize, sql);
            count = page.TotalItems;
            allpage = page.TotalPages;
            return page.Items;
        }
        public virtual List<T> GetPagerSql(out long count, out long allpage, string sqlCount, int pagesize = 20, int pageindex = 1, string sqlstr = "")
        {
            var sql = sqlstr;
            Page<T> page = db.Page<T>(pageindex, pagesize, sql, sqlCount);
            count = page.TotalItems;
            allpage = page.TotalPages;
            return page.Items;
        }
        public virtual List<T> GetPager(string str, out long count, out long allpage, int pagesize = 20, int pageindex = 1, string where = "", string order = "")
        {
            var sql = string.Format("SELECT " + str + " FROM " + modelname + " WHERE {0} {1} ", WHERE + where, order);
            Page<T> page = db.Page<T>(pageindex, pagesize, sql);
            count = page.TotalItems;
            allpage = page.TotalPages;
            return page.Items;
        }

        /// <summary>
        /// 当前数据库主键最大值
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual long GetMaxId()
        {
            long maxid = 0;
            try
            {
                maxid = db.ExecuteScalar<long>("SELECT MAX(" + primarykey + ") FROM " + modelname);
            }
            catch
            {

            }
            return maxid;
        }
    }
}
