﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;

namespace Librame.Extensions.Data
{
    using Builders;
    using Services;
    
    /// <summary>
    /// 数据库上下文接口。
    /// </summary>
    /// <typeparam name="TBuilderOptions">指定的构建器选项类型。</typeparam>
    public interface IDbContext<TBuilderOptions> : IDbContext, IService<TBuilderOptions>
        where TBuilderOptions : class, IBuilderOptions, new()
    {
    }


    /// <summary>
    /// 数据库上下文接口。
    /// </summary>
    public interface IDbContext : IDbProvider
    {
        /// <summary>
        /// 变化跟踪器上下文。
        /// </summary>
        IChangeTrackerContext TrackerContext { get; }

        /// <summary>
        /// 租户上下文。
        /// </summary>
        ITenantContext TenantContext { get; }


        /// <summary>
        /// 审计数据集。
        /// </summary>
        DbSet<Audit> Audits { get; }

        /// <summary>
        /// 审计属性数据集。
        /// </summary>
        DbSet<AuditProperty> AuditProperties { get; }

        /// <summary>
        /// 租户数据集。
        /// </summary>
        DbSet<Tenant> Tenants { get; }


        /// <summary>
        /// 当前租户。
        /// </summary>
        Tenant CurrentTenant { get; }
    }

}
