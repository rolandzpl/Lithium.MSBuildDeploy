using Microsoft.Build.Framework;
using Moq;

namespace Lithium.Build
{
	internal class TestEnvironment
	{
		internal static readonly string Host = "192.168.0.192";
		internal static readonly string User = "pi";
		internal static readonly string KeyFile = @"c:\users\rolan\.ssh\id_rsa";
		internal static readonly int Port = 22;
		internal static readonly IBuildEngine BuildEngine = Mock.Of<IBuildEngine>();
	}
}