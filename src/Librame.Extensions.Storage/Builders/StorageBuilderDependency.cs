﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Storage.Builders
{
    using Core.Builders;

    /// <summary>
    /// 存储构建器依赖。
    /// </summary>
    public class StorageBuilderDependency : AbstractExtensionBuilderDependency<StorageBuilderOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="StorageBuilderDependency"/>。
        /// </summary>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        public StorageBuilderDependency(IExtensionBuilderDependency parentDependency = null)
            : base(nameof(StorageBuilderDependency), parentDependency)
        {
        }

    }
}
