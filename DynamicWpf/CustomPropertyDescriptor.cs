using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DynamicWpf
{
    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        private Type propertyType;
        private Type componentType;
        object propertyValue;

        public CustomPropertyDescriptor(string propertyName, Type componentType,Type propertyType)
            : base(propertyName, new Attribute[] { })
        {
            this.propertyType = propertyType;
            this.componentType = componentType;
        }

        public override bool CanResetValue(object component) { return true; }
        public override Type ComponentType { get { return componentType; } }

        public override object GetValue(object component)
        {
            return propertyValue;
        }

        public override bool IsReadOnly { get { return false; } }
        public override Type PropertyType { get { return propertyType; } }
        public override void ResetValue(object component) { SetValue(component, null); }
        public override void SetValue(object component, object value)
        {
            if (!value.GetType().IsAssignableFrom(propertyType))
            {
                throw new System.Exception("Invalid type to assign");
            }

            propertyValue = value;
            OnValueChanged(component, new EventArgs());
        }

        public override bool ShouldSerializeValue(object component) { return true; }
    }
}
