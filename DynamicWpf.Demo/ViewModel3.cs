using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWpf.Demo
{
    class ViewModel3
    {

        public List<DynamicViewModelBase> Students { get; } = new List<DynamicViewModelBase>();


        public ViewModel3()
        {

            var xx = new Dictionary<string, Type> { { "FirstName", typeof(string) }, { "LastName", typeof(string) } };
            var student1 = new DynamicViewModelBase(xx);
            student1.SetPropertyValue<string>("FirstName", "sdi");
            student1.SetPropertyValue<string>("LastName", "Siddiqi");
            Students.Add(student1);

            var student2 = new DynamicViewModelBase(xx);
            student2.SetPropertyValue<string>("FirstName", "sjjdi");
            student2.SetPropertyValue<string>("LastName", "Aur");
            Students.Add(student2);
        }
    }
}
