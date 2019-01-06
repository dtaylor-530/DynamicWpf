using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace DynamicWpf
{
    public class DynamicViewModelBase : CustomTypeDescriptor, INotifyPropertyChanged
    {
        #region Private Fields

        List<PropertyDescriptor> _myProperties = new List<PropertyDescriptor>();

        #endregion

        #region INotifyPropertyChange Implementation

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChange Implementation



        public DynamicViewModelBase(Dictionary<string, Type> properties)
        {
            foreach (var kvp in properties)
                AddProperty<DynamicViewModelBase>(kvp.Key,kvp.Value);
        }



        #region Public Methods

        public void SetPropertyValue<T>(string propertyName, T propertyValue)
        {
            var properties = this.GetProperties()
                                    .Cast<PropertyDescriptor>()
                                    .Where(prop => prop.Name.Equals(propertyName));

            if (properties == null || properties.Count() != 1)
            {
                throw new Exception("The property doesn't exist.");
            }

            var property = properties.First();
            property.SetValue(this, propertyValue);

            OnPropertyChanged(propertyName);
        }

        public T GetPropertyValue<T>(string propertyName)
        {
            var properties = this.GetProperties()
                                .Cast<PropertyDescriptor>()
                                .Where(prop => prop.Name.Equals(propertyName));

            if (properties == null || properties.Count() != 1)
            {
                throw new Exception("The property doesn't exist.");
            }

            var property = properties.First();
            return (T)property.GetValue(this);
        }

        public void AddProperty<U>(string propertyName,Type type) where U : DynamicViewModelBase
        {
            var customProperty =
                    new CustomPropertyDescriptor(
                                            propertyName,
                                            typeof(U),type);

            _myProperties.Add(customProperty);
            customProperty.AddValueChanged(
                                        this,
                                        (o, e) => { OnPropertyChanged(propertyName); });
        }

        #endregion

        #region Overriden Methods

        public override PropertyDescriptorCollection GetProperties()
        {
            var properties = base.GetProperties();
            return new PropertyDescriptorCollection(
                                properties.Cast<PropertyDescriptor>()
                                          .Concat(_myProperties).ToArray());
        }

        #endregion
    }
}

