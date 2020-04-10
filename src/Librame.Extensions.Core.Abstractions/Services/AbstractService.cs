﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;

namespace Librame.Extensions.Core.Services
{
    /// <summary>
    /// 抽象服务。
    /// </summary>
    public abstract class AbstractService : IService
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractService"/>。
        /// </summary>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractService(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory.NotNull(nameof(loggerFactory));
        }

        /// <summary>
        /// 构造一个 <see cref="AbstractService"/>。
        /// </summary>
        /// <param name="service">给定的 <see cref="AbstractService"/>。</param>
        protected AbstractService(AbstractService service)
        {
            LoggerFactory = service.NotNull(nameof(service)).LoggerFactory;
        }


        /// <summary>
        /// 日志工厂。
        /// </summary>
        /// <value>返回 <see cref="ILoggerFactory"/>。</value>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// 日志。
        /// </summary>
        /// <value>返回 <see cref="ILogger"/>。</value>
        protected virtual ILogger Logger
            => LoggerFactory.CreateLogger(GetType());
    }
}
