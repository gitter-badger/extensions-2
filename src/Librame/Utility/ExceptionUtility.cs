﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.IO;

namespace Librame.Utility
{
    /// <summary>
    /// <see cref="Exception"/> 实用工具。
    /// </summary>
    public static class ExceptionUtility
    {
        /// <summary>
        /// 得到内部异常消息。
        /// </summary>
        /// <param name="ex">给定的异常。</param>
        /// <returns>返回消息字符串。</returns>
        public static string InnerMessage(this Exception ex)
        {
            if (!ReferenceEquals(ex.InnerException, null))
                return InnerMessage(ex.InnerException);

            return ex.Message;
        }


        /// <summary>
        /// 得到不为空的类型实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// item is null.
        /// </exception>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="item">给定的实例。</param>
        /// <param name="paramName">给定的参数名。</param>
        /// <returns>返回实例或抛出异常。</returns>
        public static T NotNull<T>(this T item, string paramName)
        {
            if (item == null)
                throw new ArgumentNullException(paramName);

            return item;
        }

        /// <summary>
        /// 得到不为空或空字符串的实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// item is null or empty.
        /// </exception>
        /// <param name="str">给定的字符串。</param>
        /// <param name="paramName">给定的参数名。</param>
        /// <returns>返回实例或抛出异常。</returns>
        public static string NotEmpty(this string str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(paramName);

            return str;
        }


        /// <summary>
        /// 监视整数是否超出定义的允许值范围。
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// i is out of range.
        /// </exception>
        /// <param name="i">给定的整数。</param>
        /// <param name="min">给定的最小值。</param>
        /// <param name="max">给定的最大值。</param>
        /// <param name="paramName">给定的参数名。</param>
        /// <returns>返回整数或抛出异常。</returns>
        public static int NotOutOfRange(this int i, int min, int max, string paramName)
        {
            if (i < min || i > max)
            {
                throw new ArgumentOutOfRangeException(paramName,
                    string.Format("The {0} value is out of range: {1} (min: {2}, max: {3})", paramName, i, min, max));
            }

            return i;
        }


        /// <summary>
        /// 得到文件存在的实例。
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// file not found.
        /// </exception>
        /// <param name="path">给定的文件路径。</param>
        /// <returns>返回路径字符串或抛出异常。</returns>
        public static string FileExists(this string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            return path;
        }


        /// <summary>
        /// 基础类型能从指定类型中派生。
        /// </summary>
        /// <exception cref="ArgumentException">
        /// baseType 类型不能从 fromType 类型派生。
        /// </exception>
        /// <typeparam name="TBase">指定的基础类型。</typeparam>
        /// <typeparam name="TFrom">指定的派生类型。</typeparam>
        /// <returns>返回派生类型或抛出异常。</returns>
        public static Type AssignableFrom<TBase, TFrom>()
        {
            return typeof(TBase).AssignableFrom(typeof(TFrom));
        }
        /// <summary>
        /// 基础类型能从指定类型中派生。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// baseType 或 fromType 为空。
        /// </exception>
        /// <exception cref="ArgumentException">
        /// baseType 类型不能从 fromType 类型派生。
        /// </exception>
        /// <param name="baseType">给定的基础类型。</param>
        /// <param name="fromType">给定的派生类型。</param>
        /// <returns>返回派生类型或抛出异常。</returns>
        public static Type AssignableFrom(this Type baseType, Type fromType)
        {
            baseType.NotNull(nameof(baseType));
            fromType.NotNull(nameof(fromType));

            if (!baseType.IsAssignableFrom(fromType))
            {
                throw new ArgumentException(string.Format(Properties.Resources.TypeAssignableFromExceptionFormat,
                    baseType, fromType));
            }

            return fromType;
        }


        /// <summary>
        /// 对象是指定类型。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// obj 为空。
        /// </exception>
        /// <exception cref="ArgumentException">
        /// obj 类型不是指定的类型。
        /// </exception>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="obj">给定的对象。</param>
        /// <param name="paramName">给定的参数名。</param>
        /// <returns>返回对象或抛出异常。</returns>
        public static object SameType<T>(this object obj, string paramName)
        {
            obj = obj.NotNull(paramName);

            if (!(obj is T))
            {
                var message = string.Format("The {0} is not requirement type {1}.",
                    obj.GetType().FullName, typeof(T).FullName);

                throw new ArgumentException(message);
            }

            return obj;
        }

    }
}
