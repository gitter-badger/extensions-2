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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Librame.Extensions
{
    /// <summary>
    /// 表达式静态扩展。
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// 解析指定属性表达式对应的名称。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static string AsPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> propertyExpression)
            where T : class
        {
            propertyExpression.NotNull(nameof(propertyExpression));

            string propertyName = string.Empty;
            
            //对象是不是一元运算符
            if (propertyExpression.Body is UnaryExpression)
            {
                propertyName = ((MemberExpression)((UnaryExpression)propertyExpression.Body).Operand).Member.Name;
            }
            //对象是不是访问的字段或属性
            else if (propertyExpression.Body is MemberExpression)
            {
                propertyName = ((MemberExpression)propertyExpression.Body).Member.Name;
            }
            //对象是不是参数表达式
            else if (propertyExpression.Body is ParameterExpression)
            {
                propertyName = ((ParameterExpression)propertyExpression.Body).Type.Name;
            }

            return propertyName;
        }


        /// <summary>
        /// 如果指定类型实例的属性值。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TValue">指定的属性值类型。</typeparam>
        /// <param name="source">给定要获取属性值的类型实例。</param>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回属性值。</returns>
        public static TValue AsPropertyValue<T, TValue>(this T source, Expression<Func<T, TValue>> propertyExpression)
            where T : class
        {
            source.NotNull(nameof(source));

            var name = propertyExpression.AsPropertyName();
            if (name.IsEmpty()) return default;

            try
            {
                var pi = typeof(T).GetRuntimeProperty(name);
                return (TValue)pi.GetValue(source);
            }
            catch (AmbiguousMatchException)
            {
                return default;
            }
        }


        #region AsPropertyExpression
        
        /// <summary>
        /// 转换为单个属性键的 Lambda 表达式（例：p => p.PropertyName）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyName">给定的属性名。</param>
        /// <returns>返回 lambda 表达式。</returns>
        public static Expression<Func<T, TProperty>> AsPropertyExpression<T, TProperty>(this string propertyName)
            where T : class
        {
            // 建立变量
            var p = Expression.Parameter(typeof(T), "p");

            // 建立属性
            var property = Expression.Property(p, propertyName);

            // p => p.PropertyName
            return Expression.Lambda<Func<T, TProperty>>(property, p);
        }


        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName > compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsGreaterThanPropertyExpression<T, TProperty>(this string propertyName,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(typeof(TProperty), value,
                (p, c) => Expression.GreaterThan(p, c));
        }
        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName > compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsGreaterThanPropertyExpression<T>(this string propertyName, Type propertyType,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(propertyType, value,
                (p, c) => Expression.GreaterThan(p, c));
        }


        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName >= compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsGreaterThanOrEqualPropertyExpression<T, TProperty>(this string propertyName,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(typeof(TProperty), value,
                (p, c) => Expression.GreaterThanOrEqual(p, c));
        }
        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName >= compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsGreaterThanOrEqualPropertyExpression<T>(this string propertyName, Type propertyType,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(propertyType, value,
                (p, c) => Expression.GreaterThanOrEqual(p, c));
        }


        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName 〈 compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsLessThanPropertyExpression<T, TProperty>(this string propertyName,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(typeof(TProperty), value,
                (p, c) => Expression.LessThan(p, c));
        }
        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName 〈 compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsLessThanPropertyExpression<T>(this string propertyName, Type propertyType,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(propertyType, value,
                (p, c) => Expression.LessThan(p, c));
        }


        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName 〈= compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsLessThanOrEqualPropertyExpression<T, TProperty>(this string propertyName,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(typeof(TProperty), value,
                (p, c) => Expression.LessThanOrEqual(p, c));
        }
        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName 〈= compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsLessThanOrEqualPropertyExpression<T>(this string propertyName, Type propertyType,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(propertyType, value,
                (p, c) => Expression.LessThanOrEqual(p, c));
        }


        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName != compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsNotEqualPropertyExpression<T, TProperty>(this string propertyName,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(typeof(TProperty), value,
                (p, c) => Expression.NotEqual(p, c));
        }
        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName != compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsNotEqualPropertyExpression<T>(this string propertyName, Type propertyType,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(propertyType, value,
                (p, c) => Expression.NotEqual(p, c));
        }


        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName == compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsEqualPropertyExpression<T, TProperty>(this string propertyName,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(typeof(TProperty), value,
                (p, c) => Expression.Equal(p, c));
        }
        /// <summary>
        /// 转换为比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName == compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="value">给定的参考值。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsEqualPropertyExpression<T>(this string propertyName, Type propertyType,
            object value)
            where T : class
        {
            return propertyName.AsPropertyExpression<T, BinaryExpression>(propertyType, value,
                (p, c) => Expression.Equal(p, c));
        }


        /// <summary>
        /// 转换为使用单个属性值进行比较的 Lambda 表达式（例：p => p.PropertyName.CompareTo(value)）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TExpression">指定的表达式类型。</typeparam>
        /// <param name="propertyName">给定的属性名。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="compareToFactory">给定的对比方法。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static Expression<Func<T, bool>> AsPropertyExpression<T, TExpression>(this string propertyName, Type propertyType,
            object value, Func<MemberExpression, ConstantExpression, TExpression> compareToFactory)
            where T : class
            where TExpression : Expression
        {
            compareToFactory.NotNull(nameof(compareToFactory));

            // 建立变量
            var p = Expression.Parameter(typeof(T), "p");

            // 建立属性
            var property = Expression.Property(p, propertyName);
            var constant = Expression.Constant(value, propertyType);

            // 调用方法（如：Expression.Equal(property, constant);）
            var body = compareToFactory.Invoke(property, constant);

            // p => p.PropertyName == value
            return Expression.Lambda<Func<T, bool>>(body, p);
        }


        /// <summary>
        /// 转换为使用单个属性值进行比较的 Lambda 表达式（例：p => p.PropertyName.CallMethodName(value)）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定的属性名。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="callMethodName">给定要调用的方法名。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> AsPropertyExpression<T>(this string propertyName, Type propertyType,
            object value, string callMethodName)
            where T : class
        {
            var type = typeof(T);

            // 建立变量
            var p = Expression.Parameter(type, "p");
            // 建立属性
            var property = Expression.Property(p, propertyName);
            var constant = Expression.Constant(value, propertyType);
            // 得到属性信息
            var propertyInfo = type.GetRuntimeProperty(propertyName);

            // 调用方法
            var body = Expression.Call(property,
                propertyInfo.PropertyType.GetRuntimeMethod(callMethodName, new Type[] { propertyType }),
                constant);

            // p => p.PropertyName.CallMethodName(value)
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        #endregion

    }
}