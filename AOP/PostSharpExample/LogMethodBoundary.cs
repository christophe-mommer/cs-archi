using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Serialization;

namespace PostSharpExample
{
    [PSerializable]
    public sealed class LogMethodBoundary : OnMethodBoundaryAspect
    {
        #region Build-Time Logic

        public LogMethodBoundary()
        {
        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            // TODO: Check that the aspect has been applied on a proper method.

            if (false)
            {
                Message.Write(method, SeverityType.Error, "MY001", "Cannot apply LogMethodBoundary to method '{0}'.", method);
                return false;
            }

            return true;
        }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            // TODO: Initialize any instance field whose value only depends on the method to which the aspect is applied.
        }

        #endregion


        public override void RuntimeInitialize(MethodBase method)
        {
            // This method is invoked once at run time.
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("We enter the method");
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine("We exit the method");

        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            Console.WriteLine("Method was a success !");
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine("Method was a failure !");

        }
    }
}
