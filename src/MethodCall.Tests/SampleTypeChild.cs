// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleTypeChild.cs" company="Philip Panyukov">
//   Copyright (c) Philip Panyukov. All rights reserved.
// </copyright>
// <summary>
//   Defines the SampleTypeChild type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MethodCall.Tests
{
    internal class SampleTypeChild : SampleType
    {
        protected override string XBaseVirtualMethod()
        {
            return "SampleTypeChild";
        }
    }
}