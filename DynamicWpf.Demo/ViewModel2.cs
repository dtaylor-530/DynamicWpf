using CustomHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWpf.Demo
{
    class ViewModel2
    {
        public ObservableCollection<Dynamic> Customers { get; } = new ObservableCollection<Dynamic>();
      
        public ViewModel2()
        {
            Dynamic.AddProperty("Age", typeof(int));
            Dynamic.AddProperty("Married", typeof(bool));

            Dynamic customer1 = new Dynamic();// { FirstName = "Julie", LastName = "Smith" };
            customer1.SetPropertyValue("Age", 38);
            customer1.SetPropertyValue("Married", true);

            Dynamic customer2 = new Dynamic();// { FirstName = "Mark", LastName = "Smith" };
            customer2.SetPropertyValue("Age", 43);
            customer2.SetPropertyValue("Married", true);

            Customers.Add(customer1);
            Customers.Add(customer2);

        }
    }
}
