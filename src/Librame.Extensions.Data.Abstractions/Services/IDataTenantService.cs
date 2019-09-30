﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Data
{
    using Core;

    /// <summary>
    /// 数据租户服务接口。
    /// </summary>
    public interface IDataTenantService : IService
    {
        /// <summary>
        /// 获取切换的租户。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        /// <returns>返回 <see cref="ITenant"/>。</returns>
        ITenant GetSwitchTenant(IAccessor accessor);

        /// <summary>
        /// 异步获取切换的租户。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="ITenant"/> 的异步操作。</returns>
        Task<ITenant> GetSwitchTenantAsync(IAccessor accessor, CancellationToken cancellationToken = default);
    }
}
