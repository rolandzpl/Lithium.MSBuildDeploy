using Microsoft.Build.Utilities;
using Renci.SshNet;
using System;

namespace Lithium.Build
{
	public class SshCommand : Task
	{
		public string Host { get; set; }

		public int Port { get; set; }

		public string User { get; set; }

		public string Keyfile { get; set; }

		public string Command { get; set; }

		public override bool Execute()
		{
			try
			{
				var connectionInfo = new ConnectionInfo(Host, Port, User,
					new PrivateKeyAuthenticationMethod(User, new PrivateKeyFile(Keyfile)));
				using var ssh = new SshClient(connectionInfo);
				ssh.Connect();
				var cmd = ssh.RunCommand(Command);
				if (cmd.ExitStatus != 0)
				{
					throw new SshCommandExecutionException(cmd.Error, cmd.ExitStatus);
				}
			}
			catch (Exception ex)
			{
				Log.LogError($"Failed executing command \"{Command}\". Details: {ex}");
			}
			return !Log.HasLoggedErrors;
		}
	}
}
