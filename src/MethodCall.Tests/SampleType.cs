// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleType.cs" company="Philip Panyukov">
//   Copyright (c) Philip Panyukov. All rights reserved.
// </copyright>
// <summary>
//   Defines the SampleTypeChild type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MethodCall.Tests
{
    internal class SampleType
    {
        // ReSharper disable UnusedMember.Local

#pragma warning disable 169
        private static string baseStaticField;
        private string baseInsField;
#pragma warning restore 169

        // static property
        private static string XBaseStaticProp { get; set; }

        // instance property
        private string XBaseInsProp { get; set; }

        // virtual instance method
        protected virtual string XBaseVirtualMethod()
        {
            return "SampleType";
        }

        // static methods with overloads
        private static string XBaseStaticMethod()
        {
            return "XBaseStaticMethod()";
        }

        private static string XBaseStaticMethod(int i)
        {
            return "XBaseStaticMethod(int i)";
        }

        // instance methods with overloads
        private string XBaseInsMethod()
        {
            return "XBaseInsMethod()";
        }

        private string XBaseInsMethod(int i)
        {
            return "XBaseInsMethod(int i)";
        }

        // ReSharper restore UnusedMember.Local
    }
}