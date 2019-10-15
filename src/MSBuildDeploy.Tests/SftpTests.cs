using NUnit.Framework;

namespace Lithium.Build
{
	class SftpTests
	{
		[Test]
		public void Execute_ExistingSourceAndDestination_ReturnsTrue()
		{
			var target = GetSftpTask("/home/pi/test", @"..");
			var result = target.Execute();
			Assert.That(result, Is.True);
		}

		private static Sftp GetSftpTask(string destination, string source)
		{
			return new Sftp()
			{
				BuildEngine = TestEnvironment.BuildEngine,
				Destination = destination,
				Source = source,
				Port = TestEnvironment.Port,
				Host = TestEnvironment.Host,
				User = TestEnvironment.User,
				Keyfile = TestEnvironment.KeyFile,
			};
		}
	}
}
