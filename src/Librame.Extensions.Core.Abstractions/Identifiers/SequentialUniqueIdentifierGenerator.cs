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
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Core.Identifiers
{
    using Services;
    using Threads;
    using Utilities;

    /// <summary>
    /// 有序唯一标识符生成器。
    /// </summary>
    /// <remarks>
    /// 参考：https://mp.weixin.qq.com/s/C6xk42s-4SwyszJPTM0G6A。
    /// </remarks>
    public class SequentialUniqueIdentifierGenerator : IIdentifierGenerator<Guid>
    {
        /// <summary>
        /// 构造一个 <see cref="SequentialUniqueIdentifierGenerator"/>。
        /// </summary>
        /// <param name="sequentialType">给定的 <see cref="SequentialUniqueIdentifierType"/>。</param>
        public SequentialUniqueIdentifierGenerator(SequentialUniqueIdentifierType sequentialType)
        {
            SequentialType = sequentialType;
        }


        /// <summary>
        /// 有序类型。
        /// </summary>
        public SequentialUniqueIdentifierType SequentialType { get; }


        /// <summary>
        /// 异步生成标识符。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="isUtc">相对于协调世界时（可选；默认使用选项设置）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="Guid"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "clock")]
        public Task<Guid> GenerateAsync(IClockService clock, bool? isUtc = null,
            CancellationToken cancellationToken = default)
        {
            clock.NotNull(nameof(clock));

            return clock.Locker.WaitFactoryAsync(async () =>
            {
                var timestampBytes = await GetCurrentTimestampAsync(clock, isUtc, cancellationToken)
                    .ConfigureAndResultAsync();
                var randomBytes = RandomUtility.GenerateByteArray(10);
                var guidBytes = new byte[16];

                switch (SequentialType)
                {
                    case SequentialUniqueIdentifierType.AsString:
                    case SequentialUniqueIdentifierType.AsBinary:
                        Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                        Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                        // If formatting as a string, we have to reverse the order
                        // of the Data1 and Data2 blocks on little-endian systems.
                        if (SequentialType == SequentialUniqueIdentifierType.AsString && BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(guidBytes, 0, 4);
                            Array.Reverse(guidBytes, 4, 2);
                        }
                        break;

                    case SequentialUniqueIdentifierType.AtEnd:
                        Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                        Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                        break;
                }

                return new Guid(guidBytes);
            });
        }

        private static async Task<byte[]> GetCurrentTimestampAsync(IClockService clock, bool? isUtc = null,
            CancellationToken cancellationToken = default)
        {
            var now = await clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, isUtc, cancellationToken)
                .ConfigureAndResultAsync();

            var buffer = BitConverter.GetBytes(now.Ticks / 10000L);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            return buffer;
        }


        /// <summary>
        /// 支持 MySQL 排序类型的生成器（char(36)）。
        /// </summary>
        public static readonly SequentialUniqueIdentifierGenerator MySQL
            = new SequentialUniqueIdentifierGenerator(SequentialUniqueIdentifierType.AsString);

        /// <summary>
        /// 支持 Oracle 排序类型的生成器（raw(16)）。
        /// </summary>
        public static readonly SequentialUniqueIdentifierGenerator Oracle
            = new SequentialUniqueIdentifierGenerator(SequentialUniqueIdentifierType.AsBinary);

        /// <summary>
        /// 支持 SQL Server 排序类型的生成器（uniqueidentifier）。
        /// </summary>
        public static readonly SequentialUniqueIdentifierGenerator SqlServer
            = new SequentialUniqueIdentifierGenerator(SequentialUniqueIdentifierType.AtEnd);
    }
}
