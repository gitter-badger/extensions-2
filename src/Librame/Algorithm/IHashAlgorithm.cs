﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Algorithm
{
    /// <summary>
    /// 散列算法接口。
    /// </summary>
    public interface IHashAlgorithm : IByteCodec
    {
        /// <summary>
        /// 转换为 MD5。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回 MD5 字符串。</returns>
        string ToMd5(string str);


        /// <summary>
        /// 转换为 SHA1。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回 SHA1 字符串。</returns>
        string ToSha1(string str);


        /// <summary>
        /// 转换为 SHA256。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回 SHA256 字符串。</returns>
        string ToSha256(string str);


        /// <summary>
        /// 转换为 SHA384。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回 SHA384 字符串。</returns>
        string ToSha384(string str);


        /// <summary>
        /// 转换为 SHA512。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回 SHA512 字符串。</returns>
        string ToSha512(string str);

    }
}
