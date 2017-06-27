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
using System.Linq;
using System.Linq.Expressions;

namespace Librame.Data.Repositories
{
    /// <summary>
    /// 仓库读取器接口。
    /// </summary>
    /// <typeparam name="T">指定的类型。</typeparam>
    public interface IRepositoryReader<T> : IRepositoryWriter<T>
        where T : class
    {
        /// <summary>
        /// 复制类型实例。
        /// </summary>
        /// <param name="source">给定的源类型实例。</param>
        /// <param name="target">给定的目标类型实例。</param>
        void Copy(T source, T target);


        /// <summary>
        /// 计数查询。
        /// </summary>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则计算所有条数）。</param>
        /// <returns>返回整数。</returns>
        int Count(Expression<Func<T, bool>> predicate = null);


        /// <summary>
        /// 获取单条指定编号的类型实例。
        /// </summary>
        /// <param name="id">给定的主键。</param>
        /// <returns>返回类型实例。</returns>
        T Get(object id);

        /// <summary>
        /// 获取单条符合指定查询表达式的数据。
        /// </summary>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则根据唯一性要求查询第一条数据）。</param>
        /// <param name="isUnique">是否要求唯一性（如果为 True，则表示查询出多条数据将会抛出异常）。</param>
        /// <returns>返回对象。</returns>
        T Get(Expression<Func<T, bool>> predicate = null, bool isUnique = true);


        /// <summary>
        /// 获取多条符合指定查询表达式的数据集合。
        /// </summary>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则查询所有数据）。</param>
        /// <param name="order">给定的排序方法（可选；如果为空，则采用默认排序）。</param>
        /// <returns>返回数组。</returns>
        T[] GetMany(Expression<Func<T, bool>> predicate = null, Action<Orderable<T>> order = null);

        
        /// <summary>
        /// 获取分页数据。
        /// </summary>
        /// <param name="createInfoFactory">给定创建分页信息的方法。</param>
        /// <param name="order">给定的排序方法。</param>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则查询所有数据）。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        IPageable<T> GetPaging(Func<int, PagingInfo> createInfoFactory,
            Action<Orderable<T>> order, Expression<Func<T, bool>> predicate = null);


        /// <summary>
        /// 获取指定属性值集合。
        /// </summary>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="selector">给定的属性选择器。</param>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则根据唯一性要求查询第一条数据）。</param>
        /// <param name="removeDuplicates">是否移除重复项（可选；默认移除重复项）。</param>
        /// <returns>返回数组。</returns>
        TProperty[] GetProperties<TProperty>(Expression<Func<T, TProperty>> selector,
            Expression<Func<T, bool>> predicate = null, bool removeDuplicates = true);
    }
}
