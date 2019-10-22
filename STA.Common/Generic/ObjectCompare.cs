using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace STA.Common.Generic
{
    public class ObjectCompare : System.Collections.IComparer
    {
        SortEntry[] _sortEntrys;
      
        /// <summary> 
        /// 构造函数，制定排序方法 
        /// </summary> 
        /// <param name="type">对象的类型</param> 
        /// <param name="args">排序方法</param> 
        public ObjectCompare(Type type, params SortEntry[] args)
        {
            _sortEntrys = args;

            //为防止因为指定的排序字段的值一样而造成每次的排序结果不同，再加属性和字段做为辅助 
            int j = args.Length;

            PropertyInfo[] pis = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            FieldInfo[] fis = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            _sortEntrys = new SortEntry[_sortEntrys.Length + pis.Length + fis.Length];

            args.CopyTo(_sortEntrys, 0);

            foreach (PropertyInfo pi in pis)
                _sortEntrys[j++] = new SortEntry(pi.Name, pi.PropertyType, false);
            foreach (FieldInfo fi in fis)
                _sortEntrys[j++] = new SortEntry(fi.Name, fi.FieldType, false);
        }


        #region IComparer Members
        int Compare(object x, object y, string propertyName, Type propertyType)
        {
            try
            {
                object sa = GetObjectPropertyValue(x, propertyName);
                object sb = GetObjectPropertyValue(y, propertyName);
                object ta = sa == DBNull.Value ? null : sa;
                object tb = sb == DBNull.Value ? null : sb;

                if (null == ta && null == tb)
                    return 0;
                else if (null == ta && null != tb)
                    return -1;
                else if (null != ta && null == tb)
                    return 1;
                else
                {
                    ta = Convert.ChangeType(sa, propertyType);
                    tb = Convert.ChangeType(sb, propertyType);
                    return ((IComparable)ta).CompareTo(tb);
                }
            }
            catch
            {
                throw;
            }
        }
        /**/
        /// <summary> 
        /// 比较两个对象的大小 
        /// </summary> 
        /// <param name="x"></param> 
        /// <param name="y"></param> 
        /// <returns>如果对象x大于对象y返回1，对象x小于对象y返回-1,相等则返回0</returns> 
        public int Compare(object x, object y)
        {
            try
            {
                int result;

                for (int i = 0; i < _sortEntrys.Length; i++)
                {
                    SortEntry se = _sortEntrys[i];
                    result = Compare(x, y, se.PropertyName, se.ProeprtyType);
                    if (se.Descend)
                        result = -result;
                    if (0 != result)
                        return result;
                }
                //为避免两个对象的属性值一样，再比较他们的hashcode 
                return x.GetHashCode().CompareTo(y.GetHashCode());
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region public object GetObjectPropertyValue(object obj, string propertyName)
        /**/
        /// <summary> 
        /// 得到对象的属性值 
        /// </summary> 
        /// <param name="obj">对象名</param> 
        /// <param name="propertyName">属性名</param> 
        /// <returns>属性的值，如不存在则反回Null</returns> 
        public object GetObjectPropertyValue(object instance, string propertyName)
        {
            try
            {
                Type t = instance.GetType();

                return t.InvokeMember(propertyName,
                    BindingFlags.GetProperty | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                    null, instance, null);
            }
            catch
            {
                throw new Exception(string.Format("未找到属性或字段{0}！", propertyName));
            }
        }
        #endregion
    }

    /**/
    /// <summary> 
    /// 排序方式设定 
    /// </summary> 
    public struct SortEntry
    {
        /**/
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="propertyName">属性名</param> 
        /// <param name="value">是否倒序</param> 
        public SortEntry(string propertyName) : this(propertyName, typeof(string), false) { }

        /**/
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="propertyName">属性名</param> 
        /// <param name="value">是否倒序</param> 
        public SortEntry(string propertyName, bool isDescend) : this(propertyName, typeof(string), isDescend) { }

        /**/
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="propertyName">属性名</param> 
        /// <param name="value">是否倒序</param> 
        /// <param name="type">属性类型</param> 
        public SortEntry(string propertyName, Type type, bool isDescend)
        {
            if (propertyName + "" == "")
                throw new Exception("属性名不能为空！");

            if (type == null)
                throw new Exception("属性的类型不能为空！");
            _propertyName = propertyName;
            _descend = isDescend;
            _type = type;
        }

        /**/
        /// <summary> 
        /// 属性名 
        /// </summary> 
        string _propertyName;
        public string PropertyName { get { return _propertyName; } set { _propertyName = value; } }

        /**/
        /// <summary> 
        /// 是否倒序 
        /// </summary> 
        bool _descend;
        public bool Descend { get { return _descend; } set { _descend = value; } }

        Type _type;
        /**/
        /// <summary> 
        /// 属性的类型 
        /// </summary> 
        /// <value></value> 
        public Type ProeprtyType { get { return _type; } set { _type = value; } }
    }
}
