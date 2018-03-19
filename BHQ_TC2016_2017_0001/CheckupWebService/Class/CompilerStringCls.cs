using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.VisualBasic;
using System.IO;

namespace CheckupWebService.Class
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

        public static MethodInfo CreateFunction(List<checkResult> function)
        {
            string code = @"
                Module UserFunctions          
                    Public Function Func() As Integer
                ";
            foreach (var condition in function)
            {
                code = code + @"IF " + condition.condition + @" Then
                                    Return " + condition.mlr_id.ToString() + @"
                               End IF 
                               ";
            }
            code = code + @"End Function
                       End Module";

            VBCodeProvider provider = new VBCodeProvider();
            CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), code);

            Type binaryFunction = results.CompiledAssembly.GetType("UserFunctions");
            return binaryFunction.GetMethod("Func");
        }

        public static int? CheckCondition(List<checkResult> condition)
        {
            try
            {
                MethodInfo function = CreateFunction(condition);
                object result = function.Invoke(null, null);
                return (int?)result;
            }
            catch
            {
                return null;
            }
        }
    }

    public class checkResult
    {
        public string condition { get; set; }
        public int mlr_id { get; set; }
    }
}
