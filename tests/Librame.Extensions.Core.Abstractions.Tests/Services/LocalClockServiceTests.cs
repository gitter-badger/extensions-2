﻿using System;
using Xunit;

namespace Librame.Extensions.Core.Tests
{
    using Services;

    public class ClockServiceTests
    {
        [Fact]
        public async void AllTest()
        {
            var now = await LocalClockService.Default.GetNowAsync().ConfigureAwait();
            Assert.Equal(now.Day, DateTime.UtcNow.Day);

            var nowOffset = await LocalClockService.Default.GetNowOffsetAsync().ConfigureAwait();
            Assert.Equal(nowOffset.Day, DateTimeOffset.UtcNow.Day);
        }

    }
}
