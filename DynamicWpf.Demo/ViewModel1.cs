using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWpf.Demo
{
    class ViewModel1
    {
        public DynamicViewModel<Person>[] BindingTargets { get; }

        public ViewModel1()
        {
            //編集したいオブジェクト
            var p2 = new Person { Name = "のっち" };
            var p1 = new Person { Name = "のち" };
            var targets = new[] { p1, p2 };

            //画面に見せる用のオブジェクト
            BindingTargets = targets.Select(_ => new DynamicViewModel<Person>(_)).ToArray();

            ////画面でデータに変更があったときに呼ばれるイベントハンドラ
            //BindingTarget.PropertyChanged += new PropertyChangedEventHandler(BindingTarget_PropertyChanged);

            //お約束
 
        }

        void BindingTarget_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //画面でデータが変更されるとデバッグ出力に編集後のNameが表示される。
            //Debug.WriteLine("Name = " + Target.Name);
        }
    }
}
