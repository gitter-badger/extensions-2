﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Data.Stores
{
    using Core.Identifiers;
    using Core.Services;

    /// <summary>
    /// <see cref="Guid"/> 数据存储标识生成器。
    /// </summary>
    public class GuidDataStoreIdentityGenerator : AbstractDataStoreIdentityGenerator<Guid>
    {
        /// <summary>
        /// 构造一个 <see cref="GuidDataStoreIdentityGenerator"/>。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="factory">给定的 <see cref="IIdentityGeneratorFactory"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidDataStoreIdentityGenerator(IClockService clock,
            IIdentityGeneratorFactory factory, ILoggerFactory loggerFactory)
            : base(clock, factory, loggerFactory)
        {
        }


        /// <summary>
        /// 生成标识。
        /// </summary>
        /// <param name="idName">给定的标识名称。</param>
        /// <returns>返回 <see cref="Guid"/>。</returns>
        public virtual Guid GenerateId(string idName)
            => GenerateId<Guid>(idName);

        /// <summary>
        /// 异步生成标识。
        /// </summary>
        /// <param name="idName">给定的标识名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="Guid"/> 的异步操作。</returns>
        public virtual Task<Guid> GenerateIdAsync(string idName,
            CancellationToken cancellationToken = default)
            => GenerateIdAsync<Guid>(idName, cancellationToken);

    }
}
