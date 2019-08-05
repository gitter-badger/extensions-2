using Xunit;

namespace Librame.Extensions.Core.Tests
{
    public class DnsHelperTests
    {
        [Fact]
        public async void AllTest()
        {
            var tuple = await DnsHelper.GetLocalIPAddressTupleAsync();
            var ipv4 = tuple.IPv4?.ToString();
            var ipv6 = tuple.IPv6?.ToString();

            Assert.True(ipv4.IsIPv4());
            Assert.True(ipv6.IsIPv6());
            Assert.False(ipv4.IsLocalIPv4());
            Assert.False(ipv6.IsLocalIPv6());
            Assert.False(ipv4.IsLocalIPAddress());
            Assert.False(ipv6.IsLocalIPAddress());
        }

    }
}
