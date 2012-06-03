// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodCall.Tests.cs" company="Philip Panyukov">
//   Copyright (c) Philip Panyukov. All rights reserved.
// </copyright>
// <summary>
//   Tests MethodCall test support object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable RedundantNameQualifier
// ReSharper disable SuggestUseVarKeywordEvident
// ReSharper disable CheckNamespace

using System;

namespace MethodCall
{
    using global::MethodCall.Tests;

    using NUnit.Framework;

    /// <summary>
    /// Tests MethodCall test support object.
    /// </summary>
    [TestFixture]
    [Category(TestCategory.Unit)]
    [Description("Tests for MethodCall test support class.")]
    public sealed partial class MethodCall
    {
        [Test]
        public void StaticMethodUsingInstance()
        {
            SampleType x = new SampleType();

            Assert.That(
                MethodCall.Invoke(x, "XBaseStaticMethod"),
                Is.EqualTo("XBaseStaticMethod()"));
        }

        [Test]
        public void StaticMethodUsingType()
        {
            Assert.That(
                MethodCall.Invoke(typeof(SampleType), "XBaseStaticMethod"),
                Is.EqualTo("XBaseStaticMethod()"));
        }

        [Test]
        public void StaticMethodOverloadUsingInstance()
        {
            SampleType x = new SampleType();

            Assert.That(
                MethodCall.Invoke(x, "XBaseStaticMethod", 10),
                Is.EqualTo("XBaseStaticMethod(int i)"));
        }

        [Test]
        public void StaticMethodOverloadUsingType()
        {
            Assert.That(
                MethodCall.Invoke(typeof(SampleType), "XBaseStaticMethod", 10),
                Is.EqualTo("XBaseStaticMethod(int i)"));
        }

        [Test]
        public void StaticPropUsingInstance()
        {
            SampleType x = new SampleType();

            MethodCall.SetProp(x, "XBaseStaticProp", "foobar");

            Assert.That(
                MethodCall.GetProp(x, "XBaseStaticProp"),
                Is.EqualTo("foobar"));
        }

        [Test]
        public void StaticPropUsingType()
        {
            MethodCall.SetProp(typeof(SampleType), "XBaseStaticProp", "foobar");

            Assert.That(
                MethodCall.GetProp(typeof(SampleType), "XBaseStaticProp"),
                Is.EqualTo("foobar"));
        }

        [Test]
        public void StaticFieldUsingInstance()
        {
            SampleType x = new SampleType();

            MethodCall.SetField(x, "baseStaticField", "wank wank");

            Assert.That(
                MethodCall.GetField(x, "baseStaticField"),
                Is.EqualTo("wank wank"));
        }

        [Test]
        public void StaticFieldUsingType()
        {
            MethodCall.SetField(typeof(SampleType), "baseStaticField", "lolk lolk");

            Assert.That(
                MethodCall.GetField(typeof(SampleType), "baseStaticField"),
                Is.EqualTo("lolk lolk"));
        }

        [Test]
        public void InsMethod()
        {
            SampleType x = new SampleType();

            Assert.That(
                MethodCall.Invoke(x, "XBaseInsMethod"),
                Is.EqualTo("XBaseInsMethod()"));
        }

        [Test]
        public void InsMethodOverload()
        {
            SampleType x = new SampleType();

            Assert.That(
                MethodCall.Invoke(x, "XBaseInsMethod", 10),
                Is.EqualTo("XBaseInsMethod(int i)"));
        }

        [Test]
        public void InsProp()
        {
            SampleType x = new SampleType();

            MethodCall.SetProp(x, "XBaseInsProp", "zoo");

            Assert.That(
                MethodCall.GetProp(x, "XBaseInsProp"),
                Is.EqualTo("zoo"));
        }

        [Test]
        public void InsField()
        {
            SampleType x = new SampleType();

            MethodCall.SetField(x, "baseInsField", "la la");

            Assert.That(
                MethodCall.GetField(x, "baseInsField"),
                Is.EqualTo("la la"));
        }

        [TestCase("Dynamic string", "dynamic string")]
        [TestCase("Dynamic integer", 10)]
        [TestCase("Dynamic null value", null)]
        [Description("Issue #1: invoking static method with null dynamic parameter throws NullReferenceException")]
        public void Issue_1_Dynamic_Static_Method_with_null_dynamic_parameter(string name, dynamic input)
        {
            // no throw
            MethodCall.Invoke(typeof (BugWithNullType), "StaticMethodWithDynamic", input);
        }

        [TestCase("Dynamic string", "dynamic string")]
        [TestCase("Dynamic integer", 10)]
        [TestCase("Dynamic null value", null)]
        [Description("Issue #1: invoking non-static method with null dynamic parameter throws NullReferenceException")]
        public void Issue_1_Dynamic_NonStatic_Method_with_null_dynamic_parameter(string name, dynamic input)
        {
            // no throw
            MethodCall.Invoke(new BugWithNullType(), "NonstaticMethodWithDynamic", input);
        }

        [TestCase("Non-dynamic string", "dynamic string")]
        [TestCase("Non-dynamic null value", null)]
        [Description(@"
            Issue #1: invoking static method with non-dynamic 
            null parameter throws method cannot be resolved,
            when the target method's parameter type is anything else but object.")]
        public void Issue_1_Nondynamic_Static_Method_with_null_non_dynamic_parameter(string name, string input)
        {
            // no throw
            MethodCall.Invoke(typeof(BugWithNullType), "StaticMethodTraditional", input);
        }

        [TestCase("Non-dynamic string", "dynamic string")]
        [TestCase("Non-dynamic null value", null)]
        [Description(@"
            Issue #1: invoking non-static method with non-dynamic 
            null parameter throws method cannot be resolved,
            when the target method's parameter type is anything else but object.")]
        public void Issue_1_Nondynamic_NonStatic_Method_with_null_non_dynamic_parameter(string name, string input)
        {
            // no throw
            MethodCall.Invoke(new BugWithNullType(), "NonstaticMethodTraditional", input);
        }

        internal sealed class BugWithNullType
        {
            public static dynamic StaticMethodWithDynamic(dynamic input)
            {
                return input;
            }

            public dynamic NonstaticMethodWithDynamic(dynamic input)
            {
                return input;
            }

            public static string StaticMethodTraditional(string input)
            {
                return input;
            }

            public string NonstaticMethodTraditional(string input)
            {
                return input;
            }
        }
    }
}

// ReSharper restore RedundantNameQualifier
// ReSharper restore SuggestUseVarKeywordEvident
// ReSharper restore CheckNamespace
