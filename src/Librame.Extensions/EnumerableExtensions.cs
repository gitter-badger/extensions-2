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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Librame.Extensions
{
    /// <summary>
    /// 可枚举静态扩展。
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 迭代为可枚举集合。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="instance">给定的类型实例。</param>
        /// <returns>返回 <see cref="IEnumerable{T}"/>。</returns>
        public static IEnumerable<T> YieldEnumerable<T>(this T instance)
        {
            yield return instance;
        }


        /// <summary>
        /// 转换为只读列表集合。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <returns>返回 <see cref="IReadOnlyList{T}"/>。</returns>
        public static IReadOnlyList<T> AsReadOnlyList<T>(this IEnumerable<T> enumerable)
        {
            return new ReadOnlyCollection<T>(enumerable?.ToList());
        }
        /// <summary>
        /// 转换为只读列表集合。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="list">给定的 <see cref="IList{T}"/>。</param>
        /// <returns>返回 <see cref="IReadOnlyList{T}"/>。</returns>
        public static IReadOnlyList<T> AsReadOnlyList<T>(this IList<T> list)
        {
            return new ReadOnlyCollection<T>(list);
        }


        /// <summary>
        /// 新增列表中不包含的值。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="list">给定的 <see cref="IList{T}"/>。</param>
        /// <param name="value">给定的新增的值。</param>
        public static void AddIfNotContains<T>(this IList<T> list, T value)
        {
            list.NotNull(nameof(list));

            if (list.Contains(value))
                return;

            list.Add(value);
        }


        #region Trim

        /// <summary>
        /// 修剪可枚举集合的指定初始与末尾项。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="trim">要修剪的实例。</param>
        /// <param name="isLoop">是否循环修剪末尾项（可选；默认循环修剪）。</param>
        /// <returns>返回 <see cref="IEnumerable{T}"/>。</returns>
        public static IEnumerable<T> Trim<T>(this IEnumerable<T> enumerable, T trim, bool isLoop = true)
        {
            return enumerable.Trim(item => item.Equals(trim), isLoop);
        }

        /// <summary>
        /// 修剪可枚举集合的指定初始与末尾项。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="predicateFactory">给定用于修剪的断定工厂方法。</param>
        /// <param name="isLoop">是否循环修剪末尾项（可选；默认循环修剪）。</param>
        /// <returns>返回 <see cref="IEnumerable{T}"/>。</returns>
        public static IEnumerable<T> Trim<T>(this IEnumerable<T> enumerable, Func<T, bool> predicateFactory, bool isLoop = true)
        {
            return enumerable.TrimStart(predicateFactory, isLoop).TrimEnd(predicateFactory, isLoop);
        }


        /// <summary>
        /// 修剪可枚举集合的指定末尾项。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="endItem">给定要修剪的末尾项。</param>
        /// <param name="isLoop">是否循环修剪末尾项（可选；默认循环修剪）。</param>
        /// <returns>返回 <see cref="IEnumerable{T}"/>。</returns>
        public static IEnumerable<T> TrimEnd<T>(this IEnumerable<T> enumerable, T endItem, bool isLoop = true)
        {
            return enumerable.TrimEnd(item => item.Equals(endItem), isLoop);
        }

        /// <summary>
        /// 修剪可枚举集合的指定末尾项。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="endFactory">给定要修剪末尾项的断定工厂方法。</param>
        /// <param name="isLoop">是否循环修剪末尾项（可选；默认循环修剪）。</param>
        /// <returns>返回 <see cref="IEnumerable{T}"/>。</returns>
        public static IEnumerable<T> TrimEnd<T>(this IEnumerable<T> enumerable, Func<T, bool> endFactory, bool isLoop = true)
        {
            endFactory.NotNull(nameof(endFactory));

            return enumerable.TrimLast(endFactory, isLoop);
        }


        /// <summary>
        /// 修剪可枚举集合的指定初始项。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="startItem">给定要修剪的初始项。</param>
        /// <param name="isLoop">是否循环修剪末尾项（可选；默认循环修剪）。</param>
        /// <returns>返回 <see cref="IEnumerable{T}"/>。</returns>
        public static IEnumerable<T> TrimStart<T>(this IEnumerable<T> enumerable, T startItem, bool isLoop = true)
        {
            return enumerable.TrimStart(item => item.Equals(startItem), isLoop);
        }

        /// <summary>
        /// 修剪可枚举集合的指定初始项。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="startFactory">给定要修剪初始项的断定工厂方法。</param>
        /// <param name="isLoop">是否循环修剪末尾项（可选；默认循环修剪）。</param>
        /// <returns>返回 <see cref="IEnumerable{T}"/>。</returns>
        public static IEnumerable<T> TrimStart<T>(this IEnumerable<T> enumerable, Func<T, bool> startFactory, bool isLoop = true)
        {
            startFactory.NotNull(nameof(startFactory));

            return enumerable.Reverse().TrimLast(startFactory, isLoop).Reverse();
        }


        private static IEnumerable<T> TrimLast<T>(this IEnumerable<T> enumerable, Func<T, bool> predicateFactory, bool isLoop = true)
        {
            if (enumerable.IsNullOrEmpty())
                return enumerable;

            if (predicateFactory.Invoke(enumerable.Last()))
            {
                enumerable = enumerable.Take(enumerable.Count() - 1);

                if (isLoop) // 循环修剪
                    return enumerable.TrimLast(predicateFactory, isLoop);

                return enumerable;
            }

            return enumerable;
        }

        #endregion

    }
}
