// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodCall.cs" company="Philip Panyukov">
//   Copyright (c) Philip Panyukov. All rights reserved.
// </copyright>
// <summary>
//   Test support object to test private implementation methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable PartialTypeWithSinglePart

namespace MethodCall
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Test support object to test private implementation methods.
    /// </summary>
    public sealed partial class MethodCall
    {
        /// <summary>
        /// Call an instance method on an object.
        /// </summary>
        public static object Invoke(object obj, string methodName, params object[] args)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return InvokeImpl(obj.GetType(), obj, methodName, args);
        }

        /// <summary>
        /// Call a static method on a type.
        /// </summary>
        public static object Invoke(Type t, string methodName, params object[] args)
        {
            return InvokeImpl(t, t, methodName, args);
        }

        /// <summary>
        /// Get instance property of an object.
        /// </summary>
        public static object GetProp(object obj, string propName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return GetPropImpl(obj.GetType(), obj, propName);
        }

        /// <summary>
        /// Get static property.
        /// </summary>
        public static object GetProp(Type t, string propName)
        {
            return GetPropImpl(t, t, propName);
        }

        /// <summary>
        /// Set instance property on an object.
        /// </summary>
        public static void SetProp(object obj, string propName, object propValue)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            SetPropImpl(obj.GetType(), obj, propName, propValue);
        }

        /// <summary>
        /// Set static property on an object.
        /// </summary>
        public static void SetProp(Type t, string propName, object propValue)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            SetPropImpl(t, t, propName, propValue);
        }

        /// <summary>
        /// Get instance field.
        /// </summary>
        public static object GetField(object obj, string fieldName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return GetFieldImpl(obj.GetType(), obj, fieldName);
        }

        /// <summary>
        /// Get static field.
        /// </summary>
        public static object GetField(Type t, string fieldName)
        {
            return GetFieldImpl(t, t, fieldName);
        }

        /// <summary>
        /// Set instance field.
        /// </summary>
        public static void SetField(object obj, string fieldName, object fieldValue)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            SetFieldImpl(obj.GetType(), obj, fieldName, fieldValue);
        }

        /// <summary>
        /// Set static field.
        /// </summary>
        public static void SetField(Type t, string fieldName, object fieldValue)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            SetFieldImpl(t, t, fieldName, fieldValue);
        }

        private static object InvokeImpl(Type t, object obj, string methodName, params object[] args)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (methodName == null)
            {
                throw new ArgumentNullException("methodName");
            }

            // Issue #1: fix for NullReferenceException with null values for params of dynamic types
            if (args == null)
            {
                args = new object[0];
            }

            Type[] paramTypes = new Type[args.Length];
            for (int i = 0; i < args.Length; ++i)
            {
                object paramValue = args[i];
                paramTypes[i] = paramValue != null ? args[i].GetType() : typeof(object);
            }

            const BindingFlags BFlags =
                BindingFlags.InvokeMethod |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.IgnoreCase |
                BindingFlags.FlattenHierarchy;

            try
            {
                MethodInfo mi = t.GetMethod(
                    methodName,
                    BFlags,
                    null,
                    paramTypes,
                    new ParameterModifier[0]);

                if (mi == null)
                {
                    throw new TargetException("Specified method not found.");
                }

                return mi.Invoke(obj, args);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
                else
                {
                    throw;
                }
            }
        }

        private static object GetPropImpl(Type t, object obj, string propName)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (propName == null)
            {
                throw new ArgumentNullException("propName");
            }

            const BindingFlags BFlags =
                BindingFlags.GetProperty |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.IgnoreCase |
                BindingFlags.FlattenHierarchy;

            try
            {
                PropertyInfo pi = t.GetProperty(
                    propName,
                    BFlags);

                if (pi == null)
                {
                    throw new TargetException("Specified property not found.");
                }

                MethodInfo mi = pi.GetGetMethod(true);

                if (mi == null)
                {
                    throw new TargetException("Specified property getter not found.");
                }

                return mi.Invoke(obj, new object[0]);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
                else
                {
                    throw;
                }
            }
        }

        private static object GetFieldImpl(Type t, object obj, string fieldName)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (fieldName == null)
            {
                throw new ArgumentNullException("fieldName");
            }

            const BindingFlags BFlags =
                BindingFlags.GetField |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.IgnoreCase |
                BindingFlags.FlattenHierarchy;

            try
            {
                FieldInfo fi = t.GetField(
                    fieldName,
                    BFlags);

                if (fi == null)
                {
                    throw new TargetException("Specified field not found.");
                }

                return fi.GetValue(obj);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
                else
                {
                    throw;
                }
            }
        }

        private static void SetPropImpl(Type t, object obj, string propName, object propValue)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (propName == null)
            {
                throw new ArgumentNullException("propName");
            }

            const BindingFlags BFlags =
                BindingFlags.SetProperty |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.IgnoreCase |
                BindingFlags.FlattenHierarchy;

            try
            {
                PropertyInfo pi = t.GetProperty(
                    propName,
                    BFlags);

                if (pi == null)
                {
                    throw new TargetException("Specified property not found.");
                }

                MethodInfo mi = pi.GetSetMethod(true);
                if (mi == null)
                {
                    throw new TargetException("Specified property setter not found.");
                }

                mi.Invoke(obj, new object[] { propValue });
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
                else
                {
                    throw;
                }
            }
        }

        private static void SetFieldImpl(Type t, object obj, string fieldName, object fieldValue)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (fieldName == null)
            {
                throw new ArgumentNullException("fieldName");
            }

            const BindingFlags BFlags =
                BindingFlags.SetField |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.IgnoreCase |
                BindingFlags.FlattenHierarchy;

            try
            {
                FieldInfo fi = t.GetField(
                    fieldName,
                    BFlags);

                if (fi == null)
                {
                    throw new TargetException("Specified field not found.");
                }

                fi.SetValue(obj, fieldValue);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

// ReSharper restore PartialTypeWithSinglePart