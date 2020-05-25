﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Network.Builders
{
    using Core.Builders;

    /// <summary>
    /// 网络构建器依赖。
    /// </summary>
    public class NetworkBuilderDependency : AbstractExtensionBuilderDependency<NetworkBuilderOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="NetworkBuilderDependency"/>。
        /// </summary>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        public NetworkBuilderDependency(IExtensionBuilderDependency parentDependency = null)
            : base(nameof(NetworkBuilderDependency), parentDependency)
        {
        }

    }
}
