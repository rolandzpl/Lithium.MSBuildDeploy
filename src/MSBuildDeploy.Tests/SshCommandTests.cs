using NUnit.Framework;

namespace Lithium.Build
{
	class SshCommandTests
	{
		[Test]
		public void Execute_Command_ReturnsResult()
		{
			var sshCommand = GetSshCommandTask("ls -al");
			var result = sshCommand.Execute();
			Assert.That(result, Is.True);
		}

		[Test]
		public void Execute_SudoCommand_ReturnsResult()
		{
			var sshCommand = GetSshCommandTask("sudo ls -al");
			var result = sshCommand.Execute();
			Assert.That(result, Is.True);
		}

		[Test]
		public void Execute_InvalidCommand_ReturnsResult()
		{
			var sshCommand = GetSshCommandTask("alamakota");
			var result = sshCommand.Execute();
			Assert.That(result, Is.False);
		}

		private static SshCommand GetSshCommandTask(string commandText)
		{
			return new SshCommand()
			{
				Port = TestEnvironment.Port,
				Host = TestEnvironment.Host,
				User = TestEnvironment.User,
				Keyfile = TestEnvironment.KeyFile,
				Command = commandText,
				BuildEngine = TestEnvironment.BuildEngine
			};
		}
	}
}
