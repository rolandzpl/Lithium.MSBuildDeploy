using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Renci.SshNet;
using System;
using System.IO;
using System.Linq;

namespace Lithium.Build
{
	public class Sftp : Task
	{
		public string Host { get; set; }

		public int Port { get; set; }

		public string User { get; set; }

		public string Keyfile { get; set; }

		public string Source { get; set; }

		public string Destination { get; set; }

		public override bool Execute()
		{
			try
			{
				var connectionInfo = new ConnectionInfo(Host, Port, User,
					new PrivateKeyAuthenticationMethod(User, new PrivateKeyFile(Keyfile)));
				using (var client = new SftpClient(connectionInfo))
				{
					client.Connect();
					Log.LogMessage(MessageImportance.Normal, $"Connected to {Host} as {User}");
					Log.LogMessage(MessageImportance.Normal, $"Synchronizing {Source} with {Destination}");

					var ls = client.ListDirectory(Destination)
						.Where(_ => _.IsRegularFile);
					if (File.Exists(Source))
					{
					}
					else if (Directory.Exists(Source))
					{
						client.SynchronizeDirectories(Source, Destination, "*");
					}
				}
			}
			catch (Exception ex)
			{
				Log.LogError($"Failed synchronizing, Details: {ex}");
			}
			return !Log.HasLoggedErrors;
		}
	}
}
