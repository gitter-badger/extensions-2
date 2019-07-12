﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Data
{
    /// <summary>
    /// 并发标记接口。
    /// </summary>
    public interface IConcurrencyStamp
    {
        /// <summary>
        /// 并发标记。
        /// </summary>
        string ConcurrencyStamp { get; set; }
    }
}
