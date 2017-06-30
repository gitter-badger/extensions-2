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
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Librame.Data.Descriptors
{
    /// <summary>
    /// 抽象创建、主键描述符。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public abstract class AbstractCreateIdDescriptor<TId> : AbstractIdDescriptor<TId>, ICreateIdDescriptor<TId>
        where TId : struct
    {
        /// <summary>
        /// 构建一个 <see cref="AbstractCreateIdDescriptor{TId}"/> 实例。
        /// </summary>
        public AbstractCreateIdDescriptor()
            : base()
        {
            CreateTime = DateTime.Now;
        }


        /// <summary>
        /// 创建时间。
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者主键。
        /// </summary>
        [DisplayName("创建者")]
        public virtual TId CreatorId { get; set; } = default(TId);
    }
}
