using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;

namespace WorkFlowDemo
{

    class Program
    {
        static void Main(string[] args)
        {
            Activity workflow1 = new QJActivity1();
            WorkflowInvoker.Invoke(workflow1);
            Console.ReadKey();
        }
    }
}
