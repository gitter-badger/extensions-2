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
using System.Linq.Expressions;

namespace Librame.Utility
{
    /// <summary>
    /// <see cref="Expression"/> 实用工具。
    /// </summary>
    /// <author>Librame Pang</author>
    public class ExpressionUtility
    {
        #region As

        /// <summary>
        /// 解析指定属性表达式对应的名称。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回字符串。</returns>
        public static string AsPropertyName<T, TProperty>(Expression<Func<T, TProperty>> propertyExpression)
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
        /// <param name="entity">给定要获取属性值的类型实例。</param>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回属性值。</returns>
        public static TValue AsPropertyValue<T, TValue>(T entity,
            Expression<Func<T, TValue>> propertyExpression)
        {
            if (ReferenceEquals(entity, null))
                return default(TValue);

            try
            {
                var name = AsPropertyName(propertyExpression);
                var pi = typeof(T).GetProperty(name);

                return (TValue)pi?.GetValue(entity);
            }
            catch
            {
                return default(TValue);
            }
        }

        #endregion


        #region Build

        /// <summary>
        /// 建立单个属性键的 Lambda 表达式（例：p => p.PropertyName）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TKey">指定的键类型。</typeparam>
        /// <param name="propertyName">给定的属性名。</param>
        /// <returns>返回 lambda 表达式。</returns>
        public static Expression<Func<T, TKey>> BuildProperty<T, TKey>(string propertyName)
        {
            // 建立变量
            var p = Expression.Parameter(typeof(T), "p");

            // 建立属性
            var property = Expression.Property(p, propertyName);

            // p => p.PropertyName
            return Expression.Lambda<Func<T, TKey>>(property, p);
        }

        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName > compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildGreaterThanProperty<T>(string propertyName, object value, Type propertyType)
        {
            return BuildProperty<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.GreaterThan(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName >= compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildGreaterThanOrEqualProperty<T>(string propertyName, object value, Type propertyType)
        {
            return BuildProperty<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.GreaterThanOrEqual(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName 〈 compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildLessThanProperty<T>(string propertyName, object value, Type propertyType)
        {
            return BuildProperty<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.LessThan(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName 〈= compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildLessThanOrEqualProperty<T>(string propertyName, object value, Type propertyType)
        {
            return BuildProperty<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.LessThanOrEqual(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName != compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildNotEqualProperty<T>(string propertyName, object value, Type propertyType)
        {
            return BuildProperty<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.NotEqual(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName == compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildEqualProperty<T>(string propertyName, object value, Type propertyType)
        {
            return BuildProperty<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.Equal(p, c));
        }

        /// <summary>
        /// 建立使用单个属性值进行比较的 Lambda 表达式（例：p => p.PropertyName.CompareTo(value)）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TExpression">指定的表达式类型。</typeparam>
        /// <param name="propertyName">给定的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="compareToFactory">给定的对比方法。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildProperty<T, TExpression>(string propertyName, object value, Type propertyType,
            Func<MemberExpression, ConstantExpression, TExpression> compareToFactory)
            where TExpression : Expression
        {
            compareToFactory.NotNull(nameof(compareToFactory));

            // 建立变量
            var p = Expression.Parameter(typeof(T), "p");

            // 建立属性
            var property = Expression.Property(p, propertyName);
            var constant = Expression.Constant(value, propertyType);

            // 调用方法（如：Expression.Equal(property, constant);）
            var body = compareToFactory(property, constant);

            // p => p.PropertyName == value
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        /// <summary>
        /// 建立使用单个属性值进行比较的 Lambda 表达式（例：p => p.PropertyName.CallMethodName(value)）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="callMethodName">给定要调用的方法名。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildProperty<T>(string propertyName, object value, Type propertyType, string callMethodName)
        {
            var type = typeof(T);

            // 建立变量
            var p = Expression.Parameter(type, "p");
            // 建立属性
            var property = Expression.Property(p, propertyName);
            var constant = Expression.Constant(value, propertyType);
            // 得到属性信息
            var propertyInfo = type.GetProperty(propertyName);

            // 调用方法
            var body = Expression.Call(property,
                propertyInfo.PropertyType.GetMethod(callMethodName, new Type[] { propertyType }),
                constant);

            // p => p.PropertyName.CallMethodName(value)
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        #endregion

    }


    /// <summary>
    /// <see cref="ExpressionUtility"/> 静态扩展。
    /// </summary>
    public static class ExpressionUtilityExtensions
    {
        /// <summary>
        /// 解析指定属性表达式对应的名称。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回字符串。</returns>
        public static string AsPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> propertyExpression)
        {
            return ExpressionUtility.AsPropertyName(propertyExpression);
        }

        /// <summary>
        /// 如果指定类型实例的属性值。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TValue">指定的属性值类型。</typeparam>
        /// <param name="entity">给定要获取属性值的类型实例。</param>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回属性值。</returns>
        public static TValue AsPropertyValue<T, TValue>(this T entity,
            Expression<Func<T, TValue>> propertyExpression)
        {
            return ExpressionUtility.AsPropertyValue(entity, propertyExpression);
        }

    }
}