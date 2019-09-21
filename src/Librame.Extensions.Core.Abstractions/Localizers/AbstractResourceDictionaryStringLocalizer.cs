﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Librame.Extensions.Core
{
    /// <summary>
    /// 抽象资源字典字符串组合器。
    /// </summary>
    /// <typeparam name="TResource">指定的资源类型。</typeparam>
    public abstract class AbstractResourceDictionaryStringLocalizer<TResource> : IExpressionStringLocalizer<TResource>
        where TResource : class, IResource
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractResourceDictionaryStringLocalizer{T}"/>。
        /// </summary>
        /// <param name="resourceDictionary">给定的 <see cref="IResourceDictionary"/>。</param>
        public AbstractResourceDictionaryStringLocalizer(IResourceDictionary resourceDictionary)
        {
            ResourceDictionary = resourceDictionary.NotNullOrEmpty(nameof(resourceDictionary));
        }


        /// <summary>
        /// 存储的键值对。
        /// </summary>
        public IResourceDictionary ResourceDictionary { get; }


        /// <summary>
        /// 获取指定属性表达式的本地化字符串。
        /// </summary>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
        public LocalizedString this[Expression<Func<TResource, string>> propertyExpression]
            => this[propertyExpression.AsPropertyName()];

        /// <summary>
        /// 获取指定属性表达式的本地化字符串。
        /// </summary>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <param name="arguments">给定的参数数组。</param>
        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
        public LocalizedString this[Expression<Func<TResource, string>> propertyExpression, params object[] arguments]
            => this[propertyExpression.AsPropertyName(), arguments];


        /// <summary>
        /// 获取指定名称的本地化字符串。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
        public virtual LocalizedString this[string name]
            => new LocalizedString(name, ResourceDictionary[name]?.ToString(), !ResourceDictionary.ContainsKey(name));

        /// <summary>
        /// 获取指定名称的本地化字符串。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="arguments">给定的参数数组。</param>
        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
        public virtual LocalizedString this[string name, params object[] arguments]
            => new LocalizedString(name, string.Format(ResourceDictionary[name]?.ToString(), arguments), !ResourceDictionary.ContainsKey(name));


        /// <summary>
        /// 获取所有本地化字符串集合。
        /// </summary>
        /// <param name="includeParentCultures">参数无效。</param>
        /// <returns>返回 <see cref="IEnumerable{LocalizedString}"/>。</returns>
        public virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => ResourceDictionary.Select(pair => new LocalizedString(pair.Key, pair.Value?.ToString(), !ResourceDictionary.ContainsKey(pair.Key)));


        /// <summary>
        /// 带文化信息的字符串组合器。
        /// </summary>
        /// <param name="cultureInfo">给定的 <see cref="CultureInfo"/>。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/>。</returns>
        public abstract IStringLocalizer WithCulture(CultureInfo cultureInfo);
    }
}
