using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WorkFlowDemo
{

    public sealed class InputQJCodeActivity1 : CodeActivity
    {
        // 定义一个字符串类型的活动输入参数
       // public InArgument<string> Text { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        public OutArgument<int> DayResult { get; set; }
        protected override void Execute(CodeActivityContext context)
        {

            // 获取 Text 输入参数的运行时值
            //string text = context.GetValue(this.Text);
            int dat;
            string d = Console.ReadLine();
            int.TryParse(d, out dat);
            context.SetValue(DayResult, dat);
        }
    }
}
