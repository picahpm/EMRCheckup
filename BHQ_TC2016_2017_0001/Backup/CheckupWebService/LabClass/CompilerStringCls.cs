using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.VisualBasic;

namespace CheckupWebService.LabClass
{
    public static class CompilerStringCls
    {
        public static MethodInfo CreateFunction(string function)
        {
            string code = @"
                Module UserFunctions          
                    Public Function Func() As Boolean
                        Return funcCondition
                    End Function
                End Module
            ";

            string finalCode = code.Replace("funcCondition", function);

            VBCodeProvider provider = new VBCodeProvider();
            CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), finalCode);

            Type binaryFunction = results.CompiledAssembly.GetType("UserFunctions");
            return binaryFunction.GetMethod("Func");
        }

        public static bool CheckCondition(string condition)
        {
            try
            {
                MethodInfo function = CreateFunction(condition);
                object result = function.Invoke(null, null);
                return (bool)result;
            }
            catch
            {
                return false;
            }
        }
    }
}