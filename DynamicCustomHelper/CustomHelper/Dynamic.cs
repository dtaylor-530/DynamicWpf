using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using CustomHelper;

namespace CustomHelper
{
    /// <summary>
    /// This gives an example of using encapsulation to implement
    /// the ICustomTypeProvider - where the base class approach is not
    /// available or is not appropriate.
    /// </summary>
    public class Dynamic : ICustomTypeProvider, INotifyPropertyChanged
    {
        /// <summary>
        /// Custom Type provider implementation - delegated through static methods.
        /// </summary>
        private readonly CustomTypeHelper<Dynamic> _helper = new CustomTypeHelper<Dynamic>();

        //// Existing known properties
        //private string _firstName;
        //private string _lastName;

        //public string FirstName
        //{
        //    get { return _firstName; }
        //    set { _firstName = value; RaisePropertyChanged(); }
        //}

        //public string LastName
        //{
        //    get { return _lastName; }
        //    set { _lastName = value; RaisePropertyChanged(); }
        //}

        public Dynamic()
        {
            _helper.PropertyChanged += (s, e) => PropertyChanged(this, e);
        }

        // Methods to support dynamic properties.
        public static void AddProperty(string name)
        {
            CustomTypeHelper<Dynamic>.AddProperty(name);
        }

        public static void AddProperty(string name, Type propertyType)
        {
            CustomTypeHelper<Dynamic>.AddProperty(name, propertyType);
        }

        public static void AddProperty(string name, Type propertyType, List<Attribute> attributes)
        {
            CustomTypeHelper<Dynamic>.AddProperty(name, propertyType, attributes);
        }

        public void SetPropertyValue(string propertyName, object value)
        {
            _helper.SetPropertyValue(propertyName, value);
        }

        public object GetPropertyValue(string propertyName)
        {
            return _helper.GetPropertyValue(propertyName);
        }

        public PropertyInfo[] GetProperties()
        {
            return _helper.GetProperties();
        }

        Type ICustomTypeProvider.GetCustomType()
        {
            return _helper.GetCustomType();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
