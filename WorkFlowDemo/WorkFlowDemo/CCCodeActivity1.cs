using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WorkFlowDemo
{

    public sealed class CCCodeActivity1 : CodeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<int> Days { get; set; }
        public OutArgument<int> Result { get; set; }
        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
             int text = context.GetValue(Days);
            int result;
            if (text < 2)
            {
                result = 0;
            }else if(text<5){
                result = 1;
            }else{
                result = 2;
            }
            context.SetValue(Result, result);

        }
    }
}
