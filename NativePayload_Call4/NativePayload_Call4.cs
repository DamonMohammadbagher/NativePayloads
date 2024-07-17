using System;
using System.Reflection;
using System.Reflection.Emit;

namespace NativePayload_Call4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_Call4, Published by Damon Mohammadbagher, 2024");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_Call4 Emit Call Method + Indirect Invoke C# Method");
            Console.WriteLine();
            DynamicMethod dynamicMethod = new DynamicMethod(
                "InvokeExecuteInmemory",
                typeof(void),
                Type.EmptyTypes,
                typeof(Program).Module);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
            MethodInfo targetMethod = typeof(Program).GetMethod("ExecuteInmemory");
            ilGenerator.Emit(OpCodes.Call, targetMethod);
            ilGenerator.Emit(OpCodes.Ret);
            Action executeDelegate = (Action)dynamicMethod.CreateDelegate(typeof(Action));
            executeDelegate();
        }

        public static void ExecuteInmemory()
        {
            Console.WriteLine("Executing the ExecuteInmemory method...");
            Console.Read();
        }
    }
}
