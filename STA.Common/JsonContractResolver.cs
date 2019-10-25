using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace STA.Common
{
    public class DynamicContractResolver : DefaultContractResolver
    {
        private readonly char _startingWithChar;

        public DynamicContractResolver(char startingWithChar)
        {
            _startingWithChar = startingWithChar;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            // only serializer properties that start with the specified character
            properties =
                properties.Where(p => !p.PropertyName.StartsWith(_startingWithChar.ToString())).ToList();

            return properties;
        }
    }



    //REF: http://james.newtonking.com/archive/2009/10/23/efficient-json-with-json-net-reducing-serialized-json-size.aspx

    #region  Contract Resolver:指定要序列化屬性的清單
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] props = null;
        public LimitPropsContractResolver(string[] props)
        {
            //指定要序列化屬性的清單
            this.props = props;
        }



        protected override IList<JsonProperty> CreateProperties(Type type,
            MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            // only serializer properties that start with the specified character
            properties =
                properties.Where(p => props.Contains(p.PropertyName)).ToList();

            return properties;
        }

        //protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        //{
        //    JsonProperty property = base.CreateProperty(member, memberSerialization);
        //    property.ShouldSerialize =
        //        instance =>
        //        {
        //            return props.Contains(property.PropertyName);
        //        };
        //    return property;
        //}

    }
    #endregion

    #region  Contract Resolver:移除不需要序列化的清单
    public class RemovePropsContractResolver : DefaultContractResolver
    {
        protected internal string props = "";
        public RemovePropsContractResolver(string props)
        {
            this.props = props;
        }

        protected override IList<JsonProperty> CreateProperties(Type type,
            MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list =
                base.CreateProperties(type, memberSerialization);
            return list.Where(p => !props.Split(',').Contains(p.PropertyName)).ToList();
        }

    }
    #endregion
}
