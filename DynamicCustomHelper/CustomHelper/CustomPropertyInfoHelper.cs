using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomHelper
{
    // Custom implementation of the PropertyInfo
    internal class CustomPropertyInfoHelper : PropertyInfo
    {
        readonly string _name;
        internal readonly Type _type;
        readonly MethodInfo _getMethod, _setMethod;
        readonly List<Attribute> _attributes = new List<Attribute>();

        public CustomPropertyInfoHelper(string name, Type type, Type ownerType)
        {
            _name = name;
            _type = type;
            _getMethod = ownerType.GetMethods().Single(m => m.Name == "GetPropertyValue" && !m.IsGenericMethod);
            _setMethod = ownerType.GetMethod("SetPropertyValue");
        }

        public CustomPropertyInfoHelper(string name, Type type, List<Attribute> attributes, Type propertyOwner)
        {
            _name = name;
            _type = type;
            _attributes = attributes;
        }

        public override PropertyAttributes Attributes
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override MethodInfo[] GetAccessors(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public override MethodInfo GetGetMethod(bool nonPublic)
        {
            return _getMethod;
        }

        public override ParameterInfo[] GetIndexParameters()
        {
            return null;
        }

        public override MethodInfo GetSetMethod(bool nonPublic)
        {
            return _setMethod;
        }

        // Returns the value from the dictionary stored in the Dynamic's instance.
        public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
        {
            return _getMethod.Invoke(obj, new object[] { _name });
            //return obj.GetType().GetMethod("GetPropertyValue").Invoke(obj, new object[] { _name });
        }

        public override Type PropertyType
        {
            get { return _type; }
        }

        // Sets the value in the dictionary stored in the Dynamic's instance.
        public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
        {
            _setMethod.Invoke(obj, new[] { _name, value });
            //obj.GetType().GetMethod("SetPropertyValue").Invoke(obj, new[] { _name, value });
        }

        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            var attrs = from a in _attributes where a.GetType() == attributeType select a;
            return attrs.ToArray();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return _attributes.ToArray();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override string Name
        {
            get { return _name; }
        }

        public override Type ReflectedType
        {
            get { throw new NotImplementedException(); }
        }

        internal List<Attribute> CustomAttributesInternal
        {
            get { return _attributes; }
        }
    }
}